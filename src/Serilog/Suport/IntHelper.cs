using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Serilog.Support
{
    static class IntHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParse(string str, out int result)
        {
            return int.TryParse(str, NumberStyles.None, CultureInfo.InvariantCulture, out result);
        }

#if !NETSTANDARD1_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParse(ReadOnlySpan<char> str, out int result)
        {
#if FEATURE_SPAN_COMPLETE
            return int.TryParse(str, NumberStyles.None, CultureInfo.InvariantCulture, out result);
#else
            return int.TryParse(str.ToString(), NumberStyles.None, CultureInfo.InvariantCulture, out result);
#endif
        }
#endif

    }
}
