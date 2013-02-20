using System.IO;

namespace Serilog.Values
{
    class LogEventPropertyBooleanValue : LogEventPropertyValue
    {
        private readonly bool _value;

        public LogEventPropertyBooleanValue(bool value)
        {
            _value = value;
        }

        internal override void Render(TextWriter output, string format = null)
        {
            output.Write(_value);
        }
    }
}