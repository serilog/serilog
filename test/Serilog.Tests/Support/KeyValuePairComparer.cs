using System.Collections.Generic;

namespace Serilog.Tests.Support
{
    public class KeyValuePairComparer<TK, TValue> : IEqualityComparer<KeyValuePair<TK, TValue>>
    {
        public bool Equals(KeyValuePair<TK, TValue> x, KeyValuePair<TK, TValue> y)
        {
            return x.Key.Equals(y.Key) && x.Value.Equals(y.Value);
        }

        public int GetHashCode(KeyValuePair<TK, TValue> obj)
        {
            return obj.GetHashCode();
        }
    }

}
