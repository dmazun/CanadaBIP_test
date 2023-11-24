import React, { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import {
  DataGrid,
  Column,
  Editing,
} from "devextreme-react/data-grid";
import CustomStore from 'devextreme/data/custom_store';

const API_URL = "https://localhost:7071/api/BudgetManagerRepresentative";

export class BudgetRepresentatives extends Component {
  constructor(props) {
    super(props);

    this.state = {
      brandsData: new CustomStore({
        key: 'id',
        load: () => this.sendRequest(`${API_URL}`),
        // insert: (values) => this.sendRequest(`${API_URL}/brandsManagerRep`, 'POST', JSON.stringify(values)),
        // update: (key, values) => this.sendRequest(`${API_URL}/brandsManagerRep/${key}`, 'PATCH', JSON.stringify(values)),
        // remove: (key) => this.sendRequest(`${API_URL}/brandsManagerRep/${key}`, 'DELETE', null),
      }),
    };    
  }

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

  render() {
    const { brandsData } = this.state;

    return (
      <div>
        <h2>Budget Allocation to Representatives</h2>

        <DataGrid id="grid-container" 
          dataSource={brandsData} >

          <Column dataField="product" caption="Brand/ Produit"></Column>
          <Column dataField="rep_Employee_Name" caption="Rep Rep Name/ Nom Représentant"></Column>
          <Column dataField="date_Entry" dataType="date" 
                  caption="Date of Entry/ Date de l'entrée"></Column>
          <Column
            dataField="amount_Budget"
            dataType="number"
            allowEditing={false}
            caption="Manager Budget/ Budget Gestionnaire"
          ></Column>
          <Column
            dataField="amount_Allocated"
            dataType="number"
            caption="Budget Allocated/ Budget Alloué"
          ></Column>
          <Column
            dataField="amount_Left"
            dataType="number"
            allowEditing={false}
            caption="Remaining To Be Allocated/ Budget Disponible à Allouer"
          ></Column>
          
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