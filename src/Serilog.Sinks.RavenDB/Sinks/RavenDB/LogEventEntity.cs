using System;
using System.Collections.Generic;
using Serilog.Events;

namespace Serilog.Sinks.RavenDB
{
    /// <summary>
    /// A wrapper class for <see cref="LogEvent"/> that is used to store as a document in RavenDB
    /// </summary>
    public class LogEventEntity
    {
        /// <summary>
        /// Construct a new <seealso cref="LogEventEntity"/>.
        /// </summary>
        public LogEventEntity()
        {
            
        }

        /// <summary>
        /// Construct a new <seealso cref="LogEventEntity"/>.
        /// </summary>
        public LogEventEntity(LogEvent logEvent, IFormatProvider formatProvider)
        {
            TimeStamp = logEvent.Timestamp;
            Exception = logEvent.Exception;
            MessageTemplate = logEvent.MessageTemplate.Text;
            Level = logEvent.Level;
            RenderedMessage = logEvent.RenderMessage(formatProvider);
            Properties = new Dictionary<string, LogEventProperty>();
            foreach (var pair in logEvent.Properties)
            {
                Properties.Add(pair);
            }
        }

        /// <summary>
        /// The time at which the event occurred.
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; }
        /// <summary>
        /// The template that was used for the log message.
        /// </summary>
        public string MessageTemplate { get; set; }
        /// <summary>
        /// The level of the log.
        /// </summary>
        public LogEventLevel Level { get; set; }
        /// <summary>
        /// A string representation of the exception that was attached to the log (if any).
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// The rendered log message.
        /// </summary>
        public string RenderedMessage { get; set; }
        /// <summary>
        /// Properties associated with the event, including those presented in <see cref="LogEvent.MessageTemplate"/>.
        /// </summary>
        public IDictionary<string, LogEventProperty> Properties { get; set; }
    }
}
