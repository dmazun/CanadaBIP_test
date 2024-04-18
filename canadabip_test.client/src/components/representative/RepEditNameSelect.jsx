/* eslint-disable react/prop-types */
import { Component } from "react";
import {
  DataGrid,
  Column,
  Scrolling,
  Selection,
} from "devextreme-react/data-grid";
import DropDownBox from "devextreme-react/drop-down-box";

const dropDownOptions = { width: 500 };

export class RepEditNameSelect extends Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: props.data.value,
      isDropDownOpened: false,
    };

    this.onSelectionChanged = this.onSelectionChanged.bind(this);
    this.contentRender = this.contentRender.bind(this);
    this.boxOptionChanged = this.boxOptionChanged.bind(this);
  }

  boxOptionChanged(e) {
    if (e.name === "opened") {
      this.setState({
        isDropDownOpened: e.value,
      });
    }
  }

  contentRender() {
    return (
      <DataGrid
        dataSource={this.props.data.column.lookup.dataSource}
        remoteOperations={true}
        selectedRowKeys={[this.state.selectedRowKeys]}
        hoverStateEnabled={true}
        onSelectionChanged={this.onSelectionChanged}
        height={200}
      >
        <Column dataField="employee_Name" caption="Rep Name" />
        <Column dataField="sales_Area_Name" caption="Territory Name" />
        <Column dataField="sales_Area_Code" caption="Territory ID" />

        <Scrolling mode="virtual" />
        <Selection mode="single" />
      </DataGrid>
    );
  }

  onSelectionChanged(selectionChangedArgs) {
    if (selectionChangedArgs.selectedRowKeys.length) {
      this.setState({
        selectedRowKeys: selectionChangedArgs.selectedRowKeys[0].sales_Area_Code,
        isDropDownOpened: selectionChangedArgs.selectedRowKeys.length === 0,
      });

      this.props.data.setValue(
        selectionChangedArgs.selectedRowKeys[0].sales_Area_Code
      );
    }
  }

  render() {
    return (
      <DropDownBox
        onOptionChanged={this.boxOptionChanged}
        opened={this.state.isDropDownOpened}
        dropDownOptions={dropDownOptions}
        dataSource={this.props.data.column.lookup.dataSource}
        value={this.state.selectedRowKeys}
        displayExpr="employee_Name"
        valueExpr="sales_Area_Code"
        contentRender={this.contentRender}
      ></DropDownBox>
    );
  }
}
