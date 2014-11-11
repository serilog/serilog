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
using System.Globalization;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.ApplicationInsights
{
    /// <summary>
    /// Writes log events to a Microsoft Application Insights account. Inspired by their NLog Appender implementation.
    /// </summary>
    public class ApplicationInsightsSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;

        /// <summary>
        /// The Application Insights InstrumentationKey for your application.
        /// </summary>
        private string _instrumentationKey;

        /// <summary>
        /// Holds the actual Application Insights TelemetryClient (Logging Controller) that will be used for logging.
        /// </summary>
        private TelemetryClient _telemetryClient;
        
        /// <summary>
        /// Construct a sink that saves logs to the specified storage account.
        /// </summary>
        /// <param name="applicationInsightsInstrumentationKey">The ID that determines the application component under which your data appears in Application Insights.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public ApplicationInsightsSink(string applicationInsightsInstrumentationKey, IFormatProvider formatProvider)
        {
            if (applicationInsightsInstrumentationKey == null) throw new ArgumentNullException("applicationInsightsInstrumentationKey");
            if (string.IsNullOrWhiteSpace(applicationInsightsInstrumentationKey)) throw new ArgumentOutOfRangeException("applicationInsightsInstrumentationKey", "Cannot be empty.");

            _formatProvider = formatProvider;
            _instrumentationKey = applicationInsightsInstrumentationKey;
            _telemetryClient = new TelemetryClient();

            if (string.IsNullOrWhiteSpace(_instrumentationKey) == false)
                _telemetryClient.Context.InstrumentationKey = _instrumentationKey;
        }
        
        #region Implementation of ILogEventSink

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            // writing logEvent as TraceTelemetry properties
            var traceTelemetry = new TraceTelemetry(logEvent.RenderMessage(_formatProvider));

            // and forwaring properties and logEvent Data to the traceTelemetry's properties
            var properties = traceTelemetry.Context.Properties;
            properties.Add("Level", logEvent.Level.ToString());
            properties.Add("TimeStamp", logEvent.Timestamp.ToString(CultureInfo.InvariantCulture));
            properties.Add("MessageTemplate", logEvent.MessageTemplate.Text);

            if (logEvent.Exception != null)
            {
                properties.Add("Exception", logEvent.Exception.Message);

                if (string.IsNullOrWhiteSpace(logEvent.Exception.Source) == false)
                    properties.Add("ExceptionSource", logEvent.Exception.Source);

                if (string.IsNullOrWhiteSpace(logEvent.Exception.StackTrace) == false)
                    properties.Add("ExceptionStackTrace", logEvent.Exception.StackTrace);
            }

            foreach (var property in logEvent.Properties)
            {
                if (property.Value == null)
                    continue;

                if (properties.ContainsKey(property.Key) == false)
                    properties.Add(property.Key, property.Value.ToString());
                else
                {
                    // this isn't really elegant, but as as two property dictionaries are basically merged here, it's better to append rather than to overwrite/skip
                    properties.Add(property.Key + " #2", property.Value.ToString());
                }
            }

            // an finally - this logs the message & its metadata to application insights
            _telemetryClient.Track(traceTelemetry);
        }

        #endregion
    }
}
