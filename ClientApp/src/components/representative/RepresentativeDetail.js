import React, { Component } from "react";
import CustomStore from 'devextreme/data/custom_store';
import { DataGrid, Column, Editing } from "devextreme-react/data-grid";

const API_URL = "http://localhost:3000";

class RepresentativeDetail extends Component {
  constructor(props) {
    super(props);
    const id = this.props.data.key;

    this.state = {
      repsBudgetData: new CustomStore({
        key: 'id',
        // onLoaded: (result) => this.showResult(result),
        load: () => this.sendRequest(`${API_URL}/BudgetRepresentativeDetail?Budget_Representative_ID=${id}`),
        insert: (values) => this.sendRequest(`${API_URL}/BudgetRepresentativeDetail`, 'POST', JSON.stringify({Budget_Representative_ID: id, ...values})),
        update: (key, values) => this.sendRequest(`${API_URL}/BudgetRepresentativeDetail/${key}`, 'PATCH', JSON.stringify(values)),
        remove: (key) => this.sendRequest(`${API_URL}/BudgetRepresentativeDetail/${key}`, 'DELETE', null),
      }),
      // budgetValue: {id: null, sum: 0},
    };  
    
    this.onInitNewRow = this.onInitNewRow.bind(this);
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

  onInitNewRow(e) {
    e.data.amount = 0;
    e.data.date = new Date();
  }
  
  render() {
    const { repsBudgetData } = this.state;
    // budgetValue

    return (
      <React.Fragment>
        {/* <p>SUM = {budgetValue.id}: {budgetValue.sum} </p> */}

        <DataGrid dataSource={repsBudgetData}
          onInitNewRow={this.onInitNewRow}>
            
          <Column dataField="Name"></Column>
          <Column dataField="Amount_Budget" dataType="number"></Column>

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

export default RepresentativeDetail;
