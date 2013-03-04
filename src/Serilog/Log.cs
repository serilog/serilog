using System;
using Serilog.Events;

namespace Serilog
{
    /// <summary>
    /// The methods on <see cref="Log"/> (and its dynamic sibling <see cref="ILogger"/>) are guaranteed
    /// never to throw exceptions. Methods on all other types may.
    /// </summary>
    public static class Log
    {
        public static ILogger Logger { get; set; }

        public static void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            Write(level, null, messageTemplate, propertyValues);
        }

        public static void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            var logger = Logger;
            if (logger != null)
                logger.Write(level, exception, messageTemplate, propertyValues);
        }

        public static bool IsEnabled(LogEventLevel level)
        {
            var logger = Logger;
            return logger != null && logger.IsEnabled(level);
        }

        public static void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Verbose(null, messageTemplate, propertyValues);
        }

        public static void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Verbose, null, messageTemplate, propertyValues);
        }

        public static void Debug(string messageTemplate, params object[] propertyValues)
        {
            Debug(null, messageTemplate, propertyValues);
        }

        public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Debug, null, messageTemplate, propertyValues);
        }

        public static void Information(string messageTemplate, params object[] propertyValues)
        {
            Information(null, messageTemplate, propertyValues);
        }

        public static void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Information, null, messageTemplate, propertyValues);
        }

        public static void Warning(string messageTemplate, params object[] propertyValues)
        {
            Warning(null, messageTemplate, propertyValues);
        }

        public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Warning, null, messageTemplate, propertyValues);
        }

        public static void Error(string messageTemplate, params object[] propertyValues)
        {
            Error(null, messageTemplate, propertyValues);
        }

        public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Error, null, messageTemplate, propertyValues);
        }

        public static void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Fatal(null, messageTemplate, propertyValues);
        }

        public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Fatal, null, messageTemplate, propertyValues);
        }
    }
}