using System;
using System.IO;

namespace Serilog.Values
{
    class LogEventPropertyDateTimeOffsetValue : LogEventPropertyValue
    {
        private readonly DateTimeOffset _value;

        public LogEventPropertyDateTimeOffsetValue(DateTimeOffset value)
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