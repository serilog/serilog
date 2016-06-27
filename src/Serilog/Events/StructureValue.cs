using System;
using System.Collections.Generic;
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

using System.IO;
using System.Linq;

namespace Serilog.Events
{
    /// <summary>
    /// A value represented as a collection of name-value properties.
    /// </summary>
    public class StructureValue : LogEventPropertyValue
    {
        readonly LogEventProperty[] _properties;

        /// <summary>
        /// Construct a <see cref="StructureValue"/> with the provided properties.
        /// </summary>
        /// <param name="typeTag">Optionally, a piece of metadata describing the "type" of the
        /// structure.</param>
        /// <param name="properties">The properties of the structure.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public StructureValue(IEnumerable<LogEventProperty> properties, string typeTag = null)
        {
            if (properties == null) throw new ArgumentNullException(nameof(properties));
            TypeTag = typeTag;
            _properties = properties.ToArray();
        }

        /// <summary>
        /// A piece of metadata describing the "type" of the
        /// structure, or null.
        /// </summary>
        public string TypeTag { get; }

        /// <summary>
        /// The properties of the structure.
        /// </summary>
        /// <remarks>Not presented as a dictionary because dictionary construction is
        /// relatively expensive; it is cheaper to build a dictionary over properties only
        /// when the structure is of interest.</remarks>
        public IReadOnlyList<LogEventProperty> Properties => _properties;

        /// <summary>
        /// Render the value to the output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="format">A format string applied to the value, or null.</param>
        /// <param name="formatProvider">A format provider to apply to the value, or null to use the default.</param>
        /// <seealso cref="LogEventPropertyValue.ToString(string, IFormatProvider)"/>.
        public override void Render(TextWriter output, string format = null, IFormatProvider formatProvider = null)
        {
            if (output == null) throw new ArgumentNullException(nameof(output));

            if (TypeTag != null)
            {
                output.Write(TypeTag);
                output.Write(' ');
            }
            output.Write("{ ");
            var allButLast = _properties.Length - 1;
            for (var i = 0; i < allButLast; i++)
            {
                var property = _properties[i];
                Render(output, property, formatProvider);
                output.Write(", ");
            }

            if (_properties.Length > 0)
            {
                var last = _properties[_properties.Length - 1];
                Render(output, last, formatProvider);
            }

            output.Write(" }");
        }

        static void Render(TextWriter output, LogEventProperty property, IFormatProvider formatProvider = null)
        {
            output.Write(property.Name);
            output.Write(": ");
            property.Value.Render(output, null, formatProvider);
        }
    }
}