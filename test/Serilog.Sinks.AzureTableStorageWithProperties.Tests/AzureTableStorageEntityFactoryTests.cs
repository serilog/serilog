using Microsoft.WindowsAzure.Storage.Table;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.AzureTableStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.Sinks.AzureTableStorage.Tests
{
	[TestFixture]
	public class AzureTableStorageEntityFactoryTests
	{
		[Test]
		public void CreateEntityWithPropertiesShouldGenerateValidEntity()
		{
			var timestamp = new DateTimeOffset(2014, 12, 01, 18, 42, 20, 666, TimeSpan.FromHours(2));
			var exception = new ArgumentException("Some exceptional exception happened");
			var level = LogEventLevel.Information;
			var messageTemplate = "Template {Temp} {Prop}";
			var template = new MessageTemplateParser().Parse(messageTemplate);
			var properties = new List<LogEventProperty> {
				new LogEventProperty("Temp", new ScalarValue("Temporary")),
				new LogEventProperty("Prop", new ScalarValue("Property"))
			};
			var additionalRowKeyPostfix = "Some postfix";

			var logEvent = new Events.LogEvent(timestamp, level, exception, template, properties);

			var entity = AzureTableStorageEntityFactory.CreateEntityWithProperties(logEvent, null, additionalRowKeyPostfix);

			// Partition key
			var expectedPartitionKey = "0" + new DateTime(logEvent.Timestamp.Year, logEvent.Timestamp.Month, logEvent.Timestamp.Day, logEvent.Timestamp.Hour, logEvent.Timestamp.Minute, 0).Ticks;
			Assert.AreEqual(expectedPartitionKey, entity.PartitionKey);

			// Row Key
			var expectedRowKeyWithoutGuid = "Information|Template {Temp} {Prop}|Some postfix|";
			var rowKeyWithoutGuid = entity.RowKey.Substring(0, expectedRowKeyWithoutGuid.Length);
			var rowKeyGuid = entity.RowKey.Substring(expectedRowKeyWithoutGuid.Length);

			Assert.AreEqual(expectedRowKeyWithoutGuid, rowKeyWithoutGuid);
			Assert.DoesNotThrow(() => Guid.Parse(rowKeyGuid));
			Assert.AreEqual(Guid.Parse(rowKeyGuid).ToString(), rowKeyGuid);

			// Timestamp
			Assert.AreEqual(logEvent.Timestamp, entity.Timestamp);

			// Properties
			Assert.AreEqual(6, entity.Properties.Count);

			Assert.AreEqual(new EntityProperty(messageTemplate), entity.Properties["MessageTemplate"]);
			Assert.AreEqual(new EntityProperty("Information"), entity.Properties["Level"]);
			Assert.AreEqual(new EntityProperty("Template \"Temporary\" \"Property\""), entity.Properties["RenderedMessage"]);
			Assert.AreEqual(new EntityProperty(exception.ToString()), entity.Properties["Exception"]);
			Assert.AreEqual(new EntityProperty("Temporary"), entity.Properties["Temp"]);
			Assert.AreEqual(new EntityProperty("Property"), entity.Properties["Prop"]);
		}

		[Test]
		public void CreateEntityWithPropertiesShouldGenerateValidRowKey()
		{
			var timestamp = new DateTimeOffset(2014, 12, 01, 18, 42, 20, 666, TimeSpan.FromHours(2));
			var exception = new ArgumentException("Some exceptional exception happened");
			var level = LogEventLevel.Information;
			var additionalRowKeyPostfix = "POSTFIX";

			var postLength = additionalRowKeyPostfix.Length + 1 + Guid.NewGuid().ToString().Length;
			var messageSpace = 1024 - (level.ToString().Length + 1) - (1 + postLength);

			// Message up to available space, plus some characters (Z) that will be removed
			var messageTemplate = new string('x', messageSpace-4) + "ABCD" + new string('Z', 20);

			var template = new MessageTemplateParser().Parse(messageTemplate);
			var properties = new List<LogEventProperty>();

			var logEvent = new Events.LogEvent(timestamp, level, exception, template, properties);

			var entity = AzureTableStorageEntityFactory.CreateEntityWithProperties(logEvent, null, additionalRowKeyPostfix);

			// Row Key
			var expectedRowKeyWithoutGuid = "Information|" + new string('x', messageSpace-4) + "ABCD|POSTFIX|";
			var rowKeyWithoutGuid = entity.RowKey.Substring(0, expectedRowKeyWithoutGuid.Length);
			var rowKeyGuid = entity.RowKey.Substring(expectedRowKeyWithoutGuid.Length);

			Assert.AreEqual(1024, entity.RowKey.Length);
			Assert.AreEqual(expectedRowKeyWithoutGuid, rowKeyWithoutGuid);
			Assert.DoesNotThrow(() => Guid.Parse(rowKeyGuid));
			Assert.AreEqual(Guid.Parse(rowKeyGuid).ToString(), rowKeyGuid);
			Assert.False(entity.RowKey.Contains('Z'));
		}
	}
}
