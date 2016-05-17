// Copyright 2013-2015 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using Serilog.Core.Enrichers;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Parameters;

namespace Serilog.Core
{
    /// <summary>
    /// The core Serilog logging pipeline. A <see cref="Logger"/> must
    /// be disposed to flush any events buffered within it. Most application
    /// code should depend on <see cref="ILogger"/>, not this class.
    /// </summary>
    public sealed class Logger : ILogger, ILogEventSink, IDisposable
    {
        readonly MessageTemplateProcessor _messageTemplateProcessor;
        readonly ILogEventSink _sink;
        readonly Action _dispose;
        readonly ILogEventEnricher[] _enrichers;

        // It's important that checking minimum level is a very
        // quick (CPU-cacheable) read in the simple case, hence
        // we keep a separate field from the switch, which may
        // not be specified. If it is, we'll set _minimumLevel
        // to its lower limit and fall through to the secondary check.
        readonly LogEventLevel _minimumLevel;
        readonly LoggingLevelSwitch _levelSwitch;

        internal Logger(
            MessageTemplateProcessor messageTemplateProcessor,
            LogEventLevel minimumLevel,
            ILogEventSink sink,
            IEnumerable<ILogEventEnricher> enrichers,
            Action dispose = null)
            : this(messageTemplateProcessor, minimumLevel, sink, enrichers, dispose, null)
        {
        }

        internal Logger(
            MessageTemplateProcessor messageTemplateProcessor,
            LoggingLevelSwitch levelSwitch,
            ILogEventSink sink,
            IEnumerable<ILogEventEnricher> enrichers,
            Action dispose = null)
            : this(messageTemplateProcessor, LevelAlias.Minimum, sink, enrichers, dispose, levelSwitch)
        {
        }

        Logger(
            MessageTemplateProcessor messageTemplateProcessor,
            LogEventLevel minimumLevel,
            ILogEventSink sink,
            IEnumerable<ILogEventEnricher> enrichers,
            Action dispose = null,
            LoggingLevelSwitch levelSwitch = null)
        {
            if (sink == null) throw new ArgumentNullException(nameof(sink));
            if (enrichers == null) throw new ArgumentNullException(nameof(enrichers));

            _messageTemplateProcessor = messageTemplateProcessor;
            _minimumLevel = minimumLevel;
            _sink = sink;
            _dispose = dispose;
            _levelSwitch = levelSwitch;
            _enrichers = enrichers.ToArray();
        }

        /// <summary>
        /// Create a logger that enriches log events via the provided enrichers.
        /// </summary>
        /// <param name="enrichers">Enrichers that apply in the context.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
        {
            return new Logger(
                _messageTemplateProcessor,
                _minimumLevel,
                this,
                (enrichers ?? new ILogEventEnricher[0]).ToArray(),
                null,
                _levelSwitch);
        }

        /// <summary>
        /// Create a logger that enriches log events with the specified property.
        /// </summary>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
        {
            return ForContext(new[] {
                new FixedPropertyEnricher(
                    _messageTemplateProcessor.CreateProperty(propertyName, value, destructureObjects)) });
        }

        /// <summary>
        /// Create a logger that marks log events as being from the specified
        /// source type.
        /// </summary>
        /// <param name="source">Type generating log messages in the context.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public ILogger ForContext(Type source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return ForContext(Constants.SourceContextPropertyName, source.FullName);
        }

        /// <summary>
        /// Create a logger that marks log events as being from the specified
        /// source type.
        /// </summary>
        /// <typeparam name="TSource">Type generating log messages in the context.</typeparam>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public ILogger ForContext<TSource>()
        {
            return ForContext(typeof(TSource));
        }

        /// <summary>
        /// Write a log event with the specified level.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            Write(level, null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Determine if events at the specified level will be passed through
        /// to the log sinks.
        /// </summary>
        /// <param name="level">Level to check.</param>
        /// <returns>True if the level is enabled; otherwise, false.</returns>
        public bool IsEnabled(LogEventLevel level)
        {
            if ((int)level < (int)_minimumLevel)
                return false;

            return _levelSwitch == null ||
                (int)level >= (int)_levelSwitch.MinimumLevel;
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            if (messageTemplate == null) return;
            if (!IsEnabled(level)) return;

            // Catch a common pitfall when a single non-object array is cast to object[]
            if (propertyValues != null &&
                propertyValues.GetType() != typeof(object[]))
                propertyValues = new object[] { propertyValues };

            var now = DateTimeOffset.Now;

            MessageTemplate parsedTemplate;
            IEnumerable<LogEventProperty> properties;
            _messageTemplateProcessor.Process(messageTemplate, propertyValues, out parsedTemplate, out properties);

            var logEvent = new LogEvent(now, level, exception, parsedTemplate, properties);
            Dispatch(logEvent);
        }

        /// <summary>
        /// Write an event to the log.
        /// </summary>
        /// <param name="logEvent">The event to write.</param>
        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) return;
            if (!IsEnabled(logEvent.Level)) return;
            Dispatch(logEvent);
        }

        void ILogEventSink.Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            Write(logEvent);
        }

        void Dispatch(LogEvent logEvent)
        {
            foreach (var enricher in _enrichers)
            {
                try
                {
                    enricher.Enrich(logEvent, _messageTemplateProcessor);
                }
                catch (Exception ex)
                {
                    SelfLog.WriteLine("Exception {0} caught while enriching {1} with {2}.", ex, logEvent, enricher);
                }
            }

            _sink.Emit(logEvent);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Verbose(null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            Debug(null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug(ex, "Swallowing a mundane exception.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Debug, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Information(string messageTemplate, params object[] propertyValues)
        {
            Information(null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Information, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            Warning(null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Warning, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Error(string messageTemplate, params object[] propertyValues)
        {
            Error(null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Error, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal("Process terminating.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Fatal(null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal(ex, "Process terminating.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Close and flush the logging pipeline.
        /// </summary>
        public void Dispose()
        {
            _dispose?.Invoke();
        }
    }
}
