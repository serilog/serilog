using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Sinks;
using Serilog.Sinks.Http;

namespace Serilog
{
    public class LoggerConfiguration
    {
        readonly IList<ILogEventSink> _logEventSinks = new List<ILogEventSink>();
        readonly IList<ILogEventEnricher> _enrichers = new List<ILogEventEnricher>(); 
        readonly IMessageTemplateRepository _messageTemplateRepository = new MessageTemplateRepository();

        LogEventLevel _minimumLevel = LogEventLevel.Information;

        public LoggerConfiguration WithConsoleSink(LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            return WithSink(new ConsoleSink(_messageTemplateRepository), restrictedToMinimumLevel);
        }

        private LoggerConfiguration WithSink(ILogEventSink logEventSink, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            var sink = logEventSink;
            if (restrictedToMinimumLevel > LogEventLevel.Minimum)
                sink = new RestrictedSink(sink, restrictedToMinimumLevel);

            _logEventSinks.Add(sink);
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

        public LoggerConfiguration WithDumpFile(string path, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            return WithSink(new DumpFileSink(path), restrictedToMinimumLevel);
        }

        public LoggerConfiguration WithFixedProperty(string propertyName, object value)
        {
            return EnrichedBy(new FixedPropertyEnricher(new[] { new LogEventProperty(propertyName, LogEventPropertyValue.For(value)) }));
        }

        public LoggerConfiguration WithHttpServerSink(string baseUrl, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            return WithSink(new HttpServerSink(baseUrl), restrictedToMinimumLevel);
        }
    }
}
