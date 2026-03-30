---
description: "Use when writing EF Core entities, DbContext, migrations, database configuration, SQLite setup, seed data, and data access code."
applyTo: ["**/Data/**", "**/Entities/**", "**/Migrations/**"]
---

# EF Core Guidelines

## Entity Conventions
- Use `required` keyword for non-nullable string properties
- Use `DateOnly` for dates (not `DateTime`)
- Use `DateTime` only for `CreatedAt` / `UpdatedAt` timestamps
- Primary key: `Id` (int, auto-increment)
- Configure constraints via Fluent API in `OnModelCreating`

## DbContext
- Use primary constructor for `DbContextOptions` injection
- Override `SaveChangesAsync` to auto-update timestamps
- Use `Set<T>()` expression-bodied DbSet properties
- SQLite connection string: `Data Source=hello_grpc.db`

## Migrations
```bash
dotnet ef migrations add <Name> --project src/Hello_gRPC.Backend/
dotnet ef database update --project src/Hello_gRPC.Backend/
```

## Seeding
- Check `AnyAsync()` before seeding to avoid duplicates
- Call `Database.MigrateAsync()` at startup before seeding
- Use the `DatabaseSeeder.SeedAsync()` static method
