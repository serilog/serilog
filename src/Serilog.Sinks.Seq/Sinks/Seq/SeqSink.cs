// Seq Client for .NET - Copyright 2014 Continuous IT Pty Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
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

namespace Serilog.Sinks.Seq
{
    class SeqSink : PeriodicBatchingSink
    {
        readonly string _apiKey;
        readonly HttpClient _httpClient;
        const string BulkUploadResource = "api/events/raw";
        const string ApiKeyHeaderName = "X-Seq-ApiKey";

        public const int DefaultBatchPostingLimit = 1000;
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        public SeqSink(string serverUrl, string apiKey, int batchPostingLimit, TimeSpan period)
            : base(batchPostingLimit, period)
        {
            if (serverUrl == null) throw new ArgumentNullException("serverUrl");
            _apiKey = apiKey;

            var baseUri = serverUrl;
            if (!baseUri.EndsWith("/"))
                baseUri += "/";

            _httpClient = new HttpClient { BaseAddress = new Uri(baseUri) };
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
                _httpClient.Dispose();
        }

        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            var payload = new StringWriter();
            payload.Write("{\"events\":[");

            var formatter = new JsonFormatter(closingDelimiter: "");
            var delimStart = "";
            foreach (var logEvent in events)
            {
                payload.Write(delimStart);
                formatter.Format(logEvent, payload);
                delimStart = ",";
            }

            payload.Write("]}");

            var content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            if (!string.IsNullOrWhiteSpace(_apiKey))
                content.Headers.Add(ApiKeyHeaderName, _apiKey);
    
            var result = await _httpClient.PostAsync(BulkUploadResource, content);
            if (!result.IsSuccessStatusCode)
                throw new LoggingFailedException(string.Format("Received failed result {0} when posting events to Seq", result.StatusCode));
        }
    }
}
