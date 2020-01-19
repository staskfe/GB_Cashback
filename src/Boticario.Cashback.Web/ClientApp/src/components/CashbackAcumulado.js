import React, { Component } from 'react';
import { Modal, Button, DropdownItem } from 'react-bootstrap';

export class CashbackAcumulado extends Component {
    constructor(props) {
        super(props);
        this.state = {
            modal: false
        };
        this.toggle = this.toggle.bind(this);
    }


    toggle() {
        this.setState({
            modal: !this.state.modal
        });
    }

    render() {
        return (
            <div>
                <DropdownItem onClick={this.toggle} >Cashback acumulado</DropdownItem>

                <Modal show={this.state.modal} toggle={this.toggle}>
                    <Modal.Header toggle={this.toggle}>Cashback acumulado</Modal.Header>
                    <Modal.Body>

                    </Modal.Body>
                    <Modal.Footer>
                        <Button color='secundary' onClick={this.toggle}>Cancel</Button>{' '}
                    </Modal.Footer>
                </Modal>
            </div >
        );
    }
}
