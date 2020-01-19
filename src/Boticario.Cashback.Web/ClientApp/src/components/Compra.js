import React, { Component } from 'react';
import { Table, Button, Col, Dropdown, DropdownButton, Modal, Form } from 'react-bootstrap';
import { NotificationContainer, NotificationManager } from 'react-notifications';

const columns = [
    {
        texto: ""
    },
    {
        texto: "Codigo da compra"
    },
    {
        texto: "Valor da compra"
    },
    {
        texto: "Data da compra"
    },
    {
        texto: "Procentagem do cashback"
    },
    {
        texto: "Valor do cashback"
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
            abrirModalEditar: false,
            compraId: 0,
            dataToUpdate: {}
        };
        this.salvar = this.salvar.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.deletar = this.deletar.bind(this);
        this.editar = this.editar.bind(this);
    }
    sucesso = (message) => {
        NotificationManager.success(message, 'Notification');
    };
    erro = () => {
        NotificationManager.error('Erro ao cadastrar compra');
    };

    componentDidMount() {
        fetch("http://localhost:5001/compra?revendedor_Id=" + this.getRevendedor(), {
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
            const { id, codigo, valor, data, porcentagemCashback, valorCashback, statusDesc } = compras
            return (
                <tr key={id}>
                    <td style={{ textAlign: "center" }}>{this.renderDrop({ id })}</td>
                    <td>{codigo}</td>
                    <td>R${valor}</td>
                    <td>{data}</td>
                    <td>{porcentagemCashback}%</td>
                    <td>R${valorCashback}</td>
                    <td>{statusDesc}</td>
                </tr>
            )
        })
    }
    renderDrop(id) {
        return (
            <DropdownButton id="dropdown-item-button" title="Acoes" size="sm">
                <Dropdown.Item as="button" onClick={() => this.managerModalEditar(true, id)}>Editar</Dropdown.Item>
                <Dropdown.Item as="button" onClick={() => this.managerModalDeletar(true, id)}>Excluir</Dropdown.Item>
            </DropdownButton>
        );
    }

    getRevendedor() {
        var currentToken = localStorage.getItem('token');
        var jsonToken = JSON.parse(currentToken);
        return jsonToken.revendedor_id
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

        if (id !== undefined) {
            this.setState({
                compraId: id.id
            });
        }
    }
    managerModalEditar(value, id) {
        if (id !== undefined) {
            var compra = this.findArray(this.state.data, id.id)
            compra.data = compra.data.substring(0, 10);
            this.setState({
                dataToUpdate: compra,
                compraId: id.id,
                abrirModalEditar: value
            });
        } else {
            this.setState({
                abrirModalEditar: value
            });
        }
        
    }
    findArray(array, id) {
        return array.find((element) => {
            return element.id === id;
        })
    }
    deletar(event) {
        event.preventDefault();
        var id = this.state.compraId;
        fetch("http://localhost:5001/compra?id=" + id, {
            method: "DELETE",
        })
            .then(() => {
                this.managerModalDeletar(false);
                this.sucesso('Compra deletada com sucesso!');
                this.componentDidMount();
            })
            .catch((error) => {
                console.error(error);
                this.erro();
            });
    }
    salvar(event) {
        event.preventDefault();
       ;
        var compra = {
            Codigo: this.state.codigo,
            Valor: parseFloat(this.state.valor),
            Data: this.state.date,
            Revendedor_Id:  this.getRevendedor()
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
    editar(event) {
        event.preventDefault();
        var compra = { }

        if (this.state.codigo !== undefined) {
            compra.Codigo = this.state.codigo
        } else {
            compra.Codigo = this.state.dataToUpdate.codigo;
        }

        if (this.state.valor !== undefined) {
            compra.Valor = parseFloat(this.state.valor)
        } else {
            compra.Valor = parseFloat(this.state.dataToUpdate.valor)
        }

        if (this.state.date !== undefined) {
            compra.Data = this.state.date
        } else {
            compra.Data = this.state.dataToUpdate.data
        }
        compra.Revendedor_Id = this.state.dataToUpdate.revendedor_Id;
        compra.Status_Id = this.state.dataToUpdate.status_Id;
        compra.Id = this.state.dataToUpdate.id;


        fetch("http://localhost:5001/compra", {
            method: "PUT",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(compra)
        })
            .then((response) => response.json())
            .then((responseJson) => {
                this.managerModalEditar(false);
                this.sucesso('Compra editada com sucesso!');
                this.componentDidMount();
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
                {this.modalEditar()}


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
                            <Form.Control placeholder="Codigo da compra" required onChange={this.handleChange} name="codigo" />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Valor </Form.Label>
                            <Form.Control placeholder="Valor da compra" type="number" step="0.01" required onChange={this.handleChange} name="valor" />
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
    modalEditar() {
        return (

            <Modal size="lg" show={this.state.abrirModalEditar}>
                <Modal.Header>
                    <Modal.Title>Cadastrar compras</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form onSubmit={this.editar}>
                        <Form.Group >
                            <Form.Label>Codigo</Form.Label>
                            <Form.Control placeholder="Codigo da compra" required onChange={this.handleChange} name="codigo" defaultValue={this.state.dataToUpdate.codigo} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Valor </Form.Label>
                            <Form.Control placeholder="Valor da compra" required onChange={this.handleChange} name="valor" defaultValue={this.state.dataToUpdate.valor}/>
                        </Form.Group>
                        <Form.Group >
                            <Form.Label>Data </Form.Label>
                            <Form.Control placeholder="Data da compra" type="date" required onChange={this.handleChange} name="date" defaultValue={this.state.dataToUpdate.data} />
                        </Form.Group>
                        <div style={{ textAlign: "right" }}>
                            <Button variant="primary" type="submit" > Save Changes </Button>
                            <span>   </span>
                            <Button variant="secondary" onClick={() => this.managerModalEditar(false)}> Close </Button>
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
