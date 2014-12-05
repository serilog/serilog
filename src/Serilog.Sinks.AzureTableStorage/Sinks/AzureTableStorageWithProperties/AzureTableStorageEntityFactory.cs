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
using System.Text;
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
		/// <param name="additionalRowKeyPostfix">Additional postfix string that will be appended to row keys</param>
		/// <returns></returns>
		public static DynamicTableEntity CreateEntityWithProperties(LogEvent logEvent, IFormatProvider formatProvider, string additionalRowKeyPostfix)
		{
			var tableEntity = new DynamicTableEntity();

			tableEntity.PartitionKey = GenerateValidPartitionKey(logEvent);
			tableEntity.RowKey = GenerateValidRowKey(logEvent, additionalRowKeyPostfix);
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
				dynamicProperties.Add(logProperty.Key, AzurePropertyFormatter.ToEntityProperty(logProperty.Value));
			}

			return tableEntity;
		}

		/// <summary>
		/// Generate a valid string for a table property key by removing invalid characters
		/// </summary>
		/// <param name="s">
		/// The input string
		/// </param>
		/// <returns>
		/// The string that can be used as a property
		/// </returns>
		public static string GetValidStringForTableKey(string s)
		{
			return _rowKeyNotAllowedMatch.Replace(s, "");
		}

		// Generate a valid partition key from event timestamp.
		private static string GenerateValidPartitionKey(LogEvent logEvent)
		{
			// Like WAD, round the timestamp the minute.
			return "0" + new DateTime(
				logEvent.Timestamp.Year,
				logEvent.Timestamp.Month,
				logEvent.Timestamp.Day,
				logEvent.Timestamp.Hour,
				logEvent.Timestamp.Minute,
				0).Ticks;
		}

		// Generate a valid Row Key by joining postfix and prefix. If longer
		// than 1K, prefix is truncated.
		// See http://msdn.microsoft.com/en-us/library/windowsazure/dd179338.aspx
		private static string GenerateValidRowKey(LogEvent logEvent, string additionalRowKeyPostfix)
		{
			var prefixBuilder = new StringBuilder(512);

			// Join level and message template
			prefixBuilder.Append(logEvent.Level).Append('|').Append(GetValidStringForTableKey(logEvent.MessageTemplate.Text));

			var postfixBuilder = new StringBuilder(512);

			if (additionalRowKeyPostfix != null)
			{
				// additionalRowKeyPostfix is already stripped of invalid characters
				postfixBuilder.Append('|').Append(additionalRowKeyPostfix);
			}

			// Append GUID to postfix	
			postfixBuilder.Append('|').Append(Guid.NewGuid());

			// Truncate prefix if too long
			var maxPrefixLength = 1024 - postfixBuilder.Length;
			if (prefixBuilder.Length > maxPrefixLength)
			{
				prefixBuilder.Length = maxPrefixLength;
			}

			return prefixBuilder.Append(postfixBuilder).ToString();
		}
	}
}
