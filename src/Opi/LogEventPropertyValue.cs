using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Opi.Values;

namespace Opi
{
    public abstract class LogEventPropertyValue
    {
        internal abstract void Render(TextWriter output, string format = null);

        public static LogEventPropertyValue For(object value)
        {
            if (value == null)
                return new LogEventPropertyNullValue();

            if (value is int)
                return new LogEventPropertyIntValue((int)value);

            if (value is decimal)
                return new LogEventPropertyDecimalValue((decimal)value);

            var s = value as string;
            if (s != null)
                return new LogEventPropertyStringValue(s);

            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                return new LogEventPropertyArrayValue(
                    enumerable.Cast<object>().Select(For));
            }

            var typeTag = value.GetType().Name;
            if (typeTag.Length <= 0 || !char.IsLetter(typeTag[0]))
                typeTag = null;

            return new LogEventPropertyObjectValue(
                typeTag,
                GetProperties(value));
        }

        private static IEnumerable<LogEventProperty> GetProperties(object value)
        {
            return value.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                .Select(p => new LogEventProperty(p.Name, For(p.GetValue(value))));
        }
    }
}
