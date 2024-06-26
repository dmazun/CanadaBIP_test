/* eslint-disable react/prop-types */
import React, { Component } from "react";
import CustomStore from "devextreme/data/custom_store";
import { DataGrid, Column, Editing } from "devextreme-react/data-grid";
import { ApiService } from "../../services/ApiService";

const API_ENDPOINT = "/api/BudgetRepresentative";

class RepDetailTemplate extends Component {
  constructor(props) {
    super(props);
    const repId = this.props.data.key;
    this.apiService = new ApiService();

    this.state = {
      detailsData: new CustomStore({
        key: "id",
        onModified: () => this.props.detailsUpdated(),
        load: () =>
          this.apiService.sendRequest(`${API_ENDPOINT}/RepDetails/${repId}`),
        insert: (values) =>
          this.apiService.sendRequest(
            `${API_ENDPOINT}/RepDetails`,
            "POST",
            JSON.stringify({ budget_Representative_ID: repId, ...values })
          ),
        update: (key, values) =>
          this.apiService.sendRequest(
            `${API_ENDPOINT}/RepDetails/${key}`,
            "PUT",
            JSON.stringify({ ...this.state.editingRowData, ...values })
          ),
        remove: (key) =>
          this.apiService.sendRequest(
            `${API_ENDPOINT}/RepDetails/${key}`,
            "DELETE",
            null
          ),
      }),
      editingRowData: {},
    };
  }

  onEditingStart = (e) => this.setState({ editingRowData: e.data });

  render() {
    const { detailsData } = this.state;
    return (
      <React.Fragment>
        <DataGrid
          dataSource={detailsData}
          onEditingStart={this.onEditingStart}
          width={900}
          showBorders={true}
        >
          <Column
            dataField="name"
            caption="Itemized Entry"
            width={300}
          ></Column>
          <Column
            dataField="amount_Allocated"
            dataType="number"
            caption="Amount / Montant"
            width={150}
          ></Column>
          <Column
            dataField="comment"
            caption="Comment / Commentaires"
            width={300}
          ></Column>

          <Editing
            mode="row"
            useIcons={true}
            allowUpdating={true}
            allowDeleting={true}
            allowAdding={true}
          />
        </DataGrid>
      </React.Fragment>
    );
  }
}

export default RepDetailTemplate;
