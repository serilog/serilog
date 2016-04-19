using System.IO;

using Serilog.Parsing;

namespace Serilog.Formatting.Display
{
    static class Padding
    {
        /// <summary>
        /// Writes the provided value to the output, applying direction-based padding when <paramref name="alignment"/> is provided.
        /// </summary>
        public static void Apply(TextWriter output, string value, Alignment? alignment)
        {
            if (!alignment.HasValue)
            {
                output.Write(value);
                return;
            }

            var pad = alignment.Value.Width - value.Length;

            if (alignment.Value.Direction == AlignmentDirection.Right)
                output.Write(new string(' ', pad));

            output.Write(value);

            if (alignment.Value.Direction == AlignmentDirection.Left)
                output.Write(new string(' ', pad));
        }
    }
}