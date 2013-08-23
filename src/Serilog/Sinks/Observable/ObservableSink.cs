// Copyright 2013 Serilog Contributors
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
        readonly object _syncRoot = new object();
        IList<IObserver<LogEvent>> _observers = new List<IObserver<LogEvent>>();
        bool _disposed;

        sealed class Unsubscriber : IDisposable
        {
            readonly ObservableSink _sink;
            readonly IObserver<LogEvent> _observer;

            public Unsubscriber(ObservableSink sink, IObserver<LogEvent> observer)
            {
                if (sink == null) throw new ArgumentNullException("sink");
                if (observer == null) throw new ArgumentNullException("observer");
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
            if (observer == null) throw new ArgumentNullException("observer");

            lock (_syncRoot)
            {
                // Makes the assumption that list iteration is not
                // mutating - correct but not guaranteed by the BCL.
                var newObservers = _observers.ToList();
                newObservers.Add(observer);
                Interlocked.Exchange(ref _observers, newObservers);
            }

            return new Unsubscriber(this, observer);
        }

        void Unsubscribe(IObserver<LogEvent> observer)
        {
            if (observer == null) throw new ArgumentNullException("observer");

            lock (_syncRoot)
                _observers.Remove(observer);
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            Interlocked.MemoryBarrier();

            IList<Exception> exceptions = null;

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
