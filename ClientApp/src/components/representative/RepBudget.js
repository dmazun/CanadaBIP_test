import React, { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import {
  DataGrid,
  Column,
  Summary,
  TotalItem,
  Export,
  MasterDetail,
  HeaderFilter,
  Editing,
  Toolbar,
  Item,
  Lookup,
  Paging,
  RequiredRule
} from "devextreme-react/data-grid";
import CustomStore from "devextreme/data/custom_store";
import { Workbook } from "exceljs";
import saveAs from "file-saver";
import { exportDataGrid } from "devextreme/excel_exporter";
import { ApiService } from "../../services/ApiService";
import { RepEditNameSelect } from "./RepEditNameSelect";
import CustTypeTagBoxComponent  from "./CustTypeTagBoxComponent";
import RepDetailTemplate  from "./RepDetailTemplate";
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
            JSON.stringify({
              ...this.state.editingRowData,
              ...values,
              sales_Area_Code:
                values.sales_Area_Code ||
                this.state.editingRowData.rep_Sales_Area_Code,
            })
          ),
        remove: (key) =>
          this.apiService.sendRequest(`${API_URL}/${key}`, "DELETE", null),
      }),
      editingRowData: {},
      customersData: new AspNetData.createStore({
        key: "name",
        loadUrl: `${API_URL}/RepCustomers`,
      }),
      accountsData: new AspNetData.createStore({
        key: "id",
        loadUrl: `${API_URL}/RepAccounts`,
      }),
      repNamesData: [],
      productsData: [],
      initiativesData: [],
      statusesData: [],
      eventTypesData: [],
      custTypesData: [],
    };
  }

  componentDidMount() {
    this.getRepNames();
    this.getProducts();
    this.getInitiatives();
    this.geStatuses();
    this.getEventTypes();
    this.getCustomerTypes();
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

  getCustomerTypes() {
    this.apiService
      .sendRequest(`${API_URL}/RepCustTypes`)
      .then((res) => this.setState({ custTypesData: res }));
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
      store: [ ...this.state.initiativesData, {
        bu: "PBGH",
        id: 1,
        idn: 56,
        initiative: "OTHER",
        product: options.data?.product
      }],
      filter: options.data ? ["product", "=", options.data.product] : null,
    };
  }

  setProductValue(rowData, value) {
    rowData.initiative_ID = null;
    rowData.initiative = null;
    this.defaultSetCellValue(rowData, value);
  }

  setRepNameValue(rowData, value) {
    rowData.product = null;
    rowData.initiative_ID = null;
    rowData.initiative = null;
    this.defaultSetCellValue(rowData, value);
  }

  custTypesCellTemplate(container, options) {
    const noBreakSpace = '\u00A0';    
    container.textContent = options.value || noBreakSpace;
    container.title = options.value;
  };

  calculateFilterExpression(filterValue, selectedFilterOperation, target) {
    if (target === 'search' && typeof filterValue === 'string') {
      return [this.dataField, 'contains', filterValue];
    }
    return (rowData) => (rowData.AssignedEmployee || []).indexOf(filterValue) !== -1;
  }

  render() {
    const {
      budgetData,
      repNamesData,
      statusesData,
      eventTypesData,
      customersData,
      accountsData,
      custTypesData,
    } = this.state;

    const DetailsComponent = (component) => {
      return (
        <RepDetailTemplate
          data={component.data}
          detailsUpdated={() => this.dataGrid.instance.refresh(true)}
        />
      );
    };

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
          ref={(ref) => (this.dataGrid = ref)}
          showBorders={true}
          onEditingStart={this.onEditingStart}
          onExporting={this.onExporting}
          showRowLines={true}
        >
          <HeaderFilter visible={true} />
          <Export enabled={true} />
          <Paging
            enabled={true}
            defaultPageSize={10}
          />
          <MasterDetail enabled={true} component={DetailsComponent} />

          <Column
            dataField="sales_Area_Code"
            caption="Rep Name / Nom Représentant"
            width={200}
            setCellValue={this.setRepNameValue}
            calculateCellValue={(rowData) =>
              rowData.sales_Area_Code || rowData.rep_Sales_Area_Code
            }
            calculateDisplayValue={(rowData) => rowData.rep_Employee_Name}
            editCellComponent={RepEditNameSelect}
          >
            <RequiredRule message="Rep Name is required / Nom Représentant est obligatoire." />
            <Lookup dataSource={repNamesData} />
          </Column>

          <Column
            dataField="product"
            caption="Brand / Produit"
            width={150}
            setCellValue={this.setProductValue}
          >
            <RequiredRule message="Brand is required / Produit est obligatoire." />
            <Lookup
              dataSource={this.getFilteredProducts.bind(this)}
              displayExpr="product"
              valueExpr="product"
            />
          </Column>

          <Column
            dataField="date_Entry"
            dataType="date"
            width={150}
            caption="Date of Event / Date de l'Evenement"
          >
            <RequiredRule message="Date of Entry is required / Date de l'entrée est obligatoire." />
          </Column>

          <Column
            dataField="event_Name"
            caption="Name Of Event / Nom De L'événement"
            width={300}
          >
            <RequiredRule message="Name Of Event is required / Nom De L'événement est obligatoire." />
          </Column>

          <Column
            dataField="initiative"
            caption="Initiative"
            width={200}
            setCellValue={(rowData, value) => {
              rowData.initiative = value.initiative;
              rowData.initiative_ID = value.id;
            }}
          >
            <RequiredRule message="Initiative is required / Initiative est obligatoire." />
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
            width={100}
          >
            <RequiredRule message="Amount is required (mandatory but could be '0') / Montant est obligatoire (mais peut être '0')." />
          </Column>

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

          <Column
            dataField="attendance"
            caption="Attendance / Présence"
            width={150}
          >
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
            <Lookup dataSource={customersData} displayExpr="name" />
          </Column>

          <Column
            dataField="customer_Count"
            dataType="number"
            caption="# Cust / Nombre clients"
            width={100}
          />

          <Column
            dataField="customer_Type"
            caption="Cust Type / Type de clients"
            cellTemplate={this.custTypesCellTemplate}
            editCellComponent={CustTypeTagBoxComponent}
            calculateFilterExpression={this.calculateFilterExpression}
            width={250}
            height={40}
          >
            <Lookup
              dataSource={custTypesData}
              valueExpr="cust_Type"
              displayExpr="cust_Type"
            />
          </Column>

          <Column
            dataField="fcpA_Veeva_ID"
            caption="#PW and/or #Veeva / #PW et/ou #Veeva"
            width={200}
          />

          <Column
            dataField="account_Name"
            caption="Institution"
            calculateDisplayValue={(rowData) => rowData.account_Name}
            setCellValue={(rowData, value) => {
              rowData.account_Name = value.name;
              rowData.account_ID = value.reltiO_ID;
            }}
            width={300}
          >
            <Lookup dataSource={accountsData} displayExpr="name" />
          </Column>

          <Column
            dataField="tier"
            dataType="number"
            caption="Tier / Niveau"
            width={100}
          /> 
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
            mode="cell"
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
