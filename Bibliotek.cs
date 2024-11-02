using System.Collections.Generic;
using System.Linq;

namespace BibliotekApp
{
    public class Bibliotek
    {
        public List<Bok> Böcker { get; set; } = new List<Bok>();
        public List<Författare> Författare { get; set; } = new List<Författare>();
        public MinLillaDb minLillaDb = new MinLillaDb();
        

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
                Console.WriteLine($"{bok.Titel} av {bok.Författare} ({bok.Publiceringsår})");
            }

            Console.WriteLine("---- Alla Författare ----");
            foreach (var författare in Författare)
            {
                Console.WriteLine($"{författare.Namn} från {författare.Land}");
            }
        }

        public List<Bok> ListAllaBöcker() => Böcker;
        public List<Författare> ListAllaFörfattare() => Författare;

        public void Serialize() => minLillaDb.Serialize(this);
        public void Deserialize() => minLillaDb.Deserialize(this);
    }
}
