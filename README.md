# PestControl CMS

A modern **Pest Control Contract Management** web application built with:

- **ASP.NET Core 10** — Minimal APIs + Blazor Interactive Auto
- **Clean Architecture** — Core / Application / Infrastructure / Web
- **Entity Framework Core 10** — SQL Server with migrations
- **FluentValidation 11** — server-side contract validation
- **Tailwind CSS** — utility-first UI via CDN

---

## Project Structure

```
PestControlCMS/
├── PestControlCMS.sln
└── src/
    ├── PestControlCMS.Core/            Domain entities, enums, interfaces, Result pattern
    ├── PestControlCMS.Application/     DTOs, validators, ContractService
    ├── PestControlCMS.Infrastructure/  EF Core DbContext, repository implementation
    └── PestControlCMS.Web/             Minimal API endpoints + Blazor UI
```

---

## Getting Started

### Prerequisites

- Visual Studio 2026 (or VS Code with C# Dev Kit)
- .NET 10 SDK
- SQL Server or SQL Server LocalDB

### 1. Configure the Connection String

Edit `src/PestControlCMS.Web/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PestControlCMS;Trusted_Connection=True;"
  }
}
```

### 2. Apply EF Core Migrations

The app auto-migrates on startup in Development. To add a new migration manually:

```bash
cd src/PestControlCMS.Infrastructure
dotnet ef migrations add InitialCreate \
  --startup-project ../PestControlCMS.Web \
  --context AppDbContext

dotnet ef database update \
  --startup-project ../PestControlCMS.Web
```

### 3. Run

```bash
cd src/PestControlCMS.Web
dotnet run
```

Open `https://localhost:7001` in your browser.

---

## API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET    | `/api/contracts` | List all contracts |
| GET    | `/api/contracts/{id}` | Get contract by ID |
| POST   | `/api/contracts` | Create new contract |
| PUT    | `/api/contracts/{id}` | Update contract |
| DELETE | `/api/contracts/{id}` | Delete contract |

Swagger UI available at `/swagger` in Development mode.

---

## Key Design Decisions

| Concern | Approach |
|---|---|
| Error handling | `Result<T>` / `Result` — no exceptions for control flow |
| Validation | FluentValidation server-side + DataAnnotations client-side in Blazor |
| Dependency Injection | Primary constructors (C# 12+) throughout |
| PestType storage | `[Flags]` enum stored as `int` in SQL Server |
| ServiceFrequency storage | Stored as `string` for readability |
| Render mode | `InteractiveAuto` — Server during first load, WASM after download |
