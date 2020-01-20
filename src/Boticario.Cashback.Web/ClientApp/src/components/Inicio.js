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
                            <br />
                            <br />
                            <b>Veja abaixo algumas regras do sistema: </b> <br />
                            * Ao criar uma compra, ela sera vinculada ao usuario logado automaticamente, sendo assim, informacoes sobre o revendedor nao sao necessarias ao criar uma compra <br />
                            * A sua sessao expira em 30 minutos apos a login<br />
                            * So eh possivel deletar/editar uma compra se o status for "Em aprovacao" <br />
                            * Para cadastrar um revendedor, utilize apenas numeros no campo CPF <br />
                            * As regras para calcular o cashback estao de acordo com o que foi solicitado no teste<br />
                            * Eh possivel visualizar o cashback acumulado (via API boticario) na opcao "Cashback acumulado" no menu "Seja bem vindo(a) 'nome'" <br />
                            * Nao eh possivel cadastrar dois revendedores com o mesmo email <br />
                            * A senha eh criptografada no momento de salvar o revendedor no banco de dados <br />
                            * As unicas APIs abertas sao a de login e a de criar revendedor, as demais necessitam de um token valido <br />
                            * Quando o CPF do revendedor logado for: 15350946056 o status das compras serao "Aprovado" <br />
                        </Card.Text>
                    </Card.Body>
                </Card>
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
