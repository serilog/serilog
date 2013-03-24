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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Serilog.Events;

namespace Serilog.Parsing
{
    public class LogEventPropertyToken : MessageTemplateToken
    {
        private readonly string _propertyName;
        private readonly string _format;
        private readonly Destructuring _destructuring;
        private readonly string _rawText;

        public LogEventPropertyToken(string propertyName, string rawText, string format = null, Destructuring destructuring = Destructuring.Default)
        {
            if (propertyName == null) throw new ArgumentNullException("propertyName");
            if (rawText == null) throw new ArgumentNullException("rawText");
            _propertyName = propertyName;
            _format = format;
            _destructuring = destructuring;
            _rawText = rawText;
        }

        public override void Render(IReadOnlyDictionary<string, LogEventProperty> properties, TextWriter output)
        {
            if (properties == null) throw new ArgumentNullException("properties");
            if (output == null) throw new ArgumentNullException("output");
            LogEventProperty property;
            if (properties.TryGetValue(_propertyName, out property))
                property.Value.Render(output, _format);
            else
                output.Write(_rawText);
        }

        public string PropertyName { get { return _propertyName; } }

        public Destructuring Destructuring { get { return _destructuring; } }
        
        public bool IsPositional
        {
            get { return _propertyName.All(char.IsNumber); }
        }

        public bool TryGetPositionalValue(out int position)
        {
            return
                int.TryParse(_propertyName, NumberStyles.None, CultureInfo.InvariantCulture, out position) &&
                position >= 0;
        }

        public override bool Equals(object obj)
        {
            var pt = obj as LogEventPropertyToken;
            return pt != null &&
                pt._destructuring == _destructuring &&
                pt._format == _format &&
                pt._propertyName == _propertyName &&
                pt._rawText == _rawText;
        }

        public override int GetHashCode()
        {
            return _propertyName.GetHashCode();
        }

        public override string ToString()
        {
            return _rawText;
        }
    }
}