---
name: blazor-frontend
description: "Use when creating or modifying the Blazor Server frontend: MudBlazor pages, components, gRPC client integration, CRUD UI, data tables, forms, dialogs, navigation. Use for UI, layout, theming, MudBlazor components."
argument-hint: "Describe the frontend page, component, or UI change"
---

# Blazor Frontend Skill

## When to Use
- Creating or modifying Blazor pages and components
- Implementing MudBlazor CRUD UI (tables, forms, dialogs)
- Configuring gRPC client connections
- Setting up navigation and layout
- Adding search, filtering, pagination

## Architecture

```
Hello_gRPC.Frontend/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor
│   │   └── NavMenu.razor
│   ├── Pages/
│   │   ├── Home.razor
│   │   └── Personalities/
│   │       ├── PersonalityList.razor       # Liste avec MudDataGrid
│   │       ├── PersonalityDetail.razor     # Détail / vue
│   │       └── PersonalityForm.razor       # Formulaire création/édition
│   ├── Dialogs/
│   │   ├── PersonalityFormDialog.razor     # Dialog MudBlazor pour CRUD
│   │   └── ConfirmDeleteDialog.razor       # Confirmation de suppression
│   ├── _Imports.razor
│   └── App.razor
├── Services/
│   └── PersonalityGrpcClient.cs            # Wrapper client gRPC
├── Program.cs
└── GlobalUsings.cs
```

## Procedure

### 1. gRPC Client Setup
Follow the pattern in [gRPC client reference](./references/grpc-client.md).

### 2. CRUD Pages with MudBlazor
Follow the pattern in [MudBlazor CRUD reference](./references/mudblazor-crud.md).

### 3. Component Patterns
Follow the pattern in [Component patterns reference](./references/component-patterns.md).

### 4. Build & Validate
```bash
dotnet build src/Hello_gRPC.Frontend/
```
