import React, { Component } from 'react';
import { Modal, DropdownItem } from 'react-bootstrap';
import Cookies from 'universal-cookie';
import { NotificationContainer, NotificationManager } from 'react-notifications';

const cookies = new Cookies();

export class CashbackAcumulado extends Component {
    constructor(props) {
        super(props);
        this.state = {
            modal: false,
            credito: 0
        };
        this.toggle = this.toggle.bind(this);
    }
    getToken() {
        var cookieItem = cookies.get('token')
        if (cookieItem === undefined) {
            return;
        }
        return cookieItem;
    }

    buscarDados() {
        var token = this.getToken();
        if (token === undefined) {
            return;
        }


        fetch("http://localhost:5001/revendedor/acumulado?id=" + token.revendedor_id, {
            method: "GET",
            headers: {
                'Authorization': 'Bearer ' + token.accessToken,
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
                this.setState({
                    modal: !this.state.modal,
                    credito: responseJson.body.credit
                });

            })
            .catch((error) => {
                console.error(error);
            });
    }


    toggle() {
        this.setState({
            modal: !this.state.modal
        });
    }

    render() {
        return (
            <div>
                <DropdownItem onClick={() => this.buscarDados()} >Cashback acumulado</DropdownItem>

                <Modal show={this.state.modal} toggle={this.toggle}>
                    <Modal.Header closeButton>Cashback acumulado pela API boticario</Modal.Header>
                    <Modal.Body style={{ textAlign: "center" }}>
                        <h4> Credito total: {this.state.credito}</h4>
                    </Modal.Body>
                </Modal>
                <NotificationContainer />
            </div >
        );
    }
}
