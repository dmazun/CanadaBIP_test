import React, { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import {
  DataGrid,
  Column,
  Summary,
  TotalItem,
  Export,
  HeaderFilter,
  Editing,
  Toolbar,
  Item,
  Lookup,
} from "devextreme-react/data-grid";
import CustomStore from "devextreme/data/custom_store";
import { Workbook } from "exceljs";
import saveAs from "file-saver";
import { exportDataGrid } from "devextreme/excel_exporter";
import { ApiService } from "../../services/ApiService";
import { RepEditNameSelect } from "./RepEditNameSelect";
import { RepDropDownGridSelect } from "./RepDropDownGridSelect";
import { attendanceData, sharedIndividualData } from "./data";
import * as AspNetData from "devextreme-aspnet-data-nojquery";

const API_URL = "https://localhost:7071/api/BudgetRepresentative";

export class RepBudget extends Component {
  constructor(props) {
    super(props);
    this.apiService = new ApiService();

    this.state = {
      budgetData: new CustomStore({
        key: "id",
        load: () => this.apiService.sendRequest(`${API_URL}`),
        insert: (values) =>
          this.apiService.sendRequest(
            `${API_URL}`,
            "POST",
            JSON.stringify(values)
          ),
        update: (key, values) =>
          this.apiService.sendRequest(
          `${API_URL}/${key}`,
          "PUT",
          JSON.stringify({ ...this.state.editingRowData, ...values })
          ),
        remove: (key) =>
          this.apiService.sendRequest(`${API_URL}/${key}`, "DELETE", null),
      }),
      editingRowData: {},
      customersData: new AspNetData.createStore({
        key: "name",
        loadUrl: `${API_URL}/RepCustomers`,
      }),
      repNamesData: [],
      productsData: [],
      initiativesData: [],
      statusesData: [],
      eventTypesData: [],
    };
  }

  componentDidMount() {
    this.getRepNames();
    this.getProducts();
    this.getInitiatives();
    this.geStatuses();
    this.getEventTypes();
  }

  onEditingStart = (e) => {
    this.setState({ editingRowData: e.data });
  };

  onExporting(e) {
    const workbook = new Workbook();
    const worksheet = workbook.addWorksheet("Sheet");

    exportDataGrid({
      component: e.component,
      worksheet: worksheet,
      autoFilterEnabled: true,
      customizeCell: function (options) {
        options.excelCell.font = { name: "Arial", size: 12 };
        options.excelCell.alignment = { horizontal: "left" };
      },
    }).then(function () {
      workbook.xlsx.writeBuffer().then(function (buffer) {
        const title = "Export_BudgetRepresentative";
        const date = new Date(Date.now());
        const dateString = `${date.getFullYear()}-${
          date.getMonth() + 1
        }-${date.getDate()}`;
        const time = `${date.getHours()}_${date.getMinutes()}`;
        const filename = `${title}_${dateString}_${time}.xlsx`;

        saveAs(
          new Blob([buffer], { type: "application/octet-stream" }),
          filename
        );
      });
    });
  }

  getRepNames() {
    this.apiService
      .sendRequest(`${API_URL}/RepNamesSelect`)
      .then((res) => this.setState({ repNamesData: res }));
  }

  geStatuses() {
    this.apiService
      .sendRequest(`${API_URL}/RepStatuses`)
      .then((res) => this.setState({ statusesData: res }));
  }

  getEventTypes() {
    this.apiService
      .sendRequest(`${API_URL}/RepEventTypes`)
      .then((res) => this.setState({ eventTypesData: res }));
  }

  getProducts() {
    this.apiService
      .sendRequest(`${API_URL}/RepProducts`)
      .then((products) => this.setState({ productsData: products }));
  }

  getFilteredProducts(options) {
    return {
      store: this.state.productsData,
      filter: options.data
        ? ["sales_Area_Code", "=", options.data.sales_Area_Code]
        : null,
    };
  }

  getInitiatives() {
    this.apiService
      .sendRequest(`${API_URL}/RepInitiatives`)
      .then((data) => this.setState({ initiativesData: data }));
  }

  getFilteredInitiatives(options) {
    return {
      store: this.state.initiativesData,
      filter: options.data ? ["product", "=", options.data.product] : null,
    };
  }

  setProductValue(rowData, value) {
    rowData.initiative_ID = null;
    this.defaultSetCellValue(rowData, value);
  }

  setRepNameValue(rowData, value) {
    rowData.product = null;
    this.defaultSetCellValue(rowData, value);
  }

  render() {
    const { budgetData, repNamesData, statusesData, eventTypesData, customersData } = this.state;

    return (
      <div>
        <h2>Budget Representative / Budget Représentant</h2>

        <DataGrid
          id="grid-container"
          dataSource={budgetData}
          filterValue={
            this.props.repSACode !== "ALL"
              ? ["rep_Sales_Area_Code", "=", this.props.repSACode]
              : null
          }
          height={500}
          showBorders={true}
          wordWrapEnabled={true}
          onEditingStart={this.onEditingStart}
          onExporting={this.onExporting}
        >
          <HeaderFilter visible={true} />
          <Export enabled={true} />

          <Column
            dataField="sales_Area_Code"
            caption="Rep Name / Nom Représentant"
            width={250}
            setCellValue={this.setRepNameValue}
            calculateCellValue={(rowData) =>
              rowData.sales_Area_Code || rowData.rep_Sales_Area_Code
            }
            calculateDisplayValue={(rowData) => rowData.rep_Employee_Name}
            editCellComponent={RepEditNameSelect}
          >
            <Lookup dataSource={repNamesData} />
          </Column>

          <Column
            dataField="product"
            caption="Brand / Produit"
            width={200}
            setCellValue={this.setProductValue}
          >
            <Lookup
              dataSource={this.getFilteredProducts.bind(this)}
              displayExpr="product"
              valueExpr="product"
            />
          </Column>

          <Column
            dataField="date_Entry"
            dataType="date"
            width={200}
            caption="Date of Event / Date de l'Evenement"
          />

          <Column
            dataField="event_Name"
            caption="Name Of Event / Nom De L'événement"
            width={300}
          />

          <Column
            dataField="initiative"
            caption="Initiative"
            width={250}
            setCellValue={(rowData, value) => {
              rowData.initiative = value.initiative;
              rowData.initiative_ID = value.id;
            }}
          >
            <Lookup
              dataSource={this.getFilteredInitiatives.bind(this)}
              displayExpr="initiative"
            />
          </Column>

          <Column
            dataField="amount_Allocated"
            dataType="number"
            format="currency"
            caption="Amount / Montant"
            width={150}
          />

          <Column dataField="type" caption="Status" width={200}>
            <Lookup
              dataSource={statusesData}
              displayExpr="type"
              valueExpr="type"
            />
          </Column>

          <Column dataField="note" caption="Notes" width={300} />

          <Column
            dataField="event_Type"
            caption="Type of Event / Type D'événement"
            width={150}
          >
            <Lookup
              dataSource={eventTypesData}
              displayExpr="type_Of_Event"
              valueExpr="type_Of_Event"
            />
          </Column>

          <Column dataField="attendance" caption="Attendance / Présence" width={150}>
            <Lookup
              dataSource={attendanceData}
              displayExpr="name"
              valueExpr="name"
            />
          </Column>

          <Column
            dataField="shared_Individual"
            caption="Shared/Individual / Événement partagé"
            width={200}
          >
            <Lookup
              dataSource={sharedIndividualData}
              displayExpr="name"
              valueExpr="name"
            />
          </Column>

          <Column
            dataField="cust_Name_Display"
            caption="Speaker / Conférencier"            
            calculateDisplayValue={(rowData) => rowData.cust_Name_Display}
            setCellValue={(rowData, value) => {
              rowData.cust_Name_Display = value.name;
              rowData.customer_ID = value.reltiO_ID;
            }}
            width={300}
          >
            <Lookup
              dataSource={customersData}
              displayExpr="name" 
            />
          </Column>
          
          <Column
            dataField="customer_Count"
            dataType="number"
            caption="# Cust / Nombre clients"
            width={150}
          />
         
          <Column
            dataField="customer_Type"
            caption="Cust Type / Type de clients"
            width={250}
          />
          
          <Column
            dataField="fcpA_Veeva_ID"
            caption="#PW and/or #Veeva / #PW et/ou #Veeva"
            width={200}
          />
          <Column dataField="account_Name" caption="Institution" width={300} />
          <Column dataField="tier" dataType="number" caption="Tier / Niveau" width={100}/>
          <Column dataField="rep_Sales_Area_Code" visible={false} />

          <Toolbar>
            <Item name="addRowButton" />
            <Item name="exportButton" />
          </Toolbar>

          <Summary>
            <TotalItem column="initiative" displayFormat="TOTAL:" />
            <TotalItem
              column="amount_Allocated"
              summaryType="sum"
              valueFormat="currency"
              displayFormat="{0}"
            />
          </Summary>

          <Editing
            mode="row"
            useIcons={true}
            allowUpdating={true}
            allowDeleting={true}
            allowAdding={true}
          />
        </DataGrid>
      </div>
    );
  }
}
