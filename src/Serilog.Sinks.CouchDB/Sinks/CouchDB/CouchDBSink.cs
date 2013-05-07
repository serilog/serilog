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
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Serilog.Sinks.CouchDB
{
    /// <summary>
    /// Writes log events as documents to a CouchDB database.
    /// </summary>
    public class CouchDBSink : ILogEventSink, IDisposable
    {
        readonly int _batchPostingLimit;
        readonly ConcurrentQueue<LogEvent> _queue;
        readonly Timer _timer;
        readonly HttpClient _httpClient;
        readonly TimeSpan TickInterval = TimeSpan.FromSeconds(2);
        const string BulkUploadResource = "_bulk_docs";

        volatile bool _unloading;

        /// <summary>
        /// A reasonable default for the number of events posted in
        /// each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 50;

        /// <summary>
        /// Construct a sink posting to the specified database.
        /// </summary>
        /// <param name="databaseUrl">The URL of a CouchDB database.</param>
        /// <param name="batchPostingLimit">The maximium number of events to post in a single batch.</param>
        public CouchDBSink(string databaseUrl, int batchPostingLimit)
        {
            if (databaseUrl == null) throw new ArgumentNullException("databaseUrl");
            _batchPostingLimit = batchPostingLimit;
            _queue = new ConcurrentQueue<LogEvent>();
            _httpClient = new HttpClient { BaseAddress = new Uri(databaseUrl) };
            _timer = new Timer(async s => await OnTick());

            AppDomain.CurrentDomain.DomainUnload += OnAppDomainUnloading;
            AppDomain.CurrentDomain.ProcessExit += OnAppDomainUnloading;
            SetTimer();
        }

        void OnAppDomainUnloading(object sender, EventArgs args)
        {
            _unloading = true;
            OnTick().Wait();
        }

        void SetTimer()
        {
            _timer.Change(TickInterval, TimeSpan.FromMilliseconds(-1));
        }

        async Task OnTick()
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

            var payload = new StringWriter();
            payload.Write("{\"docs\":[");

            var formatter = new SimpleJsonFormatter(true);
            var delimStart = "{";
            foreach (var logEvent in events)
            {
                payload.Write(delimStart);
                formatter.Format(logEvent, payload);
                var renderedMessage = logEvent.RenderedMessage;
                payload.Write(",\"UtcTimeStamp\":\"{0:u}\",\"RenderedMessage\":\"{1}\"}}",
                    logEvent.TimeStamp.ToUniversalTime().DateTime,
                    renderedMessage.Replace("\"", "\\\""));
                delimStart = ",{";
            }

            payload.Write("]}");

            try
            {
                var content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
                var result = await _httpClient.PostAsync(BulkUploadResource, content);
                if (!result.IsSuccessStatusCode)
                    SelfLog.WriteLine("Received failed result {0}: {1}", result.StatusCode, result.Content.ReadAsStringAsync().Result);
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
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
