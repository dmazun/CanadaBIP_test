import React, { Component } from "react";
import DataGrid, { Column, Scrolling, Selection } from 'devextreme-react/data-grid';
import DropDownBox from 'devextreme-react/drop-down-box';
import ArrayStore from 'devextreme/data/array_store';

const dropDownOptions = { width: 500 };

export class BudgetRepSelectComponent extends Component {
  constructor(props) {
    super(props);

    this.state = {
      selectedRowKeys: props.data.value,
      isDropDownOpened: false,
      repNamesFiltered: new ArrayStore({
        key: 'sales_Area_Code',
        data: this.getFilteredRepNames()
      })
    };

    this.onSelectionChanged = this.onSelectionChanged.bind(this);
    this.contentRender = this.contentRender.bind(this);
    this.boxOptionChanged = this.boxOptionChanged.bind(this);
  }

  boxOptionChanged(e) {
    if (e.name === 'opened') {
      this.setState({
        isDropDownOpened: e.value,
      });
    }
  }

  getFilteredRepNames() {
    const product = this.props.data.data.product;
    const products = this.props.data.column.lookup.dataSource;
    const data = products.filter(r => r.product === product);

    const editedRep = {
      employee_ID: this.props.data.data.rep_Employee_ID,
      employee_Name: this.props.data.data.rep_Employee_Name,
      sales_Area_Code: this.props.data.data.rep_Sales_Area_Code,
      sales_Area_Name: this.props.data.data.rep_Sales_Area_Name,
    } 

    return this.props.data.value ? [editedRep, ...data] : data;
  }

  contentRender() {
    return (
      <DataGrid
        dataSource={this.state.repNamesFiltered}
        remoteOperations={true}
        selectedRowKeys={[this.state.selectedRowKeys]}
        hoverStateEnabled={true}
        onSelectionChanged={this.onSelectionChanged}
        focusedRowEnabled={true}
        defaultFocusedRowKey={this.state.selectedRowKeys}  
      >
        <Column dataField="employee_Name" caption="Rep Name" />
        <Column dataField="sales_Area_Name" caption="Territory Name" />
        <Column dataField="sales_Area_Code" caption="Territory ID" />
        
        <Scrolling mode="virtual" />
        <Selection mode="single" />
      </DataGrid>
    )
  }

  onSelectionChanged(selectionChangedArgs) {
    if (selectionChangedArgs.selectedRowKeys.length) {
      this.setState({
        selectedRowKeys: selectionChangedArgs.selectedRowKeys[0],
        isDropDownOpened: selectionChangedArgs.selectedRowKeys.length === 0,
      });

      this.props.data.setValue(selectionChangedArgs.selectedRowKeys[0]);
    }
  }

  render() {
    return (
      <DropDownBox
        onOptionChanged={this.boxOptionChanged}
        opened={this.state.isDropDownOpened}
        dropDownOptions={dropDownOptions}
        dataSource={this.state.repNamesFiltered}
        value={this.state.selectedRowKeys}
        displayExpr="employee_Name"
        valueExpr="sales_Area_Code"
        disabled={this.props.data.row.data.is_BR === 1 ? true : false}
        contentRender={this.contentRender}>
      </DropDownBox>
    );
  }
}