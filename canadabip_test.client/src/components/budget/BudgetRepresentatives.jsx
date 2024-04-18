/* eslint-disable react/prop-types */
import { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import {
  DataGrid,
  Column,
  Editing,
  Lookup,
  Summary,
  TotalItem,
  Toolbar,
  Item,
  Export,
} from "devextreme-react/data-grid";
import CustomStore from "devextreme/data/custom_store";
import { Workbook } from "exceljs";
import saveAs from "file-saver";
import { exportDataGrid } from "devextreme/excel_exporter";
import { BudgetRepSelectComponent } from "./BudgetRepSelectComponent";
import { ApiService } from "../../services/ApiService";

const API_ENDPOINT = "/api/BudgetManagerRepresentative";

export class BudgetRepresentatives extends Component {
  constructor(props) {
    super(props);
    this.apiService = new ApiService();

    this.state = {
      brandsData: new CustomStore({
        key: "id",
        load: () => this.apiService.sendRequest(`${API_ENDPOINT}`),
        insert: (values) => this.apiService.sendRequest(`${API_ENDPOINT}`, "POST", JSON.stringify(values))
          .then(() => {
            this.getRepNames();
            this.props.budgetChanged();
          }),
        update: (key, values) => this.apiService.sendRequest(`${API_ENDPOINT}/${key}`, "PUT",
          JSON.stringify({
            ...{
              sales_Area_Code: this.state.editingRowData.rep_Sales_Area_Code,
              date_Entry: this.state.editingRowData.date_Entry,
              product: this.state.editingRowData.product,
              amount_Allocated: this.state.editingRowData.amount_Allocated,
            },
            ...values,
          }))
          .then(() => {
            this.getRepNames();
            this.props.budgetChanged();
          }),
        remove: (key) => this.apiService.sendRequest(`${API_ENDPOINT}/${key}`, "DELETE", null)
          .then(() => {
            this.getRepNames();
            this.props.budgetChanged();
          }),
      }),
      productsData: [],
      repNamesData: [],
      editingRowData: {}
    };
  }

  componentDidMount() {
    this.getProducts();
    this.getRepNames();
  }

  getProducts() {
    this.apiService
      .sendRequest(`${API_ENDPOINT}/Products`)
      .then((products) => this.setState({ productsData: products }));
  }

  getRepNames() {
    this.apiService
      .sendRequest(`${API_ENDPOINT}/RepNames`)
      .then((res) => this.setState({ repNamesData: res }));
  }

  onEditingStart = (e) => {
    this.setState({ editingRowData: e.data });
  };

  onEditorPreparing = (e) => {
    if (e.dataField === "product") {
      e.editorOptions.disabled = e.row.data && e.row.data.is_BR === 1;
    }
  };

  getProductName(rowData) {
    return rowData.product;
  }

  calculateRepName(rowData) {
    return rowData.rep_Employee_Name;
  }

  calculateRepAreaCode(rowData) {
    return rowData.rep_Sales_Area_Code;
  }

  setProductValue(rowData, value) {
    rowData.rep_Sales_Area_Code = null;
    this.defaultSetCellValue(rowData, value);
  }

  onExporting(e) {
    const workbook = new Workbook();
    const worksheet = workbook.addWorksheet("Sheet");
    exportDataGrid({
      component: e.component,
      worksheet: worksheet,
      customizeCell: function (options) {
        options.excelCell.font = { name: "Arial", size: 12 };
        options.excelCell.alignment = { horizontal: "left" };
      },
    }).then(function () {
      workbook.xlsx.writeBuffer().then(function (buffer) {
        const title = "Export_BudgetAllocationRepresentatives_Summary";
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

  render() {
    const { brandsData, productsData, repNamesData } = this.state;

    return (
      <div className="section">
        <h2 className="section__title">
          Budget Allocation To Representatives / Allocation de budget aux représentants
        </h2>

        <DataGrid
          id="grid-container"
          dataSource={brandsData}
          onEditorPreparing={this.onEditorPreparing}
          onExporting={this.onExporting}
          onEditingStart={this.onEditingStart}
          wordWrapEnabled={true}
          showBorders={true}
        >
          <Export enabled={true} />

          <Column
            dataField="product"
            caption="Brand/ Produit"
            setCellValue={this.setProductValue}
            calculateDisplayValue={this.getProductName}
          >
            <Lookup
              dataSource={productsData}
              displayExpr="product"
              valueExpr="product"
            />
          </Column>

          <Column
            dataField="sales_Area_Code"
            caption="Rep Name/ Nom Représentant"
            calculateCellValue={this.calculateRepAreaCode}
            calculateDisplayValue={this.calculateRepName}
            editCellComponent={BudgetRepSelectComponent}
          >
            <Lookup
              dataSource={repNamesData}
              calculateDisplayValue={this.calculateRepName}
              valueExpr="sales_Area_Code"
            />
          </Column>

          <Column
            dataField="date_Entry"
            dataType="date"
            caption="Date of Entry/ Date de l'entrée"
          ></Column>

          <Column
            dataField="amount_Budget"
            dataType="number"
            allowEditing={false}
            format="currency"
            caption="Manager Budget/ Budget Gestionnaire"
          ></Column>

          <Column
            dataField="amount_Allocated"
            dataType="number"
            format="currency"
            caption="Budget Allocated/ Budget Alloué"
          ></Column>

          <Column
            dataField="amount_Left"
            dataType="number"
            allowEditing={false}
            format="currency"
            caption="Remaining To Be Allocated/ Budget Disponible à Allouer"
          ></Column>

          <Toolbar>
            <Item name="addRowButton" />
            <Item name="exportButton" />
          </Toolbar>

          <Summary>
            <TotalItem column="date_Entry" displayFormat="TOTAL:" />
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
