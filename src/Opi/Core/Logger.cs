using System;
using System.Collections.Generic;
using System.Linq;

namespace Opi.Core
{
    class Logger : ILogger, ILogEventSink
    {
        private readonly IMessageTemplateRepository _messageTemplateRepository;
        private readonly LogEventLevel _minimumLevel;
        private readonly ILogEventSink _sink;
        private readonly ILogEventEnricher[] _enrichers;

        public Logger(IMessageTemplateRepository messageTemplateRepository, LogEventLevel minimumLevel, ILogEventSink sink, IEnumerable<ILogEventEnricher> enrichers)
        {
            if (sink == null) throw new ArgumentNullException("sink");
            if (enrichers == null) throw new ArgumentNullException("enrichers");
            _messageTemplateRepository = messageTemplateRepository;
            _minimumLevel = minimumLevel;
            _sink = sink;
            _enrichers = enrichers.ToArray();
        }

        public ILogger CreateContext(ILogEventEnricher[] enrichers, params LogEventProperty[] fixedProperties)
        {
            var safeEnrichers = (enrichers ?? new ILogEventEnricher[0]).ToList();
            if (fixedProperties != null && fixedProperties.Length != 0)
            {
                safeEnrichers.Add(new FixedPropertyEnricher(fixedProperties));
            }

            return new Logger(_messageTemplateRepository, _minimumLevel, this, safeEnrichers);
        }

        public ILogger CreateContext(params LogEventProperty[] fixedProperties)
        {
            return CreateContext(null, fixedProperties);
        }

        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            Write(level, null, messageTemplate, propertyValues);
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            if (messageTemplate == null)
                return;

            if ((int)level < (int)_minimumLevel)
                return;

            // Catch a common pitfall when a single non-object array is cast to object[]
            // Needs some more thought
            if (propertyValues != null &&
                propertyValues.GetType() != typeof(object[]))
                propertyValues = new object[] { propertyValues };

            var now = DateTime.Now;
            var parsedTemplate = _messageTemplateRepository.GetParsedTemplate(messageTemplate);
            var properties = parsedTemplate.ConstructPositionalProperties(propertyValues);

            var logEvent = new LogEvent(now, level, exception, messageTemplate, properties);
            Write(logEvent);
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) return;

            foreach (var enricher in _enrichers)
            {
                enricher.Enrich(logEvent);
            }

            _sink.Write(logEvent);
        }


        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Verbose(null, messageTemplate, propertyValues);
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Verbose, null, messageTemplate, propertyValues);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            Debug(null, messageTemplate, propertyValues);
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Debug, null, messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            Information(null, messageTemplate, propertyValues);
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Information, null, messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            Warning(null, messageTemplate, propertyValues);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Warning, null, messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            Error(null, messageTemplate, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Error, null, messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Fatal(null, messageTemplate, propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Fatal, null, messageTemplate, propertyValues);
        }
    }
}
