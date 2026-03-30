---
description: "Initialize the complete Hello_gRPC solution from scratch: create .sln, all .csproj projects, proto files, EF Core setup, WinForms host, Blazor frontend, MudBlazor, seed 100 personalities."
agent: "blazor-grpc-fullstack"
argument-hint: "Optional: specify port numbers or customizations"
---

Initialize the complete Hello_gRPC solution structure from scratch.

## Steps

### 1. Solution & Projects
```bash
dotnet new sln -n Hello_gRPC
mkdir src
dotnet new classlib -n Hello_gRPC.Shared -o src/Hello_gRPC.Shared
dotnet new winforms -n Hello_gRPC.Backend -o src/Hello_gRPC.Backend
dotnet new blazor -n Hello_gRPC.Frontend -o src/Hello_gRPC.Frontend --interactivity Server
dotnet sln add src/Hello_gRPC.Shared src/Hello_gRPC.Backend src/Hello_gRPC.Frontend
```

### 2. Configure Projects
- **Shared**: Change SDK to `Microsoft.NET.Sdk`, add Grpc.Tools + Google.Protobuf + Grpc.Net.Client, set RootNamespace to `HelloGrpc.Shared`
- **Backend**: Change SDK to `Microsoft.NET.Sdk.Web`, add Grpc.AspNetCore + EF Core SQLite, reference Shared, set RootNamespace to `HelloGrpc.Backend`
- **Frontend**: Add MudBlazor + Grpc.Net.ClientFactory, reference Shared, set RootNamespace to `HelloGrpc.Frontend`

### 3. Proto Definitions
Create `src/Hello_gRPC.Shared/Protos/personality.proto` with full CRUD contract.

### 4. Backend Implementation
- Entity, DbContext, DatabaseSeeder (100 personalities from skill assets)
- PersonalityService (gRPC CRUD)
- MappingExtensions
- Program.cs (WinForms + Kestrel on port 5001)
- MainForm

### 5. Frontend Implementation
- Program.cs (gRPC client + MudBlazor)
- MainLayout, NavMenu, App.razor
- PersonalityList page + dialogs
- PersonalityGrpcClient wrapper

### 6. Build & Validate
```bash
dotnet build Hello_gRPC.sln
```
