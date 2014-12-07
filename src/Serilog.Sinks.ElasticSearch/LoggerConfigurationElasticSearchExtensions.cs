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
using System.ComponentModel;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.ElasticSearch;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.ElasticSearch() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationElasticSearchExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events as documents to an ElasticSearch index.
        /// This works great with the Kibana web interface when using the default settings.
        /// Make sure to add a template to ElasticSearch like the one found here:
        /// https://gist.github.com/mivano/9688328
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="indexFormat">The index format where the events are send to. It defaults to the logstash index per day format. It uses a String.Format using the DateTime.UtcNow parameter.</param>
        /// <param name="node">The URI to the node where ElasticSearch is running. When null, will fall back to http://localhost:9200</param>
        /// <param name="connectionTimeOutInMilliseconds">The connection time out in milliseconds. Default value is 5000.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="serializer">When passing a serializer unknown object will be serialized to object instead of relying on their ToString representation</param>
        /// <returns>
        /// Logger configuration, allowing configuration to continue.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">loggerConfiguration</exception>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Please use Elasticsearch(ElasticsearchSinkOptions options), this method might not expose all options and should be removed in the next Serilog major release")]
        public static LoggerConfiguration ElasticSearch(
            this LoggerSinkConfiguration loggerConfiguration,
            string indexFormat = ElasticsearchSink.DefaultIndexFormat,
            Uri node = null,
            int connectionTimeOutInMilliseconds = ElasticsearchSink.DefaultConnectionTimeout,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = ElasticsearchSink.DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null,
            IElasticsearchSerializer serializer = null
            )
        {
            if (node == null)
                node = new Uri("http://localhost:9200");

            return Elasticsearch(loggerConfiguration, new ElasticsearchSinkOptions(new [] { node })
            {
                Serializer = serializer,
                FormatProvider = formatProvider,
                IndexFormat = indexFormat,
                ModifyConnectionSetttings = s => s.SetTimeout(connectionTimeOutInMilliseconds),
                BatchPostingLimit = batchPostingLimit,
                Period = period,
                MinimumLogEventLevel = restrictedToMinimumLevel
            });
        }

        /// <summary>
        /// Adds a sink that writes log events as documents to an ElasticSearch index.
        /// This works great with the Kibana web interface when using the default settings.
        /// Make sure to add a template to ElasticSearch like the one found here:
        /// https://gist.github.com/mivano/9688328
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="nodes">The node URIs of the Elasticsearch cluster.</param>
        /// <param name="indexFormat">The index format where the events are send to. It defaults to the logstash index per day format. It uses a String.Format using the DateTime.UtcNow parameter.</param>
        /// <param name="connectionTimeOutInMilliseconds">The connection time out in milliseconds. Default value is 5000.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="serializer">When passing a serializer unknown object will be serialized to object instead of relying on their ToString representation</param>
        /// <returns>
        /// Logger configuration, allowing configuration to continue.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">loggerConfiguration</exception>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Please use Elasticsearch(ElasticsearchSinkOptions options), this method might not expose all options and should be removed in the next Serilog major release")]
        public static LoggerConfiguration ElasticSearch(
            this LoggerSinkConfiguration loggerConfiguration,
            IEnumerable<Uri> nodes,
            string indexFormat = ElasticsearchSink.DefaultIndexFormat,
            int connectionTimeOutInMilliseconds = ElasticsearchSink.DefaultConnectionTimeout,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = ElasticsearchSink.DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null,
            IElasticsearchSerializer serializer = null
            )
        {
            return Elasticsearch(loggerConfiguration, new ElasticsearchSinkOptions(nodes)
            {
                Serializer = serializer,
                FormatProvider = formatProvider,
                IndexFormat = indexFormat,
                ModifyConnectionSetttings = s => s.SetTimeout(connectionTimeOutInMilliseconds),
                BatchPostingLimit = batchPostingLimit,
                Period = period,
                MinimumLogEventLevel = restrictedToMinimumLevel
            });
        }

        /// <summary>
        /// Adds a sink that writes log events as documents to an ElasticSearch index.
        /// This works great with the Kibana web interface when using the default settings.
        /// Make sure to add a template to ElasticSearch like the one found here:
        /// https://gist.github.com/mivano/9688328
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="connectionConfiguration">The configuration to use for connecting to the Elasticsearch cluster.</param>
        /// <param name="indexFormat">The index format where the events are send to. It defaults to the logstash index per day format. It uses a String.Format using the DateTime.UtcNow parameter.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="serializer">When passing a serializer unknown object will be serialized to object instead of relying on their ToString representation</param>
        /// <returns>
        /// Logger configuration, allowing configuration to continue.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">loggerConfiguration</exception>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Please use Elasticsearch(ElasticsearchSinkOptions options), this method might not expose all options and should be removed in the next Serilog major release")]
        public static LoggerConfiguration ElasticSearch(
            this LoggerSinkConfiguration loggerConfiguration,
            ConnectionConfiguration connectionConfiguration,
            string indexFormat = ElasticsearchSink.DefaultIndexFormat,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = ElasticsearchSink.DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null,
            IElasticsearchSerializer serializer = null
            )
        {
            if (connectionConfiguration == null)
                throw new ArgumentNullException("connectionConfiguration");
            IConnectionConfigurationValues values = connectionConfiguration;
            return Elasticsearch(loggerConfiguration, new ElasticsearchSinkOptions(values.ConnectionPool)
            {
                Serializer = serializer,
                FormatProvider = formatProvider,
                IndexFormat = indexFormat,
                ModifyConnectionSetttings = s => connectionConfiguration,
                BatchPostingLimit = batchPostingLimit,
                Period = period,
                MinimumLogEventLevel = restrictedToMinimumLevel
            });
        }

        /// <summary>
        /// Adds a sink that writes log events as documents to an ElasticSearch index.
        /// This works great with the Kibana web interface when using the default settings.
        /// Make sure to add a template to ElasticSearch like the one found here:
        /// https://gist.github.com/mivano/9688328
        /// </summary>
        /// <param name="loggerSinkConfiguration"></param>
        /// <param name="options">Provides options specific to the Elasticsearch sink</param>
        /// <returns></returns>
        public static LoggerConfiguration Elasticsearch(this LoggerSinkConfiguration loggerSinkConfiguration, ElasticsearchSinkOptions options)
        {
            options = options ?? new ElasticsearchSinkOptions(new [] { new Uri("http://locahost:9200") });
            var sink = new ElasticsearchSink(options);
            return loggerSinkConfiguration.Sink(sink, options.MinimumLogEventLevel ?? LevelAlias.Minimum);
        }
    }
}
