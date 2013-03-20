using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Serilog.Events
{
    public class LogEventPropertySequenceValue : LogEventPropertyValue
    {
        private readonly LogEventPropertyValue[] _elements;

        public LogEventPropertySequenceValue(IEnumerable<LogEventPropertyValue> elements)
        {
            if (elements == null) throw new ArgumentNullException("elements");
            _elements = elements.ToArray();
        }

        public LogEventPropertyValue[] Elements { get { return _elements; } }

        internal override void Render(TextWriter output, string format = null)
        {
            // Format string to limit length?

            output.Write('[');
            var allButLast = _elements.Length - 1;
            for (var i = 0; i < allButLast; ++i )
            {
                _elements[i].Render(output);
                output.Write(", ");
            }

            if (_elements.Length > 0)
                _elements[_elements.Length - 1].Render(output);

            output.Write(']');
        }
    }
}