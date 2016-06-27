namespace Serilog.Formatting.Display
{
    static class Casing
    {
        /// <summary>
        /// Apply upper or lower casing to <paramref name="value"/> when <paramref name="format"/> is provided.
        /// Returns <paramref name="value"/> when no or invalid format provided
        /// </summary>
        /// <returns>The provided <paramref name="value"/> with formatting applied</returns>
        public static string Format(string value, string format = null)
        {
            switch (format)
            {
                case "u":
                    return value.ToUpperInvariant();
                case "w":
                    return value.ToLowerInvariant();
                default:
                    return value;
            }
        }
    }
}