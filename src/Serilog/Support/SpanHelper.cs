using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Serilog.Support
{
    //This file is to help in the interoperability of the old frameworks with Span<t>. Making a code easy to understand with less #if's

    static class IntHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParse(string str, out int result)
        {
            return int.TryParse(str, NumberStyles.None, CultureInfo.InvariantCulture, out result);
        }

#if FEATURE_SPAN
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

    static class SpanMethodsToStringHelper
    {
#if !FEATURE_SPAN
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Slice(this string str, int startIndex) => str.Substring(startIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Slice(this string str, int startIndex, int length) => str.Substring(startIndex, length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty(this string str) => string.IsNullOrEmpty(str);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty(this ReadOnlySpan<char> span) => span.IsEmpty;
#endif
    }
}
