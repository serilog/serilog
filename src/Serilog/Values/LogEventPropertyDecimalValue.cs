using System.IO;

namespace Serilog.Values
{
    class LogEventPropertyDecimalValue : LogEventPropertyValue
    {
        private readonly decimal _value;

        public LogEventPropertyDecimalValue(decimal value)
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