---
description: "Add a new MudBlazor CRUD page: data grid with server-side pagination, search, filter, create/edit dialog, delete confirmation."
agent: "blazor-grpc-fullstack"
argument-hint: "Page name and entity (e.g., 'PersonalityList page for Personality entity')"
---

Create a new MudBlazor CRUD page in the Blazor frontend.

## Steps

1. **List Page**: Create a `.razor` page with:
   - `MudDataGrid<T>` with server-side data loading
   - Search field with debounce
   - Category/filter dropdown
   - Add button opening a create dialog
   - Edit/Delete action buttons per row

2. **Form Dialog**: Create a `MudDialog` with:
   - `MudForm` with validation
   - All entity fields as MudBlazor inputs
   - Support both create and edit modes
   - Cancel and Submit actions

3. **Delete Dialog**: Create a confirmation dialog with:
   - Display name of entity being deleted
   - Cancel and Confirm (red) buttons

4. **Navigation**: Add the page to `NavMenu.razor`

5. **Build**: Run `dotnet build` to validate

Use French labels throughout. Inject `IDialogService`, `ISnackbar`, and the gRPC client service.
