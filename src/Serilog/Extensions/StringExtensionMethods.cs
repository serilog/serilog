namespace Serilog.Extensions
{
#if NET35
    using System;
    using System.Linq;
#endif

    /// <summary>
    /// Missing methods from .net 4
    /// </summary>
    public static class StringExtensionMethods
    {
        /// <summary>
        ///  Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="theString">Source string</param>
        /// <returns>true if the value parameter is null or String.Empty, or if value consists exclusively of white-space characters. </returns>
        public static bool IsNullOrWhiteSpace(this string theString)
        {
#if NET35
            return theString == null || theString.All(Char.IsWhiteSpace);
#else
            return string.IsNullOrWhiteSpace(theString);
#endif
        }
    }
}
