namespace BibliotekApp
{
    public class Bibliotek
    {
        public List<Bok> Böcker { get; set; }
        public List<Författare> Författare { get; set; }
        private MinLillaDb minLillaDb;

        public Bibliotek()
        {
            // Ladda data från JSON-fil om den finns
            minLillaDb = MinLillaDb.Deserialize("LibraryData.Json");
            Böcker = minLillaDb.Böcker ?? new List<Bok>();
            Författare = minLillaDb.Författare ?? new List<Författare>();

            if (Böcker.Count > 0 || Författare.Count > 0)
                Console.WriteLine("Data har lästs in .");
            else
                Console.WriteLine("Databasen är tom eller saknar giltiga poster.");
        }
        public void SaveChanges()
        {
            // Spara aktuellt bibliotekstillstånd till JSON-filen
            minLillaDb.Böcker = Böcker;
            minLillaDb.Författare = Författare;
            minLillaDb.Serialize("LibraryData.Json");
        }
        // Metod för att spara biblioteket till en JSON-fil
        public void Serialize() => minLillaDb.Serialize("LibraryData.json");

        // Metod för att läsa biblioteket från en JSON-fil
        public void Deserialize() => minLillaDb = MinLillaDb.Deserialize("LibraryData.json");


        public void AddBok(Bok bok) => Böcker.Add(bok);
        public void AddFörfattare(Författare författare) => Författare.Add(författare);
        public void RemoveBok(int id) => Böcker.RemoveAll(b => b.Id == id);
        public void RemoveFörfattare(int id) => Författare.RemoveAll(f => f.Id == id);
        public void UpdateBok(int id, Bok updatedBok)
        {
            var bok = Böcker.FirstOrDefault(b => b.Id == id);
            if (bok != null)
            {
                bok.Titel = updatedBok.Titel;
            }
        }

        public void UpdateFörfattare(int id, Författare updatedFörfattare)
        {
            var författare = Författare.FirstOrDefault(f => f.Id == id);
            if (författare != null)
            {
                författare.Namn = updatedFörfattare.Namn;
                författare.Land = updatedFörfattare.Land;
            }
        }

        public void ListaAllaBöckerOchFörfattare()
        {
            Console.WriteLine("---- Alla Böcker ----");
            foreach (var bok in Böcker)
            {
                Console.WriteLine($"{bok.Titel}, Författare: {bok.Författare}, Genre: {bok.Genre}");
                Console.WriteLine($"Publiceringsår: {bok.Publiceringsår}, ISBN: {bok.Isbn}");
                Console.WriteLine($"Genomsnittligt betyg: {bok.AverageRating}");
            }

            Console.WriteLine("---- Alla Författare ----");
            foreach (var författare in Författare)
            {
                Console.WriteLine($"{författare.Namn} från {författare.Land}");
            }
        }

        public List<Bok> ListAllaBöcker() => Böcker;
        public List<Författare> ListAllaFörfattare() => Författare;

        public void VisaHuvudMeny()
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
        public static void BokMeny()
        {
            Console.WriteLine("Välj ett filteralternativ:");
            Console.WriteLine("1. Genre");
            Console.WriteLine("2. Författare");
            Console.WriteLine("3. Publiceringsår");
            Console.Write("Ange ditt val: ");
        }
    }

}
