🛠️ ERP Ferramenteiro - Backend API

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Architecture-Clean%20%2F%20DDD-success)

API RESTful de alto desempenho desenvolvida para a gestão completa de locação de ferramentas, controle de estoque e faturamento de clientes. 

Construída sob a ótica do **Domain-Driven Design (DDD)** e **Clean Architecture**, a aplicação é desenhada para ser resiliente a mudanças, isolando as regras de negócio de frameworks, interfaces de usuário e infraestrutura de dados.

---

## 🏗️ Estrutura do Projeto (Clean Architecture)

A solução está dividida em 4 camadas com fluxo de dependência unidirecional:

```text
ERP-Ferramenteiro/
├── 1. Domain/           # Entidades (Cliente, Ferramenta), Enums, Exceções de Domínio. (Sem dependências externas).
├── 2. Application/      # Casos de Uso (Services), DTOs (Requests/Responses), Interfaces (Contratos).
├── 3. Infrastructure/   # EF Core DbContext, Repositórios, Migrations, Integração ViaCEP.
└── 4. API/              # Controllers REST, Middlewares (Tratamento Global de Erros), Swagger, Program.cs.
```

---

## 🧠 Regras de Negócio (Domain & Application)

O sistema aplica regras rigorosas para garantir a integridade dos dados:
* **RN01 - Unicidade de Cadastro:** Bloqueio transacional de CPFs ou CNPJs duplicados.
* **RN02 - Logística Blindada:** Validação e preenchimento automático de endereços via integração com API ViaCEP. O usuário envia o CEP e o sistema garante a precisão do logradouro e cidade para a entrega de equipamentos.

---

## 🚀 Tecnologias e Padrões

* **Ecossistema:** .NET 10 SDK, C# 13, ASP.NET Core Web API.
* **Persistência:** PostgreSQL (Docker), Entity Framework Core 9 (Code-First), Padrão Repository.
* **Integrações:** Padrão HttpClient (Consumo de APIs externas).
* **Boas Práticas:** Injeção de Dependência (DI), Validação por DTOs, Retornos HTTP Padronizados (201, 400, 409).

---

## ⚙️ Guia de Execução Local

### 1. Pré-requisitos
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) rodando em background.
* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) instalado.

### 2. Infraestrutura (Banco de Dados)
Suba o contêiner do PostgreSQL isolado na sua máquina:
```bash
docker run --name pg-locacao -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=senha_admin -e POSTGRES_DB=locacao_db -p 5432:5432 -d postgres
```

### 3. Aplicação do Esquema (Migrations)
Com o banco rodando, aplique a estrutura de tabelas. Na raiz da Solution, execute o comando:
```bash
dotnet ef database update --project ERP-Ferramenteiro --startup-project ERP-Ferramenteiro
```
*(Se estiver no Visual Studio: execute `Update-Database` no Package Manager Console, com o Default Project apontando para a camada de Infraestrutura).*

### 4. Execução
Execute o projeto via IDE (F5) ou via CLI:
```bash
dotnet run --project ERP-Ferramenteiro
```
Acesse o **Swagger UI** em `https://localhost:<porta>/swagger` para explorar e testar os endpoints de forma interativa.

---

## 📡 Mapeamento de Endpoints Principais

| Método | Rota | Descrição | Status de Retorno |
|---|---|---|---|
| `POST` | `/api/v1/clientes` | Cadastra um novo cliente com validação de CEP e unicidade. | `201 Created`, `400 Bad Request`, `409 Conflict` |
| `GET` | `/api/v1/clientes/{id}` | Recupera os dados de um cliente específico. | *(Em Desenvolvimento)* |
| `POST` | `/api/v1/locacoes` | Inicia um novo contrato de locação de ferramentas. | *(Em Desenvolvimento)* |

---

## 🧪 Testes (Próximos Passos)
A arquitetura foi desenhada para suportar testes unitários e de integração isolados, utilizando Mocks para as interfaces de Repositório e Integrações Externas. 
*A suíte de testes será documentada nesta secção à medida que a cobertura evoluir.*
