import React, { Component } from 'react';
import {
    Collapse,
    Container,
    Navbar,
    NavbarBrand,
    NavbarToggler,
    NavItem,
    NavLink,
    UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem
} from 'reactstrap';
import { Link, Redirect } from 'react-router-dom';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import Cookies from 'universal-cookie';
import './NavMenu.css';
import { CashbackAcumulado } from './CashbackAcumulado'

import logo from './img/logo.png'; // Tell Webpack this JS file uses this image

const cookies = new Cookies();
export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
            logged: true,
        };

    }


    getToken() {
        var cookieItem = cookies.get('token')
        if (cookieItem === undefined) {
            return;
        }
        return cookieItem;
    }
    removeToken() {
        cookies.remove('token')
    }

    success = (message) => {
        NotificationManager.success(message);
    };
    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    logout() {
        this.removeToken();
        this.setState({
            logged: false
        });
        this.success("Deslogado com sucesso");
    }

    redirect() {
        var jsonToken = this.getToken();
        if (jsonToken === undefined) {
            return (
                <Redirect to="/" />
            )
        }
    }

    getRevendedor() {
        var currentToken = this.getToken();
        if (currentToken === undefined) {
            return;
        }
     
        return (
            <UncontrolledDropdown>
                <DropdownToggle tag="a" className="nav-link" caret>
                    Seja bem vindo(a) {currentToken.revendedor_nome}
                </DropdownToggle>
                <DropdownMenu>
                    <DropdownItem onClick={() => this.logout()} >Logout</DropdownItem>
                    <CashbackAcumulado />
                </DropdownMenu>
            </UncontrolledDropdown>
        )
    }
    isAuth() {
        var currentToken = this.getToken();
        return currentToken !== undefined;
    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand><img src={logo} alt="Logo" /></NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink style={this.isAuth() ? {} : { display: 'none' }} tag={Link} className="text-dark" to="/Inicio">Inicio</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink style={this.isAuth() ? {} : { display: 'none' }} tag={Link} className="text-dark" to="/Revendedor">Revendedores</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink style={this.isAuth() ? {} : { display: 'none' }} tag={Link} className="text-dark" to="/Compra">Compras</NavLink>
                                </NavItem>
                            </ul>
                        </Collapse>
                        {this.getRevendedor()}
                    </Container>
                </Navbar>
                {this.redirect()}
                <NotificationContainer />

            </header>
        );
    }
}
