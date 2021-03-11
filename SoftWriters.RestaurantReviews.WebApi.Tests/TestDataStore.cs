using System;
using System.Collections.Generic;
using System.Linq;
using SoftWriters.RestaurantReviews.DataLibrary;

namespace SoftWriters.RestaurantReviews.WebApi.Tests
{
    public class TestDataStore<T> : IDataStore<T>
    {
        private readonly List<T> _items;

        public TestDataStore(IEnumerable<T> initialItems)
        {
            _items = initialItems.ToList();
        }

        public IEnumerable<T> GetAllItems()
        {
            return _items;
        }

        public IEnumerable<T> GetItems(Func<T, bool> predicate)
        {
            return _items.Where(predicate);
        }

        public void AddItem(T item)
        {
            _items.Add(item);
        }

        public void DeleteItem(T item)
        {
            _items.Remove(item);
        }
    }
}
