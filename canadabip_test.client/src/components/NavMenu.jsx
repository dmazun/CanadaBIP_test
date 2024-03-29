/* eslint-disable react/prop-types */
import { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import UserMenu from "./UserMenu.jsx";
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
      <header className='header'>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
          <NavbarBrand tag={Link} to="/">
            <img src="src/assets/Pfizer_logo2022.png" width={100} alt="CANADA BIP" />
            <p className="navbar-title">CANADA BIP <br /> <span>Budget Tracker - {this.props.pageTitle}</span></p>
          </NavbarBrand>
          
          <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
          
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/" exact="true" activeclassname="active">Budget</NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/representative" >Representative</NavLink>
              </NavItem>
              <NavItem>                
                <UserMenu />                
              </NavItem>
            </ul>
          </Collapse>
        </Navbar>
      </header>
    );
  }
}