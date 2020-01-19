import React, { Component } from 'react';
import { Button, Form, Col, Row, Modal } from 'react-bootstrap';
import { Redirect, Switch } from 'react-router-dom';

import { NotificationContainer, NotificationManager } from 'react-notifications';
import Cookies from 'universal-cookie';

const cookies = new Cookies();

export class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "",
            senha: "",
            authenticado: false,
            openModal: false,
        };

        this.senhaAlterada = this.senhaAlterada.bind(this);
        this.ValidarUsuario = this.ValidarUsuario.bind(this);
        this.emailAlterado = this.emailAlterado.bind(this);
        this.redirect = this.redirect.bind(this);

        this.salvar = this.salvar.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    setCookie(values) {
        var date = new Date(values.expiration); 
        cookies.set('token', JSON.stringify(values), { path: '/', expires: date });
    };
    getToken() {
        var cookieItem = cookies.get('token')
        if (cookieItem === undefined) {
            return;
        }
        return cookieItem;
    }

    senhaAlterada(event) {
        const target = event.target;
        this.setState({
            senhaAlterada: target.value
        });
    }

    emailAlterado(event) {
        const target = event.target;
        this.setState({
            emailAlterado: target.value
        });
    }

    redirect() {       
        var jsonToken = this.getToken();
        if (jsonToken !== undefined) {
            return (
                <Switch>
                    <Redirect to="/Inicio" />
                </Switch>
            )
        }
    }

    ValidarUsuario(event) {
        var loginViewModel = {
            Email: this.state.emailAlterado,
            Senha: this.state.senhaAlterada
        };

        fetch("http://localhost:5001/login", {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(loginViewModel)
        })
            .then((response) => response.json())
            .then((responseJson) => {
                if (responseJson.authenticated) {
                    this.setCookie(responseJson)
                    this.sucesso("Usuario logado com sucesso")
                    this.setState({
                        authenticado: true
                    });
                } else {
                    this.erro(responseJson.message)
                }
            })
            .catch((error) => {
                this.erro("Erro ao logar");
                console.error(error);
            });

        event.preventDefault();

    }
    render() {
        return (
            <div>
                <Row >
                    <Col sm="8"  >
                        
                    </Col>
                    <Col sm="4">
                        <Form onSubmit={this.ValidarUsuario}>
                            <Form.Group controlId="formBasicEmail">
                                <Form.Label>Email </Form.Label>
                                <Form.Control placeholder="Digite seu email" onChange={this.emailAlterado} />
                            </Form.Group>
                            <Form.Group controlId="formBasicPassword">
                                <Form.Label>Senha</Form.Label>
                                <Form.Control type="password" placeholder="Senha" onChange={this.senhaAlterada} />
                            </Form.Group>
                            <Form.Group controlId="formBasicCheckbox">
                            </Form.Group>
                            <Row >
                                <Col sm="6" >
                                    <Button onClick={() => this.managerModal(true)} variant="link">Criar conta</Button>
                                </Col>
                                <Col sm="6" style={{ textAlign: "right" }}>
                                    <Button variant="primary" type="submit">Login</Button>
                                </Col>
                            </Row>
                        </Form>
                    </Col>
                </Row>

                {this.redirect()}
                {this.modalSalvar()}
                <NotificationContainer />
            </div>
        );
    }



    sucesso = (message) => {
        NotificationManager.success(message);
    };
    erro = (message) => {
        NotificationManager.error(message);
    };

    handleChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }
    salvar(event) {
        event.preventDefault();
        var revendedor = {
            Nome: this.state.nome,
            Email: this.state.email,
            cpf: this.state.cpf,
            Senha: this.state.senha,
        }

        fetch("http://localhost:5001/revendedor", {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(revendedor)
        })
            .then((response) => response.json())
            .then(() => {
                this.managerModal(false);
                this.sucesso("Revendedor criado com sucesso!");
            })
            .catch((error) => {
                console.error(error);
                this.erro("Erro ao criar revendedor");
            });
    }
    managerModal(value) {
        this.setState({
            openModal: value
        });
    }
    modalSalvar() {
        return (
            <Modal size="lg" show={this.state.openModal}>
                <Modal.Header>
                    <Modal.Title>Cadastrar revendedor</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form onSubmit={this.salvar}>
                        <Form.Group >
                            <Form.Label>Nome</Form.Label>
                            <Form.Control placeholder="Nome" required onChange={this.handleChange} name="nome" />
                        </Form.Group>
                        <Form.Group controlId="formGridEmail">
                            <Form.Label>Email</Form.Label>
                            <Form.Control type="email" placeholder="Enter email" required onChange={this.handleChange} name="email" />
                        </Form.Group>
                        <Form.Row>
                            <Form.Group as={Col}>
                                <Form.Label>CPF</Form.Label>
                                <Form.Control required onChange={this.handleChange} name="cpf" type="number" />
                            </Form.Group>
                            <Form.Group as={Col} controlId="formGridPassword">
                                <Form.Label>Senha</Form.Label>
                                <Form.Control type="password" placeholder="Password" required onChange={this.handleChange} name="senha" />
                            </Form.Group>
                        </Form.Row>
                        <div style={{ textAlign: "right" }}>
                            <Button variant="primary" type="submit" > Save Changes </Button>
                            <span>   </span>
                            <Button variant="secondary" onClick={() => this.managerModal(false)}> Close </Button>
                        </div>
                    </Form>
                </Modal.Body>
            </Modal>
        )
    }
}
