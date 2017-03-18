// Copyright 2013-2016 Serilog Contributors
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
using Serilog.Core.Enrichers;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Parameters;
using System.Threading.Tasks;

#pragma warning disable Serilog004 // Constant MessageTemplate verifier

namespace Serilog.Core
{
    /// <summary>
    /// The core Serilog logging pipeline. A <see cref="Logger"/> must
    /// be disposed to flush any events buffered within it. Most application
    /// code should depend on <see cref="ILogger"/>, not this class.
    /// </summary>
    public sealed class Logger : ILogger, ILogEventSink, IDisposable
    {
        static readonly object[] NoPropertyValues = new object[0];

        readonly MessageTemplateProcessor _messageTemplateProcessor;
        readonly ILogEventSink _sink;
        readonly Action _dispose;
        readonly ILogEventEnricher _enricher;

        // It's important that checking minimum level is a very
        // quick (CPU-cacheable) read in the simple case, hence
        // we keep a separate field from the switch, which may
        // not be specified. If it is, we'll set _minimumLevel
        // to its lower limit and fall through to the secondary check.
        readonly LogEventLevel _minimumLevel;
        readonly LoggingLevelSwitch _levelSwitch;
        readonly LevelOverrideMap _overrideMap;

        internal Logger(
            MessageTemplateProcessor messageTemplateProcessor,
            LogEventLevel minimumLevel,
            ILogEventSink sink,
            ILogEventEnricher enricher,
            Action dispose = null,
            LevelOverrideMap overrideMap = null)
            : this(messageTemplateProcessor, minimumLevel, sink, enricher, dispose, null, overrideMap)
        {
        }

        internal Logger(
            MessageTemplateProcessor messageTemplateProcessor,
            LoggingLevelSwitch levelSwitch,
            ILogEventSink sink,
            ILogEventEnricher enricher,
            Action dispose = null,
            LevelOverrideMap overrideMap = null)
            : this(messageTemplateProcessor, LevelAlias.Minimum, sink, enricher, dispose, levelSwitch, overrideMap)
        {
        }

        // The messageTemplateProcessor, sink and enricher are required. Argument checks are dropped because
        // throwing from here breaks the logger's no-throw contract, and callers are all in this file anyway.
        Logger(
            MessageTemplateProcessor messageTemplateProcessor,
            LogEventLevel minimumLevel,
            ILogEventSink sink,
            ILogEventEnricher enricher,
            Action dispose = null,
            LoggingLevelSwitch levelSwitch = null,
            LevelOverrideMap overrideMap = null)
        {
            _messageTemplateProcessor = messageTemplateProcessor;
            _minimumLevel = minimumLevel;
            _sink = sink;
            _dispose = dispose;
            _levelSwitch = levelSwitch;
            _overrideMap = overrideMap;
            _enricher = enricher;
        }

        /// <summary>
        /// Create a logger that enriches log events via the provided enrichers.
        /// </summary>
        /// <param name="enricher">Enricher that applies in the context.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public ILogger ForContext(ILogEventEnricher enricher)
        {
            if (enricher == null)
                return this; // No context here, so little point writing to SelfLog.

            return new Logger(
                        _messageTemplateProcessor,
                        _minimumLevel,
                        this,
                        enricher,
                        null,
                        _levelSwitch,
                        _overrideMap);
        }

        /// <summary>
        /// Create a logger that enriches log events via the provided enrichers.
        /// </summary>
        /// <param name="enrichers">Enrichers that apply in the context.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
        {
            if (enrichers == null)
                return this; // No context here, so little point writing to SelfLog.

            return ForContext(new SafeAggregateEnricher(enrichers));
        }

        /// <summary>
        /// Create a logger that enriches log events with the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property. Must be non-empty.</param>
        /// <param name="value">The property value.</param>
        /// <param name="destructureObjects">If true, the value will be serialized as a structured
        /// object if possible; if false, the object will be recorded as a scalar or simple array.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
        {
            if (!LogEventProperty.IsValidName(propertyName))
            {
                SelfLog.WriteLine("Attempt to call ForContext() with invalid property name `{0}` (value: `{1}`)", propertyName, value);
                return this;
            }

            // It'd be nice to do the destructuring lazily, but unfortunately `value` may be mutated between
            // now and the first log event written...
            // A future optimization opportunity may be to implement ILogEventEnricher on LogEventProperty to
            // remove one more allocation.
            var enricher = new FixedPropertyEnricher(_messageTemplateProcessor.CreateProperty(propertyName, value, destructureObjects));

            var minimumLevel = _minimumLevel;
            var levelSwitch = _levelSwitch;
            if (_overrideMap != null && propertyName == Constants.SourceContextPropertyName)
            {
                var context = value as string;
                if (context != null)
                    _overrideMap.GetEffectiveLevel(context, out minimumLevel, out levelSwitch);
            }

            return new Logger(
                _messageTemplateProcessor,
                minimumLevel,
                this,
                enricher,
                null,
                levelSwitch,
                _overrideMap);
        }

