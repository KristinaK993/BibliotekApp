namespace BibliotekApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bibliotek bibliotek = new Bibliotek();
            MinLillaDb minLillaDb = new MinLillaDb();
            string dataJsonfilPath = "LibraryData.Json";
            string allaDataSomJsonType = File.ReadAllText(dataJsonfilPath);

            bool running = true;
            while (running)
            {
                VisaHuvudMeny();

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        AddBok(bibliotek);
                        break;
                    case "2":
                        AddFörfattare(bibliotek);
                        break;
                    case "3":
                        UpdateBok(bibliotek);
                        break;
                    case "4":
                        UpdateFörfattare(bibliotek);
                        break;
                    case "5":
                        RemoveBok(bibliotek);
                        break;
                    case "6":
                        RemoveFörfattare(bibliotek);
                        break;
                    case "7":
                        ListaAlla(bibliotek);
                        break;
                    case "8":
                        SökOchFiltreraBöcker(bibliotek);
                        break;
                    case "9":
                        bibliotek.Serialize();
                        Console.WriteLine("Data sparat. Programmet avslutas.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;

                }
                if (running)
                {
                    Console.WriteLine("Tryck på valfri tangent för att återgå till menyn...");
                    Console.ReadKey();
                }
            }
        }

        private static void VisaHuvudMeny()
        {
            Console.Clear();
            Console.WriteLine("---- Bibliotekshantering ----");
            Console.WriteLine("1. Lägg till ny bok");
            Console.WriteLine("2. Lägg till ny författare");
            Console.WriteLine("3. Uppdatera bokdetaljer");
            Console.WriteLine("4. Uppdatera författardetaljer");
            Console.WriteLine("5. Ta bort bok");
            Console.WriteLine("6. Ta bort författare");
            Console.WriteLine("7. Lista alla böcker och författare");
            Console.WriteLine("8. Sök och filtrera böcker");
            Console.WriteLine("9. Avsluta och spara data");
            Console.Write("Välj ett alternativ: ");
        }

        static void AddBok(Bibliotek bibliotek)
        {
            Console.WriteLine("Ange bokens titel: ");
            string titel = Console.ReadLine();

            Console.Write("Ange författarens namn: ");
            string författareNamn = Console.ReadLine();

            Console.Write("Ange vilket land författaren kommer ifrån: ");
            string författarLand = Console.ReadLine();

            // Skapa författare objekt
            Författare nyFörfattare = new Författare
            {
                Id = bibliotek.ListAllaFörfattare().Count + 1,
                Namn = författareNamn,
                Land = författarLand
            };

            // Lägg till författaren i biblioteket
            bibliotek.AddFörfattare(nyFörfattare);

            Console.Write("Ange bokens genre: ");
            string genre = Console.ReadLine();

            Console.Write("Ange bokens publiceringsår: ");
            int år = int.Parse(Console.ReadLine());

            Console.Write("Ange bokens ISBN: ");
            string isbn = Console.ReadLine();

            Console.WriteLine("Betygsätt boken mellan 1-5");
            int initialRating;
            while (!int.TryParse(Console.ReadLine(), out initialRating) || initialRating < 1 || initialRating > 5)
            {
                Console.WriteLine("Ogiltigt betyg, angel ett tal mellan 1-5");
            }

                Bok nyBok = new Bok
            {
                Titel = titel,
                Författare = författareNamn,
                Genre = genre,
                Publiceringsår = år,
                Isbn = isbn,
                Recensioner = new List<int> {initialRating}
            };
            bibliotek.AddBok(nyBok);
            Console.WriteLine("Boken är nu tillagd!");

            //Visa det genomsnittliga betyget för boken
            Console.WriteLine($"Genomsnittligt betyg för {nyBok.Titel} är nu: {nyBok.AverageRating}");


        }
        static void AddFörfattare(Bibliotek bibliotek)
        {
            Console.WriteLine("Ange författarens namn: ");
            string namn = Console.ReadLine();

            Console.WriteLine("Ange vilket land författaren kommer ifrån: ");
            string land = Console.ReadLine();

            Författare nyFörfattare = new Författare
            {
                Namn = namn,
                Land = land,
            };

            bibliotek.AddFörfattare(nyFörfattare);
            Console.WriteLine("Författare har lagts till!");
        }
        static void UpdateBok(Bibliotek bibliotek)
        {
            Console.WriteLine("Ange bokens Id som du vill ska uppdateras");
            int bokId = int.Parse(Console.ReadLine());
            Console.WriteLine("Ange ny titel");
            string nyTitel = Console.ReadLine();

            // Skapa en ny bok med uppdaterade detaljer
            Bok nyBok = new Bok { Titel = nyTitel };
            bibliotek.UpdateBok(bokId, nyBok);
            Console.WriteLine("Boken har uppdaterats!");
        }
        static void UpdateFörfattare(Bibliotek bibliotek)
        {
            Console.Write("Ange ID för författaren som ska uppdateras: ");
            int författarId;

            while (!int.TryParse(Console.ReadLine(), out författarId))
            {
                Console.Write("Ogiltig inmatning. Ange ett giltigt ID: ");
            }

            // Hämta den författare som ska uppdateras
            var författare = bibliotek.Författare.FirstOrDefault(f => f.Id == författarId);

            if (författare == null)
            {
                Console.WriteLine("Författaren med det angivna ID:t hittades inte.");
                return;
            }

            // Frågar efter nya detaljer
            Console.Write("Ange nytt namn för författaren ------- : ");
            string nyttNamn = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nyttNamn))
            {
                författare.Namn = nyttNamn;
            }

            Console.Write("Ange nytt land för författaren -------- : ");
            string nyttLand = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nyttLand))
            {
                författare.Land = nyttLand;
            }
            Console.WriteLine("Författardetaljer har uppdaterats!");
        }
        static void RemoveBok(Bibliotek bibliotek)
        {
            Console.WriteLine("Ange Id nummer för boken som du vill radera: ");
            int bokId;

            while (!int.TryParse(Console.ReadLine(), out bokId))
            {
                Console.WriteLine("Ogilitig inmatning, ange ett giltigt Id");
            }
            var bok = bibliotek.Böcker.FirstOrDefault(b => b.Id == bokId);
            if (bok == null)
            {
                Console.WriteLine("Boken med det angivna Id:t hittades inte!");
                return;
            }
            //Ta bort boken 
            bibliotek.RemoveBok(bokId);
            Console.WriteLine("Boken har tagits bort");
        }
        static void RemoveFörfattare(Bibliotek bibliotek)
        {
            Console.WriteLine("Ange Id för den författare som ska raderas: ");
            int författarId;
            while (!int.TryParse(Console.ReadLine(), out författarId))// läser in id och konverterar till int
            {
                Console.WriteLine("Ogiltigt Id, ange ett giltigt Id: ");
            }
            var författare = bibliotek.Författare.FirstOrDefault(f => f.Id == författarId);
            if (författare == null)
            {
                Console.WriteLine("Författaren med det angivna Id:t hittades inte.");
                return;
            }
            // Ta bort författaren
            bibliotek.RemoveFörfattare(författarId);
            Console.WriteLine("Författaren har tagits bort.");
        }
        static void ListaAlla(Bibliotek bibliotek)
        {
            bibliotek.ListaAllaBöckerOchFörfattare();
            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn...");
            Console.ReadKey();
        }
        static void SökOchFiltreraBöcker(Bibliotek bibliotek)
        {
            Console.WriteLine("Välj ett filteralternativ:");
            Console.WriteLine("1. Genre");
            Console.WriteLine("2. Författare");
            Console.WriteLine("3. Publiceringsår");
            Console.Write("Ange ditt val: ");
            string val = Console.ReadLine();

            List<Bok> resultat = null;

            switch (val)
            {
                case "1":
                    Console.WriteLine("Ange genre: ");
                    string genre = Console.ReadLine();
                    resultat = bibliotek.ListAllaBöcker().Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "2":
                    Console.WriteLine("Ange författarens namn: ");
                    string författare = Console.ReadLine();
                    resultat = bibliotek.ListAllaBöcker().Where(b => b.Genre.Equals(författare, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "3":
                    Console.WriteLine("Ange bokens publiceringsår: ");
                    if (int.TryParse(Console.ReadLine(), out int år))
                    {
                        resultat = bibliotek.ListAllaBöcker().Where(b => b.Publiceringsår == år).ToList();
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt årtal.");
                    }
                    break;

                default:
                    Console.WriteLine("Ogiltigt val.");
                    return;
                    if (resultat != null && resultat.Count > 0)
                    {
                        Console.WriteLine("\nSökresultat:");
                        foreach (var bok in resultat)
                        {
                            Console.WriteLine($"{bok.Titel} av {bok.Författare} - {bok.Genre} ({bok.Publiceringsår})");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Inga böcker hittades för det valda kriteriet.");
                    }
            }
        }

    }

}
