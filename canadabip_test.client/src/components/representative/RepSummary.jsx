/* eslint-disable react/prop-types */
import { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import {
  DataGrid,
  Column,
  Summary,
  TotalItem,
  Export,
  HeaderFilter,
  Scrolling,
  Paging,
} from "devextreme-react/data-grid";
import { Workbook } from "exceljs";
import saveAs from "file-saver";
import { exportDataGrid } from "devextreme/excel_exporter";
import { ApiService } from "../../services/ApiService";
import * as AspNetData from 'devextreme-aspnet-data-nojquery';

const API_ENDPOINT = "/api/BudgetRepresentative";

export class RepSummary extends Component {
  constructor(props) {
    super(props);
    this.apiService = new ApiService();

    this.state = {      
      summaryData: new AspNetData.createStore({
        key: 'id',
        loadUrl: `${API_ENDPOINT}/Summary`,
      })
    };
  }

  getSummaryData() {
    this.apiService.sendRequest(`${API_ENDPOINT}/Summary`).then((data) => {
      this.setState({
        summaryData: data,
      });
    });
  }

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
        const title = "Export_BudgetRepresentative_Summary";
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
    const { summaryData } = this.state;
    return (
      <div className="section">
        <h2 className="section__title">Budget Representative Summary / Sommaire Budget Représentant</h2>

        <DataGrid
          id="grid-container"
          className="grid-height"
          dataSource={summaryData}
          filterValue={
            this.props.repSACode !== "ALL"
              ? ["rep_Sales_Area_Code", "=", this.props.repSACode]
              : null
          }
          onExporting={this.onExporting}
          showBorders={true}
          // remoteOperations={true}
          wordWrapEnabled={true}
        >
          <Scrolling mode="virtual" rowRenderingMode="virtual" />
          <Paging defaultPageSize={25} />
          <HeaderFilter visible={true} />
          <Export enabled={true} />

          <Column dataField="product" caption="BRANDS / PRODUITS" />
          <Column
            dataField="rep_Employee_Name"
            caption="Rep Name / Nom Représentant"
          />
          <Column
            dataField="amount_Budget"
            dataType="number"
            format="currency"
            caption="Your / Votre Budget"
          >
            <HeaderFilter groupInterval={500} />
          </Column>
          <Column
            dataField="amount_Spent"
            dataType="number"
            format="currency"
            caption="Spent / Dépensé"
          />
          <Column
            dataField="amount_Committed_Planned"
            dataType="number"
            format="currency"
            caption="Committed + Planned / Commis + Planifié"
          />
          <Column
            dataField="amount_Left"
            dataType="number"
            format="currency"
            caption="BUDGET REMAINING / BUDGET RESTANT"
          />
          <Column
            dataField="amount_Wish"
            dataType="number"
            format="currency"
            caption="Wish / Souhaité"
          />
          <Column dataField="rep_Sales_Area_Code" visible={false} />

          <Summary>
            <TotalItem column="product" displayFormat="TOTAL:" />
            <TotalItem
              column="amount_Budget"
              summaryType="sum"
              valueFormat="currency"
              displayFormat="{0}"
            />
            <TotalItem
              column="amount_Spent"
              summaryType="sum"
              valueFormat="currency"
              displayFormat="{0}"
            />
            <TotalItem
              column="amount_Committed_Planned"
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
            <TotalItem
              column="amount_Wish"
              summaryType="sum"
              valueFormat="currency"
              displayFormat="{0}"
            />
          </Summary>          
        </DataGrid>
      </div>
    );
  }
}
