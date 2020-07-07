// Copyright 2019 Serilog Contributors
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
using System.Runtime.CompilerServices;

namespace Serilog.Events
{
    /// <summary>
    /// A property associated with a <see cref="LogEvent"/>.
    /// </summary>
    /// <remarks>This type is currently internal, while we consider future directions for the logging pipeline, but should end up public
    /// in future.</remarks>
    readonly struct EventProperty
    {
        /// <summary>
        /// No property.
        /// </summary>
        public static EventProperty None = default;
        
        /// <summary>
        /// The name of the property.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The value of the property.
        /// </summary>
        public LogEventPropertyValue Value { get; }

        /// <summary>
        /// Construct a <see cref="LogEventProperty"/> with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="name"/> is <code>null</code></exception>
        /// <exception cref="ArgumentException">When <paramref name="name"/> is empty or only contains whitespace</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="value"/> is <code>null</code></exception>
        public EventProperty(string name, LogEventPropertyValue value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            LogEventProperty.EnsureValidName(name);

            Name = name;
            Value = value;
        }

        /// <summary>
        /// Permit deconstruction of the property into a name/value pair.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        public void Deconstruct(out string name, out LogEventPropertyValue value)
        {
            name = Name;
            value = Value;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is EventProperty other && Equals(other);
        }

        /// <summary>Indicates whether this instance and a specified <see cref="EventProperty"/> are equal.</summary>
        /// <param name="other">The <see cref="EventProperty"/> to compare with the current instance. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="other" /> and this instance represent the same value; otherwise, <see langword="false" />. </returns>
        public bool Equals(in EventProperty other)
        {
            return string.Equals(Name, other.Name) && Equals(Value, other.Value);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }
    }
}
