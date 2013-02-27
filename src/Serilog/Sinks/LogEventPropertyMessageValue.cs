using System.Collections.Generic;
using System.IO;
using Serilog.Core;
using Serilog.Values;

namespace Serilog.Sinks
{
    class LogEventPropertyMessageValue : LogEventPropertyValue
    {
        private readonly MessageTemplate _template;
        private readonly IReadOnlyDictionary<string, LogEventProperty> _properties;

        public LogEventPropertyMessageValue(MessageTemplate template, IReadOnlyDictionary<string, LogEventProperty> properties)
        {
            _template = template;
            _properties = properties;
        }

        internal override void Render(TextWriter output, string format = null)
        {
            _template.Render(_properties, output);
        }
    }
}
