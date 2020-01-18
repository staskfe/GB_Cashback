import React, { Component } from 'react';
import { Table, Button, Col, Modal, Form } from 'react-bootstrap';
import { NotificationContainer, NotificationManager } from 'react-notifications';

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
        this.buscarDados();
        this.salvar = this.salvar.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    sucesso = () => {
        NotificationManager.success('Revendedor criado!', 'Notification');
    };
    erro = () => {
        NotificationManager.error('Erro ao criar revendedor');
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
            .then((responseJson) => {
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
                this.erro();
            });
    }

    buscarDados() {
        fetch("http://localhost:5001/revendedor", {
            method: "GET",
        })
            .then((response) => response.json())
            .then((responseJson) => {
                this.setState({
                    data: responseJson
                });
            })
            .catch((error) => {
                console.error(error);
            });
    }

    renderTableHeader() {
        return columns.map((key, index) => {
            return <th key={index}>{key.texto}</th>
        })
    }

    renderTableData() {
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
