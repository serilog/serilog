using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog
{
    /// <summary>
    /// The methods on <see cref="Log"/> (and its dynamic sibling <see cref="ILogger"/>) are guaranteed
    /// never to throw exceptions. Methods on all other types may.
    /// </summary>
    public static class Log
    {
        static ILogger _logger = new SilentLogger();

        public static ILogger Logger
        {
            get { return _logger; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _logger = value;
            }
        }

        public static ILogger ForContext(ILogEventEnricher[] enrichers, params LogEventProperty[] fixedProperties)
        {
            return Logger.ForContext(enrichers, fixedProperties);
        }

        public static ILogger ForContext(params LogEventProperty[] fixedProperties)
        {
            return Logger.ForContext(fixedProperties);
        }

        public static ILogger ForContext<TSource>(ILogEventEnricher[] enrichers, params LogEventProperty[] fixedProperties)
        {
            return Logger.ForContext<TSource>(enrichers, fixedProperties);
        }

        public static ILogger ForContext<TSource>(params LogEventProperty[] fixedProperties)
        {
            return Logger.ForContext<TSource>(fixedProperties);
        }

        public static void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            Logger.Write(level, messageTemplate, propertyValues);
        }

        public static void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Write(level, exception, messageTemplate, propertyValues);
        }

        public static bool IsEnabled(LogEventLevel level)
        {
            return Logger.IsEnabled(level);
        }

        public static void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Logger.Verbose(messageTemplate, propertyValues);
        }

        public static void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Verbose(exception, messageTemplate, propertyValues);
        }

        public static void Debug(string messageTemplate, params object[] propertyValues)
        {
            Logger.Debug(messageTemplate, propertyValues);
        }

        public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Debug(exception, messageTemplate, propertyValues);
        }

        public static void Information(string messageTemplate, params object[] propertyValues)
        {
            Logger.Information(messageTemplate, propertyValues);
        }

        public static void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Information(exception, messageTemplate, propertyValues);
        }

        public static void Warning(string messageTemplate, params object[] propertyValues)
        {
            Logger.Warning(messageTemplate, propertyValues);
        }

        public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Warning(exception, messageTemplate, propertyValues);
        }

        public static void Error(string messageTemplate, params object[] propertyValues)
        {
            Logger.Error(messageTemplate, propertyValues);
        }

        public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Error(exception, messageTemplate, propertyValues);
        }

        public static void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Logger.Fatal(messageTemplate, propertyValues);
        }

        public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Fatal(exception, messageTemplate, propertyValues);
        }
    }
}
