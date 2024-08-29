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

class SynchronousFallbackSink(ILogEventSink inner, ILoggingFailureListener listener) : ILogEventSink
{
    public void Emit(LogEvent logEvent)
    {
        try
        {
            inner.Emit(logEvent);
        }
        catch (Exception ex)
        {
            listener.OnLoggingFailed(this, LoggingFailureKind.Permanent, "failed emitting an event", [logEvent], ex);
        }
    }
}