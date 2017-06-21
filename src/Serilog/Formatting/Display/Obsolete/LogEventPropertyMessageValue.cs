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
using Serilog.Events;

namespace Serilog.Formatting.Display.Obsolete
{
    [Obsolete("Not used by the current output formatting implementation.")]
    class LogEventPropertyMessageValue : LogEventPropertyValue
    {
        readonly MessageTemplate _template;
        readonly IReadOnlyDictionary<string, LogEventPropertyValue> _properties;

        public LogEventPropertyMessageValue(MessageTemplate template, IReadOnlyDictionary<string, LogEventPropertyValue> properties)
        {
            _template = template;
            _properties = properties;
        }

        public override void Render(TextWriter output, string format = null, IFormatProvider formatProvider = null)
        {
            _template.Render(_properties, output, formatProvider);
        }
    }
}
