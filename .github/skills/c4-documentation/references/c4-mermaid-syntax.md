# C4 Mermaid Syntax Reference

## Diagram Types

| Keyword | C4 Level |
|---------|----------|
| `C4Context` | Level 1 — System Context |
| `C4Container` | Level 2 — Container |
| `C4Component` | Level 3 — Component |
| `C4Dynamic` | Level 4 — Dynamic |
| `C4Deployment` | Deployment |

## Elements — System Context

```
Person(alias, label, ?description)
Person_Ext(alias, label, ?description)
System(alias, label, ?description)
System_Ext(alias, label, ?description)
SystemDb(alias, label, ?description)
SystemDb_Ext(alias, label, ?description)
SystemQueue(alias, label, ?description)
SystemQueue_Ext(alias, label, ?description)
```

## Elements — Container

```
Container(alias, label, ?technology, ?description)
Container_Ext(alias, label, ?technology, ?description)
ContainerDb(alias, label, ?technology, ?description)
ContainerDb_Ext(alias, label, ?technology, ?description)
ContainerQueue(alias, label, ?technology, ?description)
ContainerQueue_Ext(alias, label, ?technology, ?description)
```

## Elements — Component

```
Component(alias, label, ?technology, ?description)
Component_Ext(alias, label, ?technology, ?description)
ComponentDb(alias, label, ?technology, ?description)
ComponentDb_Ext(alias, label, ?technology, ?description)
ComponentQueue(alias, label, ?technology, ?description)
ComponentQueue_Ext(alias, label, ?technology, ?description)
```

## Elements — Deployment

```
Deployment_Node(alias, label, ?type, ?description)
Node(alias, label, ?type, ?description)
Node_L(alias, label, ?type, ?description)
Node_R(alias, label, ?type, ?description)
```

## Boundaries

```
Enterprise_Boundary(alias, label) { ... }
System_Boundary(alias, label) { ... }
Container_Boundary(alias, label) { ... }
Boundary(alias, label, ?type) { ... }
```

## Relationships

```
Rel(from, to, label, ?technology, ?description)
BiRel(from, to, label, ?technology, ?description)
Rel_U(from, to, label, ?technology)     # Up
Rel_D(from, to, label, ?technology)     # Down
Rel_L(from, to, label, ?technology)     # Left
Rel_R(from, to, label, ?technology)     # Right
Rel_Back(from, to, label, ?technology)
```

## Dynamic Diagram Relationships

```
RelIndex(index, from, to, label, ?technology)
```
Note: Index is ignored by Mermaid; order of statements determines sequence.

## Styling

```
UpdateElementStyle(elementName, $bgColor, $fontColor, $borderColor, $shadowing)
UpdateRelStyle(from, to, $textColor, $lineColor, $offsetX, $offsetY)
UpdateLayoutConfig($c4ShapeInRow, $c4BoundaryInRow)
```

## Rules

- Use `title` on the second line for diagram title
- Named parameters use `$` prefix: `$bgColor="grey"`
- Boundaries use curly braces `{ ... }` and can be nested
- Relationships are declared AFTER all elements
- Style updates go at the very end
- `UpdateLayoutConfig` controls shapes per row (default 4) and boundaries per row (default 2)
- Use `<br/>` for line breaks in descriptions
