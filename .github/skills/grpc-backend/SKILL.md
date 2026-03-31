---
name: grpc-backend
description: "Use when creating or modifying the backend layers: Data (EF Core entities, DbContext, SQLite migrations, seed data), Service (business logic, DI extensions), Backend (gRPC service implementations, WinForms Kestrel hosting, proto-entity mapping). Use for CRUD operations, database schema, gRPC endpoints."
argument-hint: "Describe the backend change (entity, service, migration, etc.)"
---

# gRPC Backend Skill

## When to Use
- Creating or modifying EF Core entities and DbContext (Data layer)
- Implementing business logic services (Service layer)
- Implementing gRPC service methods (Backend layer)
- Configuring SQLite database and migrations
- Setting up WinForms Kestrel hosting
- Seeding initial data (100 personalities)
- Adding mapping extensions (proto <-> entity)

## Layered Architecture

### Data Layer (`Hello_gRPC.Data`)
```
Hello_gRPC.Data/
+-- Entities/
|   +-- Personality.cs               # EF Core entity (POCO)
+-- AppDbContext.cs                   # EF Core DbContext with SQLite
+-- DatabaseSeeder.cs                # Static SeedAsync method
+-- Migrations/                      # EF Core migrations
+-- GlobalUsings.cs
```
**Namespace**: `HelloGrpc.Data`, `HelloGrpc.Data.Entities`

### Service Layer (`Hello_gRPC.Service`)
```
Hello_gRPC.Service/
+-- PersonalityService.cs            # CRUD business logic (GetAllAsync, GetByIdAsync, AddAsync, UpdateAsync, DeleteAsync)
+-- ServiceCollectionExtensions.cs   # DI: AddHelloGrpcAppContext, AddHelloGrpcPersonalityService
+-- GlobalUsings.cs
```
**Namespace**: `HelloGrpc.Service`

### Backend Layer (`Hello_gRPC.Backend`)
```
Hello_gRPC.Backend/
+-- Services/
|   +-- PersonalityService.cs        # gRPC service (PersonalityGrpc.PersonalityGrpcBase)
+-- Extensions/
|   +-- MappingExtensions.cs         # Proto <-> Entity mapping (ToProto, ToEntity, UpdateFrom)
+-- MainForm.cs                      # WinForms host form
+-- Program.cs                       # Entry point: Kestrel + gRPC setup + DI
+-- GlobalUsings.cs
```
**Namespace**: `HelloGrpc.Backend`, `HelloGrpc.Backend.Services`, `HelloGrpc.Backend.Extensions`

## Procedure

### 1. Entity Definition (Data Layer)
Follow the pattern in [EF Core patterns reference](./references/efcore-patterns.md).
- Entities go in `Hello_gRPC.Data/Entities/`
- DbContext in `Hello_gRPC.Data/AppDbContext.cs`
- Seeding in `Hello_gRPC.Data/DatabaseSeeder.cs`

### 2. Business Logic (Service Layer)
- Create `{Entity}Service` in `Hello_gRPC.Service/` with primary constructor injecting `AppDbContext`
- Register via DI extension in `ServiceCollectionExtensions.cs`
- Methods: `GetAllAsync`, `GetByIdAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync`

### 3. gRPC Service Implementation (Backend Layer)
Follow the pattern in [gRPC service template](./references/grpc-service-template.md).
- Services go in `Hello_gRPC.Backend/Services/`
- Mapping extensions in `Hello_gRPC.Backend/Extensions/`

### 4. WinForms + Kestrel Hosting (Backend Layer)
Follow the pattern in [WinForms host reference](./references/winforms-host.md).

### 5. Build and Validate
```bash
dotnet build src/Hello_gRPC.Data/
dotnet build src/Hello_gRPC.Service/
dotnet build src/Hello_gRPC.Backend/
dotnet ef migrations add <MigrationName> --project src/Hello_gRPC.Data/ --startup-project src/Hello_gRPC.Backend/
```