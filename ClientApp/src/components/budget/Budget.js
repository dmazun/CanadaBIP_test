import React, { Component } from "react";
import "devextreme/dist/css/dx.light.css";

import { BudgetBrandTable } from "./BudgetBrandTable";
import { BudgetRepresentatives } from "./BudgetRepresentatives";
export class Budget extends Component {
  render() {
    return (
      <div>
        <h1>Budget</h1>

        <BudgetBrandTable />
        <BudgetRepresentatives />
        
      </div>
    );
  }
}
