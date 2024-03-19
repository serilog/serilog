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

using System.Diagnostics;
// ReSharper disable IntroduceOptionalParameters.Global

namespace Serilog.Events;

/// <summary>
/// A log event.
/// </summary>
public class LogEvent
{
    Dictionary<string, LogEventPropertyValue> _properties;
    ActivityTraceId _traceId;
    ActivitySpanId _spanId;

    /// <summary>
    ///
    /// </summary>
    public LogEvent()
    {
        Timestamp = default;
        Level = default;
        Exception = default;
        MessageTemplate = default!;
        _properties = default!;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="timestamp"></param>
    /// <param name="level"></param>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="properties"></param>
    public void Fill(DateTimeOffset timestamp, LogEventLevel level, Exception? exception, MessageTemplate messageTemplate, EventProperty[] properties)
    {
        Timestamp = timestamp;
        Level = level;
        Exception = exception;
        MessageTemplate = messageTemplate;

        _properties ??= new Dictionary<string, LogEventPropertyValue>();

        for (var i = 0; i < properties.Length; ++i)
            _properties[properties[i].Name] = properties[i].Value;
    }

    /// <summary>
    ///
    /// </summary>
    public void Reset()
    {
        _properties.Clear();
        Exception = null;
    }

    LogEvent(
        DateTimeOffset timestamp,
        LogEventLevel level,
        Exception? exception,
        MessageTemplate messageTemplate,
        Dictionary<string, LogEventPropertyValue> properties,
        ActivityTraceId traceId,
        ActivitySpanId spanId)
    {
        Timestamp = timestamp;
        Level = level;
        Exception = exception;
        _traceId = traceId;
        _spanId = spanId;
        MessageTemplate = Guard.AgainstNull(messageTemplate);
        _properties = Guard.AgainstNull(properties);
    }

    /// <summary>
    /// Construct a new <seealso cref="LogEvent"/>.
    /// </summary>
    /// <param name="timestamp">The time at which the event occurred.</param>
    /// <param name="level">The level of the event.</param>
    /// <param name="exception">An exception associated with the event, or null.</param>
    /// <param name="messageTemplate">The message template describing the event.</param>
    /// <param name="properties">Properties associated with the event, including those presented in <paramref name="messageTemplate"/>.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="messageTemplate"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="properties"/> is <code>null</code></exception>
    public LogEvent(DateTimeOffset timestamp, LogEventLevel level, Exception? exception, MessageTemplate messageTemplate, IEnumerable<LogEventProperty> properties)
        : this(timestamp, level, exception, messageTemplate, properties, default, default)
    {
    }

    /// <summary>
    /// Construct a new <seealso cref="LogEvent"/>.
    /// </summary>
    /// <param name="timestamp">The time at which the event occurred.</param>
    /// <param name="level">The level of the event.</param>
    /// <param name="exception">An exception associated with the event, or null.</param>
    /// <param name="messageTemplate">The message template describing the event.</param>
    /// <param name="properties">Properties associated with the event, including those presented in <paramref name="messageTemplate"/>.</param>
    /// <param name="traceId">The id of the trace that was active when the event was created, if any.</param>
    /// <param name="spanId">The id of the span that was active when the event was created, if any.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="messageTemplate"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="properties"/> is <code>null</code></exception>
    [CLSCompliant(false)]
    public LogEvent(DateTimeOffset timestamp, LogEventLevel level, Exception? exception, MessageTemplate messageTemplate, IEnumerable<LogEventProperty> properties, ActivityTraceId traceId, ActivitySpanId spanId)
        : this(timestamp, level, exception, messageTemplate, new Dictionary<string, LogEventPropertyValue>(), traceId, spanId)
    {
        Guard.AgainstNull(properties);

        foreach (var property in properties)
            AddOrUpdateProperty(property);
    }

    internal LogEvent(DateTimeOffset timestamp, LogEventLevel level, Exception? exception, MessageTemplate messageTemplate, EventProperty[] properties, ActivityTraceId traceId, ActivitySpanId spanId)
        : this(timestamp, level, exception, messageTemplate, new Dictionary<string, LogEventPropertyValue>(Guard.AgainstNull(properties).Length), traceId, spanId)
    {
        for (var i = 0; i < properties.Length; ++i)
            _properties[properties[i].Name] = properties[i].Value;
    }

    /// <summary>
    /// The time at which the event occurred.
    /// </summary>
    public DateTimeOffset Timestamp { get; private set; }

    /// <summary>
    /// The level of the event.
    /// </summary>
    public LogEventLevel Level { get; private set; }

    /// <summary>
    /// The id of the trace that was active when the event was created, if any.
    /// </summary>
    [CLSCompliant(false)]
    public ActivityTraceId? TraceId => _traceId == default ? null : _traceId;

    /// <summary>
    /// The id of the span that was active when the event was created, if any.
    /// </summary>
    [CLSCompliant(false)]
    public ActivitySpanId? SpanId => _spanId == default ? null : _spanId;

    /// <summary>
    /// The message template describing the event.
    /// </summary>
    public MessageTemplate MessageTemplate { get; private set; }

    /// <summary>
    /// Render the message template to the specified output, given the properties associated
    /// with the event.
    /// </summary>
    /// <param name="output">The output.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
    public void RenderMessage(TextWriter output, IFormatProvider? formatProvider = null)
    {
        MessageTemplate.Render(Properties, output, formatProvider);
    }

    /// <summary>
    /// Render the message template given the properties associated
    /// with the event, and return the result.
    /// </summary>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
    public string RenderMessage(IFormatProvider? formatProvider = null)
    {
        return MessageTemplate.Render(Properties, formatProvider);
    }

    /// <summary>
    /// Properties associated with the event, including those presented in <see cref="LogEvent.MessageTemplate"/>.
    /// </summary>
    public IReadOnlyDictionary<string, LogEventPropertyValue> Properties => _properties;

    /// <summary>
    /// An exception associated with the event, or null.
    /// </summary>
    public Exception? Exception { get; private set; }

    /// <summary>
    /// Add a property to the event if not already present, otherwise, update its value.
    /// </summary>
    /// <param name="property">The property to add or update.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="property"/> is <code>null</code></exception>
    public void AddOrUpdateProperty(LogEventProperty property)
    {
        Guard.AgainstNull(property);

        _properties[property.Name] = property.Value;
    }

    /// <summary>
    /// Add a property to the event if not already present, otherwise, update its value.
    /// </summary>
    /// <param name="property">The property to add or update.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="property"/> is <code>default</code></exception>
    internal void AddOrUpdateProperty(in EventProperty property)
    {
        if (property.Equals(EventProperty.None)) throw new ArgumentNullException(nameof(property));

        _properties[property.Name] = property.Value;
    }

    /// <summary>
    /// Add a property to the event if not already present.
    /// </summary>
    /// <param name="property">The property to add.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="property"/> is <code>null</code></exception>
    public void AddPropertyIfAbsent(LogEventProperty property)
    {
        Guard.AgainstNull(property);

#if FEATURE_DICTIONARYTRYADD
        _properties.TryAdd(property.Name, property.Value);
#else
        if (!_properties.ContainsKey(property.Name))
            _properties.Add(property.Name, property.Value);
#endif
    }

    /// <summary>
    /// Add a property to the event if not already present.
    /// </summary>
    /// <param name="property">The property to add.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="property"/> is <code>default</code></exception>
    internal void AddPropertyIfAbsent(in EventProperty property)
    {
        if (property.Equals(EventProperty.None)) throw new ArgumentNullException(nameof(property));

#if FEATURE_DICTIONARYTRYADD
        _properties.TryAdd(property.Name, property.Value);
#else
        if (!_properties.ContainsKey(property.Name))
            _properties.Add(property.Name, property.Value);
#endif
    }

    internal void AddPropertyIfAbsent(ILogEventPropertyFactory factory, string name, object? value, bool destructureObjects = false)
    {
        if (!_properties.ContainsKey(name))
        {
            _properties.Add(
                name,
                factory is ILogEventPropertyValueFactory factory2
                    ? factory2.CreatePropertyValue(value, destructureObjects)
                    : factory.CreateProperty(name, value, destructureObjects).Value);
        }
    }

    /// <summary>
    /// Remove a property from the event, if present. Otherwise no action
    /// is performed.
    /// </summary>
    /// <param name="propertyName">The name of the property to remove.</param>
    public void RemovePropertyIfPresent(string propertyName)
    {
        _properties.Remove(propertyName);
    }

    /// <summary>
    /// Construct a <see cref="LogEvent"/> using pre-allocated values for internal fields. Normally,
    /// the <see cref="LogEvent"/> constructor allocates a dictionary to back <see cref="Properties"/>,
    /// so that this is not unexpectedly shared. This is unnecessary in many integration scenarios,
    /// leading to an additional nontrivial <see cref="Dictionary{TKey,TValue}"/> allocation. This
    /// method allows specialized callers to avoid that overhead.
    /// </summary>
    /// <remarks>
    /// Because this method exposes parameters that essentially map 1:1 with internal fields of <see cref="LogEvent"/>,
    /// the parameter list may change across major Serilog versions.
    /// </remarks>
    /// <param name="timestamp">The time at which the event occurred.</param>
    /// <param name="level">The level of the event.</param>
    /// <param name="exception">An exception associated with the event, or null.</param>
    /// <param name="messageTemplate">The message template describing the event.</param>
    /// <param name="properties">Properties associated with the event, including those presented in <paramref name="messageTemplate"/>.</param>
    /// <param name="traceId">The id of the trace that was active when the event was created, if any.</param>
    /// <param name="spanId">The id of the span that was active when the event was created, if any.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="messageTemplate"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="properties"/> is <code>null</code></exception>
    /// <returns>A constructed <see cref="LogEvent"/>.</returns>
    [CLSCompliant(false)]
    public static LogEvent UnstableAssembleFromParts(
        DateTimeOffset timestamp,
        LogEventLevel level,
        Exception? exception,
        MessageTemplate messageTemplate,
        Dictionary<string, LogEventPropertyValue> properties,
        ActivityTraceId traceId,
        ActivitySpanId spanId)
    {
        return new LogEvent(timestamp, level, exception, messageTemplate, properties, traceId, spanId);
    }

    internal LogEvent Copy()
    {
        var properties = new Dictionary<string, LogEventPropertyValue>(_properties);

        return new LogEvent(
            Timestamp,
            Level,
            Exception,
            MessageTemplate,
            properties,
            TraceId ?? default,
            SpanId ?? default);
    }
}
