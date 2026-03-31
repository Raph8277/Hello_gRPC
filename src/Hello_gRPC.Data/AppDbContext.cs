using HelloGrpc.Data.Entities;

namespace HelloGrpc.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Personality> Personalities => Set<Personality>();
}