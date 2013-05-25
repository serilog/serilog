using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace Serilog
{
    /// <summary>
    /// The core Serilog logging API, used for writing log events.
    /// </summary>
    /// <example>
    /// var log = new LoggerConfiguration()
    ///     .WithConsoleSink()
    ///     .CreateLogger();
    /// 
    /// var thing = "World";
    /// log.Information("Hello, {Thing}!", thing);
    /// </example>
    /// <remarks>
    /// The methods on <see cref="ILogger"/> (and its static sibling <see cref="Log"/>) are guaranteed
    /// never to throw exceptions. Methods on all other types may.
    /// </remarks>
    public interface ILogger
    {
        /// <summary>
        /// Supplies culture-specific formatting information for all logging operations, or null.
        /// </summary>
        IFormatProvider FormatProvider { get; }

        /// <summary>
        /// Create a logger that enriches log events via the provided enrichers.
        /// </summary>
        /// <param name="enrichers">Enrichers that apply in the context.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers);

        /// <summary>
        /// Create a logger that enriches log events with the specified property.
        /// </summary>
        /// <returns>A logger that will enrich log events as specified.</returns>
        ILogger ForContext(string propertyName, object value, bool destructureObjects = false);

        /// <summary>
        /// Create a logger that marks log events as being from the specified
        /// source type.
        /// </summary>
        /// <typeparam name="TSource">Type generating log messages in the context.</typeparam>
        /// <returns>A logger that will enrich log events as specified.</returns>
        ILogger ForContext<TSource>();

        /// <summary>
        /// Create a logger that marks log events as being from the specified
        /// source type.
        /// </summary>
        /// <param name="source">Type generating log messages in the context.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        ILogger ForContext(Type source);

        /// <summary>
        /// Write an event to the log.
        /// </summary>
        /// <param name="logEvent">The event to write.</param>
        void Write(LogEvent logEvent);

        /// <summary>
        /// Write a log event with the specified level.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the specified level.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        void Write(LogEventLevel level, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Write(LogEventLevel level, Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Determine if events at the specified level will be passed through
        /// to the log sinks.
        /// </summary>
        /// <param name="level">Level to check.</param>
        /// <returns>True if the level is enabled; otherwise, false.</returns>
        bool IsEnabled(LogEventLevel level);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        void Verbose(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose(CultureInfo.CurrentCulture, "Staring into space, wondering if we're alone.");
        /// </example>
        void Verbose(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        void Verbose(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose(ex, CultureInfo.CurrentCulture, ""Staring into space, wondering where this comet came from.");
        /// </example>
        void Verbose(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        void Debug(string messageTemplate, params object[] propertyValues);


        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug(CultureInfo.CurrentCulture, ""Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        void Debug(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug(ex, "Swallowing a mundane exception.");
        /// </example>
        void Debug(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug(ex, CultureInfo.CurrentCulture, ""Swallowing a mundane exception.");
        /// </example>
        void Debug(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        void Information(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information(CultureInfo.CurrentCulture, ""Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        void Information(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        void Information(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information(ex, CultureInfo.CurrentCulture, ""Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        void Information(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        void Warning(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning(CultureInfo.CurrentCulture, ""Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        void Warning(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        void Warning(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning(ex, CultureInfo.CurrentCulture, ""Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        void Warning(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        void Error(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error(CultureInfo.CurrentCulture, ""Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        void Error(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        void Error(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error(ex, CultureInfo.CurrentCulture, ""Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        void Error(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal("Process terminating.");
        /// </example>
        void Fatal(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal(CultureInfo.CurrentCulture, ""Process terminating.");
        /// </example>
        void Fatal(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal(ex, "Process terminating.");
        /// </example>
        void Fatal(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event using format provider with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="formatProvider">A format provider to apply to messageTemplate, or null to use the default.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal(ex, CultureInfo.CurrentCulture, ""Process terminating.");
        /// </example>
        void Fatal(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues);
    }
}
