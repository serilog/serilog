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

namespace Serilog.Events
{
    /// <summary>
    /// The value associated with a <see cref="LogEventProperty"/>. Divided into scalar,
    /// sequence and structure values to direct serialization into various formats.
    /// </summary>
    public abstract class LogEventPropertyValue : IFormattable
    {
        /// <summary>
        /// Render the value to the output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="format">A format string applied to the value, or null.</param>
        /// <param name="formatProvider">A format provider to apply to the value, or null to use the default.</param>
        /// <seealso cref="LogEventPropertyValue.ToString(string, IFormatProvider)"/>.
        public abstract void Render(TextWriter output, string format = null, IFormatProvider formatProvider = null);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return ToString(null, null);
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <returns>
        /// The value of the current instance in the specified format.
        /// </returns>
        /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use
        /// the default format defined for the type of the <see cref="T:System.IFormattable"/> implementation. </param>
        /// <param name="formatProvider">The provider to use to format the value.-or- A null reference
        /// (Nothing in Visual Basic) to obtain the numeric format information from the current locale
        /// setting of the operating system. </param><filterpriority>2</filterpriority>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var output = new StringWriter();
            Render(output, format, formatProvider);
            return output.ToString();
        }
    }
}
