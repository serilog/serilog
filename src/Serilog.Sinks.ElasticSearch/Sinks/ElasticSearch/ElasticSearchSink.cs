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
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System.Text;

namespace Serilog.Sinks.ElasticSearch
{
    /// <summary>
    /// Writes log events as documents to ElasticSearch.
    /// </summary>
    class ElasticSearchSink : PeriodicBatchingSink
    {
        readonly string _indexFormat;
        readonly string _type;
        readonly IFormatProvider _formatProvider;
        readonly ElasticsearchClient _client;
    
        /// <summary>
        /// A reasonable default for the number of events posted in each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 50;

        /// <summary>
        /// A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

		/// <summary>
		/// Default to the Logstash index name format
		/// </summary>
		public const string DefaultIndexFormat = "logstash-{0:yyyy.MM.dd}";

        /// <summary>
        /// Defaults to the type of logevent
        /// </summary>
        public const string DefaultType = "logevent";

		/// <summary>
		/// Default connection timeout in milliseconds
		/// </summary>
		public const int DefaultConnectionTimeout = 5000;

        /// <summary>
        /// Construct a sink posting to the specified Elasticsearch cluster.
        /// </summary>
        /// <param name="connectionConfiguration">Connection configuration to use for connecting to the cluster.</param>
        /// <param name="indexFormat">The index name formatter. A string.Format using the DateTime.UtcNow is run over this string.</param>
        /// <param name="type">The type the log event will be indexed as in elasticsearch</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public ElasticSearchSink(ConnectionConfiguration connectionConfiguration, string indexFormat, string type, int batchPostingLimit, TimeSpan period, IFormatProvider formatProvider)
            : base(batchPostingLimit, period)
        {
			_indexFormat = indexFormat;
            _formatProvider = formatProvider;
            _type = type;
            _client = new ElasticsearchClient(connectionConfiguration);
        }

        /// <summary>
        /// Emit a batch of log events, running to completion synchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <remarks>
        /// Override either <see cref="M:Serilog.Sinks.PeriodicBatching.PeriodicBatchingSink.EmitBatch(System.Collections.Generic.IEnumerable{Serilog.Events.LogEvent})" /> or <see cref="M:Serilog.Sinks.PeriodicBatching.PeriodicBatchingSink.EmitBatchAsync(System.Collections.Generic.IEnumerable{Serilog.Events.LogEvent})" />,
        /// not both.
        /// </remarks>
        protected override void EmitBatch(IEnumerable<LogEvent> events)
        {
			var logEvents = events.Select(e => new Data.LogEvent(e, e.RenderMessage(_formatProvider)));
			
			if (!logEvents.Any())
				return;

			var indexName = string.Format(_indexFormat, DateTime.UtcNow);
			var payload = new List<object>();

			foreach (var logEvent in logEvents)
			{
				var document = new Dictionary<string, object>();
				document.Add("@timestamp", logEvent.Timestamp);
				document.Add("messageTemplate", logEvent.MessageTemplate);
				document.Add("level", Enum.GetName(typeof(LogEventLevel), logEvent.Level));
				if (logEvent.Exception != null)
					document.Add("exception", logEvent.Exception);
				document.Add("message", logEvent.RenderedMessage);
				document.Add("fields", logEvent.Properties);

				payload.Add(new { index = new { _index = indexName, _type = _type } });
				payload.Add(document);
			}

			_client.Bulk(payload);
		}
    }
}
