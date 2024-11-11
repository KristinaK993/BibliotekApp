using BibliotekApp;

namespace BibliotekApp
{
    public class Författare : IIdentifiable
    {
        public int Id { get; set; }
        public string Namn { get; set; }
        public string Land {  get; set; }
        public int  Böcker { get; set; }

    }
}
 