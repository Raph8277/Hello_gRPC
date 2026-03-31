using HelloGrpc.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloGrpc.Data;

/// <summary>
/// Classe responsable de l'initialisation de la base de données avec les données de seed.
/// </summary>
public static class DatabaseSeeder
{
    /// <summary>
    /// Insère les personnalités initiales si la base de données est vide.
    /// </summary>
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
        return new List<Personality>
        {
            new Personality {
                FirstName = "Albert",
                LastName = "Einstein",
                Bio = "Physicien théoricien, père de la relativité.",
                Category = "Physicien",
                Nationality = "Allemand",
                BirthDate = new DateOnly(1879, 3, 14),
                DeathDate = new DateOnly(1955, 4, 18),
                ImageUrl = null
            },
            new Personality {
                FirstName = "Marie",
                LastName = "Curie",
                Bio = "Physicienne et chimiste, pionnière de la radioactivité.",
                Category = "Physicienne, Chimiste",
                Nationality = "Française, Polonaise",
                BirthDate = new DateOnly(1867, 11, 7),
                DeathDate = new DateOnly(1934, 7, 4),
                ImageUrl = null
            },
            new Personality {
                FirstName = "Isaac",
                LastName = "Newton",
                Bio = "Mathématicien et physicien, lois de la gravitation.",
                Category = "Mathématicien, Physicien",
                Nationality = "Anglais",
                BirthDate = new DateOnly(1643, 1, 4),
                DeathDate = new DateOnly(1727, 3, 31),
                ImageUrl = null
            },
            new Personality {
                FirstName = "Charles",
                LastName = "Darwin",
                Bio = "Naturaliste, théorie de l'évolution.",
                Category = "Naturaliste",
                Nationality = "Anglais",
                BirthDate = new DateOnly(1809, 2, 12),
                DeathDate = new DateOnly(1882, 4, 19),
                ImageUrl = null
            },
            new Personality {
                FirstName = "Nikola",
                LastName = "Tesla",
                Bio = "Inventeur, pionnier du courant alternatif.",
                Category = "Inventeur, Ingénieur",
                Nationality = "Serbe, Américain",
                BirthDate = new DateOnly(1856, 7, 10),
                DeathDate = new DateOnly(1943, 1, 7),
                ImageUrl = null
            },
            new Personality {
                FirstName = "Galilée",
                LastName = "Galilei",
                Bio = "Astronome et physicien, père de la physique moderne.",
                Category = "Astronome, Physicien",
                Nationality = "Italien",
                BirthDate = new DateOnly(1564, 2, 15),
                DeathDate = new DateOnly(1642, 1, 8),
                ImageUrl = null
            },
            new Personality {
                FirstName = "Stephen",
                LastName = "Hawking",
                Bio = "Physicien théoricien, travaux sur les trous noirs.",
                Category = "Physicien",
                Nationality = "Britannique",
                BirthDate = new DateOnly(1942, 1, 8),
                DeathDate = new DateOnly(2018, 3, 14),
                ImageUrl = null
            },
            new Personality {
                FirstName = "Rosalind",
                LastName = "Franklin",
                Bio = "Chimiste, travaux sur la structure de l'ADN.",
                Category = "Chimiste",
                Nationality = "Britannique",
                BirthDate = new DateOnly(1920, 7, 25),
                DeathDate = new DateOnly(1958, 4, 16),
                ImageUrl = null
            },
            new Personality {
                FirstName = "Ada",
                LastName = "Lovelace",
                Bio = "Mathématicienne, première programmeuse.",
                Category = "Mathématicienne",
                Nationality = "Britannique",
                BirthDate = new DateOnly(1815, 12, 10),
                DeathDate = new DateOnly(1852, 11, 27),
                ImageUrl = null
            },
            new Personality {
                FirstName = "Alan",
                LastName = "Turing",
                Bio = "Mathématicien, père de l'informatique.",
                Category = "Mathématicien, Informaticien",
                Nationality = "Britannique",
                BirthDate = new DateOnly(1912, 6, 23),
                DeathDate = new DateOnly(1954, 6, 7),
                ImageUrl = null
            },
        }
        // Génération automatique de personnalités fictives pour atteindre ~100 entrées
        .Concat(Enumerable.Range(1, 90).Select(i => new Personality {
            FirstName = $"Prénom{i}",
            LastName = $"Nom{i}",
            Bio = $"Biographie fictive de la personnalité {i}.",
            Category = i % 2 == 0 ? "Scientifique" : "Artiste",
            Nationality = i % 3 == 0 ? "Française" : "Internationale",
            BirthDate = new DateOnly(1900 + (i % 100), (i % 12) + 1, (i % 28) + 1),
            DeathDate = i % 4 == 0 ? new DateOnly(2000 + (i % 20), ((i + 6) % 12) + 1, ((i + 10) % 28) + 1) : null,
            ImageUrl = null
        })).ToList();
    }
}
