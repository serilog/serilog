using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Serilog.Support
{
    class EmptyReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        public TValue this[TKey key] => throw new KeyNotFoundException();

        public IEnumerable<TKey> Keys => Enumerable.Empty<TKey>();

        public IEnumerable<TValue> Values => Enumerable.Empty<TValue>();

        public int Count => 0;

        public bool ContainsKey(TKey key) => false;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
