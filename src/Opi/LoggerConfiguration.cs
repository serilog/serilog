using System;
using System.Collections.Generic;
using Opi.Core;
using Opi.Sinks;

namespace Opi
{
    public class LoggerConfiguration
    {
        readonly IList<ILogEventSink> _logEventSinks = new List<ILogEventSink>();
        readonly IList<ILogEventEnricher> _enrichers = new List<ILogEventEnricher>(); 
        readonly IMessageTemplateRepository _messageTemplateRepository = new MessageTemplateRepository();

        LogEventLevel _minimumLevel = LogEventLevel.Information;

        public LoggerConfiguration WithConsoleSink()
        {
            return WithSink(new ConsoleSink(_messageTemplateRepository));
        }

        private LoggerConfiguration WithSink(ILogEventSink logEventSink)
        {
            _logEventSinks.Add(logEventSink);
            return this;
        }

        public LoggerConfiguration MinimumLevel(LogEventLevel minimumLevel)
        {
            _minimumLevel = minimumLevel;
            return this;
        }

        public LoggerConfiguration EnrichedBy(ILogEventEnricher enricher)
        {
            if (enricher == null) throw new ArgumentNullException("enricher");
            _enrichers.Add(enricher);
            return this;
        }

        public ILogger CreateLogger()
        {
            return new Logger(_messageTemplateRepository, _minimumLevel, new AggregateSink(_logEventSinks), _enrichers);
        }

        public LoggerConfiguration WithDumpFile(string path)
        {
            return WithSink(new DumpFileSink(path));
        }

        public LoggerConfiguration WithFixedProperty(string propertyName, object value)
        {
            return EnrichedBy(new FixedPropertyEnricher(new[] { new LogEventProperty(propertyName, LogEventPropertyValue.For(value)) }));
        }
    }
}
