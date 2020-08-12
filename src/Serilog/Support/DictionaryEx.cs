using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Serilog.Support
{
    static class DictionaryEx
    {
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IReadOnlyDictionary<TKey, TValue> AsReadOnlyDictionary<TKey,TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, bool forceNewInstance = true)
        {
            return source switch
            {
                Dictionary<TKey, TValue> dic => forceNewInstance ? new Dictionary<TKey, TValue>(dic) : dic,
                ReadOnlyDictionary<TKey, TValue> roDic => roDic,
                IReadOnlyDictionary<TKey, TValue> roDic => roDic,
                IDictionary<TKey, TValue> dic => new Dictionary<TKey, TValue>(dic),
                _ => source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
            };
        }
    }
}
