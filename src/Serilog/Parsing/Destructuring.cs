namespace Serilog.Parsing
{
    public enum Destructuring
    {
        /// <summary>
        /// Convert known types to literals, arrays to sequences, objects
        /// to strings.
        /// </summary>
        Default,

        /// <summary>
        /// Convert all types to strings. Prefix name with '$'.
        /// </summary>
        Stringify,
        
        /// <summary>
        /// Convert known types to literals, destructure objects and collections
        /// into sequences and structures. Prefix name with '@'.
        /// </summary>
        Destructure
    }
}