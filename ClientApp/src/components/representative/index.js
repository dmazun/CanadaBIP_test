import { useState } from "react";
import { RepNameSelect } from "./RepNameSelect";

export default function RepresentativePage() {
  const [repSACode, setRepSACode] = useState('ALL');

  return (
    <>
      <h1>Representative</h1>
      <p>NAME: {repSACode}</p>

      <RepNameSelect selectRepName={(code) => setRepSACode(code)}  />
    </>
  );
}