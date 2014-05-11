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
using Serilog.Sinks.Logentries;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.Logentries() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationLogentriesExtensions
    {
        const string DefaultLogentriesOutputTemplate = "{Timestamp:G} [{Level}] {Message}{NewLine}{Exception}";
     
        /// <summary>
        /// Adds a sink that writes log events to the Logentries.com webservice. 
        /// Create a token TCP input for this on the logentries website. 
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="token">The token as found on the Logentries.com website.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{Timestamp:G} [{Level}] {Message}{NewLine}{Exception}".</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="useSsl">Specify if the connection needs to be secured.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration Logentries(
            this LoggerSinkConfiguration loggerConfiguration,
             string token, bool useSsl = true,
            int batchPostingLimit = LogentriesSink.DefaultBatchPostingLimit,
            TimeSpan? period = null,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = DefaultLogentriesOutputTemplate,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("token");

            var defaultedPeriod = period ?? LogentriesSink.DefaultPeriod;

            return loggerConfiguration.Sink(
                new LogentriesSink(outputTemplate,formatProvider, token, useSsl, batchPostingLimit, defaultedPeriod),
                restrictedToMinimumLevel);
        }

    }
}
