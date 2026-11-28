namespace Serilog.Templates.Encoding
{
    /// <summary>
    /// An encoder applied to the output substituted into template holes.
    /// </summary>
    public abstract class TemplateOutputEncoder
    {
        /// <summary>
        /// Encode <paramref name="value" />.
        /// </summary>
        /// <param name="value">The raw template output to encode.</param>
        /// <returns>The encoded output.</returns>
        public abstract string Encode(string value);
    }
}
