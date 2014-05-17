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

using System;
using System.Linq;
using Loggly;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Sinks.Loggly
{
    /// <summary>
    /// Writes log events to the Loggly.com service.
    /// </summary>
    public class LogglySink : ILogEventSink
    {
        readonly IFormatProvider _formatProvider;
        Logger _client;

        /// <summary>
        /// Construct a sink that saves logs to the specified storage account. Properties are being send as data and the level is used as tag.
        /// </summary>
        ///  <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="inputKey">The input key as found on the Loggly website.</param>
        public LogglySink(IFormatProvider formatProvider, string inputKey)
        {
            _formatProvider = formatProvider;

            _client = new Logger(inputKey);
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            var category = "info";
            switch (logEvent.Level)
            {
                case LogEventLevel.Verbose:
                case LogEventLevel.Debug:
                    category = "verbose";
                    break;
                case LogEventLevel.Information:
                    category = "info";
                    break;
                case LogEventLevel.Warning:
                    category = "warning";
                    break;
                case LogEventLevel.Error:
                case LogEventLevel.Fatal:
                    category = "error";
                    break;
                default:
                    SelfLog.WriteLine("Unexpected logging level, writing to loggly as Info");

                    break;
            }

            var properties = logEvent.Properties
                         .Select(pv => new { Name = pv.Key, Value = LogglyPropertyFormatter.Simplify(pv.Value) })
                         .ToDictionary(a => a.Name, b => b.Value);

            if (logEvent.Exception != null)
                properties.Add("Exception", logEvent.Exception);

            _client.Log(logEvent.RenderMessage(_formatProvider), category, properties);



        }
    }
}
