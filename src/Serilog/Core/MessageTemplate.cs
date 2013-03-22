using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Core
{
    public class MessageTemplate
    {
        private readonly IEnumerable<MessageTemplateToken> _tokens;

        internal MessageTemplate(IEnumerable<MessageTemplateToken> tokens)
        {
            _tokens = tokens;
        }

        internal IEnumerable<LogEventProperty> ConstructPositionalProperties(object[] positionalValues)
        {
            if (positionalValues == null)
                yield break;

            var next = 0;
            foreach (var propertyToken in _tokens.OfType<LogEventPropertyToken>())
            {
                if (next < positionalValues.Length)
                {
                    var value = positionalValues[next];
                    yield return new LogEventProperty(
                        propertyToken.PropertyName,
                        LogEventPropertyValue.For(value, propertyToken.Destructuring));
                    next++;
                }
                else
                {
                    yield break;
                }
            }
        }

        public string Render(IReadOnlyDictionary<string, LogEventProperty> properties)
        {
            var writer = new StringWriter();
            Render(properties, writer);
            return writer.ToString();
        }

        public void Render(IReadOnlyDictionary<string, LogEventProperty> properties, TextWriter output)
        {
            foreach (var token in _tokens)
            {
                token.Render(properties, output);
            }
        }
    }
}