using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Platform.Core
{
    public class LazyStaticProperties<T> : IEnumerable<T>
    {
        private readonly Lazy<IEnumerable<T>> _lazy;

        public LazyStaticProperties(Type parentType)
        {
            _lazy = new Lazy<IEnumerable<T>>(() =>
            {
                return parentType.GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Where(p => typeof(T).IsAssignableFrom(p.PropertyType))
                    .Select(p => p.GetValue(null))
                    .Cast<T>();
            });
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _lazy.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
