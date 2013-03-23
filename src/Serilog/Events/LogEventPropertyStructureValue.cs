using System;
using System.Collections.Generic;
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

using System.IO;
using System.Linq;

namespace Serilog.Events
{
    public class LogEventPropertyStructureValue : LogEventPropertyValue
    {
        private readonly string _typeTag;
        private readonly LogEventProperty[] _properties;

        public LogEventPropertyStructureValue(string typeTag, IEnumerable<LogEventProperty> properties)
        {
            if (properties == null) throw new ArgumentNullException("properties");
            _typeTag = typeTag;
            _properties = properties.ToArray();
        }

        public string TypeTag { get { return _typeTag; } }

        public LogEventProperty[] Properties { get { return _properties; } }

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