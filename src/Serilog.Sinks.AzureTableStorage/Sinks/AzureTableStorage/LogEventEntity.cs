using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.WindowsAzure.Storage.Table;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Serilog.Sinks.AzureTableStorage
{
    // todo: Figure out a better name than LogEventEntity given the table name is the same and it is weird...
    /// <summary>
    /// Represents a single log event for the Serilog Azure Table Storage Sink.
    /// </summary>
    /// <remarks>
    /// The PartitionKey is set to "0" followed by the ticks of the log event time (in UTC) as per what Azure Diagnostics logging has.
    /// The RowKey is set to "{Level}|{MessageTemplate}" to allow you to search for certain categories of log messages or indeed for a
    ///     specific log message quickly using the indexing in Azure Table Storage.
    /// </remarks>
    public class LogEventEntity : TableEntity
    {
        private static readonly Regex RowKeyNotAllowedMatch = new Regex(@"(\\|/|#|\?)");

        /// <summary>
        /// Default constructor for the Storage Client library to re-hydrate entities when querying.
        /// </summary>
        public LogEventEntity() { }

        /// <summary>
        /// Create a log event entity from a Serilog <see cref="LogEvent"/>.
        /// </summary>
        /// <param name="log">The event to log</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public LogEventEntity(LogEvent log, IFormatProvider formatProvider)
        {
            Timestamp = log.Timestamp.ToUniversalTime().DateTime;
            PartitionKey = string.Format("0{0}", Timestamp.Ticks);
            RowKey = GetValidRowKey(string.Format("{0}|{1}", log.Level, log.MessageTemplate.Text));
            MessageTemplate = log.MessageTemplate.Text;
            Level = log.Level.ToString();
            Exception = log.Exception.ToString();
            RenderedMessage = log.RenderMessage(formatProvider);
            var s = new StringWriter();
            new JsonFormatter().Format(log, s);
            Data = s.ToString();
        }

        // http://msdn.microsoft.com/en-us/library/windowsazure/dd179338.aspx
        private static string GetValidRowKey(string rowKey)
        {
            rowKey = RowKeyNotAllowedMatch.Replace(rowKey, "");
            return rowKey.Length > 1024 ? rowKey.Substring(0, 1024) : rowKey;
        }

        /// <summary>
        /// The template that was used for the log message.
        /// </summary>
        public string MessageTemplate { get; set; }
        /// <summary>
        /// The level of the log.
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// A string representation of the exception that was attached to the log (if any).
        /// </summary>
        public string Exception { get; set; }
        /// <summary>
        /// The rendered log message.
        /// </summary>
        public string RenderedMessage { get; set; }
        /// <summary>
        /// A JSON-serialised representation of the data attached to the log message.
        /// </summary>
        public string Data { get; set; }
    }
}