using System.Collections.Generic;
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
            var properties = new List<EventProperty>();
            var visitor = new PropertyBinderVisitor(properties);
            binder.ConstructProperties(messageTemplate, messageTemplateParameters, ref visitor);
            return properties.ToArray();
        }

        struct PropertyBinderVisitor : EventProperty.IBoundedPropertyVisitor
        {
            readonly List<EventProperty> _properties;

            public PropertyBinderVisitor(List<EventProperty> properties)
            {
                _properties = properties;
            }

            public void On(EventProperty property) => _properties.Add(property);
        }
    }
}
