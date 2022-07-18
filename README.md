[![Language](https://img.shields.io/badge/dotnet-core%206.0-red)](https://dotnet.microsoft.com/download)

# Bank-Account-API
**Desafio Stone:** API de Conta Bancária


# Documentação dos endpoints
Esse projeto possuí o Swagger configurado: .../swagger/index.html

### Endpoint de Depósito:
**Path:** ../Api/Bank/Transaction/Deposit
**Tipo de Requisição:** POST
**Body Request:**
```sh
"AccountId": "0d930052-064f-11ed-b939-0242ac120002"
"DepositAmount": 0.0
```
**Condições válidadas pelo endpoint:** 
 - Valor de depósito maior ou igual a 1 (Necessário para que pelo menos 1% seja cobrado da transação)
 - O campo "AccountId", deve ser preenchido e existir na tabela "Account.json"

### Endpoint de Saque:
**Path:** ../Api/Bank/Transaction/Withdraw
**Tipo de Requisição:** POST
**Body Request:**
```sh
"AccountId": "0d930052-064f-11ed-b939-0242ac120002"
"WithdrawAmount": 0.0
```
**Condições válidadas pelo endpoint:** 
 - Valor de saque maior que 4
 - O campo "AccountId", deve ser preenchido e existir na tabela "Account.json"
 - O valor mais a taxa do saque, deve ser superior ao valor que o cliente posssí em conta

### Endpoint de Transferência entre contas:
**Path:** ../Api/Bank/Transaction/TransferToAccount
**Tipo de Requisição:** POST
**Body Request:**
```sh
"AccountId": "0d930052-064f-11ed-b939-0242ac120002"
"AccountReceiveId": "0d930052-064f-11ed-b939-0242ac120002"
"TransferAmount": 0.0
```
**Condições válidadas pelo endpoint:** 
 - Valor de transferência maior que 1
 - O campo "AccountId", deve ser preenchido e existir na tabela "Account.json"
 - O campo "AccountReceiveId", deve ser preenchido e existir na tabela "Account.json"
 - Os campos "AccountId" e "AccountReceiveId" devem ser diferentes
 - O valor da transferência, deve ser superior ao valor que o cliente (AccountId) posssí em conta

### Endpoint de Extrato:
**Path:** ../Api/Bank/Transaction/Statement
**Tipo de Requisição:** GET
**Request Path:**
```sh
../Api/Bank/Transaction/Statement/0d930052-064f-11ed-b939-0242ac120002
```
**Condições válidadas pelo endpoint:** 
 - O "AccountId" informado no path, deve ser preenchido e existir na tabela "Account.json"


# Informações Relevantes

## Banco de dados
Foram utilizados dois arquivos .json para simular duas tabelas de um banco relacional.
Esses arquivos estão localizado dentro da Solution (\\Database\\Tables\\..).
Já existem algumas informações cadastradas nos arquivos, com o propósito de facilitar a validação.
**Tabelas:**
 - Account.json
 - AccountHistory.json

**Observação:** A camada Database não existiria na estrutura do projeto, porém com o intuito de facilitar a validação e desenvolvimento essa camada foi criada nesse desafio.


## Dependências
 - .NET 6
 - AutoFixture;
 - AutoMapper;
 - FluentAssertions;
 - FluentValidation;
 - MediatR;
 - Moq;
 - Newtonsoft.Json;
 - NUnit;
 - Swashbuckle.
 

## Conceitos, Padrões, Arquiteturas...
 - Mediator;
 - CQRS;
 - Clean Architecture;
 - Solid;
 - Clean Code.