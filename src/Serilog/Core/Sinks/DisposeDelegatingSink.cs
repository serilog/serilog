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

sealed class DisposeDelegatingSink : ILogEventSink, IDisposable
#if FEATURE_ASYNCDISPOSABLE
    , IAsyncDisposable
#endif
{
    readonly ILogEventSink _sink;
    readonly IDisposable? _disposable;

#if FEATURE_ASYNCDISPOSABLE
    readonly IAsyncDisposable? _asyncDisposable;
#endif

    public DisposeDelegatingSink(ILogEventSink sink, IDisposable? disposable
#if FEATURE_ASYNCDISPOSABLE
        , IAsyncDisposable? asyncDisposable
#endif
        )
    {
        _sink = sink;
        _disposable = disposable;

#if FEATURE_ASYNCDISPOSABLE
        _asyncDisposable = asyncDisposable;
#endif
    }

    public void Dispose()
    {
        _disposable?.Dispose();
    }

#if FEATURE_ASYNCDISPOSABLE
    public ValueTask DisposeAsync()
    {
        if (_asyncDisposable != null)
            return _asyncDisposable.DisposeAsync();

        Dispose();
        return default;
    }
#endif

    public void Emit(LogEvent logEvent)
    {
        _sink.Emit(logEvent);
    }
}
