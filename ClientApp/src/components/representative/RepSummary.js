import React, { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import {
  DataGrid,
  Column,
  Summary,
  TotalItem,
  Export,
} from "devextreme-react/data-grid";
import { Workbook } from "exceljs";
import saveAs from "file-saver";
import { exportDataGrid } from "devextreme/excel_exporter";
import { ApiService } from "../../services/ApiService";

const API_URL = "https://localhost:7071/api/BudgetRepresentative";

export class RepSummary extends Component {
  constructor(props) {
    super(props);
    this.apiService = new ApiService();

    this.state = {
      summaryData: [],
    };
  }

  componentDidMount() {
    this.getSummaryData();
  }

  getSummaryData() {
    this.apiService.sendRequest(`${API_URL}/Summary`).then((data) => {
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
      <div>
        <h2> Budget Representative Summary/Sommaire Budget Représentant</h2>

        <DataGrid
          id="grid-container"
          dataSource={summaryData}
          filterValue={
            this.props.repSACode !== "ALL"
              ? ["rep_Sales_Area_Code", "=", this.props.repSACode]
              : null
          }        
          onExporting={this.onExporting}
        >
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
          />
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
