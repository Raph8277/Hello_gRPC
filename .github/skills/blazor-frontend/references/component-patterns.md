# Blazor Component Patterns

## Layout Pattern

### MainLayout.razor

```razor
@inherits LayoutComponentBase

<MudThemeProvider @ref="_themeProvider" @bind-IsDarkMode="_isDarkMode" Theme="_theme" />
<MudPopoverProvider />
<MudDialogProvider />
<MudSnackbarProvider />

<MudLayout>
    <MudAppBar Elevation="1" Color="Color.Primary">
        <MudIconButton Icon="@Icons.Material.Filled.Menu"
                       Color="Color.Inherit"
                       Edge="Edge.Start"
                       OnClick="ToggleDrawer" />
        <MudText Typo="Typo.h6">Hello gRPC — Personnalités</MudText>
        <MudSpacer />
        <MudIconButton Icon="@(_isDarkMode ? Icons.Material.Filled.LightMode : Icons.Material.Filled.DarkMode)"
                       Color="Color.Inherit"
                       OnClick="ToggleDarkMode" />
    </MudAppBar>

    <MudDrawer @bind-Open="_drawerOpen" Elevation="2" Variant="DrawerVariant.Mini">
        <NavMenu />
    </MudDrawer>

    <MudMainContent Class="pa-4">
        @Body
    </MudMainContent>
</MudLayout>

@code {
    private MudThemeProvider _themeProvider = null!;
    private bool _isDarkMode;
    private bool _drawerOpen = true;

    private readonly MudTheme _theme = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#1976D2",
            Secondary = "#FF6F00",
            AppbarBackground = "#1976D2"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#90CAF9",
            Secondary = "#FFB74D"
        }
    };

    private void ToggleDrawer() => _drawerOpen = !_drawerOpen;
    private void ToggleDarkMode() => _isDarkMode = !_isDarkMode;
}
```

### NavMenu.razor

```razor
<MudNavMenu>
    <MudNavLink Href="/" Match="NavLinkMatch.All"
                Icon="@Icons.Material.Filled.Home">
        Accueil
    </MudNavLink>
    <MudNavLink Href="/personalities" Match="NavLinkMatch.Prefix"
                Icon="@Icons.Material.Filled.People">
        Personnalités
    </MudNavLink>
</MudNavMenu>
```

## _Imports.razor

```razor
@using System.Net.Http
@using Microsoft.AspNetCore.Components.Forms
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Web
@using Microsoft.JSInterop
@using MudBlazor
@using HelloGrpc.Frontend
@using HelloGrpc.Frontend.Components
@using HelloGrpc.Frontend.Components.Layout
@using HelloGrpc.Frontend.Components.Pages
@using HelloGrpc.Frontend.Components.Dialogs
@using HelloGrpc.Frontend.Services
@using HelloGrpc.Shared
```

## App.razor

```razor
<!DOCTYPE html>
<html lang="fr">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <base href="/" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500;700&display=swap" rel="stylesheet" />
    <link href="_content/MudBlazor/MudBlazor.min.css" rel="stylesheet" />
    <HeadOutlet @rendermode="InteractiveServer" />
</head>
<body>
    <Routes @rendermode="InteractiveServer" />
    <script src="_framework/blazor.web.js"></script>
    <script src="_content/MudBlazor/MudBlazor.min.js"></script>
</body>
</html>
```

## Conventions

- All pages use `@inject` for service injection
- Use `MudDialog` for create/edit forms (not separate pages)
- Use French for all UI labels and messages
- Use `MudDataGrid` with server-side pagination (not `MudTable`)
- Use `MudSnackbar` for success/error feedback
- Support dark mode via `MudThemeProvider`
- Use `MudGrid` / `MudItem` for responsive form layouts
