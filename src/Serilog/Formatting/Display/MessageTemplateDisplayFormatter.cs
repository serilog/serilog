using System;
using System.IO;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Formatting.Display
{
    class MessageTemplateTextFormatter : ITextFormatter
    {
        private readonly MessageTemplate _outputTemplate;
        readonly IMessageTemplateRepository _messageTemplateRepository;

        public MessageTemplateTextFormatter(string outputTemplate, IMessageTemplateRepository messageTemplateRepository)
        {
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            if (messageTemplateRepository == null) throw new ArgumentNullException("messageTemplateRepository");
            _messageTemplateRepository = messageTemplateRepository;
            _outputTemplate = _messageTemplateRepository.GetParsedTemplate(outputTemplate);
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (output == null) throw new ArgumentNullException("output");
            var outputProperties = OutputProperties.GetOutputProperties(logEvent, _messageTemplateRepository);
            _outputTemplate.Render(outputProperties, output);            
        }
    }
}
