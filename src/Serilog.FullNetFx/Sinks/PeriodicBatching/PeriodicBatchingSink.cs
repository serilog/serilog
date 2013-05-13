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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Sinks.PeriodicBatching
{
    /// <summary>
    /// Base class for sinks that log events in batches. Batching is
    /// triggered asynchronously on a timer.
    /// </summary>
    /// <remarks>
    /// To avoid unbounded memory growth, events are discarded after attempting
    /// to send a batch, regardless of whether the batch succeeded or not. Implementers
    /// that want to change this behavior need to either implement from scratch, or
    /// embed retry logic in the batch emitting functions.
    /// </remarks>
    public abstract class PeriodicBatchingSink : ILogEventSink, IDisposable
    {
        readonly int _batchPostingLimit;
        readonly ConcurrentQueue<LogEvent> _queue;
        readonly Timer _timer;
        readonly TimeSpan TickInterval = TimeSpan.FromSeconds(2);

        volatile bool _unloading;
        
        /// <summary>
        /// Construct a sink posting to the specified database.
        /// </summary>
        /// <param name="batchPostingLimit">The maximium number of events to include in a single batch.</param>
        protected PeriodicBatchingSink(int batchPostingLimit)
        {
            _batchPostingLimit = batchPostingLimit;
            _queue = new ConcurrentQueue<LogEvent>();
            _timer = new Timer(async s => await OnTick());

            AppDomain.CurrentDomain.DomainUnload += OnAppDomainUnloading;
            AppDomain.CurrentDomain.ProcessExit += OnAppDomainUnloading;
            SetTimer();
        }

        /// <summary>
        /// Emit a batch of log events, running to completion synchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <remarks>Override either <see cref="EmitBatch"/> or <see cref="EmitBatchAsync"/>,
        /// not both.</remarks>
        protected virtual void EmitBatch(IEnumerable<LogEvent> events)
        {
        }

        /// <summary>
        /// Emit a batch of log events, running asynchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <remarks>Override either <see cref="EmitBatch"/> or <see cref="EmitBatchAsync"/>,
        /// not both.</remarks>
#pragma warning disable 1998
        protected virtual async Task EmitBatchAsync(IEnumerable<LogEvent> events)
#pragma warning restore 1998
        {
            EmitBatch(events);
        }

        void OnAppDomainUnloading(object sender, EventArgs args)
        {
            _unloading = true;
            OnTick().Wait();
        }

        void SetTimer()
        {
            _timer.Change(TickInterval, Timeout.InfiniteTimeSpan);
        }

        async Task OnTick()
        {
            try
            {
                var count = 0;
                var events = new Queue<LogEvent>();
                LogEvent next;
                while (count < _batchPostingLimit && _queue.TryDequeue(out next))
                {
                    count++;
                    events.Enqueue(next);
                }

                if (events.Count == 0)
                    return;

                await EmitBatchAsync(events);
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Exception while emitting periodic batch: {0}", ex);
            }
            finally
            {
                if (!_unloading)
                    SetTimer();
            }
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">Log event to emit.</param>
        /// <exception cref="ArgumentNullException">The event is null.</exception>
        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (!_unloading)
                _queue.Enqueue(logEvent);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            AppDomain.CurrentDomain.DomainUnload -= OnAppDomainUnloading;
            AppDomain.CurrentDomain.ProcessExit -= OnAppDomainUnloading;
        }
    }
}
