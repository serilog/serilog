using System.IO;

namespace Serilog.Formatting.Json
{
    interface IJsonFormattable
    {
        /// <summary>
        /// Writes JSON value to the <paramref name="output"/>.
        /// </summary>
        /// <param name="output">The output writer.</param>
        void Write(TextWriter output);
    }
}
