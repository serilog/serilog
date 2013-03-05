using System;
using Serilog.Core;
using Serilog.Display;
using Serilog.Events;

namespace Serilog.Sinks.SystemConsole
{
    class ConsoleSink : ILogEventSink
    {
        const string DefaultOutputTemplate = "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}";

        private readonly IMessageTemplateRepository _messageTemplateRepository;
        private readonly MessageTemplate _outputTemplate;

        public ConsoleSink(IMessageTemplateRepository messageTemplateRepository)
        {
            if (messageTemplateRepository == null) throw new ArgumentNullException("messageTemplateRepository");
            _messageTemplateRepository = messageTemplateRepository;
            _outputTemplate = _messageTemplateRepository.GetParsedTemplate(DefaultOutputTemplate);
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            var outputProperties = OutputProperties.GetOutputProperties(logEvent, _messageTemplateRepository);
            _outputTemplate.Render(outputProperties, Console.Out);
        }
    }
}
