// Copyright 2020 Serilog Contributors
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

class DisposeDelegatingSink : ILogEventSink, IDisposable
{
    readonly ILogEventSink _sink;
    readonly IDisposable _disposable;

    public DisposeDelegatingSink(ILogEventSink sink, IDisposable disposable)
    {
        _sink = sink ?? throw new ArgumentNullException(nameof(sink));
        _disposable = disposable ?? throw new ArgumentNullException(nameof(disposable));
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }

    public void Emit(LogEvent logEvent)
    {
        _sink.Emit(logEvent);
    }
}
