// Copyright 2013-2015 Serilog Contributors
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
using Serilog.Events;

namespace Serilog.Core.Sinks
{
    class RestrictedSink : ILogEventSink, IDisposable
    {
        readonly ILogEventSink _sink;
        readonly LoggingLevelSwitch _levelSwitch;

        public RestrictedSink(ILogEventSink sink, LoggingLevelSwitch levelSwitch)
        {
            if (sink == null) throw new ArgumentNullException(nameof(sink));
            if (levelSwitch == null) throw new ArgumentNullException(nameof(levelSwitch));
            _sink = sink;
            _levelSwitch = levelSwitch;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));

            if ((int)logEvent.Level < (int)_levelSwitch.MinimumLevel)
                return;

            _sink.Emit(logEvent);
        }

        public void Dispose()
        {
           (_sink as IDisposable)?.Dispose();
        }
    }
}
