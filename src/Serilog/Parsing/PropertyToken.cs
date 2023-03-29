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

namespace Serilog.Parsing;

/// <summary>
/// A message template token representing a log event property.
/// </summary>
public sealed class PropertyToken : MessageTemplateToken
{
    readonly int? _position;

    /// <summary>
    /// Construct a <see cref="PropertyToken"/>.
    /// </summary>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="rawText">The token as it appears in the message template.</param>
    /// <param name="format">The format applied to the property, if any.</param>
    /// <param name="alignment">The alignment applied to the property, if any.</param>
    /// <param name="destructuring">The destructuring strategy applied to the property, if any.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="propertyName"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="rawText"/> is <code>null</code></exception>
    public PropertyToken(string propertyName, string rawText, string? format = null, in Alignment? alignment = null, Destructuring destructuring = Destructuring.Default)
    {
        PropertyName = Guard.AgainstNull(propertyName);
        Format = format;
        Destructuring = destructuring;
        RawText = Guard.AgainstNull(rawText);
        Alignment = alignment;

        if (int.TryParse(PropertyName, NumberStyles.None, CultureInfo.InvariantCulture, out var position) &&
            position >= 0)
        {
            _position = position;
        }
    }

    /// <summary>
    /// The token's length.
    /// </summary>
    public override int Length => RawText.Length;

    /// <summary>
    /// Render the token to the output.
    /// </summary>
    /// <param name="properties">Properties that may be represented by the token.</param>
    /// <param name="output">Output for the rendered string.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="properties"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="output"/> is <code>null</code></exception>
    public override void Render(IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output, IFormatProvider? formatProvider = null)
    {
        Guard.AgainstNull(properties);
        Guard.AgainstNull(output);

        MessageTemplateRenderer.RenderPropertyToken(this, properties, output, formatProvider, isLiteral: false, isJson: false);
    }

    /// <summary>
    /// The property name.
    /// </summary>
    public string PropertyName { get; }

    /// <summary>
    /// Destructuring strategy applied to the property.
    /// </summary>
    public Destructuring Destructuring { get; }

    /// <summary>
    /// Format applied to the property.
    /// </summary>
    public string? Format { get; }

    /// <summary>
    /// Alignment applied to the property.
    /// </summary>
    public Alignment? Alignment { get; }

    /// <summary>
    /// <see langword="true"/> if the property name is a positional index; otherwise, <see langword="false"/>.
    /// </summary>
    public bool IsPositional => _position.HasValue;

    internal string RawText { get; }

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
    /// <see langword="true"/> if the specified object  is equal to the current object; otherwise, <see langword="false"/>.
    /// </returns>
    /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
    public override bool Equals(object? obj)
    {
        return obj is PropertyToken pt &&
               pt.Destructuring == Destructuring &&
               pt.Format == Format &&
               pt.PropertyName == PropertyName &&
               pt.RawText == RawText;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public override int GetHashCode() => PropertyName.GetHashCode();

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>
    /// A string that represents the current object.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString() => RawText;
}
