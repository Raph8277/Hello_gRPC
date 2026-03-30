---
description: "Generate full C4 Model architecture documentation for Hello_gRPC with Mermaid diagrams. Produces System Context, Container, Component, Dynamic, and Deployment diagrams."
agent: "blazor-grpc-fullstack"
argument-hint: "Optional: specify which C4 levels to generate, or 'full' for all levels"
---

Generate architecture documentation with C4 Model Mermaid diagrams for Hello_gRPC.

## Steps

### 1. Analyze Current State
- Read the solution structure and existing code
- Identify all services, components, and data flows

### 2. Generate C4 Diagrams
Use the c4-documentation skill. For each level:

**Level 1 — System Context** (`C4Context`):
- Actors (users)
- Systems (frontend, backend)
- External systems if any
- Relationships

**Level 2 — Container** (`C4Container`):
- Blazor Server app, gRPC client, WinForms host, Kestrel server, EF Core layer, SQLite, Shared library
- Technologies and communication protocols

**Level 3 — Component** (`C4Component`):
- Backend: gRPC services, DbContext, entities, mapping extensions, seeder
- Frontend: pages, dialogs, gRPC client wrapper, layout

**Level 4 — Dynamic** (`C4Dynamic`):
- Create flow, Read flow, Update flow, Delete flow

**Deployment** (`C4Deployment`):
- WinForms process, Blazor Server process, browser, SQLite file

### 3. Validate & Render
- Validate each diagram with `#tool:mermaid-diagram-validator`
- Render images with `#tool:renderMermaidDiagram`

### 4. Assemble Document
Create a `docs/architecture-c4.md` file with all diagrams and descriptions in French.
