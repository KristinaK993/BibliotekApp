

namespace BibliotekApp
{
    public class Bok : IIdentifiable
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Författare { get; set; }
        public string Genre { get; set; }
        public int Publiceringsår { get; set; }
        public string Isbn { get; set; }
        public List<int>Recensioner { get; set; } = new List<int>();

        // Lista med heltalsbetyg som representerar recensioner från användare
        public List<int> Reviews { get; set; } = new List<int>();

        public int AverageRating => Recensioner.Any() ? (int)Math.Round(Recensioner.Average()) : 0;

        public void AddReview(int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException("Betyg måste vara mellan 1-5 ");
            Reviews.Add(rating);
        }
        // Visar detaljerad information om boken inklusive genomsnittligt betyg
        public void DisplayBookInfo()
        {
            Console.WriteLine($"Titel: {Titel}");
            Console.WriteLine($"Författare: {Författare ?? "Okänd"}");
            Console.WriteLine($"Genre: {Genre}");
            Console.WriteLine($"Publiceringsår: {Publiceringsår}");
            Console.WriteLine($"ISBN: {Isbn}");
            Console.WriteLine($"Genomsnittligt betyg: {AverageRating:F1} ({Reviews.Count} recensioner)");
        }
    }
}
