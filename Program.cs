namespace BibliotekApp
{
    public class Program
    {
        static Repository<Bok> bokRepo = new Repository<Bok>();
        static Repository<Författare> författareRepo = new Repository<Författare>();
        static MinLillaDb databasen = new MinLillaDb();

        static void Main(string[] args)
        {
            LaddaData(); // Läs in data från JSON-filer vid programstart
            bool running = true;
            while (running)
            {
                VisaHuvudMeny();

                string input = Console.ReadLine();
                Console.Clear();
                switch (input)
                {
                    case "1":
                        AddBok();
                        break;
                    case "2":
                        AddFörfattare();
                        break;
                    case "3":
                        UpdateBok();
                        break;
                    case "4":
                        UpdateFörfattare();
                        break;
                    case "5":
                        RemoveBok();
                        break;
                    case "6":
                        RemoveFörfattare();
                        break;
                    case "7":
                        ListaAlla();
                        break;
                    case "8":
                        SökOchFiltreraBöcker();
                        break;
                    case "9":
                        SparaData();
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
        static void SparaData()
        {
            databasen.Serialize("LibraryData.json");
        }

        static void LaddaData()
        {
            databasen = MinLillaDb.Deserialize("LibraryData.json");
        }
        public static void VisaHuvudMeny()
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


        static void AddBok()
        {
            Console.WriteLine("Ange bokens titel: ");
            string titel = Console.ReadLine();

            Console.Write("Ange författarens namn: ");
            string författareNamn = Console.ReadLine();

            Console.Write("Ange vilket land författaren kommer ifrån: ");
            string författarLand = Console.ReadLine();

            Författare nyFörfattare = new Författare    // Skapa författare objekt
            {
                Id = författareRepo.GetAll().Count + 1,
                Namn = författareNamn,
                Land = författarLand
            };

            författareRepo.Add(nyFörfattare); // Lägg till författaren i biblioteket
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
                Console.WriteLine("Ogiltigt betyg, ange ett tal mellan 1-5");
            }
            Bok nyBok = new Bok
            {
                Titel = titel,
                Författare = författareNamn,
                Genre = genre,
                Publiceringsår = år,
                Isbn = isbn,
                Recensioner = new List<int> { initialRating }
            };
            bokRepo.Add(nyBok); //Lägg till boken i bokRepo
            Console.WriteLine("Boken är nu tillagd!");
            Console.WriteLine($"Genomsnittligt betyg för {nyBok.Titel} är nu: {nyBok.AverageRating}");//Visa det genomsnittliga betyget för boken
        }

        static void AddFörfattare()
        {
            Console.WriteLine("Ange författarens namn: ");
            string namn = Console.ReadLine();
            Console.WriteLine("Ange vilket land författaren kommer ifrån: ");
            string land = Console.ReadLine();

            Författare nyFörfattare = new Författare
            {
                Id = författareRepo.GetAll().Count + 1,
                Namn = namn,
                Land = land,
            };
            författareRepo.Add(nyFörfattare);
            Console.WriteLine("Författare har lagts till!");
        }

        static void UpdateBok()
        {
            Console.WriteLine("Ange bokens Id som du vill ska uppdateras");
            int bokId = int.Parse(Console.ReadLine());

            Bok befintligBok = bokRepo.GetAll().FirstOrDefault(b => b.Id == bokId);
            if (befintligBok == null)
            {
                Console.WriteLine("Boken med det angivna ID:t hittades inte.");
                return;
            }
            Console.WriteLine("Ange ny titel");
            befintligBok.Titel = Console.ReadLine();

            bokRepo.Update(bokId, befintligBok);
            Console.WriteLine("Boken har uppdaterats!");
        }

        static void UpdateFörfattare()
        {
            Console.Write("Ange ID för författaren som ska uppdateras: ");
            int författarId = int.Parse(Console.ReadLine());

            Författare befintligFörfattare = författareRepo.GetAll().FirstOrDefault(f => f.Id == författarId);
            if (befintligFörfattare == null)
            {
                Console.WriteLine("Författaren med det angivna ID:t hittades inte.");
                return;
            }

            Console.Write("Ange nytt namn för författaren: ");
            string nyttNamn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyttNamn))
            {
                befintligFörfattare.Namn = nyttNamn;
            }

            Console.Write("Ange nytt land för författaren: ");
            string nyttLand = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyttLand))
            {
                befintligFörfattare.Land = nyttLand;
            }

            författareRepo.Update(författarId, befintligFörfattare);
            Console.WriteLine("Författardetaljer har uppdaterats!");
        }
        static void RemoveBok()
        {
            Console.WriteLine("Ange Id nummer för boken som du vill radera: ");
            int bokId = int.Parse(Console.ReadLine());

            bool borttagen = bokRepo.Delete(bokId);
            if (borttagen)
            {
                Console.WriteLine("Boken har tagits bort.");
            }
            else
            {
                Console.WriteLine("Boken med det angivna Id:t hittades inte!");
            }
        }

        static void RemoveFörfattare()
        {
            Console.WriteLine("Ange Id för den författare som ska raderas: ");
            int författarId = int.Parse(Console.ReadLine());

            bool borttagen = författareRepo.Delete(författarId);
            if (borttagen)
            {
                Console.WriteLine("Författaren har tagits bort.");
            }
            else
            {
                Console.WriteLine("Författaren med det angivna Id:t hittades inte.");
            }
        }
        static void ListaAlla()
        {
         
                Console.WriteLine("--- Lista över alla böcker ---");

                if (databasen.Böcker.Count == 0)
                {
                    Console.WriteLine("Inga böcker finns i biblioteket.");
                }
                else
                {
                    foreach (var bok in databasen.Böcker)
                    {
                        Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, Genre: {bok.Genre}, År: {bok.Publiceringsår}, ISBN: {bok.Isbn}, Betyg: {bok.AverageRating}");
                    }
                }

                Console.WriteLine("\n--- Lista över alla författare ---");

                if (databasen.Författare.Count == 0)
                {
                    Console.WriteLine("Inga författare finns i biblioteket.");
                }
                else
                {
                    foreach (var författare in databasen.Författare)
                    {
                        Console.WriteLine($"Namn: {författare.Namn}, Land: {författare.Land}");
                    }
                }

                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
                Console.ReadKey();
            

        }
        static void SökOchFiltreraBöcker()
        {
            Bibliotek.BokMeny();
            string val = Console.ReadLine();
            List<Bok> resultat = null;

            switch (val)
            {
                case "1":
                    Console.WriteLine("Ange genre: ");
                    string genre = Console.ReadLine();
                    resultat = bokRepo.GetAll().Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "2":
                    Console.WriteLine("Ange författarens namn: ");
                    string författare = Console.ReadLine();
                    resultat = bokRepo.GetAll().Where(b => b.Författare.Equals(författare, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "3":
                    Console.WriteLine("Ange bokens publiceringsår: ");
                    if (int.TryParse(Console.ReadLine(), out int år))
                    {
                        resultat = bokRepo.GetAll().Where(b => b.Publiceringsår == år).ToList();
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt årtal.");
                    }
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    return;
            }

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


