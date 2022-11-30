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

namespace Serilog.Events;

/// <summary>
/// A log event.
/// </summary>
public class LogEvent
{
    static readonly object _poolLock = new();
    static readonly Stack<LogEvent> _pool = new();

    static LogEvent GetOrCreate()
    {
        lock (_poolLock)
        {
            return _pool.Count > 0 ? _pool.Pop() : new LogEvent();
        }
    }

    /// <summary>
    /// Construct a new <seealso cref="LogEvent"/> or fetches an existing one from the internal pool.
    /// </summary>
    /// <param name="timestamp">The time at which the event occurred.</param>
    /// <param name="level">The level of the event.</param>
    /// <param name="exception">An exception associated with the event, or null.</param>
    /// <param name="messageTemplate">The message template describing the event.</param>
    /// <param name="properties">Properties associated with the event, including those presented in <paramref name="messageTemplate"/>.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="messageTemplate"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="properties"/> is <code>null</code></exception>
    public static LogEvent GetOrCreate(DateTimeOffset timestamp, LogEventLevel level, Exception? exception, MessageTemplate messageTemplate, IEnumerable<LogEventProperty> properties)
    {
        Guard.AgainstNull(properties);
        var result = GetOrCreate(timestamp, level, exception, messageTemplate);

        foreach (var property in properties)
            result.AddOrUpdateProperty(property);
        return result;
    }

    /// <summary>
    /// Construct a new <seealso cref="LogEvent"/> or fetches an existing one from the internal pool.
    /// </summary>
    /// <param name="timestamp">The time at which the event occurred.</param>
    /// <param name="level">The level of the event.</param>
    /// <param name="exception">An exception associated with the event, or null.</param>
    /// <param name="messageTemplate">The message template describing the event.</param>
    /// <param name="properties">Properties associated with the event, including those presented in <paramref name="messageTemplate"/>.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="messageTemplate"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="properties"/> is <code>null</code></exception>
    internal static LogEvent GetOrCreate(DateTimeOffset timestamp, LogEventLevel level, Exception? exception, MessageTemplate messageTemplate, EventProperty[] properties)
    {
        var result = GetOrCreate(timestamp, level, exception, messageTemplate);

        for (var i = 0; i < properties.Length; ++i)
            result._properties[properties[i].Name] = properties[i].Value;
        return result;
    }

    static LogEvent GetOrCreate(DateTimeOffset timestamp, LogEventLevel level, Exception? exception, MessageTemplate messageTemplate)
    {
        var result = GetOrCreate();
        result.Timestamp = timestamp;
        result.Level = level;
        result.Exception = exception;
        result.MessageTemplate = Guard.AgainstNull(messageTemplate);
        return result;
    }

    readonly Dictionary<string, LogEventPropertyValue> _properties = new();

    /// <summary>
    /// The time at which the event occurred.
    /// </summary>
    public DateTimeOffset Timestamp { get; private set; }

    /// <summary>
    /// The level of the event.
    /// </summary>
    public LogEventLevel Level { get; private set; }

    /// <summary>
    /// The message template describing the event.
    /// </summary>
    public MessageTemplate MessageTemplate { get; private set; } = MessageTemplate.Empty;

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

        if (!_properties.ContainsKey(property.Name))
            _properties.Add(property.Name, property.Value);
    }

    /// <summary>
    /// Add a property to the event if not already present.
    /// </summary>
    /// <param name="property">The property to add.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="property"/> is <code>default</code></exception>
    internal void AddPropertyIfAbsent(in EventProperty property)
    {
        if (property.Equals(EventProperty.None)) throw new ArgumentNullException(nameof(property));

        if (!_properties.ContainsKey(property.Name))
            _properties.Add(property.Name, property.Value);
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

    internal LogEvent Copy()
    {
        var result = GetOrCreate(Timestamp, Level, Exception, MessageTemplate);

        foreach (var key in _properties.Keys)
            result._properties.Add(key, _properties[key]);

        return result;
    }
}
