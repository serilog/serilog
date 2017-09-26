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
using Serilog.Events;

namespace Serilog.Core.Sinks
{
    class OverrideRestrictingSink : ILogEventSink
    {
        readonly ILogEventSink _sink;
        readonly LevelOverrideMap _overrideMap;

        public OverrideRestrictingSink(ILogEventSink sink, Dictionary<string, LoggingLevelSwitch> overrides)
        {
            if (overrides == null) throw new ArgumentNullException(nameof(overrides));
            _sink = sink ?? throw new ArgumentNullException(nameof(sink));
            _overrideMap = new LevelOverrideMap(overrides, LevelAlias.Minimum, null);
        }

        public OverrideRestrictingSink(ILogEventSink sink, LevelOverrideMap overrideMap)
        {
            _sink = sink ?? throw new ArgumentNullException(nameof(sink));
            _overrideMap = overrideMap ?? throw new ArgumentNullException(nameof(overrideMap));
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));

            // overrides have been specified, and because only the
            // .ForContext is called on the root logger and not on child loggers
            // we need to check for possible overrides when emitting ...

            if (logEvent.Properties.TryGetValue(Constants.SourceContextPropertyName, out var sourceContext))
            {
                if (sourceContext is ScalarValue scalar)
                {
                    // maybe do not emit based on namespace overrides
                    if (scalar.Value is string context)
                    {
                        LogEventLevel minLevel;
                        LoggingLevelSwitch levelSwitch;
                        _overrideMap.GetEffectiveLevel(context, out minLevel, out levelSwitch);

                        if (logEvent.Level < minLevel || (levelSwitch != null) && logEvent.Level < levelSwitch.MinimumLevel)
                        {
                            // event is not critical enough to carry on ...
                            return;
                        }
                    }
                }
            }

            _sink.Emit(logEvent);
        }
    }
}
