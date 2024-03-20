import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import LogoutLink from "./LogoutLink.jsx";
import AuthorizeView, { AuthorizedUser } from "./AuthorizeView.jsx";
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render() {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
          <NavbarBrand tag={Link} to="/">CanadaBIP_test</NavbarBrand>
          <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/">Budget</NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/representative">Representative</NavLink>
              </NavItem>
              <NavItem>
                {/* <LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink> */}
                <NavLink tag={Link} className="text-dark" > 
                {/* to="/logout" */}
                  <LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink>
                </NavLink>
              </NavItem>
            </ul>
          </Collapse>
        </Navbar>
      </header>
    );
  }
}
{/* <span><LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink></span> */}