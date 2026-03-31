# Architecture Hello_gRPC — Documentation C4 (Mise à jour .NET 10)

> **Projet** : Hello_gRPC — Application CRUD fullstack de gestion de personnalités célèbres
> **Date** : Mars 2026
> **Stack** : .NET 10, gRPC, Blazor Server, MudBlazor, EF Core, SQLite, WinForms

---

## Table des matières

1. [Contexte Système (Level 1)](#1-contexte-système-level-1)
2. [Diagramme de Containers (Level 2)](#2-diagramme-de-containers-level-2)
3. [Diagramme de Composants — Backend (Level 3)](#3-diagramme-de-composants--backend-level-3)
4. [Diagramme de Composants — Frontend (Level 3)](#4-diagramme-de-composants--frontend-level-3)
5. [Diagramme Dynamique — Création (Level 4)](#5-diagramme-dynamique--création-level-4)
6. [Diagramme Dynamique — Lecture (Level 4)](#6-diagramme-dynamique--lecture-level-4)
7. [Diagramme de Déploiement](#7-diagramme-de-déploiement)

---

## 1. Contexte Système (Level 1)

Vue d'ensemble du système Hello_gRPC et de ses interactions avec l'utilisateur.

L'application se compose de deux systèmes principaux :
- **Frontend** : Application Blazor Server avec MudBlazor, accessible via navigateur web
- **Backend** : Application WinForms hébergeant un serveur gRPC via Kestrel avec persistance EF Core SQLite

L'utilisateur interagit uniquement avec le frontend. Le frontend communique avec le backend via gRPC sur HTTP/2.

```mermaid
C4Context
    title Hello_gRPC — Contexte Système (Level 1)

    Person(user, "Utilisateur", "Consulte, crée, modifie et supprime des personnalités célèbres.")

    System(frontend, "Hello_gRPC Frontend", "Application Blazor Server .NET 10 avec MudBlazor.<br/>Interface CRUD pour les personnalités.")

    System(backend, "Hello_gRPC Backend", "Application WinForms .NET 10 hébergeant<br/>un serveur gRPC via Kestrel + EF Core SQLite.")

    Rel(user, frontend, "Utilise", "HTTPS / Blazor Server")
    Rel(frontend, backend, "Appelle", "gRPC / HTTP/2")

    UpdateRelStyle(user, frontend, $offsetY="-40", $offsetX="10")
    UpdateRelStyle(frontend, backend, $offsetY="-40", $offsetX="10")
```

---

## 2. Diagramme de Containers (Level 2)

Détail des containers applicatifs composant le système Hello_gRPC.

| Container | Technologie | Rôle |
|-----------|-------------|------|
| **Blazor Server App** | .NET 10, MudBlazor | Interface web CRUD (MudDataGrid, Dialogs, formulaires) |
| **gRPC Client** | Grpc.Net.Client | Client typé généré depuis les fichiers `.proto` |
| **WinForms Host** | .NET 10 WinForms | Application bureau hébergeant Kestrel en arrière-plan |
| **Kestrel gRPC Server** | Grpc.AspNetCore, HTTP/2 | Expose les services gRPC CRUD sur le port 5001 |
| **Service Layer** | .NET 10 | Logique métier, validation, mapping |
| **Data Layer** | EF Core 10 + SQLite | DbContext, entités, seed des personnalités |
| **SQLite Database** | SQLite | Fichier `hello_grpc.db` stockant les personnalités |
| **Shared Library** | .NET 10, Grpc.Tools | Fichiers `.proto`, contrats gRPC, classes générées |

```mermaid
C4Container
    title Hello_gRPC — Diagramme de Containers (Level 2)

    Person(user, "Utilisateur", "Gère les personnalités via le navigateur web.")

    System_Boundary(hello, "Hello_gRPC") {
        Container(blazor, "Blazor Server App", ".NET 10, MudBlazor", "Interface web CRUD :<br/>MudDataGrid, Dialogs, formulaires.")
        Container(grpcClient, "gRPC Client", "Grpc.Net.Client", "Client typé généré depuis les .proto.<br/>Wrapper PersonalityGrpcClient.")
        Container(winforms, "WinForms Host", ".NET 10 WinForms", "Application bureau hébergeant<br/>le serveur Kestrel en arrière-plan.")
        Container(kestrel, "Kestrel gRPC Server", "Grpc.AspNetCore, HTTP/2", "Expose les services gRPC CRUD<br/>sur le port 5001.")
        Container(service, "Service Layer", ".NET 10", "Logique métier, validation, mapping.")
        Container(data, "Data Layer", "EF Core 10 + SQLite", "DbContext, entités, migrations,<br/>seed des personnalités.")
        ContainerDb(sqlite, "SQLite Database", "SQLite", "Stocke les personnalités,<br/>fichier hello_grpc.db.")
        Container(shared, "Shared Library", ".NET 10, Grpc.Tools", "Fichiers .proto, contrats gRPC,<br/>classes générées Client + Server.")
    }

    Rel(user, blazor, "Navigue", "HTTPS")
    Rel(blazor, grpcClient, "Utilise")
    Rel(grpcClient, kestrel, "Appelle", "gRPC / HTTP/2")
    Rel(winforms, kestrel, "Héberge")
    Rel(kestrel, service, "Utilise")
    Rel(service, data, "Utilise")
    Rel(data, sqlite, "Lit / Écrit", "SQLite")
    Rel(blazor, shared, "Référence")
    Rel(kestrel, shared, "Référence")

    UpdateRelStyle(user, blazor, $offsetY="-40")
    UpdateRelStyle(grpcClient, kestrel, $offsetY="-40")
    UpdateRelStyle(service, data, $offsetY="-30")
    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

---

## 3. Diagramme de Composants — Backend (Level 3)

Architecture interne du backend (WinForms + Kestrel + gRPC).

| Composant | Type | Responsabilité |
|-----------|------|----------------|
| **PersonalityService (gRPC)** | gRPC Service | Expose les opérations CRUD, consomme la couche Service |
| **PersonalityService (Service)** | Service Métier | Logique métier, validation, mapping, accès Data |
| **AppDbContext** | EF Core DbContext | Configuration du modèle, gestion des entités |
| **Personality** | EF Core Entity | Modèle de données |

```mermaid
C4Component
    title Hello_gRPC Backend — Composants (Level 3)

    Container_Boundary(backend, "Hello_gRPC.Backend") {
        Component(grpc, "PersonalityService (gRPC)", "gRPC Service", "Exposition CRUD, consomme Service Layer.")
        Component(service, "PersonalityService (Service)", "Service Métier", "Logique métier, validation, mapping, accès Data.")
        Component(dbContext, "AppDbContext", "EF Core DbContext", "Gestion des entités, configuration EF Core.")
        Component(personality, "Personality", "EF Core Entity", "Modèle de données.")
    }
    ContainerDb(sqlite, "SQLite", "SQLite", "hello_grpc.db")

    Rel(grpc, service, "Appelle")
    Rel(service, dbContext, "Utilise")
    Rel(dbContext, personality, "Mappe")
    Rel(dbContext, sqlite, "Lit / Écrit", "SQLite")
    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

---

## 4. Diagramme de Composants — Frontend (Level 3)

Architecture interne du frontend (Blazor Server).

| Composant | Type | Responsabilité |
|-----------|------|----------------|
| **MainLayout** | Blazor Layout | AppBar, Drawer, NavMenu, ThemeProvider |
| **PersonalityList** | Blazor Page | MudDataGrid paginé, recherche, filtre |
| **PersonalityFormDialog** | MudDialog | Formulaire création/édition |
| **ConfirmDeleteDialog** | MudDialog | Confirmation de suppression |
| **PersonalityGrpcClient** | Service Scoped | Wrapper typé autour du client gRPC |

```mermaid
C4Component
    title Hello_gRPC Frontend — Composants (Level 3)

    Container_Boundary(frontend, "Hello_gRPC.Frontend") {
        Component(listPage, "PersonalityList", "Blazor Page", "MudDataGrid avec pagination, recherche, filtre.")
        Component(formDialog, "PersonalityFormDialog", "MudDialog", "Formulaire création/édition.")
        Component(deleteDialog, "ConfirmDeleteDialog", "MudDialog", "Confirmation de suppression.")
        Component(grpcWrapper, "PersonalityGrpcClient", "Service Scoped", "Wrapper typé autour du client gRPC.")
        Component(layout, "MainLayout", "Blazor Layout", "AppBar, Drawer, NavMenu, ThemeProvider.")
    }
    Container(kestrel, "Kestrel gRPC Server", "Grpc.AspNetCore", "Backend gRPC sur port 5001.")

    Rel(layout, listPage, "Route /personalities")
    Rel(listPage, formDialog, "Ouvre")
    Rel(listPage, deleteDialog, "Ouvre")
    Rel(listPage, grpcWrapper, "Charge les données")
    Rel(formDialog, grpcWrapper, "Crée / Met à jour")
    Rel(deleteDialog, grpcWrapper, "Supprime")
    Rel(grpcWrapper, kestrel, "Appelle", "gRPC / HTTP/2")
    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

---

## 5. Diagramme Dynamique — Création (Level 4)

Flux détaillé de la création d'une nouvelle personnalité.

```mermaid
C4Dynamic
    title Hello_gRPC — Flux Création d'une personnalité
    Person(user, "Utilisateur", "Crée une nouvelle personnalité.")
    Container(blazor, "Blazor Frontend", ".NET 10, MudBlazor", "Interface web.")
    Container(grpc, "gRPC Server", "Kestrel, HTTP/2", "Service backend.")
    ContainerDb(db, "SQLite", "SQLite", "hello_grpc.db")
    Rel(user, blazor, "Remplit le formulaire et valide")
    Rel(blazor, grpc, "CreatePersonality(request)", "gRPC")
    Rel(grpc, grpc, "Valide les champs requis")
    Rel(grpc, db, "INSERT INTO Personalities", "EF Core")
    Rel(db, grpc, "Retourne l'entité créée")
    Rel(grpc, blazor, "PersonalityMessage", "gRPC")
    Rel(blazor, user, "Affiche Snackbar succès et rafraîchit la grille")
    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

---

## 6. Diagramme Dynamique — Lecture (Level 4)

Flux détaillé de la consultation de la liste des personnalités avec pagination et filtrage.

```mermaid
C4Dynamic
    title Hello_gRPC — Flux Lecture avec pagination
    Person(user, "Utilisateur", "Consulte la liste des personnalités.")
    Container(blazor, "Blazor Frontend", ".NET 10, MudBlazor", "Interface web.")
    Container(grpc, "gRPC Server", "Kestrel, HTTP/2", "Service backend.")
    ContainerDb(db, "SQLite", "SQLite", "hello_grpc.db")
    Rel(user, blazor, "Navigue vers /personalities")
    Rel(blazor, grpc, "GetPersonalities(skip, take, search, category)", "gRPC")
    Rel(grpc, db, "SELECT avec pagination et filtres", "EF Core")
    Rel(db, grpc, "Retourne les entités")
    Rel(grpc, blazor, "GetPersonalitiesResponse", "gRPC")
    Rel(blazor, user, "Affiche le MudDataGrid paginé")
    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

---

## 7. Diagramme de Déploiement

```mermaid
C4Deployment
    title Hello_gRPC — Déploiement (Poste Développeur)
    Deployment_Node(dev, "Poste Développeur", "Windows 10/11") {
        Deployment_Node(winforms, "WinForms Process", ".NET 10 WinExe") {
            Deployment_Node(kestrel, "Kestrel Server", "HTTP/2, Port 5001") {
                Container(grpcServer, "gRPC Services", "Grpc.AspNetCore", "PersonalityService CRUD")
            }
            ContainerDb(sqlite, "SQLite Database", "SQLite", "hello_grpc.db")
        }
        Deployment_Node(blazorHost, "Blazor Server Process", ".NET 10 Web") {
            Container(blazorApp, "Blazor Server App", "MudBlazor", "Interface CRUD sur port 5000")
        }
        Deployment_Node(browser, "Navigateur Web", "Chrome / Edge / Firefox") {
            Container(spa, "Blazor UI", "SignalR / WebSocket", "Rendu interactif côté serveur")
        }
    }
    Rel(spa, blazorApp, "SignalR", "WebSocket")
    Rel(blazorApp, grpcServer, "gRPC", "HTTP/2, localhost:5001")
    Rel(grpcServer, sqlite, "Lit / Écrit", "EF Core SQLite")
    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

---
