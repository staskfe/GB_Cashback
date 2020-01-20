# GB_Cashback
Projeto criado para o teste do processo seletivo boticario - iniciado em 16/01/2020

## Projetos criados:

#### Na pasta: 1. Apresentação:
* Boticario.Cashback.Web - Projeto react com o frontend
* Boticario.Cashback.WebAPI - Projeto API com as rotas solicitadas
#### Na pasta: 2. Aplicação
* Boticatio.Cashback.Application - Projeto de aplicação, gerencia as regras de negocio do sistema
#### Na pasta: 3. Dominio
* Boticario.Cashback.Interface - Projeto com todas as interfaces necessarias
* Boticatio.Cashback.Dominio - Coração do backend, contem as regras de negocio e objetos do sistema
#### Na pasta: 4. Repositorio
* Boticatio.Cashback.Repositorio - Aqui é possivel encontrar todas as queries que o sistema necessita fazer
#### Na pasta: 5. Infraestrutura
* Boticatio.Cashback.Infraestrutura - Projeto responsavel por criar o banco de dados - EF Core
* Boticatio.Cashback.IoC - Projeto que resolve as dependecias da API
* Boticatio.Cashback.Utils - Projeto contem algumas funcionalidades que são compartilhadas entre as camadas/projetos
* Boticatio.Cashback.ViewModels - Projeto com todas as view models utilizadas
#### Na pasta: 6. Testes
* Boticatio.Cashback.Aplication.Test - Projeto com testes unitarios para a camada de aplicação

==============================================================================
## Como utilizar o sistema
Antes de iniciar o sistema, é necessario criar o banco de dados. Para isso, rode o comando:

* Update-Database -StartupProject Boticatio.Cashback.Infraestrutura -Project Boticatio.Cashback.Infraestrutura

Caso seja necessario alterar a connection string, vá até o projeto Boticatio.Cashback.Infraestrutura e abra a classe CashbackFactoryContext

* Após criar o banco de dados, execute o projeto Boticario.Cashback.WebAPI e rode o npm-start na pasta ClientApp do projeto Boticario.Cashback.Web
* A tela de login irá aparecer, clique no "Criar conta" e crie um revendedor.
* Após criar o revendedor, utilize-o para se logar no sistema!
* Ao logar, você será redirecionado para tela de inicio, nela é possivel encontrar algumas regras do sistema e informações do desenvolvedor
* Ao clicar no menu: "Revendedores", você será redirecionado para tela onde é possivel visualizar todos os revendedores criados, e criar um novo revendedor
* Ao clicar no menu: "Compras", você será redirecionado para tela onde é possivel visualizar/Editar/Excluir as compras do revendedor logado!
* Ao clicar no menu: "Seja bem vindo(a) {nome}", aparecerá duas opções: "Deslogar" - essa opção deslogado do sistema, e "Cashback acumulado" - essa opção utiliza a API do boticario para retornar a o cashback acumulado.
  
## Tecnologias utilizadas:
* .NET Core 3.1
* ReactJS 
* EF Core - para criação do banco de dados
* Authenticação JWT - o token expira a cada 30 minutos
