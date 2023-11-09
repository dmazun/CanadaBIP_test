import React, { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import CustomStore from 'devextreme/data/custom_store';
import { DataGrid, Column, Editing, Lookup, MasterDetail } from "devextreme-react/data-grid";
import { types, eventTypes, accessTypes, attendanceTypes } from "./data";
import RepresentativeDetail from './RepresentativeDetail'

const API_URL = "http://localhost:3000";

export class RepsTable extends Component {
  constructor(props) {
    super(props);

    this.state = {
      repsData: new CustomStore({
        key: 'id',
        load: () => this.sendRequest(`${API_URL}/BudgetRepresentative`),
        insert: (values) => this.sendRequest(`${API_URL}/BudgetRepresentative`, 'POST', JSON.stringify(values)),
        update: (key, values) => this.sendRequest(`${API_URL}/BudgetRepresentative/${key}`, 'PATCH', JSON.stringify(values)),
        remove: (key) => this.sendRequest(`${API_URL}/BudgetRepresentative/${key}`, 'DELETE', null),
      }),
    }
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
    const { repsData } = this.state;

    return (
      <div>
        <p>Reps table</p>

        <DataGrid id="grid-container" dataSource={repsData} >

          <Column dataField="date" dataType="date" caption="Month / Mois"></Column>
          <Column dataField="Event_Name" caption="Name of Event/Nom de l'événement"></Column>
          <Column dataField="BU_NAME" caption="Brand/Produit"></Column>
          <Column dataField="Note" caption="Notes/Notes"></Column>
          <Column dataField="Type" caption="Type/Type">
            <Lookup dataSource={types} displayExpr="name" valueExpr="name" />
          </Column>
          <Column dataField="Event_Type" caption="Type of Event / Type d'événement">
            <Lookup dataSource={eventTypes} displayExpr="name" valueExpr="name" />
          </Column>
          <Column dataField="Shared_Individual" caption="Shared/Individual/Événement partagé">
            <Lookup dataSource={accessTypes} displayExpr="name" valueExpr="name" />
          </Column>
          <Column dataField="Attendance" caption="Attendance/Présence">
            <Lookup dataSource={attendanceTypes} displayExpr="name" valueExpr="name" />
          </Column>
          <Column dataField="Amount_Allocated" dataType="number" caption="Amount/Montant"></Column>
          
          <Editing
            mode="row"
            allowUpdating={true}
            allowDeleting={true}
            allowAdding={true} />

          <MasterDetail enabled={true} component={RepresentativeDetail} />
        </DataGrid>
      </div>      
    )
  }
}