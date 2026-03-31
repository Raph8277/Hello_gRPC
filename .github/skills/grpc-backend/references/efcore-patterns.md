# EF Core Patterns for Hello_gRPC

All EF Core code lives in the **Data layer** (`Hello_gRPC.Data`).

## Entity Pattern

```csharp
namespace HelloGrpc.Data.Entities;

/// <summary>
/// Represente une personnalite celebre.
/// </summary>
public class Personality
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Bio { get; set; }
    public required string Category { get; set; }
    public required string Nationality { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
```

## DbContext Pattern

```csharp
namespace HelloGrpc.Data;

/// <summary>
/// Contexte de base de donnees pour l'application Hello_gRPC.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Personality> Personalities => Set<Personality>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Personality>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Bio).HasMaxLength(2000).IsRequired();
            entity.Property(e => e.Category).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Nationality).HasMaxLength(100).IsRequired();
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.HasIndex(e => new { e.LastName, e.FirstName });
            entity.HasIndex(e => e.Category);
        });
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries<Personality>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = DateTime.UtcNow;
        }
    }
}
```

## Seed Data Pattern

```csharp
namespace HelloGrpc.Data;

/// <summary>
/// Initialise la base de donnees avec les donnees de seed.
/// </summary>
public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Personalities.AnyAsync())
            return;

        var personalities = GetPersonalities();
        context.Personalities.AddRange(personalities);
        await context.SaveChangesAsync();
    }
}
```

## Service Layer Pattern

```csharp
namespace HelloGrpc.Service;

/// <summary>
/// Service metier pour la gestion des personnalites.
/// </summary>
public class PersonalityService(AppDbContext dbContext)
{
    public async Task<List<Personality>> GetAllAsync() => await dbContext.Personalities.ToListAsync();
    public async Task<Personality?> GetByIdAsync(int id) => await dbContext.Personalities.FindAsync(id);
    public async Task<Personality> AddAsync(Personality personality)
    {
        dbContext.Personalities.Add(personality);
        await dbContext.SaveChangesAsync();
        return personality;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await dbContext.Personalities.FindAsync(id);
        if (entity is null) return false;
        dbContext.Personalities.Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<Personality> UpdateAsync(Personality personality)
    {
        dbContext.Personalities.Update(personality);
        await dbContext.SaveChangesAsync();
        return personality;
    }
}
```

## DI Extensions Pattern

```csharp
namespace HelloGrpc.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHelloGrpcAppContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        return services;
    }

    public static IServiceCollection AddHelloGrpc{Entity}Service(this IServiceCollection services)
    {
        services.AddScoped<{Entity}Service>();
        return services;
    }
}
```

## Migration Commands

```bash
# Generate migration (Backend is the startup project for SQLite path resolution)
dotnet ef migrations add <Name> --project src/Hello_gRPC.Data/ --startup-project src/Hello_gRPC.Backend/

# Apply migrations (done automatically in Program.cs)
dotnet ef database update --project src/Hello_gRPC.Data/ --startup-project src/Hello_gRPC.Backend/
```