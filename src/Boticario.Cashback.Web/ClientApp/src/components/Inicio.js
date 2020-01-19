import React, { Component } from 'react';
import { Card } from 'react-bootstrap';

export class Inicio extends Component {
    render() {
        return (
            <div>

                <Card >
                    <Card.Body>
                        <Card.Title>Sobre o sistema...</Card.Title>
                        <Card.Subtitle className="mb-2 text-muted">Boticario cashback</Card.Subtitle>
                        <Card.Text>
                            <b>Ola, seja bem-vindo!</b>
                        </Card.Text>
                    </Card.Body>
                </Card>
                <br />
                <br />
                <Card >
                    <Card.Body>
                        <Card.Title>Sobre o desenvolvedor...</Card.Title>
                        <Card.Subtitle className="mb-2 text-muted">Felipe do Nascimento Staskoviak</Card.Subtitle>
                        <Card.Text>
                            <br />
                            <b>Email:</b> felipestaskoviak@gmail.com  <br />
                            <b>Telefone:</b> (41) 99616-2522. <br />
                            <b>Endereco:</b> Rua Pedro Moro Redeschi - N 133 - Sao jose dos pinhais/PR.<br />
                            <br />
                            Meus agradecimentos pela oportunidade!
                        </Card.Text>
                        <Card.Link href="https://www.linkedin.com/in/felipe-staskoviak-354b92b0">Linkedin</Card.Link>
                    </Card.Body>
                </Card>
            </div >
        );
    }
}
