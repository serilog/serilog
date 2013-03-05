using System;
using Serilog.Events;

namespace Serilog.Core
{
    class SilentLogger : ILogger
    {
        public ILogger ForContext(ILogEventEnricher[] enrichers, params LogEventProperty[] fixedProperties)
        {
            return this;
        }

        public ILogger ForContext(params LogEventProperty[] fixedProperties)
        {
            return this;
        }

        public ILogger ForContext<TSource>(ILogEventEnricher[] enrichers, params LogEventProperty[] fixedProperties)
        {
            return this;
        }

        public ILogger ForContext<TSource>(params LogEventProperty[] fixedProperties)
        {
            return this;
        }

        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public bool IsEnabled(LogEventLevel level)
        {
            return false;
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }
    }
}
