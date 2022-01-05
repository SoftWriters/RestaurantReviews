using Newtonsoft.Json;
using RestaurantReviews.Interfaces.Repositories;
using System.Collections.Generic;
using System.IO;

namespace RestaurantReviews.JsonData
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
            return JsonConvert.DeserializeObject<ICollection<T>>(
                contents,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                }
                );
        }

        public void Save(ICollection<T> contents)
        {
            File.WriteAllText(path,
                JsonConvert.SerializeObject(contents,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
                }));
        }
    }
}