# StoreAPI 🛒

Uma Web API robusta, escalável e segura desenvolvida em **.NET 10** para gerenciamento de uma plataforma de e-commerce, cobrindo o fluxo completo de autenticação, cadastro de produtos, gerenciamento de estoques e processamento de pedidos.

O projeto foi projetado e concluído seguindo rigorosamente os padrões de mercado exigidos pela engenharia de software backend moderno. Ele destaca a separação estrita de responsabilidades, arquitetura limpa, persistência relacional e um ecossistema de segurança baseado em tokens criptográficos e controle de acesso contextual.

---

## 🏗️ Arquitetura e Estrutura do Projeto

A solução foi estruturada para respeitar o Princípio de Responsabilidade Única (SRP) e o isolamento de camadas, garantindo manutenibilidade e alta testabilidade do código:

```text
├── Contracts/          # Interfaces (Contratos) para inversão de controle e isolamento dos Serviços
├── Controllers/        # Endpoints HTTP da API (Tratamento de Requests/Responses)
├── Data/               # Camada de Acesso a Dados (Contexto do EF Core e mapeamentos)
├── DTOs/               # Data Transfer Objects (Input/Output desacoplados das entidades de domínio)
├── Enums/              # Enumeradores globais de negócio (Tipos de Usuário, Status do Pedido)
├── Migrations/         # Versionamento e histórico estrutural do Banco de Dados
├── Models/             # Entidades de Domínio da aplicação mapeadas para tabelas relacionais
├── Responses/          # Modelos genéricos globais para unificação de retornos HTTP
├── Services/           # Camada de Regras de Negócio e Serviços da aplicação
└── Validators/         # Middleware de validação avançada com regras em FluentValidation

```

---

## 🛠️ Tecnologias, Frameworks e Bibliotecas

* **Plataforma Core:** .NET 10 (C# 14)
* **Framework Web:** ASP.NET Core Web API
* **Acesso a Dados e ORM:** Entity Framework Core (`Microsoft.EntityFrameworkCore.Sqlite`)
* **Banco de Dados:** SQLite (Armazenamento persistente real)
* **Autenticação e Segurança:** JWT Bearer Token (`Microsoft.AspNetCore.Authentication.JwtBearer`)
* **Validação de Dados:** FluentValidation (`FluentValidation.AspNetCore`)
* **Documentação Interativa:** OpenAPI / Swagger

---

## ⚡ Práticas de Engenharia de Software Implementadas

Ao longo da evolução do projeto, a API abandonou simulações acadêmicas em memória e foi completamente refatorada para adotar padrões de nível de produção:

* **Segurança e Autenticação com JWT:** Fluxo de autenticação stateless onde a API emite tokens digitais criptografados no login, garantindo a integridade da sessão do usuário.
* **Controle de Acesso Baseado em Funções (RBAC):** Proteção de rotas em nível de Controller por meio de permissões de perfis (`Admin`, `Vendedor`, `Cliente`). O pipeline valida contextualmente o crachá do usuário antes de dar acesso ao recurso através da injeção de `IHttpContextAccessor`.
* **Persistência e Unit of Work com EF Core:** Substituição do armazenamento em memória por banco de dados relacional. Operações ACID consolidadas de forma atômica por meio de repositórios baseados no padrão Unit of Work com o `SaveChanges()`.
* **Consultas Otimizadas (Eager Loading):** Mitigação de problemas de consultas fracionadas e dados nulos (Lazy Loading) através do uso explícito do método `.Include()` para unificar as buscas de pedidos e seus respectivos itens em um único payload de banco de dados.
* **Chaves Globais Únicas (Guid):** Adoção de chaves primárias baseadas em `Guid` em substituição aos inteiros sequenciais automáticos (`int`), eliminando riscos de vulnerabilidades do tipo *ID Harvesting* (onde atacantes adivinham IDs sequenciais) e preparando a API para sistemas distribuídos de alta concorrência.
* **Proteção contra Mass Assignment via DTOs:** Total isolamento das entidades de domínio (`Produto`, `Pedido`, `Usuario`). Os payloads de entrada e saída trafegam puramente através de DTOs específicos, blindando o estado interno do banco de dados.
* **Validação em Borda com FluentValidation:** Centralização de regras de negócio fortemente tipadas que interceptam a requisição HTTP logo na entrada, impedindo que dados corrompidos ou mal formatados onerem as camadas de serviço da aplicação.
* **Respostas Unificadas Baseadas em Generics (`APIResponse<T>`):** Padronização global de todas as respostas HTTP da API, envelopadas em uma estrutura previsível contendo indicadores de sucesso, mensagens descritivas de sistema e payloads tipados.

---

## 🛡️ Matriz de Permissões e Regras de Negócio

### Controle de Acesso (RBAC)

* **Público (Anonymous):** Leitura de produtos (`GET /api/produtos` e `GET /api/produtos/{id}`).
* **Cliente:** Apenas lê produtos, efetua login e autocadastro. Possui bloqueio automático para endpoints gerenciais.
* **Vendedor:** Acesso completo de leitura e escrita para o estoque de produtos (`POST`, `PUT`) e visualização geral de pedidos.
* **Admin:** Poder irrestrito sobre a aplicação, incluindo a exclusão física de itens do estoque (`DELETE /api/produtos/{id}`) e o gerenciamento/cadastro de usuários com credenciais elevadas de gerência.

### Regras de Validação Automatizadas

* **Produtos:** Nome obrigatório (mínimo de 3 caracteres), preço estritamente superior a R$ 0,00 e estoque controlado entre 0 (esgotado) e o limite operacional de 99 unidades por lote de entrada.
* **Pedidos:** Bloqueio de listas vazias, obrigatoriedade de identificadores estruturalmente válidos (`Guid`) e geração automatizada de carimbo de data/hora imutável diretamente no servidor (`DateTime.Now`), prevenindo fraudes ou agendamentos temporais maliciosos.

---

## 🏁 Como Executar e Testar a Aplicação

### Pré-requisitos

* [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado.
* Ferramenta de CLI do Entity Framework Core instalada globalmente:
```bash
dotnet tool install --global dotnet-ef

```



### Passo a Passo para Configuração

1. **Clone o repositório:**
```bash
git clone https://github.com/lolvbin/StoreAPI.git
cd StoreAPI

```


2. **Restaure as dependências do ecossistema NuGet:**
```bash
dotnet restore

```


3. **Gere o Banco de Dados físico através das Migrations:**
Este comando lerá o histórico estrutural do projeto e gerará automaticamente o banco de dados persistente (`store.db`) local:
```bash
dotnet ef database update

```


4. **Execute o servidor da API:**
```bash
dotnet run

```



A aplicação subirá localmente. Você poderá acessar a interface interativa do **OpenAPI / Swagger** através do endereço indicado no seu terminal (ex: `http://localhost:5048/openapi`) para gerar tokens JWT, autenticar e testar as barreiras de proteção e regras de negócio da API.
