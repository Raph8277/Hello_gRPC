# gRPC Client Integration

## Client Service Pattern

```csharp
namespace HelloGrpc.Frontend.Services;

/// <summary>
/// Client gRPC pour la gestion des personnalités.
/// </summary>
public class PersonalityGrpcClient(PersonalityGrpc.PersonalityGrpcClient client)
{
    /// <summary>
    /// Récupère les personnalités avec pagination et filtrage.
    /// </summary>
    public async Task<GetPersonalitiesResponse> GetPersonalitiesAsync(
        string? searchTerm = null, string? category = null, int skip = 0, int take = 20)
    {
        var request = new GetPersonalitiesRequest
        {
            SearchTerm = searchTerm ?? "",
            Category = category ?? "",
            Skip = skip,
            Take = take
        };
        return await client.GetPersonalitiesAsync(request);
    }

    /// <summary>
    /// Récupère une personnalité par son identifiant.
    /// </summary>
    public async Task<PersonalityMessage> GetPersonalityAsync(int id)
    {
        return await client.GetPersonalityAsync(new GetPersonalityRequest { Id = id });
    }

    /// <summary>
    /// Crée une nouvelle personnalité.
    /// </summary>
    public async Task<PersonalityMessage> CreatePersonalityAsync(CreatePersonalityRequest request)
    {
        return await client.CreatePersonalityAsync(request);
    }

    /// <summary>
    /// Met à jour une personnalité existante.
    /// </summary>
    public async Task<PersonalityMessage> UpdatePersonalityAsync(UpdatePersonalityRequest request)
    {
        return await client.UpdatePersonalityAsync(request);
    }

    /// <summary>
    /// Supprime une personnalité.
    /// </summary>
    public async Task<DeletePersonalityResponse> DeletePersonalityAsync(int id)
    {
        return await client.DeletePersonalityAsync(new DeletePersonalityRequest { Id = id });
    }
}
```

## Program.cs — Client Registration

```csharp
using HelloGrpc.Frontend.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
});

// gRPC client
builder.Services.AddGrpcClient<PersonalityGrpc.PersonalityGrpcClient>(options =>
{
    options.Address = new Uri("http://localhost:5001");
})
.ConfigureChannel(options =>
{
    options.UnsafeUseInsecureChannelCallCredentials = true;
});

builder.Services.AddScoped<PersonalityGrpcClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
```

## .csproj Configuration

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <RootNamespace>HelloGrpc.Frontend</RootNamespace>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Grpc.Net.Client" Version="2.*" />
    <PackageReference Include="MudBlazor" Version="8.*" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Hello_gRPC.Shared\Hello_gRPC.Shared.csproj" />
  </ItemGroup>
</Project>
```

## Key Configuration

- gRPC client points to `http://localhost:5001` (backend Kestrel)
- Use `AddGrpcClient<T>` from `Grpc.Net.ClientFactory`
- `PersonalityGrpcClient` wrapper is registered as `Scoped`
- MudBlazor services configured with snackbar defaults
