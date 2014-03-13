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
using Serilog.Sinks.Loggr;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.Loggr() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationLoggrExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events to the loggr.com webservice. Properties are being send as data and the level is used as tag.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="logKey">The log key as found on the loggr website.</param>
        /// <param name="apiKey">The api key as found on the loggr website.</param>
        /// <param name="useSecure">Use a SSL connection. By default this is false.</param>
        /// <param name="userNameProperty">Specifies the property name to read the username from. By default it is UserName.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration Loggr(
            this LoggerSinkConfiguration loggerConfiguration,
             string logKey, string apiKey, bool useSecure = false, string userNameProperty = "UserName",
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");

            if (string.IsNullOrWhiteSpace(logKey))
                throw new ArgumentNullException("logKey");

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException("apiKey");

            return loggerConfiguration.Sink(
                new LoggrSink(formatProvider, logKey, apiKey, useSecure, userNameProperty),
                restrictedToMinimumLevel);
        }

    }
}
