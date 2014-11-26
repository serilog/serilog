using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Parsing;

namespace Serilog.Sinks.ElasticSearch
{
    /// <summary>
    /// Custom Json formatter that respects the configured property name handling and forces 'Timestamp' to @timestamp
    /// </summary>
    public class ElasticsearchJsonFormatter : JsonFormatter
    {
        readonly IElasticsearchSerializer _serializer;
        readonly bool _inlineFields;

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
        /// <param name="inlineFields">When set to true values will be written at the root of the json document</param>
        public ElasticsearchJsonFormatter(bool omitEnclosingObject = false,
            string closingDelimiter = null,
            bool renderMessage = false,
            IFormatProvider formatProvider = null,
            IElasticsearchSerializer serializer = null,
            bool inlineFields = false)
            : base(omitEnclosingObject, closingDelimiter, renderMessage, formatProvider)
        {
            _serializer = serializer;
            _inlineFields = inlineFields;
        }

        /// <summary>
        /// Writes out individual renderings of attached properties
        /// </summary>
        protected override void WriteRenderings(IGrouping<string, PropertyToken>[] tokensWithFormat, IDictionary<string, LogEventPropertyValue> properties, TextWriter output)
        {
            output.Write(",\"{0}\":{{", "renderings");
            WriteRenderingsValues(tokensWithFormat, properties, output);
            output.Write("}");
        }
        
        /// <summary>
        /// Writes out the attached properties
        /// </summary>
        protected override void WriteProperties(IDictionary<string, LogEventPropertyValue> properties, TextWriter output)
        {
            if (!_inlineFields)
                output.Write(",\"{0}\":{{", "fields");
            else 
                output.Write(",");
            
            WritePropertiesValues(properties, output);

            if (!_inlineFields)
                output.Write("}");
        }

        /// <summary>
        /// Writes out the attached exception
        /// </summary>
        protected override void WriteException(Exception exception, ref string delim, TextWriter output)
        {
            WriteJsonProperty("exception", exception, ref delim, output);
        }
           
        /// <summary>
        /// (Optionally) writes out the rendered message
        /// </summary>
        protected override void WriteRenderedMessage(string message, ref string delim, TextWriter output)
        {
            WriteJsonProperty("renderedMessage", message, ref delim, output);
        }
        
        /// <summary>
        /// Writes out the message template for the logevent.
        /// </summary>
        protected override void WriteMessageTemplate(string template, ref string delim, TextWriter output)
        {
            WriteJsonProperty("messageTemplate", template, ref delim, output);
        }
        
        /// <summary>
        /// Writes out the log level
        /// </summary>
        protected override void WriteLevel(LogEventLevel level, ref string delim, TextWriter output)
        {
            var stringLevel = Enum.GetName(typeof(LogEventLevel), level);
            WriteJsonProperty("level", stringLevel, ref delim, output);
        }
        
        /// <summary>
        /// Writes out the log timestamp
        /// </summary>
        protected override void WriteTimestamp(DateTimeOffset timestamp, ref string delim, TextWriter output)
        {
            WriteJsonProperty("@timestamp", timestamp, ref delim, output);
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

    }
}
