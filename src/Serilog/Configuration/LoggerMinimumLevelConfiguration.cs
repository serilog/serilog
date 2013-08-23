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
using Serilog.Events;

namespace Serilog.Configuration
{
    /// <summary>
    /// Controls sink configuration.
    /// </summary>
    public class LoggerMinimumLevelConfiguration
    {
        readonly LoggerConfiguration _loggerConfiguration;
        readonly Action<LogEventLevel> _setMinimum;

        internal LoggerMinimumLevelConfiguration(LoggerConfiguration loggerConfiguration, Action<LogEventLevel> setMinimum)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (setMinimum == null) throw new ArgumentNullException("setMinimum");
            _loggerConfiguration = loggerConfiguration;
            _setMinimum = setMinimum;
        }

        /// <summary>
        /// Sets the minimum level at which events will be passed to sinks.
        /// </summary>
        /// <param name="minimumLevel">The minimum level to set.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Is(LogEventLevel minimumLevel)
        {
            _setMinimum(minimumLevel);
            return _loggerConfiguration;
        }

        /// <summary>
        /// Anything and everything you might want to know about
        /// a running block of code.
        /// </summary>
        public LoggerConfiguration Verbose()
        {
            return Is(LogEventLevel.Verbose);
        }

        /// <summary>
        /// Internal system events that aren't necessarily
        /// observable from the outside.
        /// </summary>
        public LoggerConfiguration Debug()
        {
            return Is(LogEventLevel.Debug);
        }

        /// <summary>
        /// The lifeblood of operational intelligence - things
        /// happen.
        /// </summary>
        public LoggerConfiguration Information()
        {
            return Is(LogEventLevel.Information);
        }

        /// <summary>
        /// Service is degraded or endangered.
        /// </summary>
        public LoggerConfiguration Warning()
        {
            return Is(LogEventLevel.Warning);
        }

        /// <summary>
        /// Functionality is unavailable, invariants are broken
        /// or data is lost.
        /// </summary>
        public LoggerConfiguration Error()
        {
            return Is(LogEventLevel.Error);
        }

        /// <summary>
        /// If you have a pager, it goes off when one of these
        /// occurs.
        /// </summary>
        public LoggerConfiguration Fatal()
        {
            return Is(LogEventLevel.Fatal);
        }
    }
}
