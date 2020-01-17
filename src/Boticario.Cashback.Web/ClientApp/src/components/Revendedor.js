import React, { Component } from 'react';
import { Table, Button, Col, Dropdown, DropdownButton } from 'react-bootstrap';

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
            data: []
        };
        this.buscarDados();
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
    renderDrop() {
        return (
            <DropdownButton id="dropdown-item-button" title="Acoes" size="sm">
                <Dropdown.Item as="button">Editar</Dropdown.Item>
                <Dropdown.Item as="button">Excluir</Dropdown.Item>
            </DropdownButton>
        );
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
                    <Button variant="primary" size="sm">Cadastrar revendedor</Button>
                </Col>
                <Col>
                    <Table striped bordered hover size="sm">
                        <tbody>
                            <tr>{this.renderTableHeader()}</tr>
                            {this.renderTableData()}
                        </tbody>

                    </Table>
                </Col>
            </div>
        );
    }


}
