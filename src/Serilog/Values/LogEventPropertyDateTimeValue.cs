using System;
using System.IO;

namespace Serilog.Values
{
    class LogEventPropertyDateTimeValue : LogEventPropertyValue
    {
        private readonly DateTime _value;

        public LogEventPropertyDateTimeValue(DateTime value)
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