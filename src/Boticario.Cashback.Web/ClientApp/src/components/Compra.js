import React, { Component } from 'react';
import { Table, Button, Col, Dropdown, DropdownButton, Modal, Form } from 'react-bootstrap';
import { NotificationContainer, NotificationManager } from 'react-notifications';

const columns = [
    {
        texto: ""
    },
    {
        texto: "Codigo"
    },
    {
        texto: "Valor"
    },
    {
        texto: "Data"
    },
    {
        texto: "Status",
    },
]

export class Compra extends Component {
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
        NotificationManager.success('Compra cadastrada com sucesso!', 'Notification');
    };
    erro = () => {
        NotificationManager.error('Erro ao cadastrar compra');
    };

    buscarDados() {
        fetch("http://localhost:5001/compra?revendedor_Id=1", {
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
        return this.state.data.map((compras) => {
            const { id, codigo, valor, data, statusDesc } = compras
            return (
                <tr key={id}>
                    <td style={{ textAlign: "center" }}>{this.renderDrop()}</td>
                    <td>{codigo}</td>
                    <td>{valor}</td>
                    <td>{data}</td>
                    <td>{statusDesc}</td>
                </tr>
            )
        })
    }
    renderDrop() {
        return (
            <DropdownButton id="dropdown-item-button" title="Acoes" size="sm">
                <Dropdown.Item as="button">Editar</Dropdown.Item>
                <Dropdown.Item as="button">Excluir</Dropdown.Item>
            </DropdownButton>
        );

    }
    handleChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }
    managerModal(value) {
        this.setState({
            openModal: value
        });
    }


    salvar(event) {
        event.preventDefault();
        var compra = {
            Codigo: parseInt(this.state.codigo),
            Valor: parseFloat(this.state.valor),
            Data: this.state.date
        }

        fetch("http://localhost:5001/compra", {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(compra)
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
    render() {
        return (
            <div>
                <Col>
                    <h1>Compras</h1>
                </Col>
                <br />
                <br />
                <Col style={{ textAlign: "right" }}>
                    <Button variant="primary" onClick={() => this.managerModal(true)} size="sm">Cadastrar compra</Button>
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
                                <Form.Label>Codigo</Form.Label>
                                <Form.Control placeholder="Codigo do produto" required onChange={this.handleChange} name="codigo" />
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>Valor </Form.Label>
                                <Form.Control placeholder="Valor do produto" required onChange={this.handleChange} name="valor" />
                            </Form.Group>
                            <Form.Group >
                                <Form.Label>Data </Form.Label>
                                <Form.Control placeholder="Data da compra" type="date" required onChange={this.handleChange} name="date" />
                            </Form.Group>
                            <div style={{ textAlign: "right" }}>
                                <Button variant="primary" type="submit" > Save Changes </Button>
                                <span>   </span>
                                <Button variant="secondary" onClick={() => this.managerModal(false)}> Close </Button>
                            </div>
                        </Form>
                    </Modal.Body>
                </Modal>
                <NotificationContainer />
            </div>
        );
    }
}
