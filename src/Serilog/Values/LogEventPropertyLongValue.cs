using System.IO;

namespace Serilog.Values
{
    class LogEventPropertyLongValue : LogEventPropertyValue
    {
        private readonly long _value;

        public LogEventPropertyLongValue(long value)
        {
            _value = value;
        }

        internal override void Render(TextWriter output, string format = null)
        {
            if (format == null)
                output.Write(_value);
            else
                output.Write(_value.ToString(format));
        }
    }
}