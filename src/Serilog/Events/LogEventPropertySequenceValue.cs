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
using System.IO;
using System.Linq;

namespace Serilog.Events
{
    public class LogEventPropertySequenceValue : LogEventPropertyValue
    {
        private readonly LogEventPropertyValue[] _elements;

        public LogEventPropertySequenceValue(IEnumerable<LogEventPropertyValue> elements)
        {
            if (elements == null) throw new ArgumentNullException("elements");
            _elements = elements.ToArray();
        }

        public LogEventPropertyValue[] Elements { get { return _elements; } }

        internal override void Render(TextWriter output, string format = null)
        {
            // Format string to limit length?

            output.Write('[');
            var allButLast = _elements.Length - 1;
            for (var i = 0; i < allButLast; ++i )
            {
                _elements[i].Render(output);
                output.Write(", ");
            }

            if (_elements.Length > 0)
                _elements[_elements.Length - 1].Render(output);

            output.Write(']');
        }
    }
}