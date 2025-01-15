
# TaskManager API

TaskManager API é uma aplicação backend desenvolvida com .NET 8, que permite gerenciar tarefas de forma eficiente. A API oferece funcionalidades como autenticação de usuários, gerenciamento de tarefas (CRUD) e suporte a login e registro. Foi desenvolvida como um projeto de estudo para aplicar boas práticas e tecnologias modernas no desenvolvimento de APIs, inclui testes unitários para garantir a qualidade e a confiabilidade do código.

## Índice

- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Requisitos](#requisitos)
- [Instalação](#instalação)
- [Como Usar](#como-usar)
- [Endpoints Principais](#endpoints-principais)
- [Automação com CI/CD](#automação-com-cicd)
- [Contribuindo](#contribuindo)
- [Contato](#contato)

## Funcionalidades

- Autenticação com JWT (JSON Web Token).
- Operações CRUD para tarefas:
  - Criar, visualizar, atualizar e excluir tarefas.
- Registro e login de usuários com senha criptografada.
- Documentação automatizada com Swagger.
- Integração de **Entity Framework Core** e **Dapper** para manipulação de dados.
- Controle de migrações com FluentMigrator.
- Banco de dados hospedado na Azure.
- Automação de CI/CD com Azure DevOps:
  - Build automático da aplicação.
  - Criação de imagens Docker.
  - Deploy automatizado para o Azure App Service.
- **Testes unitários** para garantir a robustez e a confiabilidade do sistema.

## Tecnologias Utilizadas

- **.NET 8** (framework principal para desenvolvimento backend)
- **Entity Framework Core** (ORM utilizado para interação com o banco de dados)
- **Dapper** (usado junto com **FluentMigrator** para controle e automação das migrações)
- **FluentValidation** (validação de dados)
- **FluentAssertions** (biblioteca para melhorar a clareza e legibilidade nos testes)
- **Bogus** (gerador de dados fake para testes)
- **xUnit** (framework para escrita de testes unitários e de integração)
- **AutoMapper** (mapeamento entre DTOs e entidades)
- **JWTBearer** (implementação de autenticação segura usando tokens JWT)
- **MySQL** (banco de dados hospedado na Azure)
- **BCrypt** (criptografia de senhas)
- **Swagger** (documentação interativa da API)
- **Azure** (utilizado para hospedagem do banco de dados, serviço web e CI/CD)

## Requisitos

- **SDK do .NET 8**
- **MySQL** ou conexão configurada no Azure.
- **Docker** (opcional, para execução em contêiner).

## Instalação

1. Clone o repositório:
   ```
   git clone https://github.com/fariasu/dotnet-api-template.git
   ```
2. Acesse o diretório do projeto:
   ```
   cd dotnet-api-template
   ```
3. Configure a string de conexão no `appsettings.json` com os dados do banco de dados da Azure.

4. Restaure as dependências:
   ```
   dotnet restore
   ```

## Como Usar

1. Inicie a aplicação:
   ```
   dotnet run
   ```
2. Acesse a documentação Swagger para explorar os endpoints:
   - URL local: `https://localhost:{port}/swagger`
3. Utilize ferramentas como Postman ou Insomnia para consumir a API.

## Endpoints Principais

- **Autenticação**
  - `POST /user/register` - Registrar um novo usuário.
  - `POST /user/login` - Fazer login e receber o token JWT.
  - `POST /user/update-profile` - Fazer update do perfil.
  - `POST /user/update/password` - Fazer update da senha.

- **Gerenciamento de Tarefas**
  - `POST /tasks/create` - Criar uma nova tarefa.
  - `GET /tasks/get/{id}` - Listar tarefa existente do usuário autenticado.
  - `GET /tasks/get` - Listar todas as tarefas do usuário autenticado.
  - `PUT /tasks/update/{id}` - Atualizar uma tarefa existente.
  - `DELETE /tasks/delete/{id}` - Excluir uma tarefa.

## Automação com CI/CD

Este projeto utiliza **Azure DevOps** para integração e entrega contínuas (CI/CD), com um pipeline configurado no arquivo YAML.

### Principais etapas do pipeline

1. **Build**: Compila a aplicação e executa os testes.
2. **Docker**: Cria uma nova imagem Docker da aplicação.
3. **Publicação**: Envia a imagem para o **Azure Container Registry**.
4. **Deploy**: Realiza o deploy da nova imagem no **Azure App Service**.

### Configuração do pipeline

O arquivo `azure-pipelines.yml` contém a definição das etapas do pipeline. Ao realizar um commit na branch `main`, o pipeline é automaticamente disparado, garantindo que a versão mais recente da aplicação seja disponibilizada no ambiente.

## Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e enviar pull requests.

## Contato

Se você tiver dúvidas, sugestões ou quiser se conectar, entre em contato:

- [Meu LinkedIn](https://www.linkedin.com/in/pablo-farias)  
- [Meu Email](mailto:pablogarciafarias@gmail.com)  
