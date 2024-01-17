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
      repNamesData: [],
      productsData: [],
      initiativesData: [],
    };
  }

  componentDidMount() {
    this.getRepNames();
    this.getProducts();
    this.getInitiatives();
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
    const { budgetData, repNamesData } = this.state;

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
          onEditingStart={this.onEditingStart}
          onExporting={this.onExporting}
        >
          <HeaderFilter visible={true} />
          <Export enabled={true} />

          <Column
            dataField="sales_Area_Code"
            caption="Rep Name / Nom Représentant"
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
            caption="Date of Event / Date de l'Evenement"
          />
          <Column
            dataField="event_Name"
            caption="Name Of Event / Nom De L'événement"
          />
          <Column
            dataField="initiative_ID"
            caption="Initiative"
            calculateCellValue={(rowData) => rowData.initiative}
            calculateDisplayValue={(rowData) => rowData.initiative}
          >
            <Lookup
              dataSource={this.getFilteredInitiatives.bind(this)}
              displayExpr="initiative"
              valueExpr="idn"
            />
          </Column>
          <Column
            dataField="amount_Allocated"
            dataType="number"
            format="currency"
            caption="Amount / Montant"
          />
          <Column dataField="status" caption="Status" />
          <Column dataField="note" caption="Notes" />
          <Column
            dataField="event_Type"
            caption="Type of Event / Type D'événement"
          />
          <Column dataField="attendance" caption="Attendance / Présence" />
          <Column
            dataField="shared_Individual"
            caption="Shared/Individual / Événement partagé"
          />
          <Column
            dataField="cust_Name_Display"
            caption="Speaker / Conférencier"
          />
          <Column
            dataField="customer_Count"
            caption="# Cust / Nombre clients"
          />
          <Column
            dataField="customer_Type"
            caption="Cust Type / Type de clients"
          />
          <Column
            dataField="fcpA_Veeva_ID"
            caption="#PW and/or #Veeva / #PW et/ou #Veeva"
          />
          <Column dataField="account_Name" caption="Institution" />
          <Column dataField="tier" caption="Tier / Niveau" />
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
