import React, { Component } from "react";
import { Container } from "reactstrap";
import "devextreme/dist/css/dx.light.css";

import { BudgetBrandTable } from "./BudgetBrandTable";
import { BudgetRepresentatives } from "./BudgetRepresentatives";

export class Budget extends Component {
  constructor(props) {
    super(props);
    this.repRef = React.createRef();
  }

  render() {
    return (
      <Container>
        <BudgetBrandTable productChanged={() => this.repRef.current.getProducts()} />
        <BudgetRepresentatives ref={this.repRef} />
      </Container>
    );
  }
}
