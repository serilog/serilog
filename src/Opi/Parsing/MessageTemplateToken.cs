using System.Collections.Generic;
using System.IO;

namespace Opi.Parsing
{
    abstract class MessageTemplateToken
    {
        public abstract void Render(IReadOnlyDictionary<string, LogEventProperty> properties, TextWriter output);
    }
}