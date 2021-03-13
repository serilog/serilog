// Copyright 2016-2021 Serilog Contributors
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
using Serilog.Events;

namespace Serilog.Core
{
    class LevelOverrideMap<TSwitch>
    {
        readonly LogEventLevel _defaultMinimumLevel;
        readonly TSwitch _defaultLevelSwitch;

        struct LevelOverride
        {
            public LevelOverride(string context, TSwitch levelSwitch)
            {
                Context = context;
                LevelSwitch = levelSwitch;
            }

            public string Context { get; }

            public TSwitch LevelSwitch { get; }
        }

        // There are two possible strategies to apply:
        //   1. Keep some bookkeeping data to consult when a new context is encountered, and a concurrent dictionary
        //        for exact matching ~ O(1), but slow and requires fences/locks; or,
        //   2. O(n) search over the raw configuration data every time (fast for small sets of overrides).
        // This implementation assumes there will only be a few overrides in each application, so chooses (2). This
        // is an assumption that's up for debate.
        private LevelOverride[] _overrides;


        public LevelOverrideMap(
            IDictionary<string, TSwitch> overrides,
            LogEventLevel defaultMinimumLevel,
            TSwitch defaultLevelSwitch)
        {
            if (overrides == null) throw new ArgumentNullException(nameof(overrides));

            _defaultLevelSwitch = defaultLevelSwitch;
            _defaultMinimumLevel = defaultLevelSwitch != null ? LevelAlias.Minimum : defaultMinimumLevel;

            // Descending order means that if we have a match, we're sure about it being the most specific.
            _overrides = overrides
                .OrderByDescending(o => o.Key)
                .Select(o => new LevelOverride(o.Key, o.Value))
                .ToArray();
        }

        /// <summary>
        /// Add a new context override.
        /// </summary>
        internal void Add(string context, TSwitch levelSwitch)
        {
            // Append() is only available on .NET Standard 2.0+
            var tempOverrides = _overrides.ToList();
            tempOverrides.Add(new LevelOverride(context, levelSwitch));

            // Descending order means that if we have a match, we're sure about it being the most specific.
            _overrides = tempOverrides
                .OrderByDescending(o => o.Context)
                .ToArray();
        }

        public void GetEffectiveLevel(
#if FEATURE_SPAN
            ReadOnlySpan<char> context,
#else
            string context,
#endif
            out LogEventLevel minimumLevel,
            out TSwitch levelSwitch)
        {
            foreach (var levelOverride in _overrides)
            {
                if (context.StartsWith(levelOverride.Context) &&
                   (context.Length == levelOverride.Context.Length || context[levelOverride.Context.Length] == '.'))
                {
                    minimumLevel = LevelAlias.Minimum;
                    levelSwitch = levelOverride.LevelSwitch;
                    return;
                }
            }

            minimumLevel = _defaultMinimumLevel;
            levelSwitch = _defaultLevelSwitch;
        }
    }
}
