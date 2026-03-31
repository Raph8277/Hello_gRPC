using HelloGrpc.Data;
using HelloGrpc.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using System.Reflection;
namespace HelloGrpc.Backend;

/// <summary>
/// Point d'entrée de l'application backend WinForms hébergeant le serveur gRPC.
/// </summary>
static class Program
{
    // Liste des types de services gRPC exposés
    public static readonly Type[] GrpcServiceTypes =
    {
        typeof(HelloGrpc.Backend.Services.PersonalityService)
        // Ajouter ici d'autres services si besoin
    };
    [STAThread]
    static async Task Main()
    {
        ApplicationConfiguration.Initialize();

        var builder = WebApplication.CreateBuilder();

        // Configure Kestrel pour gRPC
        builder.WebHost.ConfigureKestrel(options =>
        {
            // Port gRPC (HTTP/2)
            options.ListenLocalhost(5001, listenOptions =>
            {
                listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
            });
            // Port REST (HTTP/1.1)
            options.ListenLocalhost(5002, listenOptions =>
            {
                listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
            });
        });


        // Ajout des services HelloGrpc (DbContext, PersonalityService, etc.)
        builder.Services.AddGrpc();
        builder.Services.AddHelloGrpcAppContext("Data Source=../Hello_gRPC.Data/hello_grpc.db");
        builder.Services.AddHelloGrpcPersonalityService();

        var app = builder.Build();

     

        // Appliquer les migrations et seeder
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<HelloGrpc.Data.AppDbContext>();
            await db.Database.EnsureCreatedAsync();
            await HelloGrpc.Data.DatabaseSeeder.SeedAsync(db);
        }


        // Enregistre tous les services gRPC déclarés dans GrpcServiceTypes
        foreach (var svcType in GrpcServiceTypes)
        {
            // Utilise la version générique de MapGrpcService<T>() via reflection
            var method = typeof(GrpcEndpointRouteBuilderExtensions)
                .GetMethods()
                .FirstOrDefault(m => m.Name == "MapGrpcService" && m.IsGenericMethod);
            if (method != null)
            {
                var generic = method.MakeGenericMethod(svcType);
                generic.Invoke(null, new object[] { app });
            }
        }

        // Endpoint HTTP pour exposer dynamiquement la liste des services gRPC
        app.MapGet("/grpc-services", () =>
            Results.Json(GrpcServiceTypes.Select(t => new {
                Name = t.Name.Replace("Service", ""),
                FullName = t.FullName
            }).ToList())
        );

        // Démarrer Kestrel en arrière-plan
        _ = app.RunAsync();

        // Démarrer WinForms
        Application.Run(new MainForm());
    }
}