        /// <summary>
        /// Create a logger that marks log events as being from the specified
        /// source type.
        /// </summary>
        /// <param name="source">Type generating log messages in the context.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public ILogger ForContext(Type source)
        {
            if (source == null)
                return this; // Little point in writing to SelfLog here because we don't have any contextual information

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
        /// <param name="messageTemplate">Message template describing the event.</param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Write(LogEventLevel level, string messageTemplate)
        {
            // Avoid the array allocation and any boxing allocations when the level isn't enabled
            if (IsEnabled(level))
            {
                return Write(level, messageTemplate, NoPropertyValues);
            }

            return CompletedTask.Instance;
        }

        /// <summary>
        /// Write a log event with the specified level.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            // Avoid the array allocation and any boxing allocations when the level isn't enabled
            if (IsEnabled(level))
            {
                return Write(level, messageTemplate, new object[] { propertyValue });
            }

            return CompletedTask.Instance;
        }

        /// <summary>
        /// Write a log event with the specified level.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            // Avoid the array allocation and any boxing allocations when the level isn't enabled
            if (IsEnabled(level))
            {
                return Write(level, messageTemplate, new object[] { propertyValue0, propertyValue1 });
            }

            return CompletedTask.Instance;
        }

        /// <summary>
        /// Write a log event with the specified level.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            // Avoid the array allocation and any boxing allocations when the level isn't enabled
            if (IsEnabled(level))
            {
                return Write(level, messageTemplate, new object[] { propertyValue0, propertyValue1, propertyValue2 });
            }

