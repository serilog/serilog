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
using Serilog.Parsing;

#if NET40
using IPropertyDictionary = System.Collections.Generic.IDictionary<string, Serilog.Events.LogEventPropertyValue>;
#else
using IPropertyDictionary = System.Collections.Generic.IReadOnlyDictionary<string, Serilog.Events.LogEventPropertyValue>;
#endif

namespace Serilog.Events
{
    public interface IMessageTemplate
    {
        /// <summary>
        /// Convert the message template into a textual message, given the
        /// properties matching the tokens in the message template.
        /// </summary>
        /// <param name="properties">Properties matching template tokens.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>The message created from the template and properties. If the
        /// properties are mismatched with the template, the template will be
        /// returned with incomplete substitution.</returns>
        string Render(IPropertyDictionary properties, IFormatProvider formatProvider = null);

        /// <summary>
        /// Convert the message template into a textual message, given the
        /// properties matching the tokens in the message template.
        /// </summary>
        /// <param name="properties">Properties matching template tokens.</param>
        /// <param name="output">The message created from the template and properties. If the
        /// properties are mismatched with the template, the template will be
        /// returned with incomplete substitution.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        void Render(IPropertyDictionary properties, TextWriter output, IFormatProvider formatProvider = null);
        
        /// <summary>
        /// The tokens parsed from the template.
        /// </summary>
        IEnumerable<MessageTemplateToken> Tokens { get; }
        
        /// <summary>
        /// The raw text describing the template.
        /// </summary>
        string Text { get; }
    }
}