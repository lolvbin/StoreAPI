# API de Pedidos e Produtos

API REST desenvolvida em .NET com o objetivo de simular um sistema de gerenciamento de pedidos e produtos.

O projeto permite o cadastro, consulta, atualização e remoção de produtos, além da criação de pedidos vinculados a produtos previamente cadastrados.

A aplicação foi construída com foco em boas práticas de desenvolvimento, incluindo uso de DTOs, organização em controllers e simulação de relacionamento entre entidades.

## Funcionalidades

- CRUD completo de produtos
- Criação e listagem de pedidos
- Associação de pedidos com múltiplos produtos
- Uso de DTO para entrada de dados
- Validação básica de dados

## Endpoints

### Produtos
- GET /api/produtos
- GET /api/produtos/{id}
- POST /api/produtos
- PUT /api/produtos/{id}
- DELETE /api/produtos/{id}

### Pedidos
- GET /api/pedidos
- POST /api/pedidos

## Exemplo de criação de pedido

```json
{
  "status": "Pendente",
  "produtosIds": [1, 2]
}
```
## Arquitetura

O projeto foi estruturado utilizando separação de responsabilidades em camadas:

- Controllers: responsáveis pelas requisições HTTP
- Services: responsáveis pelas regras de negócio
- Contracts (Interfaces): definição de contratos entre as camadas
- DTOs: utilizados para entrada de dados

Foi utilizada injeção de dependência para desacoplar as camadas, tornando o código mais organizado, testável e próximo de aplicações reais.

## Melhorias futuras

- Integração com banco de dados (Entity Framework)
- Validações mais robustas
- Autenticação e autorização
