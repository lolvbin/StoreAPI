```markdown
# StoreAPI 🛒

Uma Web API robusta e escalável desenvolvida em **.NET 10** para gerenciamento de uma loja eletrônica, cobrindo o fluxo de produtos e pedidos. 

O projeto foi construído focando em boas práticas de arquitetura de software, separação de responsabilidades e segurança na validação de dados.

---

## 🚀 Evolução e Práticas Recentes

O projeto passou por uma grande refatoração arquitetural para adotar padrões exigidos pelo mercado de desenvolvimento de software moderno:

* **Data Transfer Objects (DTOs):** Implementados para desacoplar as entidades de domínio (`Produto`, `Pedido`) da camada de apresentação (Controllers). Isso protege a API contra vulnerabilidades como *Mass Assignment* e expõe apenas os dados necessários para cada operação.
* **FluentValidation:** Centralização de todas as regras de validação de entrada através de validadores fortemente tipados. A validação foi integrada nativamente ao pipeline do ASP.NET Core, tratando automaticamente erros de requisições malformadas.
* **Tratamento de Exceções & Respostas:** Introdução de padrões de respostas com estruturas genéricas (`APIResponse<T>`) e melhoria no fluxo de erros das camadas de serviço.

---

## 🛠️ Tecnologias e Bibliotecas Utilizadas

* **Plataforma:** .NET 10 (C# 14)
* **Framework Principal:** ASP.NET Core Web API
* **Validação:** [FluentValidation](https://fluentvalidation.net/) & `FluentValidation.AspNetCore`
* **Documentação:** OpenAPI (Swagger)

---

## 🏗️ Arquitetura do Projeto

A estrutura de pastas foi organizada para respeitar o princípio de responsabilidade única:

```text
├── Contracts/          # Interfaces (Contratos) dos Serviços
├── Controllers/        # Endpoints da API (HTTP Requests/Responses)
├── DTOs/               # Objetos de Transferência de Dados (Inputs da API)
├── Enums/              # Enumeradores do sistema (Status, TipoUsuario)
├── Models/             # Entidades de Domínio da Aplicação
├── Responses/          # Modelos globais de resposta HTTP
├── Services/           # Camada de Regras de Negócio e Lógica de Serviços
└── Validators/         # Regras de validação escritas em FluentValidation

```

---

## 🛑 Regras de Negócio Validadas

Graças à implementação do `FluentValidation`, a API barra requisições inválidas antes mesmo que elas cheguem aos serviços. Algumas das regras aplicadas:

* **Produtos:**
* Nome obrigatório com tamanho mínimo de 3 caracteres.
* Preço obrigatoriamente superior a R$ 0,00.
* O estoque não pode ser negativo (permitindo `0` para produtos esgotados) e possui um limite máximo de 99 unidades por lote de atualização.


* **Pedidos:**
* Obrigatório conter pelo menos um ID de produto válido na lista.
* Todos os IDs de produtos informados devem ser maiores que zero.
* Data do pedido não pode ser uma data futura.



---

## 🏁 Como Executar o Projeto

### Pré-requisitos

* [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado.

### Passo a Passo

1. Clone o repositório:
```bash
git clone [https://github.com/lolvbin/StoreAPI.git](https://github.com/lolvbin/StoreAPI.git)

```


2. Navegue até a pasta do projeto:
```bash
cd StoreAPI

```


3. Restaure as dependências do NuGet:
```bash
dotnet restore

```


4. Execute a aplicação:
```bash
dotnet run

```



A API estará disponível localmente. Você pode acessar a interface do **OpenAPI / Swagger** através do navegador para testar os endpoints (geralmente em `http://localhost:5xxx/openapi` ou conforme configurado no console de inicialização).

```
