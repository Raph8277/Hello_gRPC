# Hello_gRPC - Project Guidelines

## Architecture

Ce projet est une application CRUD fullstack avec une **architecture en couches** :

| Couche | Projet | Responsabilite |
|--------|--------|----------------|
| **Shared** | `Hello_gRPC.Shared` | Fichiers `.proto` et contrats partages |
| **Data** | `Hello_gRPC.Data` | Entites EF Core, DbContext, migrations, seeding SQLite |
| **Service** | `Hello_gRPC.Service` | Logique metier CRUD, extensions DI |
| **Backend** | `Hello_gRPC.Backend` | WinForms + Kestrel gRPC server, mapping proto-entite |
| **Frontend** | `Hello_gRPC.Frontend` | Blazor Server + MudBlazor, clients gRPC |

### Structure de la solution

```
Hello_gRPC.sln
+-- src/
|   +-- Hello_gRPC.Shared/           # Protos, contrats partages
|   +-- Hello_gRPC.Data/             # Entites, DbContext, migrations, seeding
|   +-- Hello_gRPC.Service/          # Logique metier, extensions DI
|   +-- Hello_gRPC.Backend/          # WinForms + gRPC server
|   +-- Hello_gRPC.Frontend/         # Blazor Server + MudBlazor
```

### Graphe de dependances

```
Shared <-- Data <-- Service <-- Backend
Shared <------------------------------ Frontend
```

## Stack technique

| Composant      | Technologie                        |
|----------------|------------------------------------|
| Runtime        | .NET 10                            |
| Backend Host   | WinForms (.NET 10)                 |
| gRPC Server    | Grpc.AspNetCore heberge via Kestrel dans WinForms |
| ORM            | Entity Framework Core 10 + SQLite  |
| Frontend       | Blazor Server (.NET 10)            |
| UI Framework   | MudBlazor (derniere version stable)|
| Communication  | gRPC (protobuf)                    |

## Conventions de code

- Langue du code : **anglais** (noms de classes, methodes, variables, commentaires inline)
- Langue des commentaires XML et documentation : **francais**
- Namespace racine : `HelloGrpc` (sous-namespaces : `.Data`, `.Data.Entities`, `.Service`, `.Backend`, `.Shared`)
- Utiliser `record` pour les DTOs
- Utiliser les `primary constructors` quand applicable
- Utiliser `global using` dans chaque projet
- Configurer le `Nullable` a `enable` partout
- Utiliser `file-scoped namespaces`
- Async/await systematique pour les operations I/O
- Nommage : PascalCase pour les membres publics, _camelCase pour les champs prives

## Build et test

```bash
dotnet build Hello_gRPC.sln
dotnet test
```

## Patterns

- **Architecture en couches** : Data -> Service -> Backend, avec le Frontend independant
- **Logique metier** dans la couche Service, pas dans les services gRPC
- **Mapping manuel** proto-entite via des extensions methods dans le Backend
- **AutoMapper** non requis
- Validation cote serveur dans les services gRPC (couche Backend)
- Gestion d'erreurs gRPC via `RpcException` avec `StatusCode` appropries
- Les pages Blazor utilisent `@inject` pour les services gRPC client
- Extensions DI dans la couche Service (`AddHelloGrpcAppContext`, `AddHelloGrpc{Entity}Service`)