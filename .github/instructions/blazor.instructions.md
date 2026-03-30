---
description: "Use when editing Blazor .razor files, Blazor components, MudBlazor pages, layout, navigation. Covers component structure, render modes, dependency injection."
applyTo: "**/*.razor"
---

# Blazor Component Guidelines

## Structure
- Use `@page` directive for routable components
- Use `@inject` for dependency injection (not constructor injection)
- Keep `@code` blocks at the bottom of the file
- Use `@rendermode InteractiveServer` for interactive components

## MudBlazor
- Always use MudBlazor components (MudButton, MudTextField, etc.) — never raw HTML inputs
- Use `MudDataGrid<T>` with server-side pagination for lists
- Use `MudDialog` for create/edit forms
- Use `MudSnackbar` for user notifications
- Use `MudForm` with validation for form inputs

## Patterns
- Dialog-based CRUD: list page opens dialogs for create/edit/delete
- French labels for all UI text
- Category filter + search box on list pages
- `CascadingParameter` for `IMudDialogInstance` in dialogs
- Cancel + Submit buttons in dialog actions
