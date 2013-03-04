using System;
using System.Globalization;
using System.IO;

namespace Serilog.Events
{
    public class LogEventPropertyLiteralValue : LogEventPropertyValue
    {
        readonly object _value;

        public LogEventPropertyLiteralValue(object value)
        {
            _value = value;
        }

        internal override void Render(TextWriter output, string format = null)
        {
            if (_value == null)
            {
                output.Write("null");
            }
            else
            {
                var s = _value as string;
                if (s != null)
                {
                    if (format != "l")
                    {
                        output.Write("\"");
                        output.Write(s.Replace("\"", "\\\""));
                        output.Write("\"");
                    }
                    else
                    {
                        output.Write(s);
                    }
                }
                else
                {
                    var f = _value as IFormattable;
                    if (f != null)
                    {
                        output.Write(f.ToString(format, CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        output.Write(_value.ToString());
                    }
                }
            }
        }
    }
}