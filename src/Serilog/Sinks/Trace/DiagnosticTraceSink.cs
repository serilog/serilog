using System;
using System.IO;
using Serilog.Core;

namespace Serilog.Sinks.Trace
{
    class DiagnosticTraceSink : ILogEventSink
    {
        const string DefaultOutputTemplate = "[{Level}] {Message}{NewLine}";

        private readonly IMessageTemplateRepository _messageTemplateRepository;
        private readonly MessageTemplate _outputTemplate;

        public DiagnosticTraceSink(IMessageTemplateRepository messageTemplateRepository)
        {
            if (messageTemplateRepository == null) throw new ArgumentNullException("messageTemplateRepository");
            _messageTemplateRepository = messageTemplateRepository;
            _outputTemplate = _messageTemplateRepository.GetParsedTemplate(DefaultOutputTemplate);
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            var outputProperties = OutputProperties.GetOutputProperties(logEvent, _messageTemplateRepository);
            var sr = new StringWriter();
            _outputTemplate.Render(outputProperties, sr);
            System.Diagnostics.Trace.Write(sr.ToString());
        }
    }
}
