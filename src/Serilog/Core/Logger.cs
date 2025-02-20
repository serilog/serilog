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

using System.Diagnostics;

#pragma warning disable Serilog004 // Constant MessageTemplate verifier

namespace Serilog.Core;

/// <summary>
/// The core Serilog logging pipeline. A <see cref="Logger"/> must
/// be disposed to flush any events buffered within it. Most application
/// code should depend on <see cref="ILogger"/>, not this class.
/// </summary>
public sealed class Logger : ILogger, ILogEventSink, IDisposable
#if FEATURE_ASYNCDISPOSABLE
    , IAsyncDisposable
#endif
{
    static readonly object[] NoPropertyValues = Array.Empty<object>();
    static readonly LogEventProperty[] NoProperties = Array.Empty<LogEventProperty>();

    readonly MessageTemplateProcessor _messageTemplateProcessor;
    readonly ILogEventSink _sink;
    readonly Action? _dispose;
#if FEATURE_ASYNCDISPOSABLE
    readonly Func<ValueTask>? _disposeAsync;
#endif
    readonly ILogEventEnricher _enricher;

    // It's important that checking minimum level is a very
    // quick (CPU-cacheable) read in the simple case, hence
    // we keep a separate field from the switch, which may
    // not be specified. If it is, we'll set _minimumLevel
    // to its lower limit and fall through to the secondary check.
    readonly LogEventLevel _minimumLevel;
    readonly LoggingLevelSwitch? _levelSwitch;
    readonly LevelOverrideMap? _overrideMap;

    internal Logger(
        MessageTemplateProcessor messageTemplateProcessor,
        LogEventLevel minimumLevel,
        LoggingLevelSwitch? levelSwitch,
        ILogEventSink sink,
        ILogEventEnricher enricher,
        Action? dispose,
#if FEATURE_ASYNCDISPOSABLE
        Func<ValueTask>? disposeAsync,
#endif
        LevelOverrideMap? overrideMap)
    {
        _messageTemplateProcessor = messageTemplateProcessor;
        _minimumLevel = minimumLevel;
        _sink = sink;
        _dispose = dispose;
#if FEATURE_ASYNCDISPOSABLE
        _disposeAsync = disposeAsync;
#endif
        _levelSwitch = levelSwitch;
        _overrideMap = overrideMap;
        _enricher = enricher;
    }

    internal bool HasOverrideMap => _overrideMap != null;

    /// <summary>
    /// Create a logger that enriches log events via the provided enrichers.
    /// </summary>
    /// <param name="enricher">Enricher that applies in the context.</param>
    /// <returns>A logger that will enrich log events as specified.</returns>
    public ILogger ForContext(ILogEventEnricher enricher)
    {
        if (enricher == null!)
            return this; // No context here, so little point writing to SelfLog.

        return new Logger(
            _messageTemplateProcessor,
            _minimumLevel,
            _levelSwitch,
            this,
            enricher,
            null,
#if FEATURE_ASYNCDISPOSABLE
            null,
#endif
            _overrideMap);
    }

    /// <summary>
    /// Create a logger that enriches log events via the provided enrichers.
    /// </summary>
    /// <param name="enrichers">Enrichers that apply in the context.</param>
    /// <returns>A logger that will enrich log events as specified.</returns>
    public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
    {
        if (enrichers == null!)
            return this; // No context here, so little point writing to SelfLog.

        return ForContext(new SafeAggregateEnricher(enrichers));
    }

    /// <summary>
    /// Create a logger that enriches log events with the specified property.
    /// </summary>
    /// <param name="propertyName">The name of the property. Must be non-empty.</param>
    /// <param name="value">The property value.</param>
    /// <param name="destructureObjects">If <see langword="true"/>, the value will be serialized as a structured
    /// object if possible; if <see langword="false"/>, the object will be recorded as a scalar or simple array.</param>
    /// <returns>A logger that will enrich log events as specified.</returns>
    public ILogger ForContext(string propertyName, object? value, bool destructureObjects = false)
    {
        if (!LogEventProperty.IsValidName(propertyName))
        {
            SelfLog.WriteLine("Attempt to call ForContext() with invalid property name `{0}` (value: `{1}`)", propertyName, value);
            return this;
        }

        // It'd be nice to do the destructuring lazily, but unfortunately `value` may be mutated between
        // now and the first log event written.
        var propertyValue = _messageTemplateProcessor.CreatePropertyValue(value, destructureObjects);
        var enricher = new FixedPropertyEnricher(new(propertyName, propertyValue));

        LogEventLevel minimumLevel;
        LoggingLevelSwitch? levelSwitch;
        if (_overrideMap != null &&
            propertyName == Constants.SourceContextPropertyName &&
            value is string context)
        {
            _overrideMap.GetEffectiveLevel(context, out minimumLevel, out levelSwitch);
        }
        else
        {
            minimumLevel = _minimumLevel;
            levelSwitch = _levelSwitch;
        }

        return new Logger(
            _messageTemplateProcessor,
            minimumLevel,
            levelSwitch,
            this,
            enricher,
            null,
#if FEATURE_ASYNCDISPOSABLE
            null,
#endif
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
        if (source == null!)
            return this; // Little point in writing to SelfLog here because we don't have any contextual information

        return ForContext(Constants.SourceContextPropertyName, source.FullName);
    }

    /// <summary>
    /// Create a logger that marks log events as being from the specified
    /// source type.
    /// </summary>
    /// <typeparam name="TSource">Type generating log messages in the context.</typeparam>
    /// <returns>A logger that will enrich log events as specified.</returns>
    public ILogger ForContext<TSource>() => ForContext(typeof(TSource));

    /// <summary>
    /// Write a log event with the specified level.
    /// </summary>
    /// <param name="level">The level of the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Write(LogEventLevel level, string messageTemplate)
    {
        // Avoid the array allocation and any boxing allocations when the level isn't enabled
        if (IsEnabled(level))
        {
            Write(level, messageTemplate, NoPropertyValues);
        }
    }

    /// <summary>
    /// Write a log event with the specified level.
    /// </summary>
    /// <param name="level">The level of the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
    {
        // Avoid the array allocation and any boxing allocations when the level isn't enabled
        if (IsEnabled(level))
        {
#if FEATURE_SPAN
            var inlineArray = new PropertiesInlineArray();
            var span = inlineArray.AsSpan(1);
            span[0] = propertyValue;
            Write(level, messageTemplate, span);
#else
            Write(level, messageTemplate, [propertyValue]);
#endif
        }
    }

    /// <summary>
    /// Write a log event with the specified level.
    /// </summary>
    /// <param name="level">The level of the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        // Avoid the array allocation and any boxing allocations when the level isn't enabled
        if (IsEnabled(level))
        {
#if FEATURE_SPAN
            var inlineArray = new PropertiesInlineArray();
            var span = inlineArray.AsSpan(2);
            span[0] = propertyValue0;
            span[1] = propertyValue1;
            Write(level, messageTemplate, span);
#else
            Write(level, messageTemplate, [propertyValue0, propertyValue1]);
#endif
        }
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
    public void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        // Avoid the array allocation and any boxing allocations when the level isn't enabled
        if (IsEnabled(level))
        {
#if FEATURE_SPAN
            var inlineArray = new PropertiesInlineArray();
            var span = inlineArray.AsSpan(3);
            span[0] = propertyValue0;
            span[1] = propertyValue1;
            span[2] = propertyValue2;
            Write(level, messageTemplate, span);
#else
            Write(level, messageTemplate, [propertyValue0, propertyValue1, propertyValue2]);
#endif
        }
    }

    /// <summary>
    /// Write a log event with the specified level.
    /// </summary>
    /// <param name="level">The level of the event.</param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Write(LogEventLevel level, string messageTemplate, params object?[]? propertyValues)
    {
        Write(level, (Exception?)null, messageTemplate, propertyValues);
    }

#if FEATURE_SPAN
    void Write(LogEventLevel level, string messageTemplate, ReadOnlySpan<object?> propertyValues)
    {
        Write(level, null, messageTemplate, propertyValues);
    }
#endif

    /// <summary>
    /// Determine if events at the specified level, and higher, will be passed through
    /// to the log sinks.
    /// </summary>
    /// <param name="level">Level to check.</param>
    /// <returns><see langword="true"/> if the level is enabled; otherwise, <see langword="false"/>.</returns>
    public bool IsEnabled(LogEventLevel level)
    {
        if (level < _minimumLevel)
            return false;

        return _levelSwitch == null ||
               level >= _levelSwitch.MinimumLevel;
    }

    /// <summary>
    /// Write a log event with the specified level and associated exception.
    /// </summary>
    /// <param name="level">The level of the event.</param>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Write(LogEventLevel level, Exception? exception, string messageTemplate)
    {
        // Avoid the array allocation and any boxing allocations when the level isn't enabled
        if (IsEnabled(level))
        {
            Write(level, exception, messageTemplate, NoPropertyValues);
        }
    }

    /// <summary>
    /// Write a log event with the specified level and associated exception.
    /// </summary>
    /// <param name="level">The level of the event.</param>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Write<T>(LogEventLevel level, Exception? exception, string messageTemplate, T propertyValue)
    {
        // Avoid the array allocation and any boxing allocations when the level isn't enabled
        if (IsEnabled(level))
        {
#if FEATURE_SPAN
            var inlineArray = new PropertiesInlineArray();
            var span = inlineArray.AsSpan(1);
            span[0] = propertyValue;
            Write(level, exception, messageTemplate, span);
#else
            Write(level, exception, messageTemplate, [propertyValue]);
#endif
        }
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
    public void Write<T0, T1>(LogEventLevel level, Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        // Avoid the array allocation and any boxing allocations when the level isn't enabled
        if (IsEnabled(level))
        {
#if FEATURE_SPAN
            var inlineArray = new PropertiesInlineArray();
            var span = inlineArray.AsSpan(2);
            span[0] = propertyValue0;
            span[1] = propertyValue1;
            Write(level, exception, messageTemplate, span);
#else
            Write(level, exception, messageTemplate, [propertyValue0, propertyValue1]);
#endif
        }
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
    public void Write<T0, T1, T2>(LogEventLevel level, Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        // Avoid the array allocation and any boxing allocations when the level isn't enabled
        if (IsEnabled(level))
        {
#if FEATURE_SPAN
            var inlineArray = new PropertiesInlineArray();
            var span = inlineArray.AsSpan(3);
            span[0] = propertyValue0;
            span[1] = propertyValue1;
            span[2] = propertyValue2;
            Write(level, exception, messageTemplate, span);
#else
            Write(level, exception, messageTemplate, [propertyValue0, propertyValue1, propertyValue2]);
#endif
        }
    }

    /// <summary>
    /// Write a log event with the specified level and associated exception.
    /// </summary>
    /// <param name="level">The level of the event.</param>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Write(LogEventLevel level, Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        if (!IsEnabled(level)) return;
        if (messageTemplate == null!) return;

        // Catch a common pitfall when a single non-object array is cast to object[]
        if (propertyValues != null &&
            propertyValues.GetType() != typeof(object[]))
            propertyValues = [propertyValues];

        var logTimestamp = DateTimeOffset.Now;
#if FEATURE_SPAN
        var propertiesSpan = propertyValues == null ? Span<object?>.Empty : propertyValues.AsSpan();
        _messageTemplateProcessor.Process(messageTemplate, propertiesSpan, out var parsedTemplate, out var boundProperties);
#else
        _messageTemplateProcessor.Process(messageTemplate, propertyValues ?? NoPropertyValues, out var parsedTemplate, out var boundProperties);
#endif

        var currentActivity = Activity.Current;
        var logEvent = new LogEvent(logTimestamp, level, exception, parsedTemplate, boundProperties, currentActivity?.TraceId ?? default, currentActivity?.SpanId ?? default);
        Dispatch(logEvent);
    }

#if FEATURE_SPAN
    [MessageTemplateFormatMethod("messageTemplate")]
    void Write(LogEventLevel level, Exception? exception, string messageTemplate, ReadOnlySpan<object?> propertyValues)
    {
        if (!IsEnabled(level)) return;
        if (messageTemplate == null!) return;

        var logTimestamp = DateTimeOffset.Now;
        _messageTemplateProcessor.Process(messageTemplate, propertyValues, out var parsedTemplate, out var boundProperties);

        var currentActivity = Activity.Current;
        var logEvent = new LogEvent(logTimestamp, level, exception, parsedTemplate, boundProperties, currentActivity?.TraceId ?? default, currentActivity?.SpanId ?? default);
        Dispatch(logEvent);
    }
#endif

    /// <summary>
    /// Write an event to the log.
    /// </summary>
    /// <param name="logEvent">The event to write.</param>
    public void Write(LogEvent logEvent)
    {
        if (logEvent == null!) return;
        if (!IsEnabled(logEvent.Level)) return;
        Dispatch(logEvent);
    }

    void ILogEventSink.Emit(LogEvent logEvent)
    {
        Guard.AgainstNull(logEvent);

        // Bypasses the level check so that child loggers
        // using this one as a sink can increase verbosity.
        Dispatch(logEvent);
    }

    void Dispatch(LogEvent logEvent)
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

        _sink.Emit(logEvent);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <example>
    /// Log.Verbose("Staring into space, wondering if we're alone.");
    /// </example>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Verbose(string messageTemplate)
    {
        Write(LogEventLevel.Verbose, messageTemplate, NoPropertyValues);
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
    public void Verbose<T>(string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Verbose, messageTemplate, propertyValue);
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
    public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1);
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
    public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Verbose(string messageTemplate, params object?[]? propertyValues)
    {
        Verbose((Exception?)null, messageTemplate, propertyValues);
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
    public void Verbose(Exception? exception, string messageTemplate)
    {
        Write(LogEventLevel.Verbose, exception, messageTemplate, NoPropertyValues);
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
    public void Verbose<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue);
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
    public void Verbose<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Verbose<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Verbose(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <example>
    /// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
    /// </example>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Debug(string messageTemplate)
    {
        Write(LogEventLevel.Debug, messageTemplate, NoPropertyValues);
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
    public void Debug<T>(string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Debug, messageTemplate, propertyValue);
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
    public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1);
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
    public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Debug(string messageTemplate, params object?[]? propertyValues)
    {
        Debug((Exception?)null, messageTemplate, propertyValues);
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
    public void Debug(Exception? exception, string messageTemplate)
    {
        Write(LogEventLevel.Debug, exception, messageTemplate, NoPropertyValues);
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
    public void Debug<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue);
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
    public void Debug<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Debug<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Debug(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        Write(LogEventLevel.Debug, exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <example>
    /// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
    /// </example>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Information(string messageTemplate)
    {
        Write(LogEventLevel.Information, messageTemplate, NoPropertyValues);
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
    public void Information<T>(string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Information, messageTemplate, propertyValue);
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
    public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1);
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
    public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Information(string messageTemplate, params object?[]? propertyValues)
    {
        Information((Exception?)null, messageTemplate, propertyValues);
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
    public void Information(Exception? exception, string messageTemplate)
    {
        Write(LogEventLevel.Information, exception, messageTemplate, NoPropertyValues);
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
    public void Information<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Information, exception, messageTemplate, propertyValue);
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
    public void Information<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Information<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Information(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        Write(LogEventLevel.Information, exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Warning"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <example>
    /// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
    /// </example>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Warning(string messageTemplate)
    {
        Write(LogEventLevel.Warning, messageTemplate, NoPropertyValues);
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
    public void Warning<T>(string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Warning, messageTemplate, propertyValue);
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
    public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1);
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
    public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Warning(string messageTemplate, params object?[]? propertyValues)
    {
        Warning((Exception?)null, messageTemplate, propertyValues);
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
    public void Warning(Exception? exception, string messageTemplate)
    {
        Write(LogEventLevel.Warning, exception, messageTemplate, NoPropertyValues);
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
    public void Warning<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue);
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
    public void Warning<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Warning<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Warning(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        Write(LogEventLevel.Warning, exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Error"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <example>
    /// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
    /// </example>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Error(string messageTemplate)
    {
        Write(LogEventLevel.Error, messageTemplate, NoPropertyValues);
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
    public void Error<T>(string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Error, messageTemplate, propertyValue);
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
    public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1);
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
    public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Error(string messageTemplate, params object?[]? propertyValues)
    {
        Error((Exception?)null, messageTemplate, propertyValues);
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
    public void Error(Exception? exception, string messageTemplate)
    {
        Write(LogEventLevel.Error, exception, messageTemplate, NoPropertyValues);
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
    public void Error<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Error, exception, messageTemplate, propertyValue);
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
    public void Error<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Error<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Error(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        Write(LogEventLevel.Error, exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <example>
    /// Log.Fatal("Process terminating.");
    /// </example>
    [MessageTemplateFormatMethod("messageTemplate")]
    public void Fatal(string messageTemplate)
    {
        Write(LogEventLevel.Fatal, messageTemplate, NoPropertyValues);
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
    public void Fatal<T>(string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Fatal, messageTemplate, propertyValue);
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
    public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1);
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
    public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Fatal(string messageTemplate, params object?[]? propertyValues)
    {
        Fatal((Exception?)null, messageTemplate, propertyValues);
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
    public void Fatal(Exception? exception, string messageTemplate)
    {
        Write(LogEventLevel.Fatal, exception, messageTemplate, NoPropertyValues);
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
    public void Fatal<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue);
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
    public void Fatal<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Fatal<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
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
    public void Fatal(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Uses configured scalar conversion and destructuring rules to bind a set of properties to a
    /// message template. Returns false if the template or values are invalid (<c>ILogger</c>
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
    public bool BindMessageTemplate(string messageTemplate, object?[]? propertyValues,
        [NotNullWhen(true)] out MessageTemplate? parsedTemplate,
        [NotNullWhen(true)] out IEnumerable<LogEventProperty>? boundProperties)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (messageTemplate == null)
        {
            parsedTemplate = null;
            boundProperties = null;
            return false;
        }

        _messageTemplateProcessor.Process(messageTemplate, propertyValues ?? NoPropertyValues, out parsedTemplate, out var boundEventProperties);
        boundProperties = boundEventProperties.Length == 0 ?
            NoProperties :
            boundEventProperties.Select(p => new LogEventProperty(p));

        return true;
    }

    /// <summary>
    /// Uses configured scalar conversion and destructuring rules to bind a property value to its captured
    /// representation.
    /// </summary>
    /// <param name="propertyName">The name of the property. Must be non-empty.</param>
    /// <param name="value">The property value.</param>
    /// <param name="destructureObjects">If <see langword="true"/>, the value will be serialized as a structured
    /// object if possible; if <see langword="false"/>, the object will be recorded as a scalar or simple array.</param>
    /// <param name="property">The resulting property.</param>
    /// <returns>True if the property could be bound, otherwise false (<summary>ILogger</summary>
    /// methods never throw exceptions).</returns>
    public bool BindProperty(string? propertyName, object? value, bool destructureObjects, [NotNullWhen(true)] out LogEventProperty? property)
    {
        if (!LogEventProperty.IsValidName(propertyName))
        {
            property = null;
            return false;
        }

        property = _messageTemplateProcessor.CreateProperty(propertyName, value, destructureObjects);
        return true;
    }

    /// <summary>
    /// Close and flush the logging pipeline.
    /// </summary>
    public void Dispose()
    {
        _dispose?.Invoke();
    }

#if FEATURE_ASYNCDISPOSABLE
    /// <summary>
    /// Close and flush the logging pipeline.
    /// </summary>
    public ValueTask DisposeAsync()
    {
        return _disposeAsync?.Invoke() ?? default;
    }
#endif

    /// <summary>
    /// An <see cref="ILogger"/> instance that efficiently ignores all method calls.
    /// </summary>
    public static ILogger None { get; } = new SilentLogger();
}
