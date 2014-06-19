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
using Microsoft.ApplicationInsights.Tracing;
using Newtonsoft.Json;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.ApplicationInsights
{
    /// <summary>
    /// Writes log events to a Microsoft Application Insights account. Inspired by their NLog Appender implementation.
    /// </summary>
    public class ApplicationInsightsSink : ILogEventSink, IDisposable
    {
        private readonly IFormatProvider _formatProvider;

        /// <summary>
        /// Holds the actual Application Insights Logging Controller
        /// </summary>
        private LoggingController _loggingController;
        
        /// <summary>
        /// Construct a sink that saves logs to the specified storage account.
        /// </summary>
        /// <param name="applicationInsightsComponentId">The ID that determines the application component under which your data appears in Application Insights.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public ApplicationInsightsSink(string applicationInsightsComponentId, IFormatProvider formatProvider)
        {
            if (applicationInsightsComponentId == null) throw new ArgumentNullException("applicationInsightsComponentId");
            if (string.IsNullOrWhiteSpace(applicationInsightsComponentId)) throw new ArgumentOutOfRangeException("applicationInsightsComponentId", "Cannot be empty.");

            _formatProvider = formatProvider;

            _loggingController = LoggingController.CreateLoggingController(applicationInsightsComponentId);
        }
        
        #region Implementation of ILogEventSink

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            // this logs the message & its metadata to application insights
            this._loggingController.LogMessageWithData(logEvent.RenderMessage(this._formatProvider), logEvent, new JsonConverter[] { ApplicationInsightsDictionaryJsonConverter.Instance });
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._loggingController != null)
            {
                this._loggingController.Dispose();
                this._loggingController = null;
            }
        }

        #endregion
    }
}
