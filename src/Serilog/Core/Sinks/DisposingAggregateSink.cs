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

sealed class DisposingAggregateSink : ILogEventSink, IDisposable
#if FEATURE_ASYNCDISPOSABLE
    , IAsyncDisposable
#endif
{
    readonly ILogEventSink[] _sinks;

    public DisposingAggregateSink(IEnumerable<ILogEventSink> sinks)
    {
        Guard.AgainstNull(sinks);
        _sinks = sinks.ToArray();
    }

    public void Emit(LogEvent logEvent)
    {
        List<Exception>? exceptions = null;
        foreach (var sink in _sinks)
        {
            try
            {
                sink.Emit(logEvent);
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Caught exception while emitting to sink {0}: {1}", sink, ex);
                exceptions ??= [];
                exceptions.Add(ex);
            }
        }

        if (exceptions != null)
            throw new AggregateException("Failed to emit a log event.", exceptions);
    }

    public void Dispose()
    {
        foreach (var sink in _sinks)
        {
            if (sink is not IDisposable disposable) continue;

            try
            {
                disposable.Dispose();
            }
            catch (Exception ex)
            {
                ReportDisposingException(sink, ex);
            }
        }
    }

#if FEATURE_ASYNCDISPOSABLE
    public async ValueTask DisposeAsync()
    {
        foreach (var sink in _sinks)
        {
            if (sink is IAsyncDisposable asyncDisposable)
            {
                try
                {
                    await asyncDisposable.DisposeAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    ReportDisposingException(sink, ex);
                }
            }
            else if (sink is IDisposable disposable)
            {
                try
                {
                    disposable.Dispose();
                }
                catch (Exception ex)
                {
                    ReportDisposingException(sink, ex);
                }
            }
        }
    }
#endif

    static void ReportDisposingException(ILogEventSink sink, Exception ex)
    {
        SelfLog.WriteLine("Caught exception while disposing sink {0}: {1}", sink, ex);
    }
}
