---
description: "Scaffold a complete new entity across all layers: proto, EF Core entity, DbContext, gRPC service, mapping extensions, and Blazor CRUD page."
agent: "blazor-grpc-fullstack"
argument-hint: "Entity name and its properties (e.g., 'Product with Name, Price, Category')"
---

Scaffold a complete CRUD entity across the entire Hello_gRPC stack.

## Steps

1. **Proto**: Add the new message and CRUD RPCs in `src/Hello_gRPC.Shared/Protos/`
2. **Entity**: Create the EF Core entity in `src/Hello_gRPC.Backend/Entities/`
3. **DbContext**: Add the `DbSet<T>` and configure in `OnModelCreating`
4. **Migration**: Create an EF Core migration
5. **gRPC Service**: Implement the CRUD service in `src/Hello_gRPC.Backend/Services/`
6. **Mapping**: Add extension methods in `src/Hello_gRPC.Backend/Extensions/`
7. **Client**: Add gRPC client wrapper in `src/Hello_gRPC.Frontend/Services/`
8. **Blazor Pages**: Create list page with MudDataGrid, form dialog, and delete confirmation
9. **Navigation**: Add nav link in `NavMenu.razor`
10. **Build**: Run `dotnet build` to validate

Follow all project conventions: French UI labels, MudBlazor components, RpcException for errors, manual mapping.
