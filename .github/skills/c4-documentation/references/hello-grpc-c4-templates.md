# Hello_gRPC — C4 Diagram Templates

Pre-built C4 diagrams for the Hello_gRPC project. Adapt and extend as the project evolves.

---

## Level 1 — System Context

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

## Level 2 — Container

```mermaid
C4Container
    title Hello_gRPC — Diagramme de Containers (Level 2)

    Person(user, "Utilisateur", "Gère les personnalités via le navigateur web.")

    System_Boundary(hello, "Hello_gRPC") {

        Container(blazor, "Blazor Server App", ".NET 10, MudBlazor", "Interface web CRUD :<br/>MudDataGrid, Dialogs, formulaires.")

        Container(grpcClient, "gRPC Client", "Grpc.Net.Client", "Client typé généré depuis les .proto.<br/>Wrapper PersonalityGrpcClient.")

        Container(winforms, "WinForms Host", ".NET 10 WinForms", "Application bureau hébergeant<br/>le serveur Kestrel en arrière-plan.")

        Container(kestrel, "Kestrel gRPC Server", "Grpc.AspNetCore, HTTP/2", "Expose les services gRPC CRUD<br/>sur le port 5001.")

        Container(efcore, "EF Core Data Layer", "EF Core 10 + SQLite", "DbContext, entités, migrations,<br/>seed des 100 personnalités.")

        ContainerDb(sqlite, "SQLite Database", "SQLite", "Stocke les personnalités,<br/>fichier hello_grpc.db.")

        Container(shared, "Shared Library", ".NET 10, Grpc.Tools", "Fichiers .proto, contrats gRPC,<br/>classes générées Client + Server.")
    }

    Rel(user, blazor, "Navigue", "HTTPS")
    Rel(blazor, grpcClient, "Utilise")
    Rel(grpcClient, kestrel, "Appelle", "gRPC / HTTP/2")
    Rel(winforms, kestrel, "Héberge")
    Rel(kestrel, efcore, "Utilise")
    Rel(efcore, sqlite, "Lit / Écrit", "SQLite")
    Rel(blazor, shared, "Référence")
    Rel(kestrel, shared, "Référence")

    UpdateRelStyle(user, blazor, $offsetY="-40")
    UpdateRelStyle(grpcClient, kestrel, $offsetY="-40")
    UpdateRelStyle(efcore, sqlite, $offsetY="-30")
    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

---

## Level 3 — Component (Backend)

```mermaid
C4Component
    title Hello_gRPC Backend — Composants (Level 3)

    Container(grpcClient, "gRPC Client", "Grpc.Net.Client", "Appels gRPC depuis le frontend.")

    Container_Boundary(backend, "Hello_gRPC.Backend") {

        Component(personalityService, "PersonalityService", "gRPC Service", "Implémente les opérations CRUD :<br/>GetPersonalities, GetPersonality,<br/>CreatePersonality, UpdatePersonality,<br/>DeletePersonality.")

        Component(mappingExt, "MappingExtensions", "Extension Methods", "Convertit Entity <-> Proto Message.<br/>ToProto(), ToEntity(), UpdateFrom().")

        Component(dbContext, "AppDbContext", "EF Core DbContext", "Configure le modèle, gère les timestamps,<br/>expose DbSet Personalities.")

        Component(seeder, "DatabaseSeeder", "Static Class", "Seed 100 personnalités au premier<br/>lancement si la base est vide.")

        Component(personality, "Personality", "EF Core Entity", "Id, FirstName, LastName, Bio,<br/>Category, Nationality, BirthDate,<br/>DeathDate, ImageUrl, timestamps.")
    }

    ContainerDb(sqlite, "SQLite", "SQLite", "hello_grpc.db")

    Rel(grpcClient, personalityService, "Appelle", "gRPC / HTTP/2")
    Rel(personalityService, mappingExt, "Utilise")
    Rel(personalityService, dbContext, "Requête via")
    Rel(dbContext, personality, "Mappe")
    Rel(dbContext, sqlite, "Lit / Écrit", "SQLite")
    Rel(seeder, dbContext, "Seed via")

    UpdateRelStyle(grpcClient, personalityService, $offsetY="-40")
    UpdateRelStyle(personalityService, dbContext, $offsetX="-40")
    UpdateRelStyle(dbContext, sqlite, $offsetY="-30")
    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

---

## Level 3 — Component (Frontend)

```mermaid
C4Component
    title Hello_gRPC Frontend — Composants (Level 3)

    Person(user, "Utilisateur", "Gère les personnalités.")

    Container_Boundary(frontend, "Hello_gRPC.Frontend") {

        Component(listPage, "PersonalityList", "Blazor Page", "MudDataGrid avec pagination serveur,<br/>recherche, filtre par catégorie.")

        Component(formDialog, "PersonalityFormDialog", "MudDialog", "Formulaire création/édition<br/>avec MudForm validation.")

        Component(deleteDialog, "ConfirmDeleteDialog", "MudDialog", "Confirmation de suppression<br/>avec nom de la personnalité.")

        Component(grpcWrapper, "PersonalityGrpcClient", "Service Scoped", "Wrapper typé autour du client gRPC.<br/>Get, Create, Update, Delete.")

        Component(layout, "MainLayout", "Blazor Layout", "AppBar, Drawer, NavMenu,<br/>ThemeProvider dark/light mode.")
    }

    Container(kestrel, "Kestrel gRPC Server", "Grpc.AspNetCore", "Backend gRPC sur port 5001.")

    Rel(user, layout, "Navigue")
    Rel(layout, listPage, "Route /personalities")
    Rel(listPage, formDialog, "Ouvre")
    Rel(listPage, deleteDialog, "Ouvre")
    Rel(listPage, grpcWrapper, "Charge les données")
    Rel(formDialog, grpcWrapper, "Crée / Met à jour")
    Rel(deleteDialog, grpcWrapper, "Supprime")
    Rel(grpcWrapper, kestrel, "Appelle", "gRPC / HTTP/2")

    UpdateRelStyle(user, layout, $offsetY="-40")
    UpdateRelStyle(grpcWrapper, kestrel, $offsetY="-40")
    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

---

## Level 4 — Dynamic (CRUD Flow)

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
    Rel(blazor, user, "Affiche Snackbar succès + rafraîchit la grille")

    UpdateRelStyle(user, blazor, $offsetY="-50")
    UpdateRelStyle(blazor, grpc, $offsetY="-40")
    UpdateRelStyle(grpc, db, $offsetY="-40")
    UpdateRelStyle(blazor, user, $offsetY="40")
```

---

## Deployment

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

    UpdateRelStyle(spa, blazorApp, $offsetY="-40")
    UpdateRelStyle(blazorApp, grpcServer, $offsetY="-40")
    UpdateRelStyle(grpcServer, sqlite, $offsetY="-40")
```
