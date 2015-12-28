// Copyright 2013-2015 Serilog Contributors
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using Serilog.Parsing;

#if NET40
using IPropertyDictionary = System.Collections.Generic.IDictionary<string, Serilog.Events.LogEventPropertyValue>;
#else
using IPropertyDictionary = System.Collections.Generic.IReadOnlyDictionary<string, Serilog.Events.LogEventPropertyValue>;
#endif

namespace Serilog.Events
{
    /// <summary>
    /// Represents a message template passed to a log method. The template
    /// can subsequently render the template in textual form given the list
    /// of properties.
    /// </summary> 
    public class MessageTemplate
    {
        readonly string _text;
        readonly MessageTemplateToken[] _tokens;

        public MessageTemplate(IEnumerable<MessageTemplateToken> tokens)
            : this(string.Join("", tokens), tokens)
        {
        }

        public MessageTemplate(string text, IEnumerable<MessageTemplateToken> tokens)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (tokens == null) throw new ArgumentNullException(nameof(tokens));

            _text = text;
            _tokens = tokens.ToArray();
        }

        public string Text
        {
            get { return _text; }
        }

        public override string ToString()
        {
            return Text;
        }
        
        public IEnumerable<MessageTemplateToken> Tokens
        {
            get { return _tokens; }
        }

        public string Render(IPropertyDictionary properties, IFormatProvider formatProvider = null)
        {
            var writer = new StringWriter(formatProvider);
            Render(properties, writer, formatProvider);
            return writer.ToString();
        }

        public void Render(IPropertyDictionary properties, TextWriter output, IFormatProvider formatProvider = null)
        {
            foreach (var token in _tokens)
            {
                token.Render(properties, output, formatProvider);
            }
        }
    }

}