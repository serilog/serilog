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

namespace Serilog.Events;

/// <summary>
/// A property value corresponding to a simple, scalar type.
/// </summary>
public class ScalarValue : LogEventPropertyValue
{
    /// <summary>
    /// Scalar value representing <see langword="null"/>.
    /// </summary>
    public static ScalarValue Null { get; } = new(null);

    /// <summary>
    /// Construct a <see cref="ScalarValue"/> with the specified
    /// value.
    /// </summary>
    /// <param name="value">The value, which may be <code>null</code>.</param>
    public ScalarValue(object? value)
    {
        Value = value;
    }

    /// <summary>
    /// The value, which may be <code>null</code>.
    /// </summary>
    public object? Value { get; }

    /// <summary>
    /// Render the value to the output.
    /// </summary>
    /// <param name="output">The output.</param>
    /// <param name="format">A format string applied to the value, or null.</param>
    /// <param name="formatProvider">A format provider to apply to the value, or null to use the default.</param>
    /// <seealso cref="LogEventPropertyValue.ToString(string, IFormatProvider)"/>.
    /// <exception cref="ArgumentNullException">When <paramref name="output"/> is <code>null</code></exception>
    public override void Render(TextWriter output, string? format = null, IFormatProvider? formatProvider = null)
    {
        Render(Value, output, format, formatProvider);
    }

    /// <exception cref="ArgumentNullException">When <paramref name="output"/> is <code>null</code></exception>
    internal static void Render(object? value, TextWriter output, string? format = null, IFormatProvider? formatProvider = null)
    {
        Guard.AgainstNull(output);

        if (value == null)
        {
            output.Write("null");
            return;
        }

        if (value is string s)
        {
            if (format != "l")
            {
                output.Write('"');
                output.Write(s.Replace("\"", "\\\""));
                output.Write('"');
            }
            else
            {
                output.Write(s);
            }
            return;
        }

        var custom = (ICustomFormatter?)formatProvider?.GetFormat(typeof(ICustomFormatter));
        if (custom != null)
        {
            output.Write(custom.Format(format, value, formatProvider));
            return;
        }

        if (value is IFormattable f)
        {
            output.Write(f.ToString(format, formatProvider ?? CultureInfo.InvariantCulture));
        }
        else
        {
            output.Write(value.ToString());
        }
    }

#if FEATURE_SPAN
    internal static void Render<T>(T value, TextWriter output, string? format = null, IFormatProvider? formatProvider = null) where T : struct, IFormattable, ISpanFormattable
    {
        Guard.AgainstNull(output);

        var custom = (ICustomFormatter?)formatProvider?.GetFormat(typeof(ICustomFormatter));
        if (custom != null)
        {
            WriteWithCustomFormatter(value, output, format, formatProvider, custom);
            return;
        }

        formatProvider ??= CultureInfo.InvariantCulture;
        if (TryWriteSpanFormattable(value, output, format, formatProvider))
            return;

        output.Write(value.ToString(format, formatProvider));
    }

    static void WriteWithCustomFormatter<T>(T value, TextWriter output, string? format, IFormatProvider? formatProvider, ICustomFormatter custom) where T : struct, IFormattable
    {
        output.Write(custom.Format(format, value, formatProvider));
    }

    static bool TryWriteSpanFormattable<T>(T value, TextWriter output, string? format, IFormatProvider formatProvider) where T : struct, ISpanFormattable
    {
        Span<char> buffer = stackalloc char[36]; // large enough to fit ISO 8601 timestamp (DateTimeOffset.ToString("O"))
        if (value.TryFormat(buffer, out var charsWritten, format, formatProvider))
        {
            output.Write(buffer[..charsWritten]);
            return true;
        }

        return false;
    }
#endif

    /// <summary>
    /// Determine if this instance is equal to <paramref name="obj"/>.
    /// </summary>
    /// <param name="obj">The instance to compare with.</param>
    /// <returns><see langword="true"/> if the instances are equal; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is ScalarValue sv && Equals(Value, sv.Value);
    }

    /// <summary>
    /// Get a hash code representing the value.
    /// </summary>
    /// <returns>The instance's hash code.</returns>
    public override int GetHashCode()
    {
        if (Value == null) return 0;
        return Value.GetHashCode();
    }
}
