using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog
{
    /// <summary>
    /// The methods on <see cref="ILogger"/> (and its static sibling <see cref="Log"/>) are guaranteed
    /// never to throw exceptions. Methods on all other types may.
    /// </summary>
    public interface ILogger
    {
        ILogger ForContext(ILogEventEnricher[] enrichers, params LogEventProperty[] fixedProperties);
        ILogger ForContext(params LogEventProperty[] fixedProperties);
        ILogger ForContext<TSource>(ILogEventEnricher[] enrichers, params LogEventProperty[] fixedProperties);
        ILogger ForContext<TSource>(params LogEventProperty[] fixedProperties);

        void Write(LogEvent logEvent);
        void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues);
        void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues);

        bool IsEnabled(LogEventLevel level);

        void Verbose(string messageTemplate, params object[] propertyValues);
        void Verbose(Exception exception, string messageTemplate, params object[] propertyValues);
        void Debug(string messageTemplate, params object[] propertyValues);
        void Debug(Exception exception, string messageTemplate, params object[] propertyValues);
        void Information(string messageTemplate, params object[] propertyValues);
        void Information(Exception exception, string messageTemplate, params object[] propertyValues);
        void Warning(string messageTemplate, params object[] propertyValues);
        void Warning(Exception exception, string messageTemplate, params object[] propertyValues);
        void Error(string messageTemplate, params object[] propertyValues);
        void Error(Exception exception, string messageTemplate, params object[] propertyValues);
        void Fatal(string messageTemplate, params object[] propertyValues);
        void Fatal(Exception exception, string messageTemplate, params object[] propertyValues);
    }
}
