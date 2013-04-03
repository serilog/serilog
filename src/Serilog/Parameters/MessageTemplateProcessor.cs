using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Parameters
{
    class MessageTemplateProcessor : ILogEventPropertyFactory
    {
        readonly IMessageTemplateParser _parser = new MessageTemplateCache(new MessageTemplateParser());
        readonly PropertyBinder _propertyBinder;
        readonly PropertyValueConverter _propertyValueConverter;

        public MessageTemplateProcessor(PropertyValueConverter propertyValueConverter)
        {
            _propertyValueConverter = propertyValueConverter;
            _propertyBinder = new PropertyBinder(_propertyValueConverter);
        }

        public void Process(string messageTemplate, object[] messageTemplateParameters, out MessageTemplate parsedTemplate, out IEnumerable<LogEventProperty> properties)
        {
            parsedTemplate = _parser.Parse(messageTemplate);
            properties = _propertyBinder.ConstructProperties(parsedTemplate, messageTemplateParameters);
        }

        public LogEventProperty CreateProperty(string name, object value, bool destructureObjects = false)
        {
            return _propertyValueConverter.CreateProperty(name, value, destructureObjects);
        }
    }
}
