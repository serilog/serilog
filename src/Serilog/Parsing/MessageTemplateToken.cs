using System.Collections.Generic;
using System.IO;
using Serilog.Events;

namespace Serilog.Parsing
{
    public abstract class MessageTemplateToken
    {
        public abstract void Render(IReadOnlyDictionary<string, LogEventProperty> properties, TextWriter output);
    }
}