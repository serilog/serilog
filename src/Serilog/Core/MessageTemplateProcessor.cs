using System.Collections.Generic;
using Serilog.Events;
using Serilog.Parameters;
using Serilog.Parsing;

namespace Serilog.Core
{
    class MessageTemplateProcessor
    {
        readonly IMessageTemplateParser _parser = new MessageTemplateCache(new MessageTemplateParser());
        readonly ParameterMatcher _parameterMatcher = new ParameterMatcher();

        public void Process(string messageTemplate, object[] messageTemplateParameters, out MessageTemplate parsedTemplate, out IEnumerable<LogEventProperty> properties)
        {
            parsedTemplate = _parser.Parse(messageTemplate);
            properties = _parameterMatcher.ConstructProperties(parsedTemplate, messageTemplateParameters);
        }
    }
}
