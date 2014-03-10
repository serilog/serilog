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
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.ElmahIO;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.ElmahIO() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationElmahIOExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events to the elmah.io webservice.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="logId">The log id as found on the elmah.io website.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration ElmahIO(
            this LoggerSinkConfiguration loggerConfiguration,
             Guid logId,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            return loggerConfiguration.Sink(
                new ElmahIOSink(formatProvider, logId),
                restrictedToMinimumLevel);
        }

    }
}
