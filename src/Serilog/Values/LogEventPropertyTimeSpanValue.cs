using System;
using System.IO;

namespace Serilog.Values
{
    class LogEventPropertyTimeSpanValue : LogEventPropertyValue
    {
        private readonly TimeSpan _value;

        public LogEventPropertyTimeSpanValue(TimeSpan value)
        {
            _value = value;
        }

        internal override void Render(TextWriter output, string format = null)
        {
            if (format == null)
                output.Write(_value);
            else
// ReSharper disable ImpureMethodCallOnReadonlyValueField
                output.Write(_value.ToString(format));
// ReSharper restore ImpureMethodCallOnReadonlyValueField
        }
    }
}