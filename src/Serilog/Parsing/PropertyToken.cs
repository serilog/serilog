// Copyright 2014 Serilog Contributors
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
using System.Globalization;
using System.IO;
using Serilog.Events;

namespace Serilog.Parsing
{
    /// <summary>
    /// A message template token representing a log event property.
    /// </summary>
    public class PropertyToken : MessageTemplateToken
    {
        private readonly string _propertyName;
        private readonly string _format;
        private readonly int _alignment;
        private readonly Destructuring _destructuring;
        private readonly string _rawText;
        private readonly int? _position;

        /// <summary>
        /// Construct a <see cref="PropertyToken"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="rawText">The token as it appears in the message template.</param>
        /// <param name="format">The format applied to the property, if any.</param>
        /// <param name="alignment">The alignment applied to the property, if any.</param>
        /// <param name="destructuring">The destructuring strategy applied to the property, if any.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PropertyToken(string propertyName, string rawText, string format = null, int alignment = 0, Destructuring destructuring = Destructuring.Default)
        {
            if (propertyName == null) throw new ArgumentNullException("propertyName");
            if (rawText == null) throw new ArgumentNullException("rawText");
            _propertyName = propertyName;
            _format = format;
            _destructuring = destructuring;
            _rawText = rawText;
            _alignment = alignment;

            int position;
            if (int.TryParse(_propertyName, NumberStyles.None, CultureInfo.InvariantCulture, out position) &&
                position >= 0)
            {
                _position = position;
            }
        }

        /// <summary>
        /// Render the token to the output.
        /// </summary>
        /// <param name="properties">Properties that may be represented by the token.</param>
        /// <param name="output">Output for the rendered string.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public override void Render(IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output, IFormatProvider formatProvider = null)
        {
            if (properties == null) throw new ArgumentNullException("properties");
            if (output == null) throw new ArgumentNullException("output");
            LogEventPropertyValue propertyValue;
            if (properties.TryGetValue(_propertyName, out propertyValue))
                propertyValue.Render(output, _format, _alignment, formatProvider);
            else
                output.Write(_rawText);
        }

        /// <summary>
        /// The property name.
        /// </summary>
        public string PropertyName { get { return _propertyName; } }

        /// <summary>
        /// Destructuring strategy applied to the property.
        /// </summary>
        public Destructuring Destructuring { get { return _destructuring; } }

        /// <summary>
        /// Format applied to the property.
        /// </summary>
        public string Format { get { return _format; } }

        /// <summary>
        /// Alignment applied to the property.
        /// </summary>
        public int Alignment { get { return _alignment; } }
        
        /// <summary>
        /// True if the property name is a positional index; otherwise, false.
        /// </summary>
        public bool IsPositional
        {
            get { return _position != null; }
        }

        /// <summary>
        /// Try to get the integer value represented by the property name.
        /// </summary>
        /// <param name="position">The integer value, if present.</param>
        /// <returns>True if the property is positional, otherwise false.</returns>
        public bool TryGetPositionalValue(out int position)
        {
            if (_position == null)
            {
                position = 0;
                return false;
            }

            position = _position.Value;
            return true;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            var pt = obj as PropertyToken;
            return pt != null &&
                pt._destructuring == _destructuring &&
                pt._format == _format &&
                pt._propertyName == _propertyName &&
                pt._rawText == _rawText;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return _propertyName.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return _rawText;
        }
    }
}