import React, { Component } from "react";
import { DataGrid, Column, Scrolling, Selection } from 'devextreme-react/data-grid';
import DropDownBox from 'devextreme-react/drop-down-box';
import CustomStore from 'devextreme/data/custom_store';
import { ApiService } from "../../services/ApiService";

const API_URL = "https://localhost:7071/api/BudgetRepresentative";

const dropDownOptions = { width: 500 };

export class RepNameSelect extends Component {
  constructor(props) {
    super(props);
    this.apiService = new ApiService();

    this.state = {
      selectedRowKeys: [0],
      selectedRowsData: [{
        employee_Name: 'All',
        sales_Area_Code: 'All',
      }],
      isDropDownOpened: false,
      repNames: new CustomStore({
        key: 'id',
        loadMode: 'raw',
        load: () => this.apiService.sendRequest(`${API_URL}/RepNames`),        
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

  contentRender() {
    return (
      <DataGrid
        dataSource={this.state.repNames}
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

      this.props?.selectRepName(selectionChangedArgs.selectedRowsData[0].sales_Area_Code);
    }
  }

  render() {
    return (
      <div>
        <p>Rep Name/Nom Représentant:</p>

        <DropDownBox
          onOptionChanged={this.boxOptionChanged}
          opened={this.state.isDropDownOpened}
          dropDownOptions={dropDownOptions}
          dataSource={this.state.repNames}
          value={this.state.selectedRowKeys}
          displayExpr="employee_Name"
          contentRender={this.contentRender}>
        </DropDownBox>
      </div>      
    );
  }
}