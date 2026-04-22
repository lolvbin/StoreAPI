# API de Pedidos e Produtos

API REST desenvolvida em .NET com o objetivo de simular um sistema de gerenciamento de pedidos e produtos.

O projeto permite o cadastro, consulta, atualização e remoção de produtos, além da criação de pedidos vinculados a produtos previamente cadastrados.

A aplicação foi construída com foco em boas práticas de desenvolvimento backend, incluindo uso de DTOs, separação de responsabilidades em camadas (Controllers, Services e Contracts) e simulação de relacionamento entre entidades.

## Tecnologias

- .NET (ASP.NET Core Web API)
- C#
- LINQ
- Injeção de Dependência
- Postman (testes de API)

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

O projeto foi refatorado para uma arquitetura em camadas, aplicando boas práticas de desenvolvimento backend:

- **Controllers**: responsáveis por lidar com as requisições HTTP
- **Services**: responsáveis pelas regras de negócio da aplicação
- **Contracts (Interfaces)**: definem contratos entre as camadas, promovendo desacoplamento
- **DTOs**: utilizados para entrada de dados de forma controlada

A aplicação utiliza **injeção de dependência** para conectar as camadas, tornando o código mais organizado, testável e escalável.

## Evolução do Projeto

O projeto iniciou como uma API simples utilizando armazenamento em memória e evoluiu para uma arquitetura em camadas, com separação de responsabilidades e uso de injeção de dependência, tornando-o mais próximo de aplicações reais utilizadas no mercado.

## Atualizações Recentes

Nesta versão, o projeto passou por uma refatoração estrutural com foco em organização e boas práticas:

- Implementação de arquitetura em camadas (Controller → Service → Contracts)
- Criação de interfaces para abstração das regras de negócio
- Uso de injeção de dependência para desacoplamento entre componentes
- Refatoração dos controllers, tornando-os mais simples e focados
- Remoção de dependências diretas entre controllers e lógica de negócio

Essas mudanças tornam o projeto mais próximo de aplicações reais utilizadas no mercado.

## Próximos Passos

- Integração com banco de dados utilizando Entity Framework
- Implementação de persistência de dados com DbContext
- Evolução das regras de negócio e validações
- Possível implementação de autenticação e autorização
