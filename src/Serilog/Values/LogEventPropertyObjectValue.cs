using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Serilog.Values
{
    class LogEventPropertyObjectValue : LogEventPropertyValue
    {
        private readonly string _typeTag;
        private readonly LogEventProperty[] _properties;

        public LogEventPropertyObjectValue(string typeTag, IEnumerable<LogEventProperty> properties)
        {
            if (properties == null) throw new ArgumentNullException("properties");
            _typeTag = typeTag;
            _properties = properties.ToArray();
        }

        internal override void Render(TextWriter output, string format = null)
        {
            if (_typeTag != null)
            {
                output.Write(_typeTag);
                output.Write(' ');
            }
            output.Write("{ ");
            var allButLast = _properties.Length - 1;
            for (var i = 0; i < allButLast; i++)
            {
                var property = _properties[i];
                Render(output, property);
                output.Write(", ");
            }

            if (_properties.Length > 0)
            {
                var last = _properties[_properties.Length - 1];
                Render(output, last);
            }

            output.Write(" }");
        }

        private static void Render(TextWriter output, LogEventProperty property)
        {
            output.Write(property.Name);
            output.Write(": ");
            property.Value.Render(output);
        }
    }
}