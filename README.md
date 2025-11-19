# **Gestão de Contatos - .NET 8 + React + MySQL (Docker)**

Este projeto é uma aplicação completa para gerenciamento de pessoas e
seus respectivos telefones, utilizando:

-   **Backend:** ASP.NET Core **8.0**, arquitetura em camadas, EF Core,
    Swagger\
-   **Banco de Dados:** **MySQL 8** rodando em Docker\
-   **Frontend:** **React 18** consumindo a API via Axios\
------------------------------------------------------------------------

## **Arquitetura do Projeto**

    /backend
      /ApiContatos
        /Models
        /Controllers
        /Services
        /Data
        ApiContatos.csproj
    /frontend
      /src
      /public
    docker-compose.yml
    .env
    README.md

------------------------------------------------------------------------

## **Tecnologias Utilizadas**

### **Backend (.NET 8)**

-   ASP.NET Core 8
-   Entity Framework Core 8 (Pomelo MySQL)
-   Swagger
-   Injeção de dependência

### **Frontend (React)**

-   React 18
-   Axios

### **Banco (Docker)**

-   MySQL 8
-   `.env` para credenciais
-   Volume persistente

------------------------------------------------------------------------

## **1. Subindo o Banco de Dados (MySQL no Docker)**

### **1.1 Criar o arquivo `.env`**

``` env
MYSQL_ROOT_PASSWORD=root
MYSQL_DATABASE=desafio_contatos
MYSQL_USER=desafio
MYSQL_PASSWORD=desafio123
DB_PORT=3306
```

### **1.2 Subir o container**

``` bash
docker-compose up -d
```

------------------------------------------------------------------------

## **2. Rodando o Backend (.NET 8)**

``` bash
cd backend/ApiContatos
dotnet restore
dotnet run
```

A API sobe em:

    http://localhost:5000

### **Swagger**

👉 http://localhost:5000/swagger

------------------------------------------------------------------------

## **3. Rodando o Frontend (React)**

``` bash
cd frontend
npm install
npm start
```

Frontend abre em:

    http://localhost:3000

------------------------------------------------------------------------

## **4. Entidades do Sistema**

### Pessoa

-   Id\
-   Nome\
-   Cpf\
-   DataNascimento\
-   EstaAtivo\
-   Telefones (1:N)

### Telefone

-   Id\
-   Numero\
-   Tipo\
-   PessoaId

------------------------------------------------------------------------

## **5. Resetando o Banco**

``` bash
docker exec -it mysql-desafio-contatos mysql -u root -p
```

``` sql
DROP DATABASE IF EXISTS desafio_contatos;
CREATE DATABASE desafio_contatos CHARACTER SET utf8mb4;
```

------------------------------------------------------------------------

## **6. Teste rápido**

1.  `docker-compose up -d`\
2.  `dotnet run`\
3.  `npm start`

Swagger: http://localhost:5000/swagger\
Frontend: http://localhost:3000

------------------------------------------------------------------------

