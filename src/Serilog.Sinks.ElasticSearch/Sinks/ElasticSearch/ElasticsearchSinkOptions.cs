using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Serialization;
using Serilog.Events;

namespace Serilog.Sinks.ElasticSearch
{
    /// <summary>
    /// Provides ElasticsearchSink with configurable options
    /// </summary>
    public class ElasticsearchSinkOptions
    {
        ///<summary>
        /// Connection configuration to use for connecting to the cluster.
        /// </summary>
        public Func<ConnectionConfiguration, ConnectionConfiguration> ModifyConnectionSetttings { get; set; }

        ///<summary>
        /// The index name formatter. A string.Format using the DateTimeOffset of the event is run over this string.
        /// defaults to "logstash-{0:yyyy.MM.dd}"
        /// </summary>
        public string IndexFormat { get; set; }

        ///<summary>
        /// The default elasticsearch type name to use for the log events defaults to: logevent
        /// </summary>
        public string TypeName { get; set; }
       
        ///<summary>
        /// The maximum number of events to post in a single batch.
        /// </summary>
        public int? BatchPostingLimit { get; set; }
       
        ///<summary>
        /// The time to wait between checking for event batches.
        /// </summary>
        public TimeSpan? Period { get; set; }
       
        ///<summary>
        /// Supplies culture-specific formatting information, or null.
        /// </summary>
        public IFormatProvider FormatProvider { get; set; }
       
        ///<summary>
        /// Allows you to override the connection used to communicate with elasticsearch
        /// </summary>
        public IConnection Connection { get; set; }
        
        /// <summary>
        /// When true fields will be written at the root of the json document
        /// </summary>
        public bool InlineFields { get; set; }

        /// <summary>
        /// The minimum log event level required in order to write an event to the sink.
        /// </summary>
        public LogEventLevel? MinimumLogEventLevel { get; set; }
       
        ///<summary>
        /// When passing a serializer unknown object will be serialized to object instead of relying on their ToString representation
        /// </summary>
        public IElasticsearchSerializer Serializer { get; set; }
         
        /// <summary>
        /// The connectionpool describing the cluster to write event to
        /// </summary>
        public IConnectionPool ConnectionPool { get; private set; }

        /// <summary>
        /// Function to decide which index to write the LogEvent to
        /// </summary>
        public Func<LogEvent, DateTimeOffset, string> IndexDecider { get; set; }

        /// <summary>
        /// Configures the elasticsearch sink
        /// </summary>
        /// <param name="connectionPool">The connectionpool to use to write events to</param>
        public ElasticsearchSinkOptions(IConnectionPool connectionPool)
        {
            ConnectionPool = connectionPool;
        }
        
        /// <summary>
        /// Configures the elasticsearch sink
        /// </summary>
        /// <param name="nodes">The nodes to write to</param>
        public ElasticsearchSinkOptions(IEnumerable<Uri> nodes)
        {
            nodes = nodes != null && nodes.Any(n=>n != null) 
                ? nodes.Where(n=>n != null) 
                : new[] { new Uri("http://localhost:9200") };
            if (nodes.Count() == 1)
                ConnectionPool = new SingleNodeConnectionPool(nodes.First());
            else 
                ConnectionPool = new StaticConnectionPool(nodes);
        }

        /// <summary>
        /// Configures the elasticsearch sink
        /// </summary>
        /// <param name="node">The node to write to</param>
        public ElasticsearchSinkOptions(Uri node) : this(new [] {node}) { }
    }
}