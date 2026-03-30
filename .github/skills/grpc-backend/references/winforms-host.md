# WinForms + Kestrel gRPC Host

## Program.cs Pattern

```csharp
namespace HelloGrpc.Backend;

/// <summary>
/// Point d'entrée de l'application backend WinForms hébergeant le serveur gRPC.
/// </summary>
static class Program
{
    [STAThread]
    static async Task Main()
    {
        ApplicationConfiguration.Initialize();

        var builder = WebApplication.CreateBuilder();

        // Configure Kestrel pour gRPC
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenLocalhost(5001, listenOptions =>
            {
                listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
            });
        });

        // EF Core SQLite
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=hello_grpc.db"));

        // gRPC
        builder.Services.AddGrpc();

        var app = builder.Build();

        // Appliquer les migrations et seeder
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();
            await DatabaseSeeder.SeedAsync(context);
        }

        app.MapGrpcService<PersonalityService>();

        // Démarrer Kestrel en arrière-plan
        _ = app.RunAsync();

        // Démarrer WinForms
        Application.Run(new MainForm());
    }
}
```

## MainForm Pattern

```csharp
namespace HelloGrpc.Backend;

/// <summary>
/// Formulaire principal de l'application backend.
/// </summary>
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        Text = "Hello gRPC - Backend Server";
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(400, 200);

        var label = new Label
        {
            Text = "Serveur gRPC en cours d'exécution sur le port 5001...",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 12)
        };
        Controls.Add(label);
    }
}
```

## .csproj Configuration

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <UseWindowsForms>true</UseWindowsForms>
    <RootNamespace>HelloGrpc.Backend</RootNamespace>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Grpc.AspNetCore" Version="2.*" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.*" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.*">
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Hello_gRPC.Shared\Hello_gRPC.Shared.csproj" />
  </ItemGroup>
</Project>
```

## Key Points

- SDK must be `Microsoft.NET.Sdk.Web` (not `Microsoft.NET.Sdk`) for Kestrel + gRPC
- OutputType `WinExe` for WinForms
- `UseWindowsForms` enabled for .NET 10 Windows Forms
- Kestrel listens on port 5001 with HTTP/2 only (required for gRPC)
- Migrations run automatically at startup
- WinForms runs on the main thread, Kestrel runs in background
