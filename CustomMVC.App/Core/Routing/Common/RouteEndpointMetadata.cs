using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Core.Routing.Common
{
    public class RouteEndpointMetadata
    {
        private object[] _items;
        private ConcurrentDictionary<Type, object[]> _cache = new();
        public RouteEndpointMetadata(IEnumerable<object> col)
        {
            _items = col.ToArray();
        }

        public T? GetMetadata<T>()
        {
            if (_cache.TryGetValue(typeof(T), out var obj))
            {
                var length = obj.Length;

                return length > 0 ? (T)obj[length - 1] : default;
            }

            return GetMetadataSlow<T>();
        }

        public T? GetMetadataSlow<T>() 
        { 
            var result = GetOrderedMetadata<T>();

            var length = result.Length;

            return length > 0 ? result[length - 1] : default;
        }

        public T[] GetOrderedMetadata<T>()
        {
            List<T> matches = null;

            var items = _items;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] is T)
                {
                    matches ??= new List<T>();
                    matches.Add((T)items[i]);
                }
            }

            var result = matches == null ? Array.Empty<T>() : matches.ToArray();
            _cache.TryAdd(typeof(T), items);

            return result;
        }
    }
}
