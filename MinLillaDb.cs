using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BibliotekApp
{
    public class MinLillaDb
    {
        [JsonPropertyName("böcker")]
        public List<Bok> Böcker { get; set; } = new List<Bok>();

        [JsonPropertyName("författare")]
        public List<Författare> Författare { get; set; } = new List<Författare>();

        // Serialisera biblioteket till JSON-fil
        public void Serialize(string filnamn)
        {
            var jsonData = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filnamn, jsonData);
        }

        // Deserialisera data från JSON-fil till MinLillaDb
        public static MinLillaDb Deserialize(string filnamn)
        {
            if (!File.Exists(filnamn))
            {
                // Om filen inte finns, returneras en ny instans 
                return new MinLillaDb();
            }

            var jsonData = File.ReadAllText(filnamn);
            return JsonSerializer.Deserialize<MinLillaDb>(jsonData);
        }
    }
}



