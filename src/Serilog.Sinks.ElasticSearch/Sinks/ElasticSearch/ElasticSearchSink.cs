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
        readonly IFormatProvider _formatProvider;
        readonly ElasticsearchClient _client;
    
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
        /// <param name="server">The server where ElasticSearch is running.</param>
        /// <param name="indexFormat">The index name formatter. A string.Format using the DateTime.UtcNow is run over this string.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="connectionTimeOutInMilliseconds">The connection time out in milliseconds.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public ElasticSearchSink(Uri server, string indexFormat, int batchPostingLimit, int connectionTimeOutInMilliseconds, TimeSpan period, IFormatProvider formatProvider)
            : base(batchPostingLimit, period)
        {
            if (connectionTimeOutInMilliseconds <= 0)
                connectionTimeOutInMilliseconds = 5000;

            _indexFormat = indexFormat ?? "logstash-{0:yyyy.MM.dd}";
            _formatProvider = formatProvider;
            _client = new ElasticsearchClient(new ConnectionConfiguration(server)
                          .SetMaximumAsyncConnections(20)
                          .SetTimeout(connectionTimeOutInMilliseconds));
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
            var indexName = string.Format(_indexFormat, DateTime.UtcNow);
            var items = events
                .Select(logEvent => new Data.LogEvent(logEvent, logEvent.RenderMessage(_formatProvider)))
                .ToList();

			if (items.Any() && _client != null)
			{
				var bulk = new List<string>();
				var builder = new StringBuilder();

				foreach (var item in items)
				{
					builder.Append("{\"index\":{\"_index\":\"").Append(indexName).Append("\",\"_type\":\"logevent\"}}");
					bulk.Add(builder.ToString());
					builder.Clear();
					builder.Append("{\"@timestamp\":").Append("\"").Append(item.Timestamp).Append("\",");
					builder.Append("\"messageTemplate\":").Append("\"").Append(item.MessageTemplate).Append("\",");
					builder.Append("\"level\":").Append("\"").Append(item.Level).Append("\",");
					if (item.Exception != null)
						builder.Append("\"exception\":").Append("\"").Append(item.Exception).Append("\",");
					builder.Append("\"message\":").Append("\"").Append(item.RenderedMessage.Replace("\"", "\\\"")).Append("\",");
					builder.Append("\"fields\":{");
					for (int i = 0; i < item.Properties.Count; i++)
					{
						var property = item.Properties.ElementAt(i);
						builder.Append("\"").Append(property.Key).Append("\":\"").Append(property.Value).Append("\"");
						if (i + 1 != item.Properties.Count)
							builder.Append(",");
					}
					builder.Append("}}");
					bulk.Add(builder.ToString());
					builder.Clear();
				}

				_client.Bulk(bulk);
			} 
        }     
    }
}
