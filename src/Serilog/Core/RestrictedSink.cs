// Copyright 2013 Nicholas Blumhardt
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

namespace Serilog.Core
{
    class RestrictedSink : ILogEventSink
    {
        private readonly ILogEventSink _sink;
        private readonly LogEventLevel _restrictedMinimumLevel;

        public RestrictedSink(ILogEventSink sink, LogEventLevel restrictedMinimumLevel)
        {
            if (sink == null) throw new ArgumentNullException("sink");
            _sink = sink;
            _restrictedMinimumLevel = restrictedMinimumLevel;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            if (logEvent.Level < _restrictedMinimumLevel)
                return;

            _sink.Emit(logEvent);
        }
    }
}
