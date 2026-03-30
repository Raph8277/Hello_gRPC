---
name: seed-data
description: "Use when seeding the SQLite database with the initial 100 personalities. Use for generating, modifying, or extending seed data. Covers categories: Science, Art, Politique, Sport, Littérature, Musique, Cinéma, Philosophie, Histoire, Technologie."
argument-hint: "Describe what seed data to generate or modify"
---

# Seed Data Skill

## When to Use
- Generating initial seed data for the 100 personalities
- Adding or modifying personality entries
- Changing categories or data structure

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
| Sport | Pelé, Ali, Bolt, Serena Williams, Zidane... |
| Littérature | Hugo, Shakespeare, Tolstoï, Austen, García Márquez... |
| Musique | Mozart, Beethoven, Beatles, Piaf, Bob Marley... |
| Cinéma | Chaplin, Hitchcock, Kubrick, Miyazaki, Meryl Streep... |
| Philosophie | Socrate, Descartes, Kant, Simone de Beauvoir, Confucius... |
| Histoire | César, Cléopâtre, Napoléon, Jeanne d'Arc, Alexandre le Grand... |
| Technologie | Turing, Jobs, Berners-Lee, Ada Lovelace, Elon Musk... |

## Procedure

### 1. Generate Seed Data
Reference the full dataset in [personalities.json](./assets/personalities.json).

### 2. Database Seeder Implementation

```csharp
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
dotnet build src/Hello_gRPC.Backend/
# Run the app — it auto-seeds on first startup
```
