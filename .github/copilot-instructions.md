# Hello_gRPC - Project Guidelines

## Architecture

Ce projet est une application CRUD fullstack composée de :

- **Backend** : Application WinForms .NET 10 hébergeant des services gRPC + EF Core SQLite
- **Frontend** : Application Blazor Server .NET 10 avec MudBlazor consommant le backend gRPC
- **Shared** : Bibliothèque contenant les fichiers `.proto` et les contrats partagés

### Structure de la solution

```
Hello_gRPC.sln
├── src/
│   ├── Hello_gRPC.Shared/           # Protos, contrats partagés
│   ├── Hello_gRPC.Backend/          # WinForms + gRPC server + EF Core
│   └── Hello_gRPC.Frontend/         # Blazor Server + MudBlazor
```

## Stack technique

| Composant      | Technologie                        |
|----------------|------------------------------------|
| Runtime        | .NET 10                            |
| Backend Host   | WinForms (.NET 10)                 |
| gRPC Server    | Grpc.AspNetCore hébergé via Kestrel dans WinForms |
| ORM            | Entity Framework Core 10 + SQLite  |
| Frontend       | Blazor Server (.NET 10)            |
| UI Framework   | MudBlazor (dernière version stable)|
| Communication  | gRPC (protobuf)                    |

## Conventions de code

- Langue du code : **anglais** (noms de classes, méthodes, variables, commentaires inline)
- Langue des commentaires XML et documentation : **français**
- Namespace racine : `HelloGrpc`
- Utiliser `record` pour les DTOs
- Utiliser les `primary constructors` quand applicable
- Utiliser `global using` dans chaque projet
- Configurer le `Nullable` à `enable` partout
- Utiliser `file-scoped namespaces`
- Async/await systématique pour les opérations I/O
- Nommage : PascalCase pour les membres publics, _camelCase pour les champs privés

## Build et test

```bash
dotnet build Hello_gRPC.sln
dotnet test
```

## Patterns

- **Repository pattern** non requis : utiliser DbContext directement dans les services gRPC
- **AutoMapper** non requis : mapper manuellement ou via des extensions methods
- Validation côté serveur dans les services gRPC
- Gestion d'erreurs gRPC via `RpcException` avec `StatusCode` appropriés
- Les pages Blazor utilisent `@inject` pour les services gRPC client
