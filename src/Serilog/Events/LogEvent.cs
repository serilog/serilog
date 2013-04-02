// Copyright 2013 Nicholas Blumhardt
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
using Serilog.Core;

namespace Serilog.Events
{
    /// <summary>
    /// A log event.
    /// </summary>
    public class LogEvent
    {
        private readonly DateTimeOffset _timeStamp;
        private readonly LogEventLevel _level;
        private readonly Exception _exception;
        private readonly MessageTemplate _messageTemplate;
        private readonly Dictionary<string, LogEventProperty> _properties;

        /// <summary>
        /// Construct a new <seealso cref="LogEvent"/>.
        /// </summary>
        /// <param name="timeStamp">The time at which the event occurred.</param>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">An exception associated with the event, or null.</param>
        /// <param name="messageTemplate">The message template describing the event.</param>
        /// <param name="properties">Properties associated with the event, including those presented in <paramref name="messageTemplate"/>.</param>
        public LogEvent(DateTimeOffset timeStamp, LogEventLevel level, Exception exception, MessageTemplate messageTemplate, IEnumerable<LogEventProperty> properties)
        {
            if (messageTemplate == null) throw new ArgumentNullException("messageTemplate");
            if (properties == null) throw new ArgumentNullException("properties");
            _timeStamp = timeStamp;
            _level = level;
            _exception = exception;
            _messageTemplate = messageTemplate;
            _properties = new Dictionary<string, LogEventProperty>();
            foreach (var p in properties)
                AddOrUpdateProperty(p);
        }

        /// <summary>
        /// The time at which the event occurred.
        /// </summary>
        public DateTimeOffset TimeStamp
        {
            get { return _timeStamp; }
        }

        /// <summary>
        /// The level of the event.
        /// </summary>
        public LogEventLevel Level
        {
            get { return _level; }
        }

        /// <summary>
        /// The message template describing the event.
        /// </summary>
        public MessageTemplate MessageTemplate
        {
            get { return _messageTemplate; }
        }

        /// <summary>
        /// The result of rendering the message template given the properties associated
        /// with the event.
        /// </summary>
        public string RenderedMessage
        {
            get { return MessageTemplate.Render(Properties); }
        }

        /// <summary>
        /// Properties associated with the event, including those presented in <see cref="LogEvent.MessageTemplate"/>.
        /// </summary>
        public IReadOnlyDictionary<string, LogEventProperty> Properties
        {
            get { return _properties; }
        }

        /// <summary>
        /// An exception associated with the event, or null.
        /// </summary>
        public Exception Exception
        {
            get { return _exception; }
        }

        /// <summary>
        /// Add a property to the event if not already present, otherwise, update its value. 
        /// </summary>
        /// <param name="propertyName">The name of the property to add or update.</param>
        /// <param name="value">The property value.</param>
        /// <param name="destructureObjects">If true, and the value is a non-primitive, non-array type,
        /// then the value will be stored as a structure; otherwise, unknown types will be rendered as strings.</param>
        public void AddOrUpdateProperty(string propertyName, object value, bool destructureObjects = false)
        {
            AddOrUpdateProperty(LogEventProperty.For(propertyName, value, destructureObjects));
        }

        /// <summary>
        /// Add a property to the event if not already present, otherwise, update its value. 
        /// </summary>
        /// <param name="property">The property to add or update.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddOrUpdateProperty(LogEventProperty property)
        {
            if (property == null) throw new ArgumentNullException("property");
            _properties[property.Name] = property;
        }

        /// <summary>
        /// Add a property to the event, if not already present.
        /// </summary>
        /// <param name="propertyName">The name of the property to add.</param>
        /// <param name="value">The property value.</param>
        /// <param name="destructureObjects">If true, and the value is a non-primitive, non-array type,
        /// then the value will be stored as a structure; otherwise, unknown types will be rendered as strings.</param>
        public void AddPropertyIfAbsent(string propertyName, object value, bool destructureObjects = false)
        {
            AddPropertyIfAbsent(LogEventProperty.For(propertyName, value));
        }

        /// <summary>
        /// Add a property to the event if not already present. 
        /// </summary>
        /// <param name="property">The property to add.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddPropertyIfAbsent(LogEventProperty property)
        {
            if (property == null) throw new ArgumentNullException("property");
            if (!_properties.ContainsKey(property.Name))
                _properties.Add(property.Name, property);
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
    }
}