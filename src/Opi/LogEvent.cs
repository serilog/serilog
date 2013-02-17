using System;
using System.Collections.Generic;

namespace Opi
{
    public class LogEvent
    {
        private readonly DateTime _timeStamp;
        private readonly LogEventLevel _level;
        private readonly Exception _exception;
        private readonly string _messageTemplate;
        private readonly Dictionary<string, LogEventProperty> _properties;

        public LogEvent(DateTime timeStamp, LogEventLevel level, Exception exception, string messageTemplate, IEnumerable<LogEventProperty> properties)
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

        public DateTime TimeStamp
        {
            get { return _timeStamp; }
        }

        public LogEventLevel Level
        {
            get { return _level; }
        }

        public string MessageTemplate
        {
            get { return _messageTemplate; }
        }

        public IReadOnlyDictionary<string, LogEventProperty> Properties
        {
            get { return _properties; }
        }

        public Exception Exception
        {
            get { return _exception; }
        }

        public void AddOrUpdateProperty(string propertyName, object value)
        {
            AddOrUpdateProperty(new LogEventProperty(propertyName, LogEventPropertyValue.For(value)));
        }

        public void AddOrUpdateProperty(LogEventProperty property)
        {
            if (property == null) throw new ArgumentNullException("property");
            _properties[property.Name] = property;
        }

        public void AddPropertyIfAbsent(string propertyName, object value)
        {
            AddPropertyIfAbsent(new LogEventProperty(propertyName, LogEventPropertyValue.For(value)));
        }

        public void AddPropertyIfAbsent(LogEventProperty property)
        {
            if (property == null) throw new ArgumentNullException("property");
            if (!_properties.ContainsKey(property.Name))
                _properties.Add(property.Name, property);
        }

        public void RemovePropertyIfPresent(string propertyName)
        {
            _properties.Remove(propertyName);
        }
    }
}