using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace BibliotekApp
{
    public class Repository<T> where T : IIdentifiable
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }
        public T GetById(int id)
        {
            return items.FirstOrDefault(item=> item.Id == id);
        }
        public bool Update(int Id,T UpdatedItem)
        {
            var index = items.FindIndex(i=> i.Id == UpdatedItem.Id);
            if(index ==  -1) return false;
            items[index] = UpdatedItem;
            return true;
        }
        public bool Delete(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return false;

            items.Remove(item);
            return true;
        }
        public List<T> GetAll()
        {
            return new List<T>(items);
        }
        public void Serialize(string filePath)
        {
            string jsonData = JsonSerializer.Serialize(items);
            File.WriteAllText(filePath, jsonData);
        }

        public void Deserialize(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                items = JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
                Console.WriteLine($"Laddade in {items.Count} objekt från {filePath}");
            }
        }
    }

}
