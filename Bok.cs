

namespace BibliotekApp
{
    public class Bok
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Författare { get; set; }
        public string Genre { get; set; }
        public int Publiceringsår { get; set; }
        public string Isbn { get; set; }
        public List<int>Recensioner { get; set; }

        public Bok ()
        {
            Recensioner = new List<int>();
        }
        public double GetAverageRating()
        {
            if (Recensioner.Count == 0) return 0;
            double total = 0;
            foreach(var rating in Recensioner)
            {
                total += rating;
            }
            return total / Recensioner.Count;
        }
    }
}
