import React, { Component } from "react";
import CustomStore from 'devextreme/data/custom_store';
import { DataGrid, Column, Editing } from "devextreme-react/data-grid";

const API_URL = "https://localhost:7071/api/BudgetManagerDetail";

class BudgetDetailTemplate extends Component {
  constructor(props) {
    super(props);
    const managerId = this.props.data.data.key;

    this.state = {
      budgetData: new CustomStore({
        key: 'id',
        // onLoaded: (result) => this.showResult(result),
        load: () => this.sendRequest(`${API_URL}/ByManager/${managerId}`),
        insert: (values) => this.sendRequest(`${API_URL}`, 'POST', JSON.stringify({budget_Manager_ID: managerId, ...values})),
        update: (key, values) => this.sendRequest(`${API_URL}/${key}`, 'PATCH', JSON.stringify(values)),
        remove: (key) => this.sendRequest(`${API_URL}/${key}`, 'DELETE', null),
      }),
      // budgetValue: {id: null, sum: 0},
    };  
    
    // this.onInitNewRow = this.onInitNewRow.bind(this);
  }

  // showResult(result) {
  //   const budgetValue = {
  //     id: this.props.data.data.key,
  //     sum: result?.reduce((acc, val) => acc + val.amount, 0)
  //   };

  //   console.log('budgetValue', budgetValue);
  //   this.setState(() => {return {budgetValue}});
  //   this.props.setBudget(budgetValue)
  // }

  sendRequest(url, method = 'GET', data = {}) {    
    console.log('data: ', data);
    if (method === 'GET') {
      return fetch(url).then((result) => result.json().then((data) => {
        if (result.ok) return data;
        throw data.Message;
      }));
    }

    return fetch(url, {
      method,
      body: data,
      headers: { 'Content-Type': 'application/json;charset=UTF-8' },
    }).then((result) => {
      if (result.ok) {
        return result.text().then((text) => text && JSON.parse(text));
      }
      return result.json().then((json) => {
        throw json.Message;
      });
    });
  }
  
  render() {
    const { budgetData } = this.state;
    return (
      <React.Fragment>
        <DataGrid dataSource={budgetData}>            
          <Column dataField="date_Entry" dataType="date" format="MM/dd/yyyy" caption="Date of Entry"></Column>
          <Column dataField="type" caption="Type"></Column>
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
