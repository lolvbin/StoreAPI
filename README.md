```markdown
# StoreAPI 🛒

Uma Web API robusta e escalável desenvolvida em **.NET 10** para gerenciamento de uma loja eletrônica, cobrindo o fluxo completo de cadastro de produtos e processamento de pedidos. 

O projeto foi construído focando em padrões arquiteturais exigidos pelo mercado de desenvolvimento de software moderno, separação estrita de responsabilidades, persistência relacional e segurança na validação de dados.

---

## 🚀 Evolução e Práticas Recentes

O projeto passou por grandes refatorações estruturais para adotar práticas recomendadas para engenharia de software backend:

* **Persistência Relacional (EF Core):** Substituição do armazenamento temporário em memória por um banco de dados **SQLite** real. Toda a camada de dados é gerenciada via **Entity Framework Core**, utilizando o padrão *Unit of Work* para consolidação de operações de escrita através do `SaveChanges()`.
* **Mapeamento de Relacionamentos (Eager Loading):** Implementação de carregamento imediato via `.Include()` para unificar as consultas de pedidos aos seus respectivos produtos no banco de dados, evitando o comportamento de dados nulos (*Lazy Loading*).
* **Identificadores Globais Únicos (Guid):** Transição de chaves primárias sequenciais (`int`) para chaves baseadas em `Guid`, mitigando vulnerabilidades de ID harvesting e preparando a API para cenários de concorrência e alta escalabilidade.
* **Data Transfer Objects (DTOs):** Implementados para desacoplar as entidades de domínio (`Produto`, `Pedido`) da camada de apresentação (Controllers), protegendo a API contra *Mass Assignment* e otimizando o payload trafegado.
* **FluentValidation:** Centralização de todas as regras de validação de entrada através de validadores fortemente tipados inseridos nativamente no pipeline de requisições do ASP.NET Core.
* **Injeção de Dependência:** Utilização estrita de inversão de controle via interfaces (contratos de serviços) estruturados com construtores únicos não ambíguos.

---

## 🛠️ Tecnologias e Bibliotecas Utilizadas

* **Plataforma:** .NET 10 (C# 14)
* **Framework Principal:** ASP.NET Core Web API
* **ORM / Acesso a Dados:** Entity Framework Core (`Microsoft.EntityFrameworkCore.Sqlite`)
* **Banco de Dados:** SQLite
* **Ferramental de Banco:** Entity Framework Core Tools (`Microsoft.EntityFrameworkCore.Tools`)
* **Validação:** [FluentValidation](https://fluentvalidation.net/) & `FluentValidation.AspNetCore`
* **Documentação:** OpenAPI (Swagger)

---

## 🏗️ Arquitetura do Projeto

A estrutura de pastas foi organizada para respeitar o princípio de responsabilidade única (SRP) e o isolamento de camadas:

```text
├── Contracts/          # Interfaces (Contratos) de isolamento dos Serviços
├── Controllers/        # Endpoints da API (HTTP Requests/Responses)
├── Data/               # Contexto de Banco de Dados (AppDbContext) e Configurações do EF Core
├── DTOs/               # Objetos de Transferência de Dados (Input/Output da API)
├── Enums/              # Enumeradores do sistema (Status do Pedido, etc)
├── Migrations/         # Arquivos de histórico e versionamento do Banco de Dados
├── Models/             # Entidades de Domínio da Aplicação (Mapeadas para o DB)
├── Responses/          # Modelos globais e genéricos de resposta HTTP
├── Services/           # Camada de Regras de Negócio (Lógica de Serviços)
└── Validators/         # Regras de validação escritas em FluentValidation

```

---

## 🛑 Regras de Negócio Validadas

Graças à integração do `FluentValidation`, a API intercepta e barra requisições inválidas na borda da aplicação, retornando logs estruturados antes mesmo de onerar a camada de serviços.

* **Produtos:**
* Nome obrigatório com tamanho mínimo de 3 caracteres.
* Preço obrigatoriamente superior a R$ 0,00.
* O estoque não pode ser negativo (permitindo `0` para produtos esgotados) e possui um limite máximo de 99 unidades por lote de atualização.


* **Pedidos:**
* Obrigatório conter pelo menos um ID de produto válido na lista.
* Todos os IDs de produtos informados devem ser identificadores estruturalmente válidos (`Guid`).
* Data do pedido gerada de forma automatizada no servidor (`DateTime.Now`), prevenindo adulterações temporais ou datas futuras.



---

## 🏁 Como Executar o Projeto

### Pré-requisitos

* [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado.
* Ferramenta de CLI do Entity Framework instalada globalmente. Se não tiver, instale rodando:
```bash
dotnet tool install --global dotnet-ef

```



### Passo a Passo

1. **Clone o repositório:**
```bash
git clone [https://github.com/lolvbin/StoreAPI.git](https://github.com/lolvbin/StoreAPI.git)

```


2. **Navegue até a pasta do projeto:**
```bash
cd StoreAPI

```


3. **Restaure as dependências do NuGet:**
```bash
dotnet restore

```


4. **Aplique as Migrations para criar o Banco de Dados local:**
Este comando lerá o histórico da pasta `Migrations` e gerará automaticamente o arquivo físico do banco (`store.db`) na raiz do seu projeto:
```bash
dotnet ef database update

```


5. **Execute a aplicação:**
```bash
dotnet run

```



A API estará disponível localmente. Você pode acessar a interface do **OpenAPI / Swagger** através do navegador para realizar os testes de requisições (`POST`, `GET`, etc) nos endpoints através do endereço informado no terminal (ex: `http://localhost:5xxx/openapi`).

```
