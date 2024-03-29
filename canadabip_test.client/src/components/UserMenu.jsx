/* eslint-disable react/prop-types */
/* eslint-disable no-empty */
import { useState } from 'react';
import { useNavigate } from "react-router-dom";
import { AuthorizedUser, AuthorizedUserLogo } from "./AuthorizeView.jsx";
import {
  Dropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
} from 'reactstrap';

function UserMenu() {
  const navigate = useNavigate();
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const toggle = () => setDropdownOpen((prevState) => !prevState);

  const handleSubmit = (e) => {
    e.preventDefault();
    fetch("/logout", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: "",
    })
      .then((data) => {
        if (data.ok) {
          navigate("/login");
        }
      })
      .catch((error) => {
        console.error(error);
      });
  };

  return (
    <>
      <Dropdown isOpen={dropdownOpen} toggle={toggle} >
        <DropdownToggle className="header__logo" style={{ borderRadius: "50%", backgroundColor: "#201a31" }} >
          <AuthorizedUserLogo value="email" />
        </DropdownToggle>

        <DropdownMenu>
          <DropdownItem header><AuthorizedUser value="email" /></DropdownItem>
          <DropdownItem onClick={handleSubmit}>Logout</DropdownItem>
        </DropdownMenu>
      </Dropdown>
    </>
  );
}

export default UserMenu;
