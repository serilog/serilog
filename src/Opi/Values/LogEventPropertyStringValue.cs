using System;
using System.IO;

namespace Opi.Values
{
    class LogEventPropertyStringValue : LogEventPropertyValue
    {
        private readonly string _toString;

        public LogEventPropertyStringValue(string toString)
        {
            if (toString == null) throw new ArgumentNullException("toString");
            _toString = toString;
        }

        internal override void Render(TextWriter output, string format = null)
        {
            output.Write("\"");
            output.Write(_toString.Replace("\"", "\\\""));
            output.Write("\"");
        }
    }
}