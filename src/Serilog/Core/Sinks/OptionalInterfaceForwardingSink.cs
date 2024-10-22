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

sealed class OptionalInterfaceForwardingSink : ILogEventSink, IDisposable, ISetLoggingFailureListener
#if FEATURE_ASYNCDISPOSABLE
    , IAsyncDisposable
#endif
{
    readonly ILogEventSink _sink;
    readonly ILogEventSink _receiver;

    public OptionalInterfaceForwardingSink(ILogEventSink sink, ILogEventSink receiver)
    {
        _sink = sink;
        _receiver = receiver;
    }

    public void Dispose()
    {
        if (_sink is IDisposable inner)
        {
            inner.Dispose();
        }

        (_receiver as IDisposable)?.Dispose();
    }

#if FEATURE_ASYNCDISPOSABLE
    public async ValueTask DisposeAsync()
    {
        if (_sink is IAsyncDisposable innerAsync)
        {
            await innerAsync.DisposeAsync();
        }
        else if (_sink is IDisposable inner)
        {
            inner.Dispose();
        }

        if (_receiver is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync();
            return;
        }

        (_receiver as IDisposable)?.Dispose();
    }
#endif

    public void Emit(LogEvent logEvent)
    {
        _sink.Emit(logEvent);
    }

    public void SetFailureListener(ILoggingFailureListener listener)
    {
        (_sink as ISetLoggingFailureListener)?.SetFailureListener(listener);
        (_receiver as ISetLoggingFailureListener)?.SetFailureListener(listener);
    }

    public static bool SupportsAny(ILogEventSink possibleReceiver)
    {
        return possibleReceiver is ISetLoggingFailureListener
                or IDisposable
#if FEATURE_ASYNCDISPOSABLE
                or IAsyncDisposable
#endif
            ;
    }

    public static bool SupportsAll(ILogEventSink possibleReceiver)
    {
        return possibleReceiver is ISetLoggingFailureListener
                and IDisposable
#if FEATURE_ASYNCDISPOSABLE
                and IAsyncDisposable
#endif
            ;
    }
}
