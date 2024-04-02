/* eslint-disable react/prop-types */
import { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import {
  DataGrid,
  Column,
  MasterDetail,
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
import BudgetDetailTemplate from "./BudgetDetailTemplate";
import { ApiService } from "../../services/ApiService";

const API_ENDPOINT = "/api/BudgetManager";

export class BudgetBrandTable extends Component {
  constructor(props) {
    super(props);
    this.apiService = new ApiService();

    this.state = {
      brandsData: new CustomStore({
        key: "id",
        load: () => this.apiService.sendRequest(`${API_ENDPOINT}`),
        insert: (values) =>
          this.apiService.sendRequest(`${API_ENDPOINT}`, "POST", JSON.stringify(values))
            .then(() => this.props.productChanged()),
        update: (key, values) =>
          this.apiService.sendRequest(
            `${API_ENDPOINT}/${key}`,
            "PUT",
            JSON.stringify({ ...this.state.editingRowData, ...values })
          )
          .then(() => this.props.productChanged()),
        remove: (key) =>
          this.apiService.sendRequest(`${API_ENDPOINT}/${key}`, "DELETE", null)
            .then(() => this.props.productChanged()),
      }),
      productsData: [],
      editingRowData: {},
    };

    this.getProducts = this.getProducts.bind(this);
    this.handleBudgetDetailsUpdate = this.handleBudgetDetailsUpdate.bind(this);
    this.getProductName = this.getProductName.bind(this);
    this.allowUpdating = this.allowUpdating.bind(this);
  }

  onEditingStart = (e) => {
    this.getProducts(e);
    this.setState({ editingRowData: e.data });
  };

  getProducts(editingRowData) {
    this.apiService.sendRequest(`${API_ENDPOINT}/Products`).then((products) => {
      return this.setState({
        productsData: editingRowData.data.product
          ? [{ product: editingRowData.data.product }, ...products]
          : products,
      });
    });
  }

  handleBudgetDetailsUpdate() {
    this.dataGrid.instance.refresh(true);
  }

  getProductName(rowData) {
    return rowData.product;
  }

  allowUpdating(e) {
    return e.row.data.amount_Allocated === 0;
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
        const title = "Export_BudgetManager_Summary";
        const date = new Date(Date.now());
        const dateString = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
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
    const { brandsData, productsData } = this.state;

    const DetailsComponent = (e) => {
      return (
        <BudgetDetailTemplate
          data={e}
          budgetDetailsUpdated={this.handleBudgetDetailsUpdate}
        />
      );
    };

    return (
      <div className="section">
        <h2 className="section__title">Brand Level Budget / Budget par produit</h2>

        <DataGrid
          id="grid-container"
          dataSource={brandsData}
          ref={(ref) => (this.dataGrid = ref)}
          onExporting={this.onExporting}
          onInitNewRow={this.getProducts}
          onEditingStart={this.onEditingStart}
          showBorders={true}          
          wordWrapEnabled={true}
        >
          <Export enabled={true} />

          <Column
            dataField="product"
            caption="Brand / Produit"
            calculateDisplayValue={this.getProductName}
          >
            <Lookup
              dataSource={productsData}
              displayExpr="product"
              valueExpr="product"
            />
          </Column>
          <Column
            dataField="amount_Budget"
            dataType="number"
            caption="Manager Budget / Budget Gestionnaire"
            format="currency"
          ></Column>
          <Column
            dataField="amount_Allocated"
            dataType="number"
            allowEditing={false}
            format="currency"
            caption="Budget Allocated / Budget Alloué"
          ></Column>
          <Column
            dataField="amount_Left"
            dataType="number"
            allowEditing={false}
            format="currency"
            caption="Left to Allocate / Budget Disponible à Allouer"
          ></Column>

          <Toolbar>
            <Item name="addRowButton" />
            <Item name="exportButton" />
          </Toolbar>

          <Summary>
            <TotalItem column="product" displayFormat="TOTAL:" />
            <TotalItem
              column="amount_Budget"
              summaryType="sum"
              valueFormat="currency"
              displayFormat="{0}"
            />
            <TotalItem
              column="amount_Allocated"
              summaryType="sum"
              valueFormat="currency"
              displayFormat="{0}"
            />
            <TotalItem
              column="amount_Left"
              summaryType="sum"
              valueFormat="currency"
              displayFormat="{0}"
            />
          </Summary>

          <Editing
            mode="row"
            useIcons={true}
            allowUpdating={this.allowUpdating}
            allowDeleting={true}
            allowAdding={true}
          />

          <MasterDetail enabled={true} component={DetailsComponent} />
        </DataGrid>
      </div>
    );
  }
}
