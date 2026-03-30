namespace HelloGrpc.Backend.Data;

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
        return
        [
            // ==================== Science ====================
            new Personality
            {
                FirstName = "Albert",
                LastName = "Einstein",
                Bio = "Physicien théoricien d'origine allemande, père de la théorie de la relativité et lauréat du prix Nobel de physique 1921.",
                Category = "Science",
                Nationality = "Allemagne",
                BirthDate = DateOnly.Parse("1879-03-14"),
                DeathDate = DateOnly.Parse("1955-04-18"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Marie",
                LastName = "Curie",
                Bio = "Physicienne et chimiste franco-polonaise, pionnière de la radioactivité et double lauréate du prix Nobel.",
                Category = "Science",
                Nationality = "Pologne",
                BirthDate = DateOnly.Parse("1867-11-07"),
                DeathDate = DateOnly.Parse("1934-07-04"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Isaac",
                LastName = "Newton",
                Bio = "Mathématicien et physicien anglais, auteur des lois de la mécanique classique et de la gravitation universelle.",
                Category = "Science",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1643-01-04"),
                DeathDate = DateOnly.Parse("1727-03-31"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Charles",
                LastName = "Darwin",
                Bio = "Naturaliste anglais, auteur de la théorie de l'évolution par sélection naturelle.",
                Category = "Science",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1809-02-12"),
                DeathDate = DateOnly.Parse("1882-04-19"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Louis",
                LastName = "Pasteur",
                Bio = "Chimiste et microbiologiste français, pionnier de la vaccination et de la pasteurisation.",
                Category = "Science",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1822-12-27"),
                DeathDate = DateOnly.Parse("1895-09-28"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Nikola",
                LastName = "Tesla",
                Bio = "Inventeur et ingénieur serbo-américain, pionnier du courant alternatif et de l'électromagnétisme.",
                Category = "Science",
                Nationality = "Serbie",
                BirthDate = DateOnly.Parse("1856-07-10"),
                DeathDate = DateOnly.Parse("1943-01-07"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Galilée",
                LastName = "Galilei",
                Bio = "Astronome et physicien italien, père de l'astronomie observationnelle moderne et de la physique moderne.",
                Category = "Science",
                Nationality = "Italie",
                BirthDate = DateOnly.Parse("1564-02-15"),
                DeathDate = DateOnly.Parse("1642-01-08"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Stephen",
                LastName = "Hawking",
                Bio = "Physicien théoricien britannique, connu pour ses travaux sur les trous noirs et la cosmologie.",
                Category = "Science",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1942-01-08"),
                DeathDate = DateOnly.Parse("2018-03-14"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Rosalind",
                LastName = "Franklin",
                Bio = "Chimiste et cristallographe britannique, dont les travaux ont été essentiels à la découverte de la structure de l'ADN.",
                Category = "Science",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1920-07-25"),
                DeathDate = DateOnly.Parse("1958-04-16"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Dmitri",
                LastName = "Mendeleïev",
                Bio = "Chimiste russe, inventeur du tableau périodique des éléments chimiques.",
                Category = "Science",
                Nationality = "Russie",
                BirthDate = DateOnly.Parse("1834-02-08"),
                DeathDate = DateOnly.Parse("1907-02-02"),
                ImageUrl = null
            },

            // ==================== Art ====================
            new Personality
            {
                FirstName = "Pablo",
                LastName = "Picasso",
                Bio = "Peintre et sculpteur espagnol, cofondateur du cubisme et l'un des artistes les plus influents du XXe siècle.",
                Category = "Art",
                Nationality = "Espagne",
                BirthDate = DateOnly.Parse("1881-10-25"),
                DeathDate = DateOnly.Parse("1973-04-08"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Léonard",
                LastName = "de Vinci",
                Bio = "Artiste, scientifique et inventeur italien de la Renaissance, auteur de La Joconde et de L'Homme de Vitruve.",
                Category = "Art",
                Nationality = "Italie",
                BirthDate = DateOnly.Parse("1452-04-15"),
                DeathDate = DateOnly.Parse("1519-05-02"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Vincent",
                LastName = "Van Gogh",
                Bio = "Peintre postimpressionniste néerlandais, auteur de La Nuit étoilée et des Tournesols.",
                Category = "Art",
                Nationality = "Pays-Bas",
                BirthDate = DateOnly.Parse("1853-03-30"),
                DeathDate = DateOnly.Parse("1890-07-29"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Claude",
                LastName = "Monet",
                Bio = "Peintre français, fondateur de l'impressionnisme et auteur des Nymphéas.",
                Category = "Art",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1840-11-14"),
                DeathDate = DateOnly.Parse("1926-12-05"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Frida",
                LastName = "Kahlo",
                Bio = "Peintre mexicaine, connue pour ses autoportraits et son art inspiré de la culture mexicaine.",
                Category = "Art",
                Nationality = "Mexique",
                BirthDate = DateOnly.Parse("1907-07-06"),
                DeathDate = DateOnly.Parse("1954-07-13"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Michel-Ange",
                LastName = "Buonarroti",
                Bio = "Sculpteur, peintre et architecte italien de la Renaissance, auteur du David et de la chapelle Sixtine.",
                Category = "Art",
                Nationality = "Italie",
                BirthDate = DateOnly.Parse("1475-03-06"),
                DeathDate = DateOnly.Parse("1564-02-18"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Salvador",
                LastName = "Dalí",
                Bio = "Peintre surréaliste espagnol, célèbre pour ses œuvres oniriques comme La Persistance de la mémoire.",
                Category = "Art",
                Nationality = "Espagne",
                BirthDate = DateOnly.Parse("1904-05-11"),
                DeathDate = DateOnly.Parse("1989-01-23"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Rembrandt",
                LastName = "van Rijn",
                Bio = "Peintre et graveur néerlandais du siècle d'or, maître du clair-obscur et de l'autoportrait.",
                Category = "Art",
                Nationality = "Pays-Bas",
                BirthDate = DateOnly.Parse("1606-07-15"),
                DeathDate = DateOnly.Parse("1669-10-04"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Auguste",
                LastName = "Rodin",
                Bio = "Sculpteur français, considéré comme le père de la sculpture moderne, auteur du Penseur.",
                Category = "Art",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1840-11-12"),
                DeathDate = DateOnly.Parse("1917-11-17"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Andy",
                LastName = "Warhol",
                Bio = "Artiste américain, figure de proue du pop art et auteur des sérigraphies de Marilyn Monroe.",
                Category = "Art",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1928-08-06"),
                DeathDate = DateOnly.Parse("1987-02-22"),
                ImageUrl = null
            },

            // ==================== Politique ====================
            new Personality
            {
                FirstName = "Charles",
                LastName = "de Gaulle",
                Bio = "Général et homme d'État français, chef de la France libre et fondateur de la Ve République.",
                Category = "Politique",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1890-11-22"),
                DeathDate = DateOnly.Parse("1970-11-09"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Nelson",
                LastName = "Mandela",
                Bio = "Militant anti-apartheid et premier président noir d'Afrique du Sud, symbole de la réconciliation.",
                Category = "Politique",
                Nationality = "Afrique du Sud",
                BirthDate = DateOnly.Parse("1918-07-18"),
                DeathDate = DateOnly.Parse("2013-12-05"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Abraham",
                LastName = "Lincoln",
                Bio = "16e président des États-Unis, artisan de l'abolition de l'esclavage et de la préservation de l'Union.",
                Category = "Politique",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1809-02-12"),
                DeathDate = DateOnly.Parse("1865-04-15"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Winston",
                LastName = "Churchill",
                Bio = "Premier ministre britannique durant la Seconde Guerre mondiale, orateur et stratège légendaire.",
                Category = "Politique",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1874-11-30"),
                DeathDate = DateOnly.Parse("1965-01-24"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Angela",
                LastName = "Merkel",
                Bio = "Chancelière fédérale d'Allemagne de 2005 à 2021, figure majeure de la politique européenne.",
                Category = "Politique",
                Nationality = "Allemagne",
                BirthDate = DateOnly.Parse("1954-07-17"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Mahatma",
                LastName = "Gandhi",
                Bio = "Leader du mouvement d'indépendance indien, apôtre de la non-violence et de la désobéissance civile.",
                Category = "Politique",
                Nationality = "Inde",
                BirthDate = DateOnly.Parse("1869-10-02"),
                DeathDate = DateOnly.Parse("1948-01-30"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Martin Luther",
                LastName = "King Jr.",
                Bio = "Pasteur et militant américain pour les droits civiques, célèbre pour son discours I Have a Dream.",
                Category = "Politique",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1929-01-15"),
                DeathDate = DateOnly.Parse("1968-04-04"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Simón",
                LastName = "Bolívar",
                Bio = "Homme politique et militaire vénézuélien, libérateur de plusieurs nations d'Amérique du Sud.",
                Category = "Politique",
                Nationality = "Venezuela",
                BirthDate = DateOnly.Parse("1783-07-24"),
                DeathDate = DateOnly.Parse("1830-12-17"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Margaret",
                LastName = "Thatcher",
                Bio = "Première ministre du Royaume-Uni de 1979 à 1990, surnommée la Dame de fer.",
                Category = "Politique",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1925-10-13"),
                DeathDate = DateOnly.Parse("2013-04-08"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Thomas",
                LastName = "Jefferson",
                Bio = "3e président des États-Unis et principal rédacteur de la Déclaration d'indépendance.",
                Category = "Politique",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1743-04-13"),
                DeathDate = DateOnly.Parse("1826-07-04"),
                ImageUrl = null
            },

            // ==================== Sport ====================
            new Personality
            {
                FirstName = "Pelé",
                LastName = "(Edson Arantes)",
                Bio = "Footballeur brésilien, considéré comme l'un des plus grands joueurs de tous les temps avec trois Coupes du monde.",
                Category = "Sport",
                Nationality = "Brésil",
                BirthDate = DateOnly.Parse("1940-10-23"),
                DeathDate = DateOnly.Parse("2022-12-29"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Muhammad",
                LastName = "Ali",
                Bio = "Boxeur américain, triple champion du monde des poids lourds et icône de la lutte pour les droits civiques.",
                Category = "Sport",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1942-01-17"),
                DeathDate = DateOnly.Parse("2016-06-03"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Usain",
                LastName = "Bolt",
                Bio = "Sprinter jamaïcain, détenteur des records du monde du 100m et 200m, huit fois champion olympique.",
                Category = "Sport",
                Nationality = "Jamaïque",
                BirthDate = DateOnly.Parse("1986-08-21"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Serena",
                LastName = "Williams",
                Bio = "Joueuse de tennis américaine, 23 titres du Grand Chelem en simple, l'une des plus grandes sportives de l'histoire.",
                Category = "Sport",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1981-09-26"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Zinédine",
                LastName = "Zidane",
                Bio = "Footballeur français, Ballon d'or et champion du monde 1998, devenu entraîneur du Real Madrid.",
                Category = "Sport",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1972-06-23"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Michael",
                LastName = "Jordan",
                Bio = "Basketteur américain, six fois champion NBA et considéré comme le plus grand joueur de basketball.",
                Category = "Sport",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1963-02-17"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Nadia",
                LastName = "Comăneci",
                Bio = "Gymnaste roumaine, première à obtenir la note parfaite de 10 aux Jeux olympiques de 1976.",
                Category = "Sport",
                Nationality = "Roumanie",
                BirthDate = DateOnly.Parse("1961-11-12"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Roger",
                LastName = "Federer",
                Bio = "Joueur de tennis suisse, 20 titres du Grand Chelem, considéré comme l'un des plus élégants du circuit.",
                Category = "Sport",
                Nationality = "Suisse",
                BirthDate = DateOnly.Parse("1981-08-08"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Jesse",
                LastName = "Owens",
                Bio = "Athlète américain, quadruple champion olympique aux JO de Berlin 1936, symbole de la lutte contre le racisme.",
                Category = "Sport",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1913-09-12"),
                DeathDate = DateOnly.Parse("1980-03-31"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Marie-José",
                LastName = "Pérec",
                Bio = "Athlète française, triple championne olympique du 200m et 400m, légende du sprint français.",
                Category = "Sport",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1968-05-09"),
                DeathDate = null,
                ImageUrl = null
            },

            // ==================== Littérature ====================
            new Personality
            {
                FirstName = "Victor",
                LastName = "Hugo",
                Bio = "Écrivain français, auteur des Misérables et de Notre-Dame de Paris, figure majeure du romantisme.",
                Category = "Littérature",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1802-02-26"),
                DeathDate = DateOnly.Parse("1885-05-22"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "William",
                LastName = "Shakespeare",
                Bio = "Dramaturge et poète anglais, auteur de Hamlet, Roméo et Juliette et Macbeth.",
                Category = "Littérature",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1564-04-23"),
                DeathDate = DateOnly.Parse("1616-04-23"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Léon",
                LastName = "Tolstoï",
                Bio = "Romancier russe, auteur de Guerre et Paix et Anna Karénine, maître du réalisme littéraire.",
                Category = "Littérature",
                Nationality = "Russie",
                BirthDate = DateOnly.Parse("1828-09-09"),
                DeathDate = DateOnly.Parse("1910-11-20"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Jane",
                LastName = "Austen",
                Bio = "Romancière anglaise, auteure d'Orgueil et Préjugés et de Raison et Sentiments.",
                Category = "Littérature",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1775-12-16"),
                DeathDate = DateOnly.Parse("1817-07-18"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Gabriel",
                LastName = "García Márquez",
                Bio = "Écrivain colombien, auteur de Cent ans de solitude, prix Nobel de littérature 1982.",
                Category = "Littérature",
                Nationality = "Colombie",
                BirthDate = DateOnly.Parse("1927-03-06"),
                DeathDate = DateOnly.Parse("2014-04-17"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Marcel",
                LastName = "Proust",
                Bio = "Écrivain français, auteur d'À la recherche du temps perdu, chef-d'œuvre de la littérature mondiale.",
                Category = "Littérature",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1871-07-10"),
                DeathDate = DateOnly.Parse("1922-11-18"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Fiodor",
                LastName = "Dostoïevski",
                Bio = "Romancier russe, auteur de Crime et Châtiment et des Frères Karamazov, maître du roman psychologique.",
                Category = "Littérature",
                Nationality = "Russie",
                BirthDate = DateOnly.Parse("1821-11-11"),
                DeathDate = DateOnly.Parse("1881-02-09"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Toni",
                LastName = "Morrison",
                Bio = "Romancière américaine, auteure de Beloved, première femme noire à recevoir le prix Nobel de littérature.",
                Category = "Littérature",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1931-02-18"),
                DeathDate = DateOnly.Parse("2019-08-05"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Franz",
                LastName = "Kafka",
                Bio = "Écrivain praguois de langue allemande, auteur de La Métamorphose et Le Procès, figure de l'absurde.",
                Category = "Littérature",
                Nationality = "Autriche-Hongrie",
                BirthDate = DateOnly.Parse("1883-07-03"),
                DeathDate = DateOnly.Parse("1924-06-03"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Simone",
                LastName = "de Beauvoir",
                Bio = "Écrivaine et philosophe française, auteure du Deuxième Sexe, figure majeure du féminisme.",
                Category = "Littérature",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1908-01-09"),
                DeathDate = DateOnly.Parse("1986-04-14"),
                ImageUrl = null
            },

            // ==================== Musique ====================
            new Personality
            {
                FirstName = "Wolfgang Amadeus",
                LastName = "Mozart",
                Bio = "Compositeur autrichien, prodige musical et auteur de La Flûte enchantée et du Requiem.",
                Category = "Musique",
                Nationality = "Autriche",
                BirthDate = DateOnly.Parse("1756-01-27"),
                DeathDate = DateOnly.Parse("1791-12-05"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Ludwig van",
                LastName = "Beethoven",
                Bio = "Compositeur et pianiste allemand, auteur de la 9e symphonie, ayant composé malgré sa surdité.",
                Category = "Musique",
                Nationality = "Allemagne",
                BirthDate = DateOnly.Parse("1770-12-17"),
                DeathDate = DateOnly.Parse("1827-03-26"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "John",
                LastName = "Lennon",
                Bio = "Musicien et compositeur britannique, cofondateur des Beatles et militant pour la paix.",
                Category = "Musique",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1940-10-09"),
                DeathDate = DateOnly.Parse("1980-12-08"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Édith",
                LastName = "Piaf",
                Bio = "Chanteuse française, interprète de La Vie en rose et de Non, je ne regrette rien, icône de la chanson française.",
                Category = "Musique",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1915-12-19"),
                DeathDate = DateOnly.Parse("1963-10-10"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Bob",
                LastName = "Marley",
                Bio = "Chanteur et musicien jamaïcain, légende du reggae et ambassadeur du mouvement rastafari.",
                Category = "Musique",
                Nationality = "Jamaïque",
                BirthDate = DateOnly.Parse("1945-02-06"),
                DeathDate = DateOnly.Parse("1981-05-11"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Johann Sebastian",
                LastName = "Bach",
                Bio = "Compositeur et musicien allemand de l'époque baroque, auteur de cantates, fugues et concertos.",
                Category = "Musique",
                Nationality = "Allemagne",
                BirthDate = DateOnly.Parse("1685-03-31"),
                DeathDate = DateOnly.Parse("1750-07-28"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Frédéric",
                LastName = "Chopin",
                Bio = "Compositeur et pianiste polonais, virtuose du piano romantique et auteur de nocturnes et polonaises.",
                Category = "Musique",
                Nationality = "Pologne",
                BirthDate = DateOnly.Parse("1810-03-01"),
                DeathDate = DateOnly.Parse("1849-10-17"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Ella",
                LastName = "Fitzgerald",
                Bio = "Chanteuse de jazz américaine, surnommée la First Lady of Song, connue pour son scat virtuose.",
                Category = "Musique",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1917-04-25"),
                DeathDate = DateOnly.Parse("1996-06-15"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Giuseppe",
                LastName = "Verdi",
                Bio = "Compositeur italien, maître de l'opéra romantique, auteur de La Traviata et Aida.",
                Category = "Musique",
                Nationality = "Italie",
                BirthDate = DateOnly.Parse("1813-10-10"),
                DeathDate = DateOnly.Parse("1901-01-27"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Aretha",
                LastName = "Franklin",
                Bio = "Chanteuse américaine, reine de la soul et icône des droits civiques, interprète de Respect.",
                Category = "Musique",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1942-03-25"),
                DeathDate = DateOnly.Parse("2018-08-16"),
                ImageUrl = null
            },

            // ==================== Cinéma ====================
            new Personality
            {
                FirstName = "Charlie",
                LastName = "Chaplin",
                Bio = "Acteur et réalisateur britannique, créateur du personnage de Charlot, pionnier du cinéma muet.",
                Category = "Cinéma",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1889-04-16"),
                DeathDate = DateOnly.Parse("1977-12-25"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Alfred",
                LastName = "Hitchcock",
                Bio = "Réalisateur britannique, maître du suspense, auteur de Psychose, Vertigo et Les Oiseaux.",
                Category = "Cinéma",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1899-08-13"),
                DeathDate = DateOnly.Parse("1980-04-29"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Stanley",
                LastName = "Kubrick",
                Bio = "Réalisateur américain, perfectionniste visionnaire, auteur de 2001, Orange mécanique et Shining.",
                Category = "Cinéma",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1928-07-26"),
                DeathDate = DateOnly.Parse("1999-03-07"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Hayao",
                LastName = "Miyazaki",
                Bio = "Réalisateur et animateur japonais, cofondateur du Studio Ghibli, auteur du Voyage de Chihiro.",
                Category = "Cinéma",
                Nationality = "Japon",
                BirthDate = DateOnly.Parse("1941-01-05"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Meryl",
                LastName = "Streep",
                Bio = "Actrice américaine, recordwoman des nominations aux Oscars, considérée comme la plus grande actrice de sa génération.",
                Category = "Cinéma",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1949-06-22"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Akira",
                LastName = "Kurosawa",
                Bio = "Réalisateur japonais, maître du cinéma mondial, auteur des Sept Samouraïs et Rashōmon.",
                Category = "Cinéma",
                Nationality = "Japon",
                BirthDate = DateOnly.Parse("1910-03-23"),
                DeathDate = DateOnly.Parse("1998-09-06"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Federico",
                LastName = "Fellini",
                Bio = "Réalisateur italien, figure du néoréalisme et du cinéma onirique, auteur de La Dolce Vita et 8½.",
                Category = "Cinéma",
                Nationality = "Italie",
                BirthDate = DateOnly.Parse("1920-01-20"),
                DeathDate = DateOnly.Parse("1993-10-31"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Agnès",
                LastName = "Varda",
                Bio = "Réalisatrice française, pionnière de la Nouvelle Vague et documentariste engagée.",
                Category = "Cinéma",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1928-05-30"),
                DeathDate = DateOnly.Parse("2019-03-29"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Steven",
                LastName = "Spielberg",
                Bio = "Réalisateur et producteur américain, auteur de La Liste de Schindler, E.T. et Jurassic Park.",
                Category = "Cinéma",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1946-12-18"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Ingmar",
                LastName = "Bergman",
                Bio = "Réalisateur et scénariste suédois, maître du cinéma existentiel, auteur du Septième Sceau.",
                Category = "Cinéma",
                Nationality = "Suède",
                BirthDate = DateOnly.Parse("1918-07-14"),
                DeathDate = DateOnly.Parse("2007-07-30"),
                ImageUrl = null
            },

            // ==================== Philosophie ====================
            new Personality
            {
                FirstName = "Socrate",
                LastName = "d'Athènes",
                Bio = "Philosophe grec, fondateur de la philosophie occidentale, maître de Platon et inventeur de la maïeutique.",
                Category = "Philosophie",
                Nationality = "Grèce",
                BirthDate = DateOnly.Parse("0470-01-01"),
                DeathDate = DateOnly.Parse("0399-01-01"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "René",
                LastName = "Descartes",
                Bio = "Philosophe et mathématicien français, père du rationalisme moderne et auteur du Discours de la méthode.",
                Category = "Philosophie",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1596-03-31"),
                DeathDate = DateOnly.Parse("1650-02-11"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Emmanuel",
                LastName = "Kant",
                Bio = "Philosophe allemand, auteur de la Critique de la raison pure, figure centrale de la philosophie moderne.",
                Category = "Philosophie",
                Nationality = "Allemagne",
                BirthDate = DateOnly.Parse("1724-04-22"),
                DeathDate = DateOnly.Parse("1804-02-12"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Simone",
                LastName = "Weil",
                Bio = "Philosophe et mystique française, engagée pour la justice sociale et auteure de La Pesanteur et la Grâce.",
                Category = "Philosophie",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1909-02-03"),
                DeathDate = DateOnly.Parse("1943-08-24"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Confucius",
                LastName = "(Kong Qiu)",
                Bio = "Philosophe chinois, fondateur du confucianisme et penseur de l'éthique, de la morale et du gouvernement.",
                Category = "Philosophie",
                Nationality = "Chine",
                BirthDate = DateOnly.Parse("0551-09-28"),
                DeathDate = DateOnly.Parse("0479-04-11"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Friedrich",
                LastName = "Nietzsche",
                Bio = "Philosophe allemand, auteur d'Ainsi parlait Zarathoustra et penseur de la volonté de puissance.",
                Category = "Philosophie",
                Nationality = "Allemagne",
                BirthDate = DateOnly.Parse("1844-10-15"),
                DeathDate = DateOnly.Parse("1900-08-25"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Platon",
                LastName = "d'Athènes",
                Bio = "Philosophe grec, fondateur de l'Académie d'Athènes et auteur de La République et du Banquet.",
                Category = "Philosophie",
                Nationality = "Grèce",
                BirthDate = DateOnly.Parse("0428-01-01"),
                DeathDate = DateOnly.Parse("0348-01-01"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Jean-Paul",
                LastName = "Sartre",
                Bio = "Philosophe et écrivain français, fondateur de l'existentialisme, auteur de L'Être et le Néant.",
                Category = "Philosophie",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1905-06-21"),
                DeathDate = DateOnly.Parse("1980-04-15"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Hannah",
                LastName = "Arendt",
                Bio = "Philosophe et politologue allemande naturalisée américaine, auteure de La Banalité du mal.",
                Category = "Philosophie",
                Nationality = "Allemagne",
                BirthDate = DateOnly.Parse("1906-10-14"),
                DeathDate = DateOnly.Parse("1975-12-04"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Aristote",
                LastName = "de Stagire",
                Bio = "Philosophe grec, disciple de Platon, fondateur du Lycée et penseur universel couvrant logique, physique et éthique.",
                Category = "Philosophie",
                Nationality = "Grèce",
                BirthDate = DateOnly.Parse("0384-01-01"),
                DeathDate = DateOnly.Parse("0322-01-01"),
                ImageUrl = null
            },

            // ==================== Histoire ====================
            new Personality
            {
                FirstName = "Jules",
                LastName = "César",
                Bio = "Général et homme d'État romain, conquérant de la Gaule et dictateur à vie de la République romaine.",
                Category = "Histoire",
                Nationality = "Rome",
                BirthDate = DateOnly.Parse("0100-07-12"),
                DeathDate = DateOnly.Parse("0044-03-15"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Cléopâtre",
                LastName = "VII",
                Bio = "Dernière reine d'Égypte de la dynastie ptolémaïque, figure emblématique de l'Antiquité.",
                Category = "Histoire",
                Nationality = "Égypte",
                BirthDate = DateOnly.Parse("0069-01-01"),
                DeathDate = DateOnly.Parse("0030-08-12"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Napoléon",
                LastName = "Bonaparte",
                Bio = "Empereur des Français, stratège militaire de génie et réformateur du droit avec le Code civil.",
                Category = "Histoire",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1769-08-15"),
                DeathDate = DateOnly.Parse("1821-05-05"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Jeanne",
                LastName = "d'Arc",
                Bio = "Héroïne de la guerre de Cent Ans, cheffe de guerre française brûlée vive puis canonisée.",
                Category = "Histoire",
                Nationality = "France",
                BirthDate = DateOnly.Parse("1412-01-06"),
                DeathDate = DateOnly.Parse("1431-05-30"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Alexandre",
                LastName = "le Grand",
                Bio = "Roi de Macédoine et conquérant d'un empire s'étendant de la Grèce à l'Inde.",
                Category = "Histoire",
                Nationality = "Macédoine",
                BirthDate = DateOnly.Parse("0356-07-20"),
                DeathDate = DateOnly.Parse("0323-06-10"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Gengis",
                LastName = "Khan",
                Bio = "Fondateur et premier empereur de l'Empire mongol, le plus vaste empire terrestre de l'histoire.",
                Category = "Histoire",
                Nationality = "Mongolie",
                BirthDate = DateOnly.Parse("1162-01-01"),
                DeathDate = DateOnly.Parse("1227-08-18"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Ramsès",
                LastName = "II",
                Bio = "Pharaon d'Égypte, l'un des plus puissants et célèbres souverains de l'Égypte antique.",
                Category = "Histoire",
                Nationality = "Égypte",
                BirthDate = DateOnly.Parse("1303-01-01"),
                DeathDate = DateOnly.Parse("1213-01-01"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Christophe",
                LastName = "Colomb",
                Bio = "Navigateur génois, découvreur de l'Amérique en 1492 au service de la couronne espagnole.",
                Category = "Histoire",
                Nationality = "Italie",
                BirthDate = DateOnly.Parse("1451-10-31"),
                DeathDate = DateOnly.Parse("1506-05-20"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Léonidas",
                LastName = "Ier",
                Bio = "Roi de Sparte, héros de la bataille des Thermopyles contre l'Empire perse en 480 av. J.-C.",
                Category = "Histoire",
                Nationality = "Grèce",
                BirthDate = DateOnly.Parse("0540-01-01"),
                DeathDate = DateOnly.Parse("0480-08-11"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Catherine",
                LastName = "de Médicis",
                Bio = "Reine de France, régente influente durant les guerres de religion et mécène des arts.",
                Category = "Histoire",
                Nationality = "Italie",
                BirthDate = DateOnly.Parse("1519-04-13"),
                DeathDate = DateOnly.Parse("1589-01-05"),
                ImageUrl = null
            },

            // ==================== Technologie ====================
            new Personality
            {
                FirstName = "Alan",
                LastName = "Turing",
                Bio = "Mathématicien et cryptologue britannique, père de l'informatique théorique et du concept de machine universelle.",
                Category = "Technologie",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1912-06-23"),
                DeathDate = DateOnly.Parse("1954-06-07"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Steve",
                LastName = "Jobs",
                Bio = "Cofondateur d'Apple, visionnaire de l'informatique personnelle et du design technologique.",
                Category = "Technologie",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1955-02-24"),
                DeathDate = DateOnly.Parse("2011-10-05"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Tim",
                LastName = "Berners-Lee",
                Bio = "Informaticien britannique, inventeur du World Wide Web et défenseur de l'internet ouvert.",
                Category = "Technologie",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1955-06-08"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Ada",
                LastName = "Lovelace",
                Bio = "Mathématicienne britannique, considérée comme la première programmeuse de l'histoire pour ses travaux sur la machine analytique.",
                Category = "Technologie",
                Nationality = "Angleterre",
                BirthDate = DateOnly.Parse("1815-12-10"),
                DeathDate = DateOnly.Parse("1852-11-27"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Linus",
                LastName = "Torvalds",
                Bio = "Ingénieur logiciel finlandais, créateur du noyau Linux et du système de contrôle de version Git.",
                Category = "Technologie",
                Nationality = "Finlande",
                BirthDate = DateOnly.Parse("1969-12-28"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Grace",
                LastName = "Hopper",
                Bio = "Informaticienne et contre-amiral américaine, pionnière de la programmation et co-créatrice du COBOL.",
                Category = "Technologie",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1906-12-09"),
                DeathDate = DateOnly.Parse("1992-01-01"),
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Bill",
                LastName = "Gates",
                Bio = "Cofondateur de Microsoft, pionnier de l'industrie du logiciel et philanthrope engagé.",
                Category = "Technologie",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1955-10-28"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Margaret",
                LastName = "Hamilton",
                Bio = "Informaticienne américaine, directrice du développement logiciel pour le programme Apollo de la NASA.",
                Category = "Technologie",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1936-08-17"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Vint",
                LastName = "Cerf",
                Bio = "Informaticien américain, co-inventeur du protocole TCP/IP et considéré comme l'un des pères d'Internet.",
                Category = "Technologie",
                Nationality = "États-Unis",
                BirthDate = DateOnly.Parse("1943-06-23"),
                DeathDate = null,
                ImageUrl = null
            },
            new Personality
            {
                FirstName = "Hedy",
                LastName = "Lamarr",
                Bio = "Actrice et inventrice austro-américaine, co-inventrice du saut de fréquence à la base du Wi-Fi et du Bluetooth.",
                Category = "Technologie",
                Nationality = "Autriche",
                BirthDate = DateOnly.Parse("1914-11-09"),
                DeathDate = DateOnly.Parse("2000-01-19"),
                ImageUrl = null
            },
        ];
    }
}