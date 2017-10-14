using System;
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

using System.Collections.Generic;

namespace Serilog.Settings
{
    /// <summary>
    /// A basic implementation of ISettingsSource that can be use as a starting point for other implementations.
    /// </summary>
    public class SettingsSource : ISettingsSource
    {
        List<KeyValuePair<string, string>> _keyValuePairs = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Adds a key-value pair to the source
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        public void AddKeyValuePair(string key, string value)
        {
            this.AddKeyValuePair(new KeyValuePair<string, string>(key, value));
        }

        /// <summary>
        /// Adds a key-value pair to the source
        /// </summary>
        /// <param name="keyValuePair">a key-value pair</param>
        public void AddKeyValuePair(KeyValuePair<string, string> keyValuePair)
        {
            if (keyValuePair.Key == null) throw new ArgumentNullException($"{nameof(keyValuePair)}.{nameof(keyValuePair.Key)}");
            _keyValuePairs.Add(keyValuePair);
        }

        /// <summary>
        /// Retrives the key value pairs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs()
        {
            return _keyValuePairs;
        }
    }
}
