import React, { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import {
  DataGrid,
  Column,
  Editing,
  Lookup,
} from "devextreme-react/data-grid";
import CustomStore from 'devextreme/data/custom_store';
import { BudgetRepSelectComponent } from "./BudgetRepSelectComponent";

const API_URL = "https://localhost:7071/api/BudgetManagerRepresentative";

export class BudgetRepresentatives extends Component {
  constructor(props) {
    super(props);

    this.state = {
      brandsData: new CustomStore({
        key: 'id',
        load: () => this.sendRequest(`${API_URL}`),
        insert: (values) => this.sendRequest(`${API_URL}`, 'POST', JSON.stringify(values))
          .then(() => this.getRepNames()),
        update: (key, values) => this.sendRequest(`${API_URL}/${key}`, 'PUT', 
          JSON.stringify({
            ...{
              sales_Area_Code: this.state.editingRowData.rep_Sales_Area_Code,
              date_Entry: this.state.editingRowData.date_Entry,
              product: this.state.editingRowData.product,
              amount_Allocated: this.state.editingRowData.amount_Allocated
            },
            ...values
          }))
          .then(() => this.getRepNames()),
        remove: (key) => this.sendRequest(`${API_URL}/${key}`, 'DELETE', null)
          .then(() => this.getRepNames())
      }),
      productsData: [],
      repNamesData: [],
      editingRowData: {}
    };    
  }

  componentDidMount() {
    this.getProducts();
    this.getRepNames();
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

  getProducts() {
    this.sendRequest(`${API_URL}/Products`)
    .then(products => this.setState({ productsData: products }));
  }

  getRepNames() {
    this.sendRequest(`${API_URL}/RepNames`)
      .then(res => this.setState({ repNamesData: res }));
  }

  onEditingStart = (e) => {
    this.setState({editingRowData: e.data});
  }

  onEditorPreparing = (e) => {
    if (e.dataField === "product") {
      e.editorOptions.disabled = e.row.data && e.row.data.is_BR === 1;
    }
  }
  
  getProductName(rowData) {
    return rowData.product
  }
   
  calculateRepName(rowData) {
    return rowData.rep_Employee_Name
  }
   
  calculateRepAreaCode(rowData) {
    return rowData.rep_Sales_Area_Code
  }

  setProductValue(rowData, value) {
    rowData.rep_Sales_Area_Code = null;
    this.defaultSetCellValue(rowData, value);    
  }

  render() {
    const { brandsData, productsData, repNamesData } = this.state;

    return (
      <div>
        <h2>Budget Allocation to Representatives</h2>

        <DataGrid id="grid-container" 
                  dataSource={brandsData}
                  onEditorPreparing={this.onEditorPreparing}
                  onEditingStart={this.onEditingStart}>

          <Column dataField="product" caption="Brand/ Produit"
                  setCellValue={this.setProductValue}
                  calculateDisplayValue={this.getProductName} >
            <Lookup dataSource={productsData} 
                    displayExpr="product" 
                    valueExpr="product" />
          </Column>

          <Column dataField="sales_Area_Code" 
                  caption="Rep Name/ Nom Représentant"
                  calculateCellValue={this.calculateRepAreaCode}
                  calculateDisplayValue={this.calculateRepName}
                  editCellComponent={BudgetRepSelectComponent}>
            <Lookup dataSource={repNamesData} 
                    calculateDisplayValue={this.calculateRepName}
                    valueExpr="sales_Area_Code"/>
          </Column>

          <Column dataField="date_Entry" 
                  dataType="date" 
                  caption="Date of Entry/ Date de l'entrée">
          </Column>

          <Column
            dataField="amount_Budget"
            dataType="number"
            allowEditing={false}
            caption="Manager Budget/ Budget Gestionnaire">
          </Column>

          <Column
            dataField="amount_Allocated"
            dataType="number"
            caption="Budget Allocated/ Budget Alloué">
          </Column>

          <Column
            dataField="amount_Left"
            dataType="number"
            allowEditing={false}
            caption="Remaining To Be Allocated/ Budget Disponible à Allouer">
          </Column>
          
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