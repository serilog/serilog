// Copyright 2015 Serilog Contributors
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
using Serilog.Settings;
using Serilog.Settings.KeyValuePairs;

namespace Serilog.Configuration
{
    /// <summary>
    /// Allows additional setting sources to drive the logger configuration.
    /// </summary>
    public class LoggerSettingsConfiguration
    {
        readonly LoggerConfiguration _loggerConfiguration;

        internal LoggerSettingsConfiguration(LoggerConfiguration loggerConfiguration)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));
            _loggerConfiguration = loggerConfiguration;
        }

        /// <summary>
        /// Apply external settings to the logger configuration.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Settings(ILoggerSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            settings.Configure(_loggerConfiguration);

            return _loggerConfiguration;
        }

        /// <summary>
        /// Apply settings specified in the Serilog key-value setting format to the logger configuration.
        /// </summary>
        /// <param name="settings">A list of key-value pairs describing logger settings.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration KeyValuePairs(IEnumerable<KeyValuePair<string, string>> settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            return Source(new ConstantSettingsSource(settings));
        }

        /// <summary>
        /// Apply settings specified in the Serilog key-value setting format to the logger configuration.
        /// </summary>
        /// <param name="source">A source of key-value settings.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Source(ISettingsSource source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return Settings(new KeyValuePairSettings(source));
        }

        /// <summary>
        /// Apply settings specified from multiple source and combine them keeping the last defined value for each key.
        /// </summary>
        /// <param name="builder">a callback that allows to add Sources of settings to the configuration</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Combined(Func<ISettingsSourceBuilder, ISettingsSourceBuilder> builder)
        {
            var empty = new CombinedSettingsSource();
            var full = (CombinedSettingsSource)builder(empty);

            return Source(full);
        }

        /// <summary>
        /// Apply settings specified from multiple source and combine them keeping the last defined value for each key.
        /// </summary>
        /// <param name="sources">the sources of Settings to combine</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Sources(params ISettingsSource[] sources)
        {
            var combinedSources = new CombinedSettingsSource(sources);
            return Source(combinedSources);
        }
    }
}
