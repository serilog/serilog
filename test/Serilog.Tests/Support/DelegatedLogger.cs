using System;
using System.Collections.Generic;

using Serilog.Core;
using Serilog.Events;

namespace Serilog.Tests.Support
{
#if FEATURE_DEFAULT_INTERFACE
    public class DelegatedLogger : ILogger
    {
        readonly ILogger _inner;

        public DelegatedLogger(ILogger logger)
        {
            _inner = logger;
        }

        public bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate parsedTemplate, out IEnumerable<LogEventProperty> boundProperties) => _inner.BindMessageTemplate(messageTemplate, propertyValues, out parsedTemplate, out boundProperties);
        public bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty property) => _inner.BindProperty(propertyName, value, destructureObjects, out property);
        public ILogger ForContext(ILogEventEnricher enricher) => _inner.ForContext(enricher);
        public ILogger ForContext(string propertyName, object value, bool destructureObjects = false) => _inner.ForContext(propertyName, value, destructureObjects);
        public bool IsEnabled(LogEventLevel level) => _inner.IsEnabled(level);
        public void Write(LogEvent logEvent) => _inner.Write(logEvent);
        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues) => _inner.Write(level, exception, messageTemplate, propertyValues);
    }
#endif
}
