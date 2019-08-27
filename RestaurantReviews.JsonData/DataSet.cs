using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RestaurantReviews.JsonData
{
    internal class DataSet<T>
    {
        private string path;

        internal DataSet(string path)
        {
            this.path = path;
        }

        internal ICollection<T> GetAll()
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

        internal void Save(ICollection<T> contents)
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