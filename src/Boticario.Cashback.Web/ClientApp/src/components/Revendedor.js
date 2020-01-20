import React, { Component } from 'react';
import { Table, Button, Col, Modal, Form } from 'react-bootstrap';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import Cookies from 'universal-cookie';

const cookies = new Cookies();
const columns = [
    {
        texto: "Nome"
    },
    {
        texto: "Email"
    },
    {
        texto: "CPF"
    }
]

export class Revendedor extends Component {
    constructor(props) {
        super(props);

        this.state = {
            data: [],
            openModal: false

        };
        this.salvar = this.salvar.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    componentDidMount() {
        fetch("http://localhost:5001/revendedor", {
            method: "GET",
            headers: {
                'Authorization': 'Bearer ' + this.getToken(),
            },
        })
            .then(function (response) {
                if (response.status === 401) {
                    NotificationManager.error("401 - O token do usuario logado expirou.");
                    cookies.remove('token')

                    setTimeout(() => {
                        window.location.reload();
                    }, 1500);
                    return;
                }
                return response.json();
            })
            .then((responseJson) => {
                if (responseJson.code === 500) {
                    this.erro(responseJson.message)
                    return;
                }
                if (responseJson === undefined) {
                    return;
                }
                this.setState({
                    data: responseJson
                });
            })
            .catch((error) => {
                console.error(error);
            });

    }
       
    sucesso = () => {
        NotificationManager.success('Revendedor criado!');
    };
    erro = (message) => {
        NotificationManager.error(message);
    };

    handleChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }
    getToken() {
        var cookieItem = cookies.get('token')
        if (cookieItem === undefined) {
            return;
        }
        return cookieItem.accessToken;
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
            headers: {
                'Authorization': 'Bearer ' + this.getToken(),
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(revendedor)
        })
            .then(function (response) {
                if (response.status === 401) {
                    NotificationManager.error("401 - O token do usuario logado expirou.");
                    cookies.remove('token')

                    setTimeout(() => {
                        window.location.reload();
                    }, 1500);
                    return;
                }
                return response.json();
            })
            .then((responseJson) => {
                if (responseJson.code === 500) {
                    this.erro(responseJson.message)
                    return;
                }
                var updateData = this.state.data;
                updateData.unshift(responseJson);
                this.setState({
                    data: updateData
                });
                this.managerModal(false);
                this.sucesso();
            })
            .catch((error) => {
                console.error(error);
                this.erro("Erro ao cadastrar revendedor");
            });
    }

    renderTableHeader() {
        return columns.map((key, index) => {
            return <th key={index}>{key.texto}</th>
        })
    }

    renderTableData() {
        if (this.state.data.length === 0) {
            return (<tr style={{ textAlign: "center" }} ><td colSpan="3">Nao foram encontrados registros</td></tr>);
        }

        return this.state.data.map((revendedores) => {
            const { id, nome, email, cpf } = revendedores
            return (
                <tr key={id}>
                    <td>{nome}</td>
                    <td>{email}</td>
                    <td>{cpf}</td>
                </tr>
            )
        })
    }

    managerModal(value) {
        this.setState({
            openModal: value
        });
    }

    render() {
        return (
            <div>
                <Col>
                    <h1>Revendedores</h1>
                </Col>
                <br />
                <br />
                <Col style={{ textAlign: "right" }}>

                    <Button variant="primary" onClick={() => this.managerModal(true)} size="sm">Cadastrar revendedor</Button>
                </Col>
                <Col>
                    <Table striped bordered hover size="sm">
                        <tbody>
                            <tr>{this.renderTableHeader()}</tr>
                            {this.renderTableData()}
                        </tbody>
                    </Table>
                </Col>
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
                <NotificationContainer />
            </div >
        );
    }
}
