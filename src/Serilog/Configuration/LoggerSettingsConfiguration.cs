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
            _loggerConfiguration = loggerConfiguration ?? throw new ArgumentNullException(nameof(loggerConfiguration));
        }

        /// <summary>
        /// Apply external settings to the logger configuration.
        /// </summary>
        /// <param name="settings">The logger settings.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="settings"/> is <code>null</code></exception>
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
        /// <remarks>In case of duplicate keys, the last value for the key is kept and the previous ones are ignored.</remarks>
        /// <exception cref="ArgumentNullException">When <paramref name="settings"/> is <code>null</code></exception>
        public LoggerConfiguration KeyValuePairs(IEnumerable<KeyValuePair<string, string>> settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            var uniqueSettings = new Dictionary<string, string>();
            foreach (var kvp in settings)
            {
                uniqueSettings[kvp.Key] = kvp.Value;
            }
            return KeyValuePairs(uniqueSettings);
        }

        LoggerConfiguration KeyValuePairs(IReadOnlyDictionary<string, string> settings)
        {
            return Settings(new KeyValuePairSettings(settings));
        }
    }
}
