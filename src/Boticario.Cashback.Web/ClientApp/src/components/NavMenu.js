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
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
            logged: false
        };

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
        this.success("Deslogado com sucesso");

        localStorage.removeItem("token");

        if (this.state.logged) {
            this.setState({
                logged: false
            });
        }
    }

    getRevendedor() {
        var currentToken = localStorage.getItem('token');
        var jsonToken = JSON.parse(currentToken);
        if (jsonToken === null) {
            return;
        }

        var tokenDate = Date.parse(jsonToken.expiration);
        var date = new Date();
        var currentDate = Date.parse(date);

        if (currentDate > tokenDate) {
            this.erro("O token expirou!");
            localStorage.removeItem("token");
            return (<Redirect to="/" />)
        } else {
            if (!this.state.logged) {
                this.setState({
                    logged: true
                });
            }
        }
        return (
            <UncontrolledDropdown>
                <DropdownToggle tag="a" className="nav-link" caret>
                    Seja bem vindo(a) {jsonToken.revendedor_nome}
                </DropdownToggle>
                <DropdownMenu>
                    <DropdownItem onClick={() => this.logout()} active>Logout</DropdownItem>                   
                </DropdownMenu>
            </UncontrolledDropdown>
        )
    }

    redirect() {
        if (this.state.logged) {
            return (<Redirect to="/Compra" />)
        } else {
            return (<Redirect to="/" />)
        }

    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand > Boticario </NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink style={this.state.logged ? {} : { display: 'none' }} tag={Link} className="text-dark" to="/Revendedor">Revendedor</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink style={this.state.logged ? {} : { display: 'none' }} tag={Link} className="text-dark" to="/Compra">Compra</NavLink>
                                </NavItem>
                            </ul>
                        </Collapse>
                        {this.getRevendedor()}
                    </Container>
                </Navbar>
                <NotificationContainer />
                {this.redirect()}
            </header>

        );
    }
}
