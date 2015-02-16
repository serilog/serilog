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
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System.Text;

namespace Serilog.Sinks.ElasticSearch
{
    /// <summary>
    /// Writes log events as documents to ElasticSearch.
    /// </summary>
    public class ElasticsearchSink : PeriodicBatchingSink
    {
        readonly ElasticsearchJsonFormatter _formatter;
        readonly string _typeName;
        readonly ElasticsearchClient _client;
        readonly Func<LogEvent, DateTimeOffset, string> _indexDecider;

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
        public const string DefaultTypeName = "logevent";

        /// <summary>
        /// Default connection timeout in milliseconds
        /// </summary>
        public const int DefaultConnectionTimeout = 5000;

        /// <summary>
        /// Creates a new ElasticsearchSink instance with the provided options
        /// </summary>
        /// <param name="options">Options configuring how the sink behaves</param>
        public ElasticsearchSink(ElasticsearchSinkOptions options)
            : base(options.BatchPostingLimit ?? DefaultBatchPostingLimit, options.Period ?? DefaultPeriod)
        {
            _indexDecider = options.IndexDecider ?? DefaultIndexDecider(options.IndexFormat);
            _typeName = !string.IsNullOrWhiteSpace(options.TypeName) ? options.TypeName : DefaultTypeName;
            var configuration = new ConnectionConfiguration(options.ConnectionPool)
                .SetTimeout(DefaultConnectionTimeout)
                .SetMaximumAsyncConnections(20);
            if (options.ModifyConnectionSetttings != null)
                configuration = options.ModifyConnectionSetttings(configuration);
            _client = new ElasticsearchClient(configuration, connection: options.Connection, serializer: options.Serializer);
            _formatter = new ElasticsearchJsonFormatter(
                formatProvider: options.FormatProvider,
                renderMessage: true,
                closingDelimiter: string.Empty,
                serializer: options.Serializer,
                inlineFields: options.InlineFields
            );
        }

        Func<LogEvent, DateTimeOffset, string> DefaultIndexDecider(string indexFormat)
        {
            var closedIndexFormat = !string.IsNullOrWhiteSpace(indexFormat) ? indexFormat : DefaultIndexFormat;
            return (@event, offset) => string.Format(closedIndexFormat, offset);
        }

        /// <summary>
        /// Emit a batch of log events, running to completion synchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <remarks>
        /// Override either <see cref="M:Serilog.Sinks.PeriodicBatching.PeriodicBatchingSink.EmitBatch(System.Collections.Generic.IEnumerable{Serilog.Events.LogEvent})" />
        ///  or <see cref="M:Serilog.Sinks.PeriodicBatching.PeriodicBatchingSink.EmitBatchAsync(System.Collections.Generic.IEnumerable{Serilog.Events.LogEvent})" />,
        /// not both.
        /// </remarks>
        protected override void EmitBatch(IEnumerable<LogEvent> events)
        {
            // ReSharper disable PossibleMultipleEnumeration
            if (!events.Any())
                return;

            var payload = new List<string>();

            foreach (var e in events)
            {
                var indexName = _indexDecider(e, e.Timestamp.ToUniversalTime());
                var action = new { index = new { _index = indexName, _type = _typeName } };
                var actionJson = _client.Serializer.Serialize(action, SerializationFormatting.None);
                payload.Add(Encoding.UTF8.GetString(actionJson));
                var sw = new StringWriter();
                _formatter.Format(e, sw);
                payload.Add(sw.ToString());
            }

            _client.Bulk(payload);
        }
    }
}
