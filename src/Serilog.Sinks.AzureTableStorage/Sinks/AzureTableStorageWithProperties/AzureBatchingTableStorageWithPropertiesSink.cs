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

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.Sinks.AzureTableStorage
{
	/// <summary>
	/// Writes log events as records to an Azure Table Storage table.
	/// </summary>
	public class AzureBatchingTableStorageWithPropertiesSink : PeriodicBatchingSink
	{
		private readonly IFormatProvider _formatProvider;
		private readonly CloudTable _table;
		private readonly string _additionalRowKeyPostfix;
		private const int _maxAzureOperationsPerBatch = 100;

		/// <summary>
		/// Construct a sink that saves logs to the specified storage account.
		/// </summary>
		/// <param name="storageAccount">The Cloud Storage Account to use to insert the log entries to.</param>
		/// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
		/// <param name="batchSizeLimit"></param>
		/// <param name="period"></param>
		/// <param name="storageTableName">Table name that log entries will be written to. Note: Optional, setting this may impact performance</param>
		/// <param name="additionalRowKeyPostfix">Additional postfix string that will be appended to row keys</param>
		public AzureBatchingTableStorageWithPropertiesSink(CloudStorageAccount storageAccount, IFormatProvider formatProvider, int batchSizeLimit, TimeSpan period, string storageTableName = null, string additionalRowKeyPostfix = null)
			: base(batchSizeLimit, period)
		{
			var tableClient = storageAccount.CreateCloudTableClient();

			if (string.IsNullOrEmpty(storageTableName))
			{
				storageTableName = "LogEventEntity";
			}

			_table = tableClient.GetTableReference(storageTableName);
			_table.CreateIfNotExists();

			_formatProvider = formatProvider;

			if (additionalRowKeyPostfix != null)
			{
				_additionalRowKeyPostfix = AzureTableStorageEntityFactory.GetValidStringForTableKey(additionalRowKeyPostfix);
			}
		}

		/// <summary>
		/// Emit a batch of log events, running to completion synchronously.
		/// </summary>
		/// <param name="events">The events to emit.</param>
		/// <remarks>Override either <see cref="PeriodicBatchingSink.EmitBatch"/> or <see cref="PeriodicBatchingSink.EmitBatchAsync"/>,
		/// not both.</remarks>
		protected override void EmitBatch(IEnumerable<LogEvent> events)
		{
			string lastPartitionKey = null;
			TableBatchOperation operation = null;
			int insertsPerOperation = 0;

			foreach (var logEvent in events)
			{
				var tableEntity = AzureTableStorageEntityFactory.CreateEntityWithProperties(logEvent, _formatProvider, _additionalRowKeyPostfix);

				// If partition changed, store the new and force an execution
				if (lastPartitionKey != tableEntity.PartitionKey)
				{
					lastPartitionKey = tableEntity.PartitionKey;

					// Force a new execution
					insertsPerOperation = _maxAzureOperationsPerBatch;
				}

				// If reached max operations per batch, we need a new batch operation
				if (insertsPerOperation == _maxAzureOperationsPerBatch)
				{
					// If there is an operation currently in use, execute it
					if (operation != null)
					{
						_table.ExecuteBatch(operation);
					}

					// Create a new batch operation and zero count
					operation = new TableBatchOperation();
					insertsPerOperation = 0;
				}

				// Add current entry to the batch
				operation.Add(TableOperation.Insert(tableEntity));

				insertsPerOperation++;
			}

			// Execute last batch
			_table.ExecuteBatch(operation);
		}
	}
}
