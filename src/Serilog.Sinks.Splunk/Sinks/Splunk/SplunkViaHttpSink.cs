// Copyright 2014 Serilog Contributors
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
using SplunkClient = Splunk.Client;


namespace Serilog.Sinks.Splunk
{
    /// <summary>
    /// A log event sink that writes to SplunkViaHttp.
    /// </summary>
    public class SplunkViaHttpSink : ILogEventSink, IDisposable
    {
        readonly SplunkClient.Context _context;
        readonly string _userName;
        readonly string _password;
        readonly int _batchSizeLimit;
        readonly TimeSpan _batchInterval;
        readonly SplunkClient.TransmitterArgs _transmitterArgs;
        readonly IFormatProvider _formatProvider;
        readonly SplunkClient.Service _service;
        string _index;
        ConcurrentQueue<LogEvent> _queue;

        /// <summary>
        /// Creates a new instance of the splunk sink
        /// </summary>
        /// <param name="context">The Splunk Context</param>
        /// <param name="batchSizeLimit">The size of the batch prior to logging</param>
        /// <param name="batchInterval">The interval on which to log via http</param>
        /// <param name="formatProvider">The format provider to use when rendering the message</param>
        public SplunkViaHttpSink(
            SplunkContext context,
            int batchSizeLimit,
            TimeSpan batchInterval,
            IFormatProvider formatProvider = null)
            : this(context, context.Index, context.Username, context.Password, batchSizeLimit, batchInterval, context.ResourceNamespace, context.TransmitterArgs, formatProvider)
        {
        }

        /// <summary>
        /// Create an instance of the SplunkViaHttp sink.
        /// </summary>
        /// <param name="context">Connection info.</param>
        /// <param name="index">The name of the splunk index to log to</param>
        /// <param name="userName">The username to authenticate with</param>
        /// <param name="password">The password to authenticate with</param>
        /// <param name="batchSizeLimit">The size of the batch prior to logging</param>
        /// <param name="batchInterval">The interval on which to log via http</param>
        /// <param name="resourceNamespace">The resource namespaces</param>
        /// <param name="transmitterArgs">The </param>
        /// <param name="formatProvider">The format provider to be used when rendering the message</param>
        public SplunkViaHttpSink(
            SplunkClient.Context context,
            string index,
            string userName,
            string password,
            int batchSizeLimit,
            TimeSpan batchInterval,
            SplunkClient.Namespace resourceNamespace = null,
            SplunkClient.TransmitterArgs transmitterArgs = null,
            IFormatProvider formatProvider = null
           
            )
        {
            _context = context;
            _index = index;
            _userName = userName;
            _password = password;
            _batchSizeLimit = batchSizeLimit;
            _batchInterval = batchInterval;
            _transmitterArgs = transmitterArgs;
            _formatProvider = formatProvider;

            _queue = new ConcurrentQueue<LogEvent>();

            _service = resourceNamespace == null
                ? new SplunkClient.Service(_context, new SplunkClient.Namespace("nobody", "search"))
                : new SplunkClient.Service(_context, resourceNamespace);

            RepeatAction.OnInterval(_batchInterval, () => ProcessQueue(), new CancellationToken());
        }

        private async Task ProcessQueue()
        {
            try
            {
                do
                {
                    var count = 0;
                    var events = new Queue<LogEvent>();
                    LogEvent next;

                    while (count < _batchSizeLimit && _queue.TryDequeue(out next))
                    {
                        count++;
                        events.Enqueue(next);
                    }

                    if (events.Count == 0)
                        return;

                    //Login
                    await _service.LogOnAsync(_userName, _password);

                    //Ensure that the index has been created
                    var index = await _service.Indexes.GetOrNullAsync(_index)
                                ?? await _service.Indexes.CreateAsync(_index);

                    var transmitter = _service.Transmitter;

                    foreach (var logEvent in events)
                    {

                        var message = logEvent.SimplifyAndFormat();

                        if (_transmitterArgs == null)
                        {
                            await transmitter.SendAsync(message, index.Name);
                        }
                        else
                        {
                            await transmitter.SendAsync(message, index.Name, _transmitterArgs);
                        }
                    }
                }
                while (true);
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Exception while emitting batch from {0}: {1}", this, ex);
            }
        }

        /// <inheritdoc/>
        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            _queue.Enqueue(logEvent);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _service.Dispose();
        }
    }

    public static class RepeatAction
    {
        public static Task OnInterval(TimeSpan pollInterval, Action action, CancellationToken token,
            TaskCreationOptions taskCreationOptions, TaskScheduler taskScheduler)
        {
            return Task.Factory.StartNew(() =>
            {
                for (; ; )
                {
                    if (token.WaitCancellationRequested(pollInterval))
                        break;
                    action();
                }
            }, token, taskCreationOptions, taskScheduler);
        }

        public static Task OnInterval(TimeSpan pollInterval, Action action, CancellationToken token)
        {
            return OnInterval(pollInterval, action, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public static bool WaitCancellationRequested(this CancellationToken token, TimeSpan timeout)
        {
            return token.WaitHandle.WaitOne(timeout);
        }
    }
}