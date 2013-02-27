using System;
using Serilog.Parsing;
using Serilog.Values;

namespace Serilog
{
    public class LogEventProperty
    {
        private readonly string _name;

        public static LogEventProperty For(string name, object value, bool destructureObjects = false)
        {
            return new LogEventProperty(name, LogEventPropertyValue.For(value,
                    destructureObjects ?
                        DestructuringHint.Destructure :
                        DestructuringHint.Default));
        }

        public LogEventProperty(string name, LogEventPropertyValue value)
        {
            if (!IsValidName(name))
                throw new ArgumentException("Property name is not valid.");

            _name = name;
            Value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public LogEventPropertyValue Value { get; set; }

        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
    }
}