// Copyright 2016-2020 Serilog Contributors
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

namespace Serilog.Core;

class LevelOverrideMap
{
    readonly LogEventLevel _defaultMinimumLevel;
    readonly LoggingLevelSwitch? _defaultLevelSwitch;

    struct LevelOverride
    {
        public LevelOverride(string context, LoggingLevelSwitch levelSwitch)
        {
            Context = context;
            LevelSwitch = levelSwitch;
        }

        public string Context { get; }

        public LoggingLevelSwitch LevelSwitch { get; }
    }

    // There are two possible strategies to apply:
    //   1. Keep some bookkeeping data to consult when a new context is encountered, and a concurrent dictionary
    //        for exact matching ~ O(1), but slow and requires fences/locks; or,
    //   2. O(n) search over the raw configuration data every time (fast for small sets of overrides).
    // This implementation assumes there will only be a few overrides in each application, so chooses (2). This
    // is an assumption that's up for debate.
    readonly LevelOverride[] _overrides;

    public LevelOverrideMap(
        IDictionary<string, LoggingLevelSwitch> overrides,
        LogEventLevel defaultMinimumLevel,
        LoggingLevelSwitch? defaultLevelSwitch)
    {
        Guard.AgainstNull(overrides);

        _defaultLevelSwitch = defaultLevelSwitch;
        _defaultMinimumLevel = defaultLevelSwitch != null ? LevelAlias.Minimum : defaultMinimumLevel;

        // Descending order means that if we have a match, we're sure about it being the most specific.
        _overrides = overrides
            .OrderByDescending(o => o.Key)
            .Select(o => new LevelOverride(o.Key, o.Value))
            .ToArray();
    }

    public void GetEffectiveLevel(
        ReadOnlySpan<char> context,
        out LogEventLevel minimumLevel,
        out LoggingLevelSwitch? levelSwitch)
    {
        foreach (var levelOverride in _overrides)
        {
            var overrideContext = levelOverride.Context;
            if (context.StartsWith(overrideContext.AsSpan()) &&
                (context.Length == overrideContext.Length || context[overrideContext.Length] == '.'))
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
