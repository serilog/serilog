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
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.CouchDB
{
    /// <summary>
    /// Writes log events as documents to a CouchDB database.
    /// </summary>
    public class CouchDBSink : PeriodicBatchingSink
    {
        readonly HttpClient _httpClient;
        const string BulkUploadResource = "_bulk_docs";

        /// <summary>
        /// A reasonable default for the number of events posted in
        /// each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 50;

        /// <summary>
        /// A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Construct a sink posting to the specified database.
        /// </summary>
        /// <param name="databaseUrl">The URL of a CouchDB database.</param>
        /// <param name="batchPostingLimit">The maximium number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        public CouchDBSink(string databaseUrl, int batchPostingLimit, TimeSpan period)
            : base(batchPostingLimit, period)
        {
            if (databaseUrl == null) throw new ArgumentNullException("databaseUrl");
            _httpClient = new HttpClient { BaseAddress = new Uri(databaseUrl) };
        }

        /// <summary>
        /// Free resources held by the sink.
        /// </summary>
        /// <param name="disposing">If true, called because the object is being disposed; if false,
        /// the object is being disposed from the finalizer.</param>
        protected override void Dispose(bool disposing)
        {
            // First flush the buffer
            base.Dispose(disposing);

            if (disposing)
                _httpClient.Dispose();
        }

        /// <summary>
        /// Emit a batch of log events, running asynchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <remarks>Override either <see cref="PeriodicBatchingSink.EmitBatch"/> or <see cref="PeriodicBatchingSink.EmitBatchAsync"/>,
        /// not both.</remarks>
        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            var payload = new StringWriter();
            payload.Write("{\"docs\":[");

            var formatter = new SimpleJsonFormatter(true);
            var delimStart = "{";
            foreach (var logEvent in events)
            {
                payload.Write(delimStart);
                formatter.Format(logEvent, payload);
                var renderedMessage = logEvent.RenderedMessage;
                payload.Write(",\"UtcTimestamp\":\"{0:u}\",\"RenderedMessage\":\"{1}\"}}",
                    logEvent.Timestamp.ToUniversalTime().DateTime,
                    SimpleJsonFormatter.Escape(renderedMessage));
                delimStart = ",{";
            }

            payload.Write("]}");

            var content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(BulkUploadResource, content);
            if (!result.IsSuccessStatusCode)
                SelfLog.WriteLine("Received failed result {0}: {1}", result.StatusCode, result.Content.ReadAsStringAsync().Result);
        }
    }
}
