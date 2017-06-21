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
using Serilog.Rendering;

namespace Serilog.Formatting.Display.Obsolete
{
    // A special case (non-null) string value for use in output
    // templates. Does not apply "quoted" formatting by default.
    [Obsolete("Not used by the current output formatting implementation.")]
    class LiteralStringValue : LogEventPropertyValue
    {
        readonly string _value;

        public LiteralStringValue(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override void Render(TextWriter output, string format = null, IFormatProvider formatProvider = null)
        {
            output.Write(Casing.Format(_value, format));
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
