using System.Text.Json.Serialization;

namespace BibliotekApp
{
    public class MinLillaDb
    {
        [JsonPropertyName("books")]
        public List<Bok> Böcker { get; set; } = new List<Bok>();

        [JsonPropertyName("authors")]
        public List<Författare> Författare { get; set; } = new List<Författare>();
    }


}
    


