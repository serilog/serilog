// Copyright 2013-2017 Serilog Contributors
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

// in this namespace so they appear when using LoggerConfiguration extension methods
namespace Serilog
{
    /// <summary>
    /// Extensions to allow combining settings from a sequence of key-value settings.
    /// </summary>
    public static class SettingsSourceBuilderExtensions
    {
        /// <summary>
        /// Adds a series of key-value settings to the combined configuration
        /// </summary>
        /// <param name="self">the builder</param>
        /// <param name="keyValuePairs">the key-value pairs to add</param>
        /// <returns>the builder object to allow chaining</returns>
        public static ISettingsSourceBuilder AddKeyValuePairs(this ISettingsSourceBuilder self,
            IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            if (self == null) throw new ArgumentNullException(nameof(self));
            var source = new ConstantSettingsSource(keyValuePairs);
            return self.AddSource(source);
        }

        /// <summary>
        /// Adds a single key-value setting to the combined configuration
        /// </summary>
        /// <param name="self">the builder</param>
        /// <param name="kvp">a key-value setting</param>
        /// <returns>the builder object to allow chaining</returns>
        public static ISettingsSourceBuilder AddKeyValuePair(this ISettingsSourceBuilder self,
            KeyValuePair<string, string> kvp)
        {
            if (self == null) throw new ArgumentNullException(nameof(self));
            var source = new ConstantSettingsSource(new[] { kvp });
            return self.AddSource(source);
        }

        /// <summary>
        /// Adds a single key-value setting to the combined configuration
        /// </summary>
        /// <param name="self">the builder</param>
        /// <param name="key">the key of the setting</param>
        /// <param name="value">the value of the setting</param>
        /// <returns>the builder object to allow chaining</returns>
        public static ISettingsSourceBuilder AddKeyValuePair(this ISettingsSourceBuilder self,
            string key, string value)
        {
            if (self == null) throw new ArgumentNullException(nameof(self));
            if (key == null) throw new ArgumentNullException(nameof(key));
            var source = new ConstantSettingsSource(new[] { new KeyValuePair<string, string>(key, value) });
            return self.AddSource(source);
        }
    }
}
