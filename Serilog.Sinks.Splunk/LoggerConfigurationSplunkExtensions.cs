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
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.Splunk;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.SplunkViaHttp() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationSplunkExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events as to a Splunk instance via http.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="context">The Splunk context to log to</param>
        /// <param name="batchSizeLimit">The size of the batch to log</param>
        /// <param name="defaultPeriod">The default time for batching</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration SplunkViaHttp(
            this LoggerSinkConfiguration loggerConfiguration,
            SplunkContext context,
            int batchSizeLimit,
            TimeSpan? defaultPeriod,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Debug,
            IFormatProvider formatProvider = null)
        {
  //          var defaultedPeriod = defaultPeriod ?? SplunkViaHttpSink.DefaultPeriod;

//            var sink = new SplunkViaHttpSink(batchSizeLimit, defaultedPeriod, context, formatProvider);

  //          return loggerConfiguration.Sink(sink);
            return null;

        }

        /// <summary>
        /// Adds a sink that writes log events as to a Splunk instance via udp.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration SplunkViaUdp(
            this LoggerSinkConfiguration loggerConfiguration,
            string host,
            int port,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Debug,
            IFormatProvider formatProvider = null)
        {


         //   var sink = new SplunkViaUdpSink(host, port, batchSizeLimit, defaultedPeriod, formatProvider);

           // return loggerConfiguration.Sink(sink);

            return null;
        }
    }
}