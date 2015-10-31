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
using System.Globalization;
using System.IO;

namespace Serilog.Events
{
    /// <summary>
    /// A property value corresponding to a simple, scalar type.
    /// </summary>
    public class ScalarValue : LogEventPropertyValue
    {
        readonly object _value;

        /// <summary>
        /// Construct a <see cref="ScalarValue"/> with the specified
        /// value.
        /// </summary>
        /// <param name="value">The value, which may be <code>null</code>.</param>
        public ScalarValue(object value)
        {
            _value = value;
        }

        /// <summary>
        /// The value, which may be <code>null</code>.
        /// </summary>
        public object Value { get { return _value; } }

        /// <summary>
        /// Render the value to the output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="format">A format string applied to the value, or null.</param>
        /// <param name="formatProvider">A format provider to apply to the value, or null to use the default.</param>
        /// <seealso cref="LogEventPropertyValue.ToString(string, IFormatProvider)"/>.
        public override void Render(TextWriter output, string format = null, IFormatProvider formatProvider = null)
        {
            if (output == null) throw new ArgumentNullException("output");

            if (_value == null)
            {
                output.Write("null");
            }
            else
            {
                var s = _value as string;
                if (s != null)
                {
                    if (format != "l")
                    {
                        output.Write("\"");
                        output.Write(s.Replace("\"", "\\\""));
                        output.Write("\"");
                    }
                    else
                    {
                        output.Write(s);
                    }
                }
                else
                {
                    var f = _value as IFormattable;
                    if (f != null)
                    {
                        output.Write(f.ToString(format, formatProvider ?? CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        output.Write(_value.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Determine if this instance is equal to <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The instance to compare with.</param>
        /// <returns>True if the instances are equal; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var sv = obj as ScalarValue;
            if (sv == null) return false;
            return Equals(_value, sv._value);
        }

        /// <summary>
        /// Get a hash code representing the value.
        /// </summary>
        /// <returns>The instance's hash code.</returns>
        public override int GetHashCode()
        {
            if (_value == null) return 0;
            return _value.GetHashCode();
        }
    }
}