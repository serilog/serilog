using System;
using System.Collections.Generic;
using System.IO;
using Serilog.Formatting.Json;

namespace Serilog.Events
{
    /// <summary>
    /// A property value corresponding to a simple, scalar type.
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    public class ScalarValue<T> : ScalarValue
    {
        readonly T _value;

        /// <summary>
        /// Construct a <see cref="ScalarValue"/> with the specified value.
        /// </summary>
        /// <param name="value">The value, which may be <code>null</code>.</param>
        public ScalarValue(T value)
            : base(NotBoxedYet)
        {
            _value = value;
        }

        /// <summary>
        /// Render the value to the output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="format">A format string applied to the value, or null.</param>
        /// <param name="formatProvider">A format provider to apply to the value, or null to use the default.</param>
        /// <seealso cref="LogEventPropertyValue.ToString(string, IFormatProvider)"/>.
        public override void Render(TextWriter output, string format = null, IFormatProvider formatProvider = null)
        {
            Render(_value, output, format, formatProvider);
        }

        /// <summary>
        /// Determine if this instance is equal to <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The instance to compare with.</param>
        /// <returns>True if the instances are equal; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is ScalarValue<T> sv && EqualityComparer<T>.Default.Equals(_value, sv._value);
        }

        /// <summary>
        /// Get a hash code representing the value.
        /// </summary>
        /// <returns>The instance's hash code.</returns>
        public override int GetHashCode() => _value.GetHashCode();

        /// <inheritdoc />
        protected override object GetValue(ref object value)
        {
            if (value == NotBoxedYet)
            {
                // value has not been boxed yet, box it and store in "value"
                value = _value;
                return value;
            }

            return value;
        }

        /// <summary>
        /// Gets the raw, not-boxed value of this.
        /// </summary>
        public T RawValue => _value;
    }

    /// <summary>
    /// A property value corresponding to a simple, scalar type.
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    public class FormattableScalarValue<T> : ScalarValue<T>, IJsonFormattable
        where T :IFormattable
    {
        /// <summary>
        /// Construct a <see cref="ScalarValue"/> with the specified value.
        /// </summary>
        /// <param name="value">The value, which may be <code>null</code>.</param>
        public FormattableScalarValue(T value) : base(value)
        {
        }

        void IJsonFormattable.Write(TextWriter output)
        {
            JsonValueFormatter.FormatExactNumericValue(RawValue, output);
        }
    }
}
