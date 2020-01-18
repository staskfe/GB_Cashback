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
            abrirModalSalvar: false,
            abrirModalDeletar: false,
            deletarId: 0
        };
        this.buscarDados();
        this.salvar = this.salvar.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.deletar = this.deletar.bind(this);
    }
    sucesso = (message) => {
        NotificationManager.success(message, 'Notification');
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
                    <td style={{ textAlign: "center" }}>{this.renderDrop({ id })}</td>
                    <td>{codigo}</td>
                    <td>{valor}</td>
                    <td>{data}</td>
                    <td>{statusDesc}</td>
                </tr>
            )
        })
    }
    renderDrop(id) {
        return (
            <DropdownButton id="dropdown-item-button" title="Acoes" size="sm">
                <Dropdown.Item as="button">Editar</Dropdown.Item>
                <Dropdown.Item as="button" onClick={() => this.managerModalDeletar(true, id)}>Excluir</Dropdown.Item>
            </DropdownButton>
        );

    }
    handleChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }
    managerModalSalvar(value) {
        this.setState({
            abrirModalSalvar: value
        });
    }
    managerModalDeletar(value, id) {
        this.setState({
            abrirModalDeletar: value
        });

        if (id != undefined) {
            this.setState({
                deletarId: id.id
            });
        }
    }
    deletar(event) {
        event.preventDefault();
        var id = this.state.deletarId;
        fetch("http://localhost:5001/compra?id=" + id, {
            method: "DELETE",
        })
            .then(() => {
                this.managerModalDeletar(false);
                this.sucesso('Compra deletada com sucesso!');
                this.buscarDados();
            })
            .catch((error) => {
                console.error(error);
                this.erro();
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
                this.managerModalSalvar(false);
                this.sucesso('Compra cadastrada com sucesso!');
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
                    <Button variant="primary" onClick={() => this.managerModalSalvar(true)} size="sm">Cadastrar compra</Button>
                </Col>
                <Col>
                    <Table striped bordered hover size="sm">
                        <tbody>
                            <tr>{this.renderTableHeader()}</tr>
                            {this.renderTableData()}
                        </tbody>

                    </Table>
                </Col>
                {this.modalSalvar()}
                {this.modalDeletar()}


                <NotificationContainer />
            </div>
        );
    }
    
    modalSalvar() {
        return (

            <Modal size="lg" show={this.state.abrirModalSalvar}>
                <Modal.Header>
                    <Modal.Title>Cadastrar compras</Modal.Title>
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
                            <Button variant="secondary" onClick={() => this.managerModalSalvar(false)}> Close </Button>
                        </div>
                    </Form>
                </Modal.Body>
            </Modal>
        )
    }
    modalDeletar() {
        return (

            <Modal size="sm" show={this.state.abrirModalDeletar}>
                <Modal.Header>
                    <Modal.Title>Deletar compra</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form onSubmit={this.deletar}>
                        <Modal.Body>
                            <p>Tem certeza que deseja deletar?</p>
                        </Modal.Body>
                        <div style={{ textAlign: "right" }}>
                            <Button variant="primary" type="submit" > Deletar </Button>
                            <span>   </span>
                            <Button variant="secondary" onClick={() => this.managerModalDeletar(false)}> Close </Button>
                        </div>
                    </Form>
                </Modal.Body>
            </Modal>
        )
    }
}
