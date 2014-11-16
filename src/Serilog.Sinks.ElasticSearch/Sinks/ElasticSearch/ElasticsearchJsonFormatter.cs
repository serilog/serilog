using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using Serilog.Formatting.Json;

namespace Serilog.Sinks.ElasticSearch
{
    /// <summary>
    /// Custom Json formatter that respects the configured property name handling and forces 'Timestamp' to @timestamp
    /// </summary>
    public class ElasticsearchJsonFormatter : JsonFormatter
    {
        readonly IElasticsearchSerializer _serializer;

        /// <summary>
        /// Construct a <see cref="ElasticsearchJsonFormatter"/>.
        /// </summary>
        /// <param name="omitEnclosingObject">If true, the properties of the event will be written to
        /// the output without enclosing braces. Otherwise, if false, each event will be written as a well-formed
        /// JSON object.</param>
        /// <param name="closingDelimiter">A string that will be written after each log event is formatted.
        /// If null, <see cref="Environment.NewLine"/> will be used. Ignored if <paramref name="omitEnclosingObject"/>
        /// is true.</param>
        /// <param name="renderMessage">If true, the message will be rendered and written to the output as a
        /// property named RenderedMessage.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="serializer">Inject a serializer to force objects to be serialized over being ToString()</param>
        public ElasticsearchJsonFormatter(bool omitEnclosingObject = false,
            string closingDelimiter = null,
            bool renderMessage = false,
            IFormatProvider formatProvider = null,
            IElasticsearchSerializer serializer = null)
            : base(omitEnclosingObject, closingDelimiter, renderMessage, formatProvider)
        {
            this._serializer = serializer;
        }


        /// <summary>
        /// Allows a subclass to write out objects that have no configured literal writer.
        /// </summary>
        /// <param name="value">The value to be written as a json construct</param>
        /// <param name="output">The writer to write on</param>
        protected override void WriteObjectValue(object value, TextWriter output)
        {
            if (this._serializer != null)
            {
                var json = this._serializer.Serialize(value, SerializationFormatting.None);
                var jsonString = Encoding.UTF8.GetString(json);
                output.Write(jsonString);
                return;
            }

            base.WriteObjectValue(value, output);
        }

        /// <summary>
        /// Allows you to override the default property names that JsonFormatter emits
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected override string FormatPropertyName(string name)
        {
            if (Stage != JsonFormattingStage.MetaData)
                return name;

            switch (name.ToLowerInvariant())
            {
                case "timestamp": return "@timestamp";
                case "level": return "level";
                case "messagetemplate": return "messageTemplate";
                case "renderedmessage": return "renderedMessage";
                case "exception": return "exception";
                case "properties": return "fields";
                case "renderings": return "renderings";
            }
            return name;
        }
    }
}
