using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.AzureTableStorage
{
    /// <summary>
    /// Writes log events as records to an Azure Table Storage table.
    /// </summary>
    public class AzureTableStorageSink : ILogEventSink
    {
        readonly IFormatProvider _formatProvider;
        private readonly CloudTable _table;

        /// <summary>
        /// Construct a sink that saves logs to the specified storage account.
        /// </summary>
        /// <param name="storageAccount">The Cloud Storage Account to use to insert the log entries to.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public AzureTableStorageSink(CloudStorageAccount storageAccount, IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
            var tableClient = storageAccount.CreateCloudTableClient();
            // todo: Should we make the table name configurable? On one hand it's recommended to
            //  use the same name as the entity for performance reasons, but on the other it means
            //  you can only log to one table in a storage account...
            _table = tableClient.GetTableReference(typeof(LogEventEntity).Name);
            _table.CreateIfNotExists();
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            // todo: Use batch insert operation via timer like the Mongo and Couch sinks?
            _table.Execute(TableOperation.Insert(new LogEventEntity(logEvent, _formatProvider)));
        }
    }
}