            return CompletedTask.Instance;
        }

        /// <summary>
        /// Write a log event with the specified level.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            return Write(level, (Exception)null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Determine if events at the specified level will be passed through
        /// to the log sinks.
        /// </summary>
        /// <param name="level">Level to check.</param>
        /// <returns>True if the level is enabled; otherwise, false.</returns>
        public bool IsEnabled(LogEventLevel level)
        {
            if ((int) level < (int) _minimumLevel)
                return false;

            return _levelSwitch == null ||
                   (int) level >= (int) _levelSwitch.MinimumLevel;
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Write(LogEventLevel level, Exception exception, string messageTemplate)
        {
            // Avoid the array allocation and any boxing allocations when the level isn't enabled
            if (IsEnabled(level))
            {
                return Write(level, exception, messageTemplate, NoPropertyValues);
            }

            return CompletedTask.Instance;
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
        {
            // Avoid the array allocation and any boxing allocations when the level isn't enabled
            if (IsEnabled(level))
            {
                return Write(level, exception, messageTemplate, new object[] { propertyValue });
            }

            return CompletedTask.Instance;
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            // Avoid the array allocation and any boxing allocations when the level isn't enabled
            if (IsEnabled(level))
            {
                return Write(level, exception, messageTemplate, new object[] { propertyValue0, propertyValue1 });
            }

            return CompletedTask.Instance;
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            // Avoid the array allocation and any boxing allocations when the level isn't enabled
            if (IsEnabled(level))
            {
                return Write(level, exception, messageTemplate, new object[] { propertyValue0, propertyValue1, propertyValue2 });
            }

            return CompletedTask.Instance;
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            if (!IsEnabled(level)) return CompletedTask.Instance;
            if (messageTemplate == null) return CompletedTask.Instance;

            // Catch a common pitfall when a single non-object array is cast to object[]
            if (propertyValues != null &&
                propertyValues.GetType() != typeof(object[]))
                propertyValues = new object[] { propertyValues };

            MessageTemplate parsedTemplate;
            IEnumerable<LogEventProperty> boundProperties;
            _messageTemplateProcessor.Process(messageTemplate, propertyValues, out parsedTemplate, out boundProperties);

            var logEvent = new LogEvent(DateTimeOffset.Now, level, exception, parsedTemplate, boundProperties);
            return Dispatch(logEvent);
        }

        /// <summary>
        /// Write an event to the log.
        /// </summary>
        /// <param name="logEvent">The event to write.</param>
        public Task Write(LogEvent logEvent)
        {
            if (logEvent == null) return CompletedTask.Instance;
            if (!IsEnabled(logEvent.Level)) return CompletedTask.Instance;
            return Dispatch(logEvent);
        }

        Task ILogEventSink.Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));

            // Bypasses the level check so that child loggers
            // using this one as a sink can increase verbosity.
            return Dispatch(logEvent);
        }

        Task Dispatch(LogEvent logEvent)
        {
            // The enricher may be a "safe" aggregate one, but is most commonly bare and so
            // the exception handling from SafeAggregateEnricher is duplicated here.
            try
            {
                _enricher.Enrich(logEvent, _messageTemplateProcessor);
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Exception {0} caught while enriching {1} with {2}.", ex, logEvent, _enricher);
            }

            return _sink.Emit(logEvent);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Verbose(string messageTemplate)
        {
            return Write(LogEventLevel.Verbose, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Verbose<T>(string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Verbose, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Verbose(string messageTemplate, params object[] propertyValues)
        {
            return Verbose((Exception)null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Verbose(Exception exception, string messageTemplate)
        {
            return Write(LogEventLevel.Verbose, exception, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Debug(string messageTemplate)
        {
            return Write(LogEventLevel.Debug, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Debug<T>(string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Debug, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Debug(string messageTemplate, params object[] propertyValues)
        {
            return Debug((Exception)null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Debug(ex, "Swallowing a mundane exception.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Debug(Exception exception, string messageTemplate)
        {
            return Write(LogEventLevel.Debug, exception, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug(ex, "Swallowing a mundane exception.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug(ex, "Swallowing a mundane exception.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Debug(ex, "Swallowing a mundane exception.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return Write(LogEventLevel.Debug, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Information(string messageTemplate)
        {
            return Write(LogEventLevel.Information, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Information<T>(string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Information, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Information(string messageTemplate, params object[] propertyValues)
        {
            return Information((Exception)null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Information(Exception exception, string messageTemplate)
        {
            return Write(LogEventLevel.Information, exception, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Information, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return Write(LogEventLevel.Information, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Warning(string messageTemplate)
        {
            return Write(LogEventLevel.Warning, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Warning<T>(string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Warning, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Warning(string messageTemplate, params object[] propertyValues)
        {
            return Warning((Exception)null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Warning(Exception exception, string messageTemplate)
        {
            return Write(LogEventLevel.Warning, exception, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return Write(LogEventLevel.Warning, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Error(string messageTemplate)
        {
            return Write(LogEventLevel.Error, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Error<T>(string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Error, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Error(string messageTemplate, params object[] propertyValues)
        {
            return Error((Exception)null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Error(Exception exception, string messageTemplate)
        {
            return Write(LogEventLevel.Error, exception, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Error, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return Write(LogEventLevel.Error, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Fatal("Process terminating.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Fatal(string messageTemplate)
        {
            return Write(LogEventLevel.Fatal, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal("Process terminating.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Fatal<T>(string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Fatal, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal("Process terminating.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal("Process terminating.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Fatal(string messageTemplate, params object[] propertyValues)
        {
            return Fatal((Exception)null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Log.Fatal(ex, "Process terminating.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Fatal(Exception exception, string messageTemplate)
        {
            return Write(LogEventLevel.Fatal, exception, messageTemplate, NoPropertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal(ex, "Process terminating.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal(ex, "Process terminating.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Log.Fatal(ex, "Process terminating.");
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public Task Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
        public Task Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Uses configured scalar conversion and destructuring rules to bind a set of properties to a
        /// message template. Returns false if the template or values are invalid (<summary>ILogger</summary>
        /// methods never throw exceptions).
        /// </summary>
        /// <param name="messageTemplate">Message template describing an event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <param name="parsedTemplate">The internal representation of the template, which may be used to
        /// render the <paramref name="boundProperties"/> as text.</param>
        /// <param name="boundProperties">Captured properties from the template and <paramref name="propertyValues"/>.</param>
        /// <example>
        /// MessageTemplate template;
        /// IEnumerable&lt;LogEventProperty&gt; properties>;
        /// if (Log.BindMessageTemplate("Hello, {Name}!", new[] { "World" }, out template, out properties)
        /// {
        ///     var propsByName = properties.ToDictionary(p => p.Name, p => p.Value);
        ///     Console.WriteLine(template.Render(propsByName, null));
        ///     // -> "Hello, World!"
        /// }
        /// </example>
        [MessageTemplateFormatMethod("messageTemplate")]
        public bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate parsedTemplate, out IEnumerable<LogEventProperty> boundProperties)
        {
            if (messageTemplate == null)
            {
                parsedTemplate = null;
                boundProperties = null;
                return false;
            }

            _messageTemplateProcessor.Process(messageTemplate, propertyValues, out parsedTemplate, out boundProperties);
            return true;
        }

        /// <summary>
        /// Uses configured scalar conversion and destructuring rules to bind a property value to its captured
        /// representation.
        /// </summary>
        /// <returns>True if the property could be bound, otherwise false (<summary>ILogger</summary>
        /// <param name="propertyName">The name of the property. Must be non-empty.</param>
        /// <param name="value">The property value.</param>
        /// <param name="destructureObjects">If true, the value will be serialized as a structured
        /// object if possible; if false, the object will be recorded as a scalar or simple array.</param>
        /// <param name="property">The resulting property.</param>
        /// methods never throw exceptions).</returns>
        public bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty property)
        {
            if (!LogEventProperty.IsValidName(propertyName))
            {
                property = null;
                return false;
            }

            property =_messageTemplateProcessor.CreateProperty(propertyName, value, destructureObjects);
            return true;
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
