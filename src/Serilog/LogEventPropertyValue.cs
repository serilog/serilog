using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Serilog.Parsing;
using Serilog.Values;

namespace Serilog
{
    public abstract class LogEventPropertyValue
    {
        internal abstract void Render(TextWriter output, string format = null);

        internal static LogEventPropertyValue For(object value, DestructuringHint destructuringHint)
        {
            if (value == null)
                return new LogEventPropertyNullValue();

            if (destructuringHint == DestructuringHint.Stringify)
                return new LogEventPropertyStringValue(value.ToString());

            // Known literals

            if (value is int || value is uint || value is long || value is short || value is ushort)
                return new LogEventPropertyLongValue((long)Convert.ChangeType(value, typeof(long)));

            if (value is bool)
                return new LogEventPropertyBooleanValue((bool)value);

            if (value is decimal || value is float || value is double)
                return new LogEventPropertyDecimalValue((decimal)Convert.ChangeType(value, typeof(decimal)));

            var s = value as string;
            if (s != null)
                return new LogEventPropertyStringValue(s);

            if (value is DateTimeOffset)
                return new LogEventPropertyDateTimeOffsetValue((DateTimeOffset)value);

            if (value is DateTime)
                return new LogEventPropertyDateTimeValue((DateTime)value);

            if (value is TimeSpan)
                return new LogEventPropertyTimeSpanValue((TimeSpan)value);

            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                return new LogEventPropertyArrayValue(
                    enumerable.Cast<object>().Select(o => For(o, destructuringHint)));
            }

            // Unknown types

            if (destructuringHint == DestructuringHint.Destructure)
            {
                var typeTag = value.GetType().Name;
                if (typeTag.Length <= 0 || !char.IsLetter(typeTag[0]))
                    typeTag = null;

                return new LogEventPropertyObjectValue(
                    typeTag,
                    GetProperties(value, destructuringHint));
            }

            return new LogEventPropertyTokenValue(value.ToString());
        }

        private static IEnumerable<LogEventProperty> GetProperties(object value, DestructuringHint destructuringHint)
        {
            return value.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                .Select(p => new LogEventProperty(p.Name, For(p.GetValue(value), destructuringHint)));
        }
    }
}
