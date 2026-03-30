---
name: grpc-backend
description: "Use when creating or modifying the gRPC backend: EF Core entities, DbContext, SQLite migrations, gRPC service implementations, WinForms Kestrel hosting, seed data. Use for CRUD operations, database schema, gRPC endpoints."
argument-hint: "Describe the backend change (entity, service, migration, etc.)"
---

# gRPC Backend Skill

## When to Use
- Creating or modifying EF Core entities and DbContext
- Implementing gRPC service methods (CRUD)
- Configuring SQLite database and migrations
- Setting up WinForms Kestrel hosting
- Seeding initial data (100 personalities)

## Architecture

```
Hello_gRPC.Backend/
├── Data/
│   ├── AppDbContext.cs              # EF Core DbContext with SQLite
│   └── Migrations/                  # EF Core migrations
├── Entities/
│   └── Personality.cs               # EF Core entity
├── Services/
│   └── PersonalityService.cs        # gRPC service implementation
├── Extensions/
│   └── MappingExtensions.cs         # Proto <-> Entity mapping
├── MainForm.cs                      # WinForms host form
├── Program.cs                       # Entry point, Kestrel + gRPC setup
└── GlobalUsings.cs
```

## Procedure

### 1. Entity Definition
Follow the pattern in [EF Core patterns reference](./references/efcore-patterns.md).

### 2. gRPC Service Implementation
Follow the pattern in [gRPC service template](./references/grpc-service-template.md).

### 3. WinForms + Kestrel Hosting
Follow the pattern in [WinForms host reference](./references/winforms-host.md).

### 4. Build & Validate
```bash
dotnet build src/Hello_gRPC.Backend/
dotnet ef migrations add <MigrationName> --project src/Hello_gRPC.Backend/
```
