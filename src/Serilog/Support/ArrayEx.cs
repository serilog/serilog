using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Serilog.Support
{
    static class ArrayEx
    {
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static T[] AsArray<T>(this IEnumerable<T> source, bool forceNewInstance = true)
        {
            return source switch
            {
                T[] arr => forceNewInstance ? (T[]) arr.Clone() : arr,
                List<T> listOfT => listOfT.ToArray(),
                HashSet<T> hashSetOfT => hashSetOfT.ToArray(),
                SortedSet<T> sortSetOfT => sortSetOfT.ToArray(),
                Queue<T> listOfT => listOfT.ToArray(),
                Stack<T> stackOfT => stackOfT.ToArray(),
                _ => source.ToArray(),
            };
        }
    }
}
