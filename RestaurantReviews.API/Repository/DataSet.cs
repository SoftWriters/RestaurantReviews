using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RestaurantReviews.API.Repository
{
    public class DataSet<T> : IDataSet<T>
    {
        private string path;

        public DataSet(string path)
        {
            this.path = path;
        }

        public ICollection<T> GetAll()
        {
            if (!File.Exists(path))
                return new List<T>();
            var contents = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ICollection<T>>(contents);
        }

        public void Save(ICollection<T> contents)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(contents));
        }
    }
}