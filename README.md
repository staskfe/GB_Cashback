# GB_Cashback
Projeto criado para o teste do processo seletivo boticario - iniciado em 16/01/2020

## Projetos criados:

#### Na pasta: 1. Apresentação:
Boticario.Cashback.Web - Projeto react com o frontend

Boticario.Cashback.WebAPI - Projeto API com as rotas solicitadas

#### Na pasta: 2. Aplicação
Boticatio.Cashback.Application - Projeto de aplicação, gerencia as regras de negocio do sistema

#### Na pasta: 3. Dominio
Boticario.Cashback.Interface - Projeto com todas as interfaces necessarias

Boticatio.Cashback.Dominio - Coração do backend, contem as regras de negocio e objetos do sistema

#### Na pasta: 4. Repositorio

Boticatio.Cashback.Repositorio - Aqui é possivel encontrar todas as queries que o sistema necessita fazer

#### Na pasta: 5. Infraestrutura
Boticatio.Cashback.Infraestrutura - Projeto responsavel por criar o banco de dados - EF Core

Boticatio.Cashback.Utils - Projeto contem algumas funcionalidades que são compartilhadas entre as camadas/projetos

Boticatio.Cashback.ViewModels - Projeto com todas as view models utilizadas

#### Na pasta: 6. Testes
Boticatio.Cashback.Aplication.Test - Projeto com testes unitarios para a camada de aplicação

==============================================================================
## Como utilizar o sistema
Antes de iniciar o sistema, é necessario criar o banco de dados. Para isso, rode o comando:

Update-Database -StartupProject Boticatio.Cashback.Infraestrutura -Project Boticatio.Cashback.Infraestrutura

Caso seja necessario 
