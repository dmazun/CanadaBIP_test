import "./App.css";

import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./components/account/Login";
import { Budget } from "./components/budget";
import Representative from "./components/representative";
import AuthorizeView from "./components/AuthorizeView";
import ForgotPassword from "./components/account/ForgotPassword";
import ResetPassword from "./components/account/ResetPassword";
import ChangePassword from "./components/account/ChangePassword";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/forgot-password" element={<ForgotPassword />} />
        <Route path="/reset-password" element={<ResetPassword />} />
        <Route path="/change-password" element={<ChangePassword />} />
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
