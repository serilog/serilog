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
using System.ComponentModel;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.Log4Net;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.Log4Net() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationLog4NetExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events as documents to log4net.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="defaultLoggerName">If events are logged using Serilog's <see cref="ILogger.ForContext{T}"/> method,
        /// the type name will be used as the logger name (in line with log4net's behaviour. If no context type is specified,
        /// Serilog will use this value (which defaults to <code>"serilog"</code>) as the logger name.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration Log4Net(
            this LoggerSinkConfiguration loggerConfiguration,
            string defaultLoggerName = "serilog",
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (defaultLoggerName == null) throw new ArgumentNullException("defaultLoggerName");

            return loggerConfiguration.Sink(new Log4NetSink(defaultLoggerName, formatProvider), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Adds a sink that writes log events as documents to log4net.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        [Obsolete("Use named arguments with this method to guarantee forwards-compatibility."), EditorBrowsable(EditorBrowsableState.Never)]
        public static LoggerConfiguration Log4Net(
            this LoggerSinkConfiguration loggerConfiguration,
            LogEventLevel restrictedToMinimumLevel,
            IFormatProvider formatProvider)
        {
            return loggerConfiguration.Log4Net(null, restrictedToMinimumLevel, formatProvider);
        }
    }
}
