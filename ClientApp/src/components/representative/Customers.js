import React, { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import {
  DataGrid,
  Column,
  Scrolling,
  Paging,
} from "devextreme-react/data-grid";
import { ApiService } from "../../services/ApiService";

import * as AspNetData from "devextreme-aspnet-data-nojquery";

const API_URL = "https://localhost:7071/api/BudgetRepresentative";

export class Customers extends Component {
  constructor(props) {
    super(props);
    this.apiService = new ApiService();

    this.state = {
      customersData: new AspNetData.createStore({
        key: "id",
        loadUrl: `${API_URL}/RepCustomersPartial`,
      }),
    };
  }

  render() {
    const { customersData } = this.state;
    return (
      <div>
        <h2> Customers </h2>

        <DataGrid
          id="grid-container"
          dataSource={customersData}
          height={400}
          showBorders={true}
          remoteOperations={true}
          wordWrapEnabled={true}
        >
          <Scrolling mode="virtual" rowRenderingMode="virtual" />
          <Paging defaultPageSize={10} />

          <Column dataField="id" />
          <Column dataField="reltiO_ID" />
          <Column dataField="name" />
        </DataGrid>
      </div>
    );
  }
}
