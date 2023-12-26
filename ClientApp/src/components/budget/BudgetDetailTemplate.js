import React, { Component } from "react";
import CustomStore from 'devextreme/data/custom_store';
import { DataGrid, Column, Editing, Lookup } from "devextreme-react/data-grid";
import { budgetDetailTypes } from "./data";
import { ApiService } from "../../services/ApiService";

const API_URL = "https://localhost:7071/api/BudgetManagerDetail";

class BudgetDetailTemplate extends Component {
  constructor(props) {
    super(props);
    const managerId = this.props.data.data.key;
    this.apiService = new ApiService();

    this.state = {
      budgetData: new CustomStore({
        key: 'id',
        onModified: () => this.props.budgetDetailsUpdated(),
        load: () => this.apiService.sendRequest(`${API_URL}/ByManager/${managerId}`),
        insert: (values) => this.apiService.sendRequest(`${API_URL}`, 'POST', JSON.stringify({budget_Manager_ID: managerId, ...values})),
        update: (key, values) => this.apiService.sendRequest(`${API_URL}/${key}`, 'PUT', JSON.stringify({...this.state.editingRowData, ...values})),
        remove: (key) => this.apiService.sendRequest(`${API_URL}/${key}`, 'DELETE', null),
      }),
      editingRowData: {}
    };  
  }

  onEditingStart = (e) => this.setState({ editingRowData: e.data });
  
  render() {
    const { budgetData } = this.state;
    return (
      <React.Fragment>
        <DataGrid dataSource={budgetData}
                  onEditingStart={this.onEditingStart}>            
          <Column dataField="date_Entry" dataType="date" format="MM/dd/yyyy" caption="Date of Entry"></Column>
          <Column dataField="type" caption="Type">            
            <Lookup dataSource={budgetDetailTypes} displayExpr="name" valueExpr="name" />
          </Column>
          <Column dataField="amount_Budget" dataType="number" caption="Manager Budget / Budget Gestionnaire"></Column>
          <Column dataField="comment" caption="Comment"></Column>

          <Editing
              mode="row"
              allowUpdating={true}
              allowDeleting={true}
              allowAdding={true} />
        </DataGrid>
      </React.Fragment>
    );
  }
}

export default BudgetDetailTemplate;
