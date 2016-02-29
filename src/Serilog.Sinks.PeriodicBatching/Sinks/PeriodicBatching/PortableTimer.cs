// Copyright 2013-2016 Serilog Contributors
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

#if !WAITABLE_TIMER

using Serilog.Debugging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Serilog.Sinks.PeriodicBatching
{
    class PortableTimer : IDisposable
    {
        enum PortableTimerState
        {
            NotWaiting,
            Waiting,
            Active,
            Disposed
        }

        readonly object _stateLock = new object();
        PortableTimerState _state = PortableTimerState.NotWaiting;

        readonly Action<CancellationToken> _onTick;
        readonly CancellationTokenSource _cancel = new CancellationTokenSource();

        public PortableTimer(Action<CancellationToken> onTick)
        {
            if (onTick == null) throw new ArgumentNullException(nameof(onTick));
            _onTick = onTick;
        }

        public async void Start(TimeSpan interval)
        {
            if (interval < TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(interval));

            lock (_stateLock)
            {
                if (_state == PortableTimerState.Disposed)
                    throw new ObjectDisposedException("PortableTimer");

                // There's a little bit of raciness here, but it's needed to support the
                // current API, which allows the tick handler to reenter and set the next interval.

                if (_state == PortableTimerState.Waiting)
                    throw new InvalidOperationException("The timer is already set.");

                if (_cancel.IsCancellationRequested) return;

                _state = PortableTimerState.Waiting;
            }

            try
            {
                if (interval > TimeSpan.Zero)
                    await Task.Delay(interval, _cancel.Token).ConfigureAwait(false);

                _state = PortableTimerState.Active;

                if (!_cancel.Token.IsCancellationRequested)
                {
                    _onTick(_cancel.Token);
                }
            }
            catch (TaskCanceledException tcx)
            {
                SelfLog.WriteLine("The timer was canceled during invocation: {0}", tcx);
            }
            finally
            {
                lock (_stateLock)
                    _state = PortableTimerState.NotWaiting;
            }
        }

        public void Dispose()
        {
            _cancel.Cancel();

            while (true)
            {
                lock (_stateLock)
                {
                    if (_state == PortableTimerState.Disposed ||
                        _state == PortableTimerState.NotWaiting)
                    {
                        _state = PortableTimerState.Disposed;
                        return;
                    }
                }

                Thread.Sleep(10);
            }
        }
    }
}
#endif
