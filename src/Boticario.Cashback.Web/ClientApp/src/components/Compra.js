import React, { Component } from 'react';
import { Table, Button, Col, Dropdown, DropdownButton, Modal, Form } from 'react-bootstrap';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import Cookies from 'universal-cookie';
import Moment from 'react-moment';

const cookies = new Cookies();

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
        texto: "Porcentagem do cashback"
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
    erroMessage = (message) => {
        NotificationManager.error(message);
    };
    componentDidMount() {
        var token = this.getToken();
        if (token === undefined) {
            return;
        }

        fetch("http://localhost:5001/compra?revendedor_Id=" + token.revendedor_id, {
            method: "GET",
            headers: { 'Authorization': 'Bearer ' + token.accessToken },
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
            .catch(function (error) {
                console.error(error);
            });


    }


    renderTableHeader() {
        return columns.map((key, index) => {
            return <th key={index}>{key.texto}</th>
        })
    }
    renderTableData() {
        if (this.state.data.length === 0) {
            return (<tr style={{ textAlign: "center" }} ><td colSpan="7">Nao foram encontrados registros</td></tr>);
        }

        return this.state.data.map((compras) => {

            const { id, codigo, valor, data, porcentagemCashback, valorCashback, statusDesc, status_Id } = compras

            var item = {
                id: id,
                status_Id: status_Id
            }


            return (
                <tr key={id}>
                    <td style={{ textAlign: "center" }}>{this.renderDrop({ item })}</td>
                    <td>{codigo}</td>
                    <td>R${valor}</td>
                    <td> <Moment format="DD/MM/YYYY">{data}</Moment></td>
                    <td>{porcentagemCashback}%</td>
                    <td>R${valorCashback}</td>
                    <td>{statusDesc}</td>
                </tr>
            )
        })
    }
    renderDrop(item) {
        var itemValue = item.item;
        return (
            <DropdownButton id="dropdown-item-button" title="Acoes" size="sm">
                <Dropdown.Item disabled={itemValue.status_Id !== 1} as="button" onClick={() => this.managerModalEditar(true, itemValue.id)}>Editar</Dropdown.Item>
                <Dropdown.Item disabled={itemValue.status_Id !== 1} as="button" onClick={() => this.managerModalDeletar(true, itemValue.id)}>Excluir</Dropdown.Item>
            </DropdownButton>
        );
    }
    getToken() {
        var cookieItem = cookies.get('token')
        if (cookieItem === undefined) {
            return;
        }
        return cookieItem;
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
                compraId: id
            });
        }
    }
    managerModalEditar(value, id) {
        if (id !== undefined) {
            var compra = this.findArray(this.state.data, id)
            compra.data = compra.data.substring(0, 10);
            this.setState({
                dataToUpdate: compra,
                compraId: id,
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

        var token = this.getToken();
        if (token === undefined) {
            return;
        }


        fetch("http://localhost:5001/compra?id=" + id, {
            method: "DELETE",
            headers: { 'Authorization': 'Bearer ' + token.accessToken }
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

        var compra = {
            Codigo: this.state.codigo,
            Valor: parseFloat(this.state.valor),
            Data: this.state.date
        }
        var token = this.getToken();
        if (token !== undefined) {
            compra.Revendedor_Id = token.revendedor_id;
        }

        fetch("http://localhost:5001/compra", {
            method: "POST",
            headers: {
                'Authorization': 'Bearer ' + token.accessToken,
                'Content-Type': 'application/json'

            },
            body: JSON.stringify(compra)
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
        var compra = {}

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

        var token = this.getToken();
        if (token !== undefined) {
            compra.Revendedor_Id = token.revendedor_id;
        }
        fetch("http://localhost:5001/compra", {
            method: "PUT",
            headers: {
                'Authorization': 'Bearer ' + token.accessToken,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(compra)
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
                            <Form.Control placeholder="Valor da compra" required onChange={this.handleChange} name="valor" defaultValue={this.state.dataToUpdate.valor} />
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
