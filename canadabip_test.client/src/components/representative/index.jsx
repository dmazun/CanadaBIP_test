import { useState } from "react";
import { RepNameSelect } from "./RepNameSelect";
import { RepSummary } from "./RepSummary";
import { RepBudget } from "./RepBudget";

export default function Representative() {
  const [repSACode, setRepSACode] = useState('ALL');
  
  return (
    <>
      <h1>Representative</h1>

      <RepNameSelect selectRepName={(code) => setRepSACode(code)}  />
      <RepSummary repSACode={repSACode} />
      <RepBudget repSACode={repSACode} />
    </>
  );
}