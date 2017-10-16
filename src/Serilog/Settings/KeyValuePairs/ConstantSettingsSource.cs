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

namespace Serilog.Settings.KeyValuePairs
{
    class ConstantSettingsSource : ISettingsSource
    {
        IEnumerable<KeyValuePair<string, string>> _keyValuePairs;

        public ConstantSettingsSource(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            if (keyValuePairs == null) throw new ArgumentNullException(nameof(keyValuePairs));
            _keyValuePairs = keyValuePairs;
        }

        public IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs()
        {
            return _keyValuePairs;
        }
    }
}
