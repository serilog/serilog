// Copyright 2016 Serilog Contributors
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
using Serilog.Events;

namespace Serilog.Core
{
    class LevelOverrideMap
    {
        readonly LogEventLevel _defaultMinimumLevel;
        readonly LoggingLevelSwitch _defaultLevelSwitch;

        // There are two possible strategies to apply:
        //   1. Keep some bookkeeping data to consult when a new context is encountered, and a concurrent dictionary
        //        for exact matching ~ O(1), but slow and requires fences/locks; or,
        //   2. O(n) search over the raw configuration data every time (fast for small sets of overrides).
        // This implementation has the ability to update loglevels durig runtime that is why option 1. is preferred
        // concurrent dictionary is not available in netstandard 1.0 thats why a normal dictionary with manual locking is used
        readonly Dictionary<string, LoggingLevelSwitch> _overrides;

        public LevelOverrideMap(
            IDictionary<string, LoggingLevelSwitch> overrides,
            LogEventLevel defaultMinimumLevel,
            LoggingLevelSwitch defaultLevelSwitch)
        {
            if (overrides == null) throw new ArgumentNullException(nameof(overrides));
            _defaultLevelSwitch = defaultLevelSwitch;
            _defaultMinimumLevel = defaultLevelSwitch != null ? LevelAlias.Minimum : defaultMinimumLevel;
            _overrides = new Dictionary<string, LoggingLevelSwitch>(overrides);
        }

        public event Action OverrideAdded;

        public bool HasAnyOverrides()
        {
            lock (_overrides)
            {
                return _overrides.Count > 0;
            }
        }

        public LoggingLevelSwitch GetOrAddOverride(string context)
        {
            var added = false;
            LoggingLevelSwitch levelSwitch;

            lock (_overrides)
            {
                if (!_overrides.TryGetValue(context, out levelSwitch))
                {
                    levelSwitch = _overrides[context] = new LoggingLevelSwitch(_defaultMinimumLevel);
                    added = true;
                }
            }

            if (added)
            {
                OverrideAdded?.Invoke();
            }

            return levelSwitch;
        }

        public void GetEffectiveLevel(string context, out LogEventLevel minimumLevel, out LoggingLevelSwitch levelSwitch)
        {
            lock (_overrides)
            {
                foreach (var prefix in GetKeyPrefixes(context))
                {
                    if (_overrides.TryGetValue(prefix, out levelSwitch))
                    {
                        minimumLevel = LevelAlias.Minimum;
                        return;
                    }
                }
            }

            minimumLevel = _defaultMinimumLevel;
            levelSwitch = _defaultLevelSwitch;
        }

        IEnumerable<string> GetKeyPrefixes(string name)
        {
            while (!string.IsNullOrEmpty(name))
            {
                yield return name;
                var lastIndexOfDot = name.LastIndexOf('.');
                if (lastIndexOfDot == -1)
                {
                    break;
                }
                name = name.Substring(0, lastIndexOfDot);
            }
        }
    }
}
