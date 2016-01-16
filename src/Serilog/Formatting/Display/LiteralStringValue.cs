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
using System.IO;
using Serilog.Events;

namespace Serilog.Formatting.Display
{
    // A special case (non-null) string value for use in output
    // templates. Does not apply "quoted" formatting by default.
    class LiteralStringValue : LogEventPropertyValue
    {
        readonly string _value;

        public LiteralStringValue(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _value = value;
        }

        public override void Render(TextWriter output, string format = null, IFormatProvider formatProvider = null)
        {
            var toRender = _value;

            switch (format)
            {
                case "u":
                    toRender = _value.ToUpperInvariant();
                    break;
                case "w":
                    toRender = _value.ToLowerInvariant();
                    break;
            }

            output.Write(toRender);
        }

        public override bool Equals(object obj)
        {
            var sv = obj as LiteralStringValue;
            return sv != null && Equals(_value, sv._value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
