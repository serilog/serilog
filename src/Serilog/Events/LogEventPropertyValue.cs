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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Serilog.Parsing;

namespace Serilog.Events
{
    /// <summary>
    /// The value associated with a <see cref="LogEventProperty"/>. Divided into scalar,
    /// sequence and structure values to direct serialization into various formats.
    /// </summary>
    public abstract class LogEventPropertyValue : IFormattable
    {
        static readonly HashSet<Type> KnownLiteralTypes = new HashSet<Type>
            {
                typeof(bool),
                typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint),
                    typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal),
                typeof(string),
                typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan)
            };

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

        /// <summary>
        /// Create a property value from a .NET object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="destructuring">Directs the algorithm for determining how the
        /// object will be represented (e.g. sclar, sequence, structure).</param>
        /// <returns></returns>
        public static LogEventPropertyValue For(object value, Destructuring destructuring)
        {
            if (value == null)
                return new ScalarValue(null);

            if (destructuring == Destructuring.Stringify)
                return new ScalarValue(value.ToString());

            // Known literals
            var valueType = value.GetType();
            if (KnownLiteralTypes.Contains(valueType) || valueType.IsEnum)
                return new ScalarValue(value);

            // Dictionaries should be treated here, probably as
            // structures...

            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                return new SequenceValue(
                    enumerable.Cast<object>().Select(o => For(o, destructuring)));
            }

            // Unknown types

            if (destructuring == Destructuring.Destructure)
            {
                var typeTag = value.GetType().Name;
                if (typeTag.Length <= 0 || !char.IsLetter(typeTag[0]))
                    typeTag = null;

                return new StructureValue(GetProperties(value, destructuring), typeTag);
            }

            return new ScalarValue(value);
        }

        private static IEnumerable<LogEventProperty> GetProperties(object value, Destructuring destructuring)
        {
            return value.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                .Select(p => new LogEventProperty(p.Name, For(p.GetValue(value), destructuring)));
        }
    }
}
