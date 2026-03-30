# EF Core Patterns for Hello_gRPC

## Entity Pattern

```csharp
namespace HelloGrpc.Backend.Entities;

/// <summary>
/// Représente une personnalité célèbre.
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
namespace HelloGrpc.Backend.Data;

/// <summary>
/// Contexte de base de données pour l'application Hello_gRPC.
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
/// <summary>
/// Initialise la base de données avec les données de seed.
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

## Conventions

- Use primary constructors for DI
- Use `required` keyword for non-nullable string properties
- Use `DateOnly` for dates, not `DateTime`
- SQLite connection string: `Data Source=hello_grpc.db`
- Always call `Database.MigrateAsync()` at startup
