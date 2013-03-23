// Copyright 2013 Nicholas Blumhardt
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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