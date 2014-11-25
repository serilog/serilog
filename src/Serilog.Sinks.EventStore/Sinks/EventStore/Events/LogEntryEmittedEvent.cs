// Copyright 2014 Serilog Contributors
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

using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serilog.Sinks.EventStore
{
    /// <summary>
    /// A wrapper class for <see cref="Events.LogEvent"/> that is used to store as an event in the EventStore.
    /// </summary>
    public class LogEntryEmittedEvent
    {
        /// <summary>
        /// Construct a new <see cref="LogEvent"/>.
        /// </summary>
        public LogEntryEmittedEvent()
        {
        }

        /// <summary>
        /// Construct a new <see cref="LogEvent"/>.
        /// </summary>
        public LogEntryEmittedEvent(LogEvent logEvent, string renderedMessage)
        {
            if (logEvent == null)
            {
                throw new ArgumentNullException("The logEvent must not be null.");
            }
            if (renderedMessage == null)
            {
                throw new ArgumentNullException("The renderedMessage cannot be null.");
            }
            else if (!renderedMessage.Any())
            {
                throw new ArgumentException("The renderedMessage cannot be an empty string.");
            }

            Timestamp = logEvent.Timestamp;
            Exception = logEvent.Exception;
            MessageTemplate = logEvent.MessageTemplate.Text;
            Level = logEvent.Level;
            RenderedMessage = renderedMessage;
            Properties = new Dictionary<string, object>();
            foreach (var pair in logEvent.Properties)
            {
                Properties.Add(pair.Key, EventStorePropertyFormatter.Simplify(pair.Value));
            }
        }

        /// <summary>
        /// The time at which the event occurred.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

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
        /// Properties associated with the event, including those presented in <see cref="Events.LogEvent.MessageTemplate"/>.
        /// </summary>
        public IDictionary<string, object> Properties { get; set; }
    }
}