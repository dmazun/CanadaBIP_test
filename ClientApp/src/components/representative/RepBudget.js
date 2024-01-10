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
} from "devextreme-react/data-grid";
import CustomStore from "devextreme/data/custom_store";
import { Workbook } from "exceljs";
import saveAs from "file-saver";
import { exportDataGrid } from "devextreme/excel_exporter";
import { ApiService } from "../../services/ApiService";

const API_URL = "https://localhost:7071/api/BudgetRepresentative";

export class RepBudget extends Component {
  constructor(props) {
    super(props);
    this.apiService = new ApiService();

    this.state = {
      budgetData: new CustomStore({
        key: "id",
        load: () => this.apiService.sendRequest(`${API_URL}`),
      }),
    };
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

  render() {
    const { budgetData } = this.state;
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
          onExporting={this.onExporting}
        >
          <HeaderFilter visible={true} />
          <Export enabled={true} />

          <Column
            dataField="rep_Employee_Name"
            caption="Rep Name / Nom Représentant"
          />
          <Column dataField="product" caption="Brand / Produit" />
          <Column
            dataField="date_Entry"
            dataType="date"
            caption="Date of Event / Date de l'Evenement"
          />
          <Column
            dataField="event_Name"
            caption="Name Of Event / Nom De L'événement"
          />
          <Column dataField="initiative" caption="Initiative" />
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
          <Column dataField="cust_Name_Display" caption="Speaker / Conférencier" />
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
              allowAdding={true} />
        </DataGrid>
      </div>
    );
  }
}
