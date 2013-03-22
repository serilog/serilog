using System;
using System.IO;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Formatting.Display
{
    class MessageTemplateTextFormatter : ITextFormatter
    {
        private readonly MessageTemplate _outputTemplate;
        readonly IMessageTemplateCache _messageTemplateCache;

        public MessageTemplateTextFormatter(string outputTemplate, IMessageTemplateCache messageTemplateCache)
        {
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            if (messageTemplateCache == null) throw new ArgumentNullException("messageTemplateCache");
            _messageTemplateCache = messageTemplateCache;
            _outputTemplate = _messageTemplateCache.GetParsedTemplate(outputTemplate);
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (output == null) throw new ArgumentNullException("output");
            var outputProperties = OutputProperties.GetOutputProperties(logEvent, _messageTemplateCache);
            _outputTemplate.Render(outputProperties, output);            
        }
    }
}
