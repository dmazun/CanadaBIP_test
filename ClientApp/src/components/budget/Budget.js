import React, { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import {
  DataGrid,
  Column,
  MasterDetail,
  Editing,
} from "devextreme-react/data-grid";
import CustomStore from 'devextreme/data/custom_store';
import BudgetDetailTemplate from "./BudgetDetailTemplate";
import { BudgetReps } from "./BudgetReps";

const API_URL = "https://localhost:7071/api/BudgetManager";

export class Budget extends Component {
  constructor(props) {
    super(props);

    this.state = {
      brandsData: new CustomStore({
        key: 'id',
        load: () => this.sendRequest(`${API_URL}`),
        insert: (values) => this.sendRequest(`${API_URL}`, 'POST', JSON.stringify(values)),
        update: (key, values) => this.sendRequest(`${API_URL}/${key}`, 'PUT', JSON.stringify({...this.state.editingRowData, ...values})),
        remove: (key) => this.sendRequest(`${API_URL}/${key}`, 'DELETE', null),
      }),
      editingRowData: {}
    };
    
    this.handleBudgetDetailsUpdate = this.handleBudgetDetailsUpdate.bind(this);
  }

  onEditingStart = (e) => this.setState({ editingRowData: e.data });

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

  handleBudgetDetailsUpdate() {
    this.dataGrid.instance.refresh(true);    
  }

  render() {
    const { brandsData } = this.state;
    
    const DetailsComponent = (e) => {
      return <BudgetDetailTemplate data={e} budgetDetailsUpdated={this.handleBudgetDetailsUpdate}/>;
    };

    return (
      <div>
        <h1>Budget</h1>
        <h2>Brand Level Budget</h2>

        <DataGrid id="grid-container" 
                  dataSource={brandsData}
                  ref={ref => this.dataGrid = ref}
                  onEditingStart={this.onEditingStart}> 

          <Column dataField="product" caption="Brand / Produit"></Column>
          <Column dataField="amount_Budget" dataType="number" caption="Manager Budget / Budget Gestionnaire"></Column>
          <Column dataField="amount_Allocated" dataType="number" allowEditing={false}
                  caption="Budget Allocated / Budget Alloué" >
          </Column>
          <Column dataField="amount_Left" dataType="number" allowEditing={false}
                  caption="Left to Allocate / Budget Disponible à Allouer">
          </Column>
        
          <Editing
              mode="row"
              allowUpdating={true}
              allowDeleting={true}
              allowAdding={true} />

          <MasterDetail enabled={true} component={DetailsComponent} />
        </DataGrid>

        <hr />

        <div>
          {/* <BudgetReps /> */}
        </div>
      </div>
    );
  }
}