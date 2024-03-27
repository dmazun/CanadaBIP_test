/* eslint-disable react/prop-types */
import { Component } from "react";
import {
  DataGrid,
  Column,
  Paging,
  Scrolling,
  Selection,
  FilterRow,
} from "devextreme-react/data-grid";
import DropDownBox from "devextreme-react/drop-down-box";

const dropDownOptions = { width: 500 };

export class RepDropDownGridSelect extends Component {
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
        wordWrapEnabled={true}
        height={400}
      >
        <Column dataField="id" caption="id" />
        <Column dataField="name" caption="Name" />

        <Scrolling mode="virtual" />
        <Selection mode="single" />
        <Paging defaultPageSize={25} />
        <FilterRow />
      </DataGrid>
    );
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
      <div>
        <DropDownBox
          onOptionChanged={this.boxOptionChanged}
          opened={this.state.isDropDownOpened}
          dropDownOptions={dropDownOptions}
          dataSource={this.props.data.column.lookup.dataSource}
          contentRender={this.contentRender}
        ></DropDownBox>
      </div>
    );
  }
}
