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

using Microsoft.WindowsAzure.Storage.Table;
using Serilog.Events;
using System;
using System.Text.RegularExpressions;

namespace Serilog.Sinks.AzureTableStorage
{
	/// <summary>
	/// Utility class for Azure Storage Table entity
	/// </summary>
	public static class AzureTableStorageEntityFactory
	{
		static readonly Regex _rowKeyNotAllowedMatch = new Regex(@"(\\|/|#|\?)");

		/// <summary>
		/// Creates a DynamicTableEntity for Azure Storage, given a Serilog <see cref="LogEvent"/>.Properties
		/// are stored as separate columns.
		/// </summary>
		/// <param name="logEvent">The event to log</param>
		/// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
		/// <param name="partitionKey"></param>
		/// <returns></returns>
		public static DynamicTableEntity CreateEntityWithProperties(LogEvent logEvent, IFormatProvider formatProvider, long partitionKey)
		{
			var tableEntity = new DynamicTableEntity();

			// It is not a good idea to use ticks directly as this may fragment the partition
			// too much. WAD rounds the DateTime to the nearest minute.
			// TODO: Copy what's done in WAD
			tableEntity.PartitionKey = string.Format("0{0}", partitionKey);

			// TODO: Find a good way to create unique Id even if running on multiple servers.
			tableEntity.RowKey = GetValidRowKey(string.Format("{0}|{1}", logEvent.Level, logEvent.MessageTemplate.Text));

			tableEntity.Timestamp = logEvent.Timestamp;

			var dynamicProperties = tableEntity.Properties;

			dynamicProperties.Add("MessageTemplate", new EntityProperty(logEvent.MessageTemplate.Text));
			dynamicProperties.Add("Level", new EntityProperty(logEvent.Level.ToString()));
			dynamicProperties.Add("RenderedMessage", new EntityProperty(logEvent.RenderMessage(formatProvider)));

			if (logEvent.Exception != null)
			{
				dynamicProperties.Add("Exception", new EntityProperty(logEvent.Exception.ToString()));
			}

			foreach (var logProperty in logEvent.Properties)
			{
				// TODO: Convert values to JSON. Can properties in LogEvent preserve their original types?

				dynamicProperties.Add(logProperty.Key, new EntityProperty(logProperty.Value.ToString()));
			}

			return tableEntity;
		}

		// http://msdn.microsoft.com/en-us/library/windowsazure/dd179338.aspx
		// TODO: Leave some space for additional postfixes at the end...
		private static string GetValidRowKey(string rowKey)
		{
			rowKey = _rowKeyNotAllowedMatch.Replace(rowKey, "");
			return rowKey.Length > 1024 ? rowKey.Substring(0, 1024) : rowKey;
		}
	}
}
