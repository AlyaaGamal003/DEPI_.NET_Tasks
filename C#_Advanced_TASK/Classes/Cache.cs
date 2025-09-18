using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Implement a generic `Cache<TKey, TValue>` class with expiration functionality using constraints.
    internal class Cache<TKey, TValue> where TKey : notnull
    {

        private class CacheItem
        {
            public TValue Value { get; set; }
            public DateTime Expiration { get; set; }
        }

        private Dictionary<TKey, CacheItem> store = new();

        public void Add(TKey key, TValue value, TimeSpan t)
        {
            store[key] = new CacheItem { Value = value, Expiration = DateTime.Now.Add(t) };
        }

        public bool TryGet(TKey key, out TValue value)
        {
            if (store.TryGetValue(key, out var item))
            {
                if (item.Expiration > DateTime.Now)
                {
                    value = item.Value;
                    return true;
                }
                else
                {
                    // expired
                    store.Remove(key);
                }
            }
            value = default!;
            return false;
        }
    }

}

