using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    public class DisposableLogger : ILogger, IDisposable
    {
        public bool Disposed { get; set; }

        public void Dispose()
        {
            Disposed = true;
        }

        public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
        {
            throw new NotImplementedException();
        }

        public ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
        {
            throw new NotImplementedException();
        }

        public ILogger ForContext<TSource>()
        {
            throw new NotImplementedException();
        }

        public ILogger ForContext(Type source)
        {
            throw new NotImplementedException();
        }

        public void Write(LogEvent logEvent)
        {
            throw new NotImplementedException();
        }

        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogEventLevel level)
        {
            throw new NotImplementedException();
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate parsedTemplate, out IEnumerable<LogEventProperty> boundProperties)
        {
            throw new NotImplementedException();
        }

        public bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty property)
        {
            throw new NotImplementedException();
        }
    }
}