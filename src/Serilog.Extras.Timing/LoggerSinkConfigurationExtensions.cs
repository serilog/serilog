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
using System.Collections.Generic;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extras.Sinks;
using Serilog.Extras.Timing;

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="LoggerSinkConfiguration"/> to add additional
    /// capabilities.
    /// </summary>
    public static class LoggerSinkConfigurationExtensions
    {
        /// <summary>
        /// Write buffered log events to a sub-logger, where further processing may occur. 
        /// Only when the threshold level is reached, the buffered log events will be send to the sink.
        /// Events through the sub-logger will be constrained by filters and enriched by enrichers that are
        /// active in the parent. A sub-logger cannot be used to log at a more verbose level, but
        /// a less verbose level is possible.
        /// </summary>
        /// <param name="threshHoldLevel">The minimum level that needs to be reached before the buffer is send to the sink.</param>
        /// <param name="loggerSinkConfiguration"></param>
        /// <param name="bufferSize">The number of logevents to keep in the buffer.</param>
        /// <param name="configureLogger">An action that configures the sub-logger.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration ThresholdLogger(this LoggerSinkConfiguration loggerSinkConfiguration,
            int bufferSize,
            LogEventLevel threshHoldLevel,
            Action<LoggerConfiguration> configureLogger,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (configureLogger == null) throw new ArgumentNullException("configureLogger");
            var lc = new LoggerConfiguration();
            configureLogger(lc);
            return loggerSinkConfiguration.Sink(new BufferedSink(bufferSize, threshHoldLevel, (ILogEventSink)lc.CreateLogger()), restrictedToMinimumLevel);
        }
    }
}