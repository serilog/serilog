namespace Serilog.Parsing
{
    enum DestructuringHint
    {
        /// <summary>
        /// Convert known types to literals, arrays to sequences, objects
        /// to strings.
        /// </summary>
        Default,

        /// <summary>
        /// Convert all types to strings.
        /// </summary>
        Stringify,
        
        /// <summary>
        /// Convert known types to literals, destructure objects and collections
        /// into sequences and structures.
        /// </summary>
        Destructure
    }
}