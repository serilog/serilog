using Serilog.Events;

namespace Serilog.Templates.Encoding
{
    class PreEncodedValue
    {
        public LogEventPropertyValue? Inner { get; }

        public PreEncodedValue(LogEventPropertyValue? inner)
        {
            Inner = inner;
        }

        public override string ToString()
        {
            // This code path indicates that the template expects encoding to be performed, but no encoder is
            // registered (probably a bad situation to be in).
            throw new InvalidOperationException("No output encoder is registered.");
        }
    }
}