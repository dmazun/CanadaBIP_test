import { useState } from "react";
import { RepNameSelect } from "./RepNameSelect";
import { RepSummary } from "./RepSummary";

export default function RepresentativePage() {
  const [repSACode, setRepSACode] = useState('ALL');
  
  return (
    <>
      <h1>Representative</h1>
      <p>NAME: {repSACode}</p>

      <RepNameSelect selectRepName={(code) => setRepSACode(code)}  />
      <RepSummary repSACode={repSACode} />
    </>
  );
}