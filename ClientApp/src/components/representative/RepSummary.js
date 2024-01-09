import React, { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import { DataGrid, Column} from "devextreme-react/data-grid";
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
        >
          <Column dataField="product" caption="BRANDS / PRODUITS" />
          <Column
            dataField="rep_Employee_Name"
            caption="Rep Name / Nom Représentant"
          />
          <Column dataField="amount_Budget" caption="Your / Votre Budget" />
          <Column dataField="amount_Spent" caption="Spent / Dépensé" />
          <Column
            dataField="amount_Committed_Planned"
            caption="Committed + Planned / Commis + Planifié"
          />
          <Column
            dataField="amount_Left"
            caption="BUDGET REMAINING / BUDGET RESTANT"
          />
          <Column dataField="amount_Wish" caption="Wish / Souhaité" />
          <Column dataField="rep_Sales_Area_Code" visible={false} />
        </DataGrid>
      </div>
    );
  }
}
