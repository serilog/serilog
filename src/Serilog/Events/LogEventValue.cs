using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Serilog.Parsing;

namespace Serilog.Events
{
    public abstract class LogEventPropertyValue
    {
        internal abstract void Render(TextWriter output, string format = null);

        static readonly HashSet<Type> KnownLiteralTypes = new HashSet<Type>
            {
                typeof(bool),
                typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint),
                    typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal),
                typeof(string),
                typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan)
            }; 

        internal static LogEventPropertyValue For(object value, DestructuringHint destructuringHint)
        {
            if (value == null)
                return new LogEventPropertyLiteralValue(null);

            if (destructuringHint == DestructuringHint.Stringify)
                return new LogEventPropertyLiteralValue(value.ToString());

            // Known literals
            var valueType = value.GetType();
            if (KnownLiteralTypes.Contains(valueType) || valueType.IsEnum)
                return new LogEventPropertyLiteralValue(value);

            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                return new LogEventPropertySequenceValue(
                    enumerable.Cast<object>().Select(o => For(o, destructuringHint)));
            }

            // Unknown types

            if (destructuringHint == DestructuringHint.Destructure)
            {
                var typeTag = value.GetType().Name;
                if (typeTag.Length <= 0 || !char.IsLetter(typeTag[0]))
                    typeTag = null;

                return new LogEventPropertyStructureValue(
                    typeTag,
                    GetProperties(value, destructuringHint));
            }

            return new LogEventPropertyLiteralValue(value);
        }

        private static IEnumerable<LogEventProperty> GetProperties(object value, DestructuringHint destructuringHint)
        {
            return value.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                .Select(p => new LogEventProperty(p.Name, For(p.GetValue(value), destructuringHint)));
        }
    }
}
