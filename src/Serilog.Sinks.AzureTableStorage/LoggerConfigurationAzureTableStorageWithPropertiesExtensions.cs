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
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.AzureTableStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog
{
	/// <summary>
	/// Adds the WriteTo.AzureTableStorageWithProperties() extension method to <see cref="LoggerConfiguration"/>.
	/// </summary>
	public static class LoggerConfigurationAzureTableStorageWithPropertiesExtensions
	{
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
		/// Adds a sink that writes log events as records in the 'LogEventEntity' Azure Table Storage table in the given storage account.
		/// </summary>
		/// <param name="loggerConfiguration">The logger configuration.</param>
		/// <param name="storageAccount">The Cloud Storage Account to use to insert the log entries to.</param>
		/// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
		/// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
		/// <param name="storageTableName">Table name that log entries will be written to. Note: Optional, setting this may impact performance</param>
		/// <param name="writeInBatches">Use a periodic batching sink, as opposed to a synchronous one-at-a-time sink; this alters the partition
		/// key used for the events so is not enabled by default.</param>
		/// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
		/// <param name="period">The time to wait between checking for event batches.</param>
		/// <param name="additionalRowKeyPostfix">Additional postfix string that will be appended to row keys</param>
		/// <returns>Logger configuration, allowing configuration to continue.</returns>
		/// <exception cref="ArgumentNullException">A required parameter is null.</exception>
		public static LoggerConfiguration AzureTableStorageWithProperties(
			this LoggerSinkConfiguration loggerConfiguration,
			CloudStorageAccount storageAccount,
			LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
			IFormatProvider formatProvider = null,
			string storageTableName = null,
			bool writeInBatches = false,
			TimeSpan? period = null,
			int? batchPostingLimit = null,
			string additionalRowKeyPostfix = null)
		{
			if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
			if (storageAccount == null) throw new ArgumentNullException("storageAccount");

			var sink = writeInBatches ?
				(ILogEventSink)new AzureBatchingTableStorageWithPropertiesSink(storageAccount, formatProvider, batchPostingLimit ?? DefaultBatchPostingLimit, period ?? DefaultPeriod, storageTableName, additionalRowKeyPostfix) :
				new AzureTableStorageWithPropertiesSink(storageAccount, formatProvider, storageTableName, additionalRowKeyPostfix);

			return loggerConfiguration.Sink(sink, restrictedToMinimumLevel);
		}

		/// <summary>
		/// Adds a sink that writes log events as records in the 'LogEventEntity' Azure Table Storage table in the given storage account.
		/// </summary>
		/// <param name="loggerConfiguration">The logger configuration.</param>
		/// <param name="connectionString">The Cloud Storage Account connection string to use to insert the log entries to.</param>
		/// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
		/// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
		/// <param name="storageTableName">Table name that log entries will be written to. Note: Optional, setting this may impact performance</param>
		/// <param name="writeInBatches">Use a periodic batching sink, as opposed to a synchronous one-at-a-time sink; this alters the partition
		/// key used for the events so is not enabled by default.</param>
		/// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
		/// <param name="period">The time to wait between checking for event batches.</param>
		/// <param name="additionalRowKeyPostfix">Additional postfix string that will be appended to row keys</param>
		/// <returns>Logger configuration, allowing configuration to continue.</returns>
		/// <exception cref="ArgumentNullException">A required parameter is null.</exception>
		public static LoggerConfiguration AzureTableStorageWithProperties(
			this LoggerSinkConfiguration loggerConfiguration,
			string connectionString,
			LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
			IFormatProvider formatProvider = null,
			string storageTableName = null,
			bool writeInBatches = false,
			TimeSpan? period = null,
			int? batchPostingLimit = null,
			string additionalRowKeyPostfix = null)
		{
			if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
			if (String.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");
			var storageAccount = CloudStorageAccount.Parse(connectionString);
			return AzureTableStorageWithProperties(loggerConfiguration, storageAccount, restrictedToMinimumLevel, formatProvider, storageTableName, writeInBatches, period, batchPostingLimit, additionalRowKeyPostfix);
		}
	}
}
