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
using Serilog.Debugging;
using Serilog.Parsing;

#if NET40
using IPropertyDictionary = System.Collections.Generic.IDictionary<string, Serilog.Events.LogEventPropertyValue>;
#else
using IPropertyDictionary = System.Collections.Generic.IReadOnlyDictionary<string, Serilog.Events.LogEventPropertyValue>;
#endif

namespace Serilog.Events
{
    /// <summary>
    /// Represents a cached message template
    /// </summary> 
    class CachedMessageTemplate : MessageTemplate
    {

        readonly PropertyToken[] _positionalProperties;
        readonly PropertyToken[] _namedProperties;

        public CachedMessageTemplate(string text, IEnumerable<MessageTemplateToken> tokens)
            : base(text, tokens)
        {
            var propertyTokens = tokens.OfType<PropertyToken>().ToArray();
            if (propertyTokens.Length != 0)
            {
                var allPositional = true;
                var anyPositional = false;
                foreach (var propertyToken in propertyTokens)
                {
                    if (propertyToken.IsPositional)
                        anyPositional = true;
                    else
                        allPositional = false;
                }

                if (allPositional)
                {
                    _positionalProperties = propertyTokens;
                }
                else
                {
                    if (anyPositional)
                        SelfLog.WriteLine("Message template is malformed: {0}", text);

                    _namedProperties = propertyTokens;
                }
            }
        }

        public PropertyToken[] NamedProperties
        {
            get { return _namedProperties; }
        }

        public PropertyToken[] PositionalProperties
        {
            get { return _positionalProperties; }
        }
    }
}