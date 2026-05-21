# Guia Rápido: Configuração de Ambiente (Setup)

Este documento contém estritamente os comandos necessários para clonar, configurar e rodar a API do ERP Ferramenteiro numa máquina local do zero. Sem teoria, apenas execução.

## 1. Pré-requisitos
* **Docker Desktop** instalado e a rodar em background (Obrigatório para o banco de dados).
* **.NET SDK** instalado (Versão correspondente à definida no projeto).
* IDE recomendada: **Visual Studio 2022** (ou VS Code).

## 2. Subir a Base de Dados (Docker)
Não instale o PostgreSQL fisicamente na sua máquina. Abra o seu terminal (CMD ou PowerShell) e execute o comando abaixo para levantar o contêiner do banco de dados isolado:

```bash
docker run --name pg-locacao -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=senha_admin -e POSTGRES_DB=locacao_db -p 5432:5432 -d postgres
```
> **Teste de Vida:** Digite `docker ps` no terminal e verifique se o contêiner `pg-locacao` está com o status "Up".

## 3. Configurar as Credenciais (appsettings.json)
Navegue até ao projeto da API e abra o ficheiro `appsettings.json`. Certifique-se de que a string de conexão está idêntica às credenciais que acabaram de ser criadas no Docker:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=locacao_db;Username=postgres;Password=senha_admin"
}
```

## 4. Criar as Tabelas (EF Core Migrations)
O banco de dados no Docker nasce vazio. O Entity Framework Core deve ser acionado para construir as tabelas com base nas Entidades do sistema.

**Se estiver a utilizar o Visual Studio 2022:**
1. Abra o **Console do Gerenciador de Pacotes** (Ferramentas > Gerenciador de Pacotes NuGet).
2. Altere o menu suspenso "Projeto Padrão" (Default Project) para o projeto de Infrastructure (onde reside o AppDbContext).
3. Execute o comando abaixo (substituindo pelo nome real da pasta do seu projeto Web API):

```powershell
Update-Database -StartupProject "NomeDoSeuProjetoDaAPI"
```

**Se estiver a utilizar o Terminal (.NET CLI):**
Na raiz da Solução, execute:

```bash
dotnet ef database update --project [Caminho_Para_Pasta_Infra] --startup-project [Caminho_Para_Pasta_API]
```

## 5. Rodar a Aplicação
1. No Visual Studio, clique com o botão direito no projeto da API e selecione "Definir como Projeto de Inicialização" (Set as Startup Project).
2. Pressione F5 para compilar e rodar em modo Debug.
3. A interface do Swagger abrirá automaticamente no navegador (ex: `https://localhost:<porta>/swagger`), permitindo o teste imediato 
