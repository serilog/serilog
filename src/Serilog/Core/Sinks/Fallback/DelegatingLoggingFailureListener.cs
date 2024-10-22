// Copyright © Serilog Contributors
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

namespace Serilog.Core.Sinks;

sealed class DelegatingLoggingFailureListener(ILogEventSink sink) : ILoggingFailureListener
{
    public void OnLoggingFailed(object sender, LoggingFailureKind kind, string message, IReadOnlyCollection<LogEvent>? events, Exception? exception)
    {
        if (events is not null && kind is LoggingFailureKind.Final or LoggingFailureKind.Permanent)
        {
            foreach (var logEvent in events)
            {
                sink.Emit(logEvent);
            }
        }
    }
}