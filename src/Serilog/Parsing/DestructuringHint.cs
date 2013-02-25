namespace Serilog.Parsing
{
    enum DestructuringHint
    {
        /// <summary>
        /// Convert known types to literals, convert objects
        /// to strings.
        /// </summary>
        Default,

        /// <summary>
        /// Convert all types to strings.
        /// </summary>
        Stringify,
        
        /// <summary>
        /// Convert known types to literals, destructure objects
        /// into literals.
        /// </summary>
        Destructure
    }
}