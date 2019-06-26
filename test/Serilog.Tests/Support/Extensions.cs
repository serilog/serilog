using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Serilog.Capturing;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    public static class Extensions
    {
        public static object LiteralValue(this LogEventPropertyValue @this)
        {
            return ((ScalarValue)@this).Value;
        }

        internal static EventProperty[] ConstructProperties(this PropertyBinder binder, MessageTemplate messageTemplate, object[] messageTemplateParameters)
        {
            var properties = new PropertiesCollector();
            binder.ConstructProperties(messageTemplate, messageTemplateParameters, properties);
            return properties.Properties.ToArray();
        }

        class PropertiesCollector : IDictionary<string, LogEventPropertyValue>
        {
            public List<EventProperty> Properties = new List<EventProperty>();

            IEnumerator<KeyValuePair<string, LogEventPropertyValue>> IEnumerable<KeyValuePair<string, LogEventPropertyValue>>.GetEnumerator() => throw new NotImplementedException();

            IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

            void ICollection<KeyValuePair<string, LogEventPropertyValue>>.Add(KeyValuePair<string, LogEventPropertyValue> item) => throw new NotImplementedException();

            void ICollection<KeyValuePair<string, LogEventPropertyValue>>.Clear() => throw new NotImplementedException();

            bool ICollection<KeyValuePair<string, LogEventPropertyValue>>.Contains(KeyValuePair<string, LogEventPropertyValue> item) => throw new NotImplementedException();

            void ICollection<KeyValuePair<string, LogEventPropertyValue>>.CopyTo(KeyValuePair<string, LogEventPropertyValue>[] array, int arrayIndex) => throw new NotImplementedException();

            bool ICollection<KeyValuePair<string, LogEventPropertyValue>>.Remove(KeyValuePair<string, LogEventPropertyValue> item) => throw new NotImplementedException();

            int ICollection<KeyValuePair<string, LogEventPropertyValue>>.Count => throw new NotImplementedException();

            bool ICollection<KeyValuePair<string, LogEventPropertyValue>>.IsReadOnly => false;

            void IDictionary<string, LogEventPropertyValue>.Add(string key, LogEventPropertyValue value)
            {
                Properties.Add(new EventProperty(key, value));
            }

            bool IDictionary<string, LogEventPropertyValue>.ContainsKey(string key) => throw new NotImplementedException();

            bool IDictionary<string, LogEventPropertyValue>.Remove(string key) => throw new NotImplementedException();

            bool IDictionary<string, LogEventPropertyValue>.TryGetValue(string key, out LogEventPropertyValue value) => throw new NotImplementedException();

            LogEventPropertyValue IDictionary<string, LogEventPropertyValue>.this[string key]
            {
                get => throw new NotImplementedException();
                set => throw new NotImplementedException();
            }

            ICollection<string> IDictionary<string, LogEventPropertyValue>.Keys => throw new NotImplementedException();
            ICollection<LogEventPropertyValue> IDictionary<string, LogEventPropertyValue>.Values => throw new NotImplementedException();
        }
    }
}
