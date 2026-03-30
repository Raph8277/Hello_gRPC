---
name: c4-documentation
description: "Use when generating architecture documentation with C4 Model Mermaid diagrams. Produces System Context, Container, Component, Dynamic, and Deployment diagrams. Use for documenting architecture, visualizing gRPC flows, explaining system structure, creating technical documentation."
argument-hint: "Describe the documentation to generate (e.g., 'full C4 documentation', 'component diagram for backend')"
---

# C4 Documentation Skill

## When to Use
- Generating architecture documentation for the Hello_gRPC project
- Creating C4 Model diagrams (Context, Container, Component, Dynamic, Deployment)
- Documenting gRPC communication flows
- Producing technical documentation with Mermaid diagrams
- Explaining system structure to new developers

## C4 Model Levels

| Level | Diagram | Purpose |
|-------|---------|---------|
| 1 | **System Context** (`C4Context`) | Vue d'ensemble : utilisateurs et systèmes externes |
| 2 | **Container** (`C4Container`) | Applications, bases de données, communication |
| 3 | **Component** (`C4Component`) | Composants internes d'un container |
| 4 | **Dynamic** (`C4Dynamic`) | Flux de données pour un scénario précis |
| — | **Deployment** (`C4Deployment`) | Infrastructure de déploiement |

## Procedure

### 1. Analyze the Scope
Determine which C4 level(s) are needed:
- **Full documentation**: Generate all 5 diagram types
- **Specific layer**: Focus on the requested level

### 2. Generate Diagrams
Follow the Mermaid C4 syntax reference in [C4 Mermaid syntax](./references/c4-mermaid-syntax.md).
Use the project-specific diagram templates in [Hello_gRPC C4 templates](./references/hello-grpc-c4-templates.md).

### 3. Render & Validate
Use `#tool:mermaid-diagram-validator` to validate diagram syntax before rendering.
Use `#tool:renderMermaidDiagram` to produce the final image.

### 4. Assemble Documentation
Structure the output as a Markdown document with:
- Title and date
- Each C4 level as a section
- Mermaid diagram code blocks
- Textual descriptions in French

## Output Format

```markdown
# Architecture Hello_gRPC — Documentation C4

## 1. Contexte Système (Level 1)
Description + diagramme C4Context

## 2. Diagramme de Containers (Level 2)
Description + diagramme C4Container

## 3. Diagramme de Composants (Level 3)
Description + diagramme C4Component (backend) + C4Component (frontend)

## 4. Diagramme Dynamique (Level 4)
Description + diagramme C4Dynamic pour chaque flux CRUD

## 5. Diagramme de Déploiement
Description + diagramme C4Deployment
```
