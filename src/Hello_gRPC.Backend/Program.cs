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
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await db.Database.EnsureCreatedAsync();
            await DatabaseSeeder.SeedAsync(db);
        }

        app.MapGrpcService<PersonalityService>();

        // Démarrer Kestrel en arrière-plan
        _ = app.RunAsync();

        // Démarrer WinForms
        Application.Run(new MainForm());
    }
}
