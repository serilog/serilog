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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.Observable
{
    sealed class ObservableSink : IObservable<LogEvent>, ILogEventSink, IDisposable
    {
        // Uses memory barriers for non-blocking reads during Emit, and replaces the
        // list of observers completely upon subscribe/unsubscribe.
        // Makes the assumption that list iteration is not
        // mutating - correct but not guaranteed by the BCL.
        readonly object _syncRoot = new object();
        IList<IObserver<LogEvent>> _observers = new List<IObserver<LogEvent>>();
        bool _disposed;

        sealed class Unsubscriber : IDisposable
        {
            readonly ObservableSink _sink;
            readonly IObserver<LogEvent> _observer;

            public Unsubscriber(ObservableSink sink, IObserver<LogEvent> observer)
            {
                if (sink == null) throw new ArgumentNullException(nameof(sink));
                if (observer == null) throw new ArgumentNullException(nameof(observer));
                _sink = sink;
                _observer = observer;
            }

            public void Dispose()
            {
                _sink.Unsubscribe(_observer);
            }
        }

        public IDisposable Subscribe(IObserver<LogEvent> observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));

            lock (_syncRoot)
            {
                if (_disposed)
                    throw new ObjectDisposedException(message: "The Serilog Observable sink is disposed.", innerException: null);

                var old = _observers;
                var newObservers = _observers.Concat(new [] { observer}).ToList();
                while (old != Interlocked.Exchange(ref _observers, newObservers))
                {
                    old = _observers;
                    newObservers = _observers.Concat(new[] { observer }).ToList();
                }
            }

            return new Unsubscriber(this, observer);
        }

        void Unsubscribe(IObserver<LogEvent> observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));

            lock (_syncRoot)
            {
                if (_disposed)
                    throw new ObjectDisposedException(message: "The Serilog Observable sink is disposed.", innerException: null);

                var old = _observers;
                var newObservers = _observers.Except(new[] { observer }).ToList();
                while (old != Interlocked.Exchange(ref _observers, newObservers))
                {
                    old = _observers;
                    newObservers = _observers.Except(new[] { observer }).ToList();
                }
            }
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));

#if NET40
            Thread.MemoryBarrier();
#else
            Interlocked.MemoryBarrier();
#endif

            IList<Exception> exceptions = null;

            // Mutations are made by replacing _observers wholesale.
            // ReSharper disable once InconsistentlySynchronizedField
            foreach (var observer in _observers)
            {
                try
                {
                    observer.OnNext(logEvent);
                }
                catch (Exception ex)
                {
                    if (exceptions == null)
                        exceptions = new List<Exception>();
                    exceptions.Add(ex);
                }
            }

            if (exceptions != null)
                throw new AggregateException("At least one observer failed to accept the event", exceptions);
        }

        public void Dispose()
        {
            lock (_syncRoot)
            {
                if (_disposed) return;
                
                _disposed = true;
                foreach (var observer in _observers)
                {
                    observer.OnCompleted();
                }
            }
        }
    }
}
