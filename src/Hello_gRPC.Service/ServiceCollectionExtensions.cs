using HelloGrpc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HelloGrpc.Service;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddHelloGrpcAppContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        return services;
    }

    public static IServiceCollection AddHelloGrpcPersonalityService(this IServiceCollection services)
    {
        services.AddScoped<PersonalityService>();
        return services;
    }
}
