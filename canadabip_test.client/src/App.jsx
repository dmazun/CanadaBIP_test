import "./App.css";

import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import { Budget } from "./components/budget";
import Representative from "./components/representative";
import AuthorizeView from "./components/AuthorizeView";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route
          path="/"
          element={
            <AuthorizeView pageTitle="Budget">
              <Budget />
            </AuthorizeView>
          }
        />
        <Route
          path="/representative"
          element={
            <AuthorizeView pageTitle="Representative">
              <Representative />
            </AuthorizeView>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}
export default App;
