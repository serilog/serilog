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
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.PeriodicBatching;
using System.Text;

namespace Serilog.Sinks.ElasticSearch
{
    /// <summary>
    /// Writes log events as documents to ElasticSearch.
    /// </summary>
    public class ElasticSearchSink : PeriodicBatchingSink
    {
        readonly string _indexFormat;
        readonly IFormatProvider _formatProvider;
        readonly IElasticsearchSerializer _serializer;
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
        /// Default connection timeout in milliseconds
        /// </summary>
        public const int DefaultConnectionTimeout = 5000;

        /// <summary>
        /// Construct a sink posting to the specified Elasticsearch cluster.
        /// </summary>
        /// <param name="connectionConfiguration">Connection configuration to use for connecting to the cluster.</param>
        /// <param name="indexFormat">The index name formatter. A string.Format using the DateTimeOffset of the event is run over this string.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="connection">Allows you to override the connection used to communicate with elasticsearch</param>
        /// <param name="serializer">When passing a serializer unknown object will be serialized to object instead of relying on their ToString representation</param>
        public ElasticSearchSink(
            ConnectionConfiguration connectionConfiguration,
            string indexFormat,
            int batchPostingLimit,
            TimeSpan period,
            IFormatProvider formatProvider,
            IConnection connection = null,
            IElasticsearchSerializer serializer = null
            )
            : base(batchPostingLimit, period)
        {
            _indexFormat = indexFormat;
            _formatProvider = formatProvider;
            _serializer = serializer;
            _client = new ElasticsearchClient(connectionConfiguration, connection: connection);
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
            if (!events.Any())
                return;

            var formatter = new ElasticsearchJsonFormatter(
                omitEnclosingObject: false,
                formatProvider: _formatProvider,
                renderMessage: true,
                closingDelimiter: string.Empty,
                serializer: _serializer
            );
            var payload = new List<string>();

            foreach (var e in events)
            {
                var indexName = string.Format(_indexFormat, e.Timestamp);
                var action = new { index = new { _index = indexName, _type = "logevent" } };
                var actionJson = _client.Serializer.Serialize(action);
                payload.Add(Encoding.UTF8.GetString(actionJson));
                var sw = new StringWriter();
                formatter.Format(e, sw);
                payload.Add(sw.ToString());
            }

            _client.Bulk(payload);
        }
    }
}
