using System;
using System.IO;

namespace Serilog.Values
{
    class LogEventPropertyTokenValue : LogEventPropertyStringValue
    {
        private readonly string _toString;

        public LogEventPropertyTokenValue(string toString)
            : base(toString)
        {
            if (toString == null) throw new ArgumentNullException("toString");
            _toString = toString;
        }

        internal override void Render(TextWriter output, string format = null)
        {
            output.Write(_toString);
        }
    }
}