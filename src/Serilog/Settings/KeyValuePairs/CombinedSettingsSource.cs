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
using System.Linq;

namespace Serilog.Settings.KeyValuePairs
{
    class CombinedSettingsSource : ISettingsSource, ISettingsSourceBuilder
    {
        List<ISettingsSource> SettingsSources { get; } = new List<ISettingsSource>();

        internal CombinedSettingsSource()
        {
        }

        internal CombinedSettingsSource(IEnumerable<ISettingsSource> sources)
        {
            SettingsSources.AddRange(sources);
        }

        public ISettingsSourceBuilder AddSource(ISettingsSource settingSource)
        {
            if (settingSource == null) throw new ArgumentNullException(nameof(settingSource));
            SettingsSources.Add(settingSource);
            return this;
        }

        IEnumerable<KeyValuePair<string, string>> ISettingsSource.GetKeyValuePairs()
        {
            var result = new Dictionary<string, string>();
            foreach (var kvp in SettingsSources.SelectMany(x => x.GetKeyValuePairs()))
            {
                result[kvp.Key] = kvp.Value;
            }
            return result;

        }
    }
}
