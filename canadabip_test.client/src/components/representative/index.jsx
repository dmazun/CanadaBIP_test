import { useState } from "react";
import { Container } from "reactstrap";
import { RepNameSelect } from "./RepNameSelect";
import { RepSummary } from "./RepSummary";
import { RepBudget } from "./RepBudget";

export default function Representative() {
  const [repSACode, setRepSACode] = useState('ALL');
  
  return (
    <>
      <Container>
        <RepNameSelect selectRepName={(code) => setRepSACode(code)}  />
        <RepSummary repSACode={repSACode} />
      </Container>

      <Container fluid={true}>
        <RepBudget repSACode={repSACode} />
      </Container>
    </>
  );
}