---
name: seed-data
description: "Use when seeding the SQLite database with the initial 100 personalities. Use for generating, modifying, or extending seed data in the Data layer (Hello_gRPC.Data). Covers categories: Science, Art, Politique, Sport, Litterature, Musique, Cinema, Philosophie, Histoire, Technologie."
argument-hint: "Describe what seed data to generate or modify"
---

# Seed Data Skill

## When to Use
- Generating initial seed data for the 100 personalities
- Adding or modifying personality entries
- Changing categories or data structure

## Architecture

Seed data is managed in the **Data layer**:

```
Hello_gRPC.Data/
+-- DatabaseSeeder.cs                # Static SeedAsync method
+-- Entities/
|   +-- Personality.cs               # Entity definition
+-- AppDbContext.cs                   # DbContext with Personalities DbSet
```

**Namespace**: `HelloGrpc.Data`

## Data Structure

Each personality has:
- **FirstName** / **LastName**: Full name in original language
- **Bio**: Short biography (1-2 sentences, in French)
- **Category**: One of the 10 categories
- **Nationality**: Country of origin
- **BirthDate**: Format `yyyy-MM-dd`
- **DeathDate**: Format `yyyy-MM-dd` or null if still alive
- **ImageUrl**: Optional, can be null

## Categories (10 per category = 100 total)

| Category | Examples |
|----------|----------|
| Science | Einstein, Curie, Newton, Darwin, Pasteur... |
| Art | Picasso, Da Vinci, Van Gogh, Monet, Frida Kahlo... |
| Politique | De Gaulle, Mandela, Lincoln, Churchill, Merkel... |
| Sport | Pele, Ali, Bolt, Serena Williams, Zidane... |
| Litterature | Hugo, Shakespeare, Tolstoi, Austen, Garcia Marquez... |
| Musique | Mozart, Beethoven, Beatles, Piaf, Bob Marley... |
| Cinema | Chaplin, Hitchcock, Kubrick, Miyazaki, Meryl Streep... |
| Philosophie | Socrate, Descartes, Kant, Simone de Beauvoir, Confucius... |
| Histoire | Cesar, Cleopatre, Napoleon, Jeanne d'Arc, Alexandre le Grand... |
| Technologie | Turing, Jobs, Berners-Lee, Ada Lovelace, Elon Musk... |

## Procedure

### 1. Generate Seed Data
Reference the full dataset in [personalities.json](./assets/personalities.json).

### 2. Database Seeder Implementation (Data Layer)

```csharp
namespace HelloGrpc.Data;

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

    private static List<Personality> GetPersonalities()
    {
        // Load from embedded JSON or build inline
        return
        [
            // ... 100 personalities from the assets/personalities.json
        ];
    }
}
```

### 3. Validate
```bash
dotnet build src/Hello_gRPC.Data/
# Run the app --- it auto-seeds on first startup via Program.cs in Backend
```