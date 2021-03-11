using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace SoftWriters.RestaurantReviews.DataLibrary
{
    public interface IDataStore<T>
    {
        IEnumerable<T> GetAllItems();
        IEnumerable<T> GetItems(Func<T, bool> predicate);
        void AddItem(T item);
        void DeleteItem(T item);
    }

    /// <summary>
    /// Implements our data store as xml files in the local users documents folder.
    /// It's not a great solution, but this would ultimately be stored in a database.
    /// Just saving some time implementing a file based solution.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XmlDataStore<T> : IDataStore<T>
    {
        private static List<Type> SerializableTypes = new List<Type>
        {
            typeof(T),
            typeof(Restaurant),
            typeof(List<Restaurant>),
            typeof(User),
            typeof(List<User>),
            typeof(Review),
            typeof(List<Review>),
            typeof(Address)
        };

        private readonly string _filename;
        private readonly object _lock = new object();

        public XmlDataStore()
        {
            string myDocsDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _filename = Path.Combine(myDocsDir, string.Format("xmlDataStore_{0}.xml", typeof(T).Name));
            EnsureDataExists();
        }

        public IEnumerable<T> GetAllItems()
        {
            lock (_lock)
            {
                var items = Deserialize();
                return items;
            }
        }

        public IEnumerable<T> GetItems(Func<T, bool> predicate)
        {
            IEnumerable<T> items;
            lock (_lock)
            {
                items = Deserialize();
            }

            return items.Where(predicate);
        }

        public void AddItem(T item)
        {
            lock (_lock)
            {
                var items = Deserialize().ToList();
                items.Add(item);
                Serialize(items);
            }
        }

        public void DeleteItem(T item)
        {
            lock (_lock)
            {
                var items = Deserialize().ToList();
                if(items.Contains(item))
                    items.Remove(item);
            }
        }

        private IEnumerable<T> Deserialize()
        {
            EnsureDataExists();

            List<T> items;
            var serializer = new DataContractSerializer(typeof(T), SerializableTypes);
            using (var reader = XmlReader.Create(_filename))
            {
                items = serializer.ReadObject(reader) as List<T>;
            }

            return items;
        }

        private void EnsureDataExists()
        {
            if (File.Exists(_filename))
                return;

            Serialize(new List<T>());
        }

        private void Serialize(IEnumerable<T> items)
        {
            var serializer = new DataContractSerializer(typeof(T), SerializableTypes);
            using (var writer = new XmlTextWriter(_filename, Encoding.UTF8))
            {
                serializer.WriteObject(writer, items);
            }
        }
    }
}
