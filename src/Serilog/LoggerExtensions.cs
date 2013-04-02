using System;
using Serilog.Events;

namespace Serilog
{
    /// <summary>
    /// Helper methods for <see cref="ILogger"/>.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        public static void Verbose(this ILogger logger, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Verbose, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        public static void Verbose(this ILogger logger, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        public static void Debug(this ILogger logger, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Debug, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug(ex, "Swallowing a mundane exception.");
        /// </example>
        public static void Debug(this ILogger logger, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Debug, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information(this ILogger logger, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Information, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information(this ILogger logger, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Information, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning(this ILogger logger, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Warning, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning(this ILogger logger, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Warning, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error(this ILogger logger, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Error, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error(this ILogger logger, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Error, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal("Process terminating.");
        /// </example>
        public static void Fatal(this ILogger logger, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Fatal,  messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal(ex, "Process terminating.");
        /// </example>
        public static void Fatal(this ILogger logger, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValues);
        }
    }
}
