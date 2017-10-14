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
    /// The core interface for a source of key-value settings to define a <see cref="LoggerConfiguration"/>
    /// </summary>
    public interface ISettingsSource
    {
        /// <summary>
        /// Retrieves the key-value settings defined by this source.
        /// </summary>
        /// <returns>A sequence of key-value settings</returns>
        IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs();
    }
}
