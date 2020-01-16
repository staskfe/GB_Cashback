import React, { Component } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Col from 'react-bootstrap/Col';


export class Login extends Component {

    constructor(props) {
        super(props);

        this.state = {
            email: "",
            senha: ""
        };

        this.senhaAlterada = this.senhaAlterada.bind(this);
        this.ValidarUsuario = this.ValidarUsuario.bind(this);
        this.emailAlterado = this.emailAlterado.bind(this);
    }

    setLocalStorage(values) {
        localStorage.setItem('token', JSON.stringify(values));
    };

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
                this.setLocalStorage(responseJson)


                console.log(responseJson);
            })
            .catch((error) => {
                console.error(error);
            });

        event.preventDefault();

    }



    render() {
        return (
            <div>
                <Col sm="6">
                    <Form onSubmit={this.ValidarUsuario}>
                        <Form.Group controlId="formBasicEmail">
                            <Form.Label>Email address</Form.Label>
                            <Form.Control placeholder="Enter email" onChange={this.emailAlterado} />
                        </Form.Group>
                        <Form.Group controlId="formBasicPassword">
                            <Form.Label>Password</Form.Label>
                            <Form.Control type="password" placeholder="Password" onChange={this.senhaAlterada} />
                        </Form.Group>
                        <Form.Group controlId="formBasicCheckbox">
                        </Form.Group>
                        <Button variant="primary" type="submit">
                            Submit
                    </Button>
                    </Form>
                </Col>
            </div>
        );
    }
}
