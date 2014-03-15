// Copyright 2013 Serilog Contributors
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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog.Events;

namespace Serilog.Sinks.ElasticSearch.Data
{
    /// <summary>
    /// A wrapper class for <see cref="Events.LogEvent"/> that is used to store as a document in ElasticSearch.
    /// </summary>
    public class LogEvent
    {
        /// <summary>
        /// Construct a new <see cref="LogEvent"/>.
        /// </summary>
        public LogEvent()
        {
        }

        /// <summary>
        /// Construct a new <see cref="LogEvent"/>.
        /// </summary>
        public LogEvent(Events.LogEvent logEvent, string renderedMessage)
        {
            Timestamp = logEvent.Timestamp;
            Exception = logEvent.Exception;
            MessageTemplate = logEvent.MessageTemplate.Text;
            Level = logEvent.Level;
            RenderedMessage = renderedMessage;
            Properties = new Dictionary<string, object>();
            foreach (var pair in logEvent.Properties)
            {
                Properties.Add(pair.Key, ESPropertyFormatter.Simplify(pair.Value));
            }
        }

        /// <summary>
        /// The time at which the event occurred.
        /// </summary>
        [JsonProperty(PropertyName = "@timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// The template that was used for the log message.
        /// </summary>
        [JsonProperty(PropertyName = "messageTemplate")]
        public string MessageTemplate { get; set; }

        /// <summary>
        /// The level of the log.
        /// </summary>
        [JsonProperty(PropertyName = "level")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogEventLevel Level { get; set; }

        /// <summary>
        /// A string representation of the exception that was attached to the log (if any).
        /// </summary>
        [JsonProperty(PropertyName = "exception")]
        public Exception Exception { get; set; }

        /// <summary>
        /// The rendered log message.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string RenderedMessage { get; set; }

        /// <summary>
        /// Properties associated with the event, including those presented in <see cref="Events.LogEvent.MessageTemplate"/>.
        /// </summary>
        [JsonProperty(PropertyName = "fields")]
        public IDictionary<string, object> Properties { get; set; }
    }
}
