import React, { Component } from "react";
import "devextreme/dist/css/dx.light.css";
import {
  DataGrid,
  Column,
  MasterDetail,
  Editing,
  Lookup,
  Summary,
  TotalItem,
} from "devextreme-react/data-grid";
import CustomStore from 'devextreme/data/custom_store';
import BudgetDetailTemplate from "./BudgetDetailTemplate";
import { BudgetRepresentatives } from "./BudgetRepresentatives";

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
      productsData: [],
      editingRowData: {}
    };
    
    this.getProducts = this.getProducts.bind(this);
    this.handleBudgetDetailsUpdate = this.handleBudgetDetailsUpdate.bind(this);
    this.getProductName = this.getProductName.bind(this);
    this.allowUpdating = this.allowUpdating.bind(this);
  }

  onEditingStart = (e) => {    
    this.getProducts(e);
    this.setState({editingRowData: e.data});
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

  getProducts(editingRowData) {
    this.sendRequest(`${API_URL}/Products`)
    .then(products => {
      return this.setState({
        productsData: editingRowData.data.product
          ? [{product: editingRowData.data.product}, ...products]
          : products
      });
    })
  }

  handleBudgetDetailsUpdate() {
    this.dataGrid.instance.refresh(true);    
  }

  getProductName(rowData) {
    return rowData.product
  }

  allowUpdating(e) {
    return e.row.data.amount_Allocated === 0
  }

  render() {
    const { brandsData, productsData } = this.state;
    
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
          onInitNewRow={this.getProducts}
          onEditingStart={this.onEditingStart}>
          <Column dataField="product" caption="Brand / Produit" calculateDisplayValue={this.getProductName}>
            <Lookup dataSource={productsData} displayExpr="product" valueExpr="product" />
          </Column>
          <Column dataField="amount_Budget" dataType="number" caption="Manager Budget / Budget Gestionnaire"></Column>
          <Column dataField="amount_Allocated" dataType="number" allowEditing={false}
            caption="Budget Allocated / Budget Alloué" >
          </Column>
          <Column dataField="amount_Left" dataType="number" allowEditing={false}
            caption="Left to Allocate / Budget Disponible à Allouer">
          </Column>

          <Summary>
            <TotalItem column="product" summaryType="custom" displayFormat="TOTAL:"/>
            <TotalItem column="amount_Budget" summaryType="sum" valueFormat="currency" displayFormat="{0}"/>
            <TotalItem column="amount_Allocated" summaryType="sum" valueFormat="currency" displayFormat="{0}"/>
            <TotalItem column="amount_Left" summaryType="sum" valueFormat="currency" displayFormat="{0}"/>
          </Summary>

          <Editing
            mode="row"
            useIcons={true}
            allowUpdating={this.allowUpdating}
            allowDeleting={true}
            allowAdding={true} />

          <MasterDetail enabled={true} component={DetailsComponent} />
        </DataGrid>

        <hr />

        <div>
          <BudgetRepresentatives />
        </div>
      </div>
    );
  }
}