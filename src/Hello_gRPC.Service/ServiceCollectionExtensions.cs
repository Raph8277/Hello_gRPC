using HelloGrpc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HelloGrpc.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHelloGrpcServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<PersonalityService>();
        return services;
    }
}
