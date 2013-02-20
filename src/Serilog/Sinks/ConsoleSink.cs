using System;
using Serilog.Core;

namespace Serilog.Sinks
{
    class ConsoleSink : ILogEventSink
    {
        private readonly IMessageTemplateRepository _messageTemplateRepository;

        public ConsoleSink(IMessageTemplateRepository messageTemplateRepository)
        {
            if (messageTemplateRepository == null) throw new ArgumentNullException("messageTemplateRepository");
            _messageTemplateRepository = messageTemplateRepository;
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            var template = _messageTemplateRepository.GetParsedTemplate(logEvent.MessageTemplate);

            var output = Console.Out;
            output.Write(logEvent.TimeStamp + " " + logEvent.Level + " ");
            template.Render(logEvent.Properties, output);
            output.WriteLine();
        }
    }
}
