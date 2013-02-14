using System.IO;

namespace Opi.Values
{
    class LogEventPropertyIntValue : LogEventPropertyValue
    {
        private readonly int _value;

        public LogEventPropertyIntValue(int value)
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