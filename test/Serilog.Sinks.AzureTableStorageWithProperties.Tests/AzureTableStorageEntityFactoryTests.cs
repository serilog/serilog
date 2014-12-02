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

		[Test]
		public void CreateEntityWithPropertiesShouldSupportAzureTableTypesForScalar()
		{
			var messageTemplate = "{ByteArray} {Boolean} {DateTime} {DateTimeOffset} {Double} {Guid} {Int} {Long} {String}";
			var bytearrayValue = new byte[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 250, 251, 252, 253, 254, 255 };
			var booleanValue = true;
			var datetimeValue = DateTime.UtcNow;
			var datetimeoffsetValue = new DateTimeOffset(datetimeValue, TimeSpan.FromHours(0));
			var doubleValue = Math.PI;
			var guidValue = Guid.NewGuid();
			var intValue = int.MaxValue;
			var longValue = long.MaxValue;
			var stringValue = "Some string value";

			var properties = new List<LogEventProperty> {
				new LogEventProperty("ByteArray", new ScalarValue(bytearrayValue)),
				new LogEventProperty("Boolean", new ScalarValue(booleanValue)),
				new LogEventProperty("DateTime", new ScalarValue(datetimeValue)),
				new LogEventProperty("DateTimeOffset", new ScalarValue(datetimeoffsetValue)),
				new LogEventProperty("Double", new ScalarValue(doubleValue)),
				new LogEventProperty("Guid", new ScalarValue(guidValue)),
				new LogEventProperty("Int", new ScalarValue(intValue)),
				new LogEventProperty("Long", new ScalarValue(longValue)),
				new LogEventProperty("String", new ScalarValue(stringValue))
			};

			var template = new MessageTemplateParser().Parse(messageTemplate);

			var logEvent = new Events.LogEvent(DateTime.Now, LogEventLevel.Information, null, template, properties);

			var entity = AzureTableStorageEntityFactory.CreateEntityWithProperties(logEvent, null, null);

			Assert.AreEqual(3 + properties.Count, entity.Properties.Count);

			Assert.IsInstanceOf(typeof(byte[]), entity.Properties["ByteArray"].BinaryValue);
			Assert.AreEqual(bytearrayValue, entity.Properties["ByteArray"].BinaryValue);
			Assert.AreEqual(booleanValue, entity.Properties["Boolean"].BooleanValue);
			Assert.AreEqual(datetimeValue, entity.Properties["DateTime"].DateTime);
			Assert.AreEqual(datetimeoffsetValue, entity.Properties["DateTimeOffset"].DateTimeOffsetValue);
			Assert.AreEqual(doubleValue, entity.Properties["Double"].DoubleValue);
			Assert.AreEqual(guidValue, entity.Properties["Guid"].GuidValue);
			Assert.AreEqual(intValue, entity.Properties["Int"].Int32Value);
			Assert.AreEqual(longValue, entity.Properties["Long"].Int64Value);
			Assert.AreEqual(stringValue, entity.Properties["String"].StringValue);
		}

		[Test]
		public void CreateEntityWithPropertiesShouldSupportAzureTableTypesForDictionary()
		{
			var messageTemplate = "{Dictionary}";

			var dict1 = new DictionaryValue(new List<KeyValuePair<ScalarValue, LogEventPropertyValue>>{
				new KeyValuePair<ScalarValue, LogEventPropertyValue>(new ScalarValue("d1k1"), new ScalarValue("d1k1v1")),
				new KeyValuePair<ScalarValue, LogEventPropertyValue>(new ScalarValue("d1k2"), new ScalarValue("d1k2v2")),
				new KeyValuePair<ScalarValue, LogEventPropertyValue>(new ScalarValue("d1k3"), new ScalarValue("d1k3v3")),
			});

			var dict2 = new DictionaryValue(new List<KeyValuePair<ScalarValue, LogEventPropertyValue>>{
				new KeyValuePair<ScalarValue, LogEventPropertyValue>(new ScalarValue("d2k1"), new ScalarValue("d2k1v1")),
				new KeyValuePair<ScalarValue, LogEventPropertyValue>(new ScalarValue("d2k2"), new ScalarValue("d2k2v2")),
				new KeyValuePair<ScalarValue, LogEventPropertyValue>(new ScalarValue("d2k3"), new ScalarValue("d2k3v3")),
			});

			var dict0 = new DictionaryValue(new List<KeyValuePair<ScalarValue, LogEventPropertyValue>>{
				 new KeyValuePair<ScalarValue, LogEventPropertyValue>(new ScalarValue("d1"), dict1),
				 new KeyValuePair<ScalarValue, LogEventPropertyValue>(new ScalarValue("d2"), dict2),
				 new KeyValuePair<ScalarValue, LogEventPropertyValue>(new ScalarValue("d0"), new ScalarValue(0))
			});



			var properties = new List<LogEventProperty> {
				new LogEventProperty("Dictionary", dict0)
			};

			var template = new MessageTemplateParser().Parse(messageTemplate);

			var logEvent = new Events.LogEvent(DateTime.Now, LogEventLevel.Information, null, template, properties);

			var entity = AzureTableStorageEntityFactory.CreateEntityWithProperties(logEvent, null, null);

			Assert.AreEqual(3 + properties.Count, entity.Properties.Count);
			Assert.AreEqual("[(\"d1\": [(\"d1k1\": \"d1k1v1\"), (\"d1k2\": \"d1k2v2\"), (\"d1k3\": \"d1k3v3\")]), (\"d2\": [(\"d2k1\": \"d2k1v1\"), (\"d2k2\": \"d2k2v2\"), (\"d2k3\": \"d2k3v3\")]), (\"d0\": 0)]", entity.Properties["Dictionary"].StringValue);
		}

		[Test]
		public void CreateEntityWithPropertiesShouldSupportAzureTableTypesForSequence()
		{
			var messageTemplate = "{Sequence}";

			var sequence1 = new SequenceValue(new List<LogEventPropertyValue>
			{
				new ScalarValue(1),
				new ScalarValue(2),
				new ScalarValue(3),
				new ScalarValue(4),
				new ScalarValue(5)
			});

			var sequence2 = new SequenceValue(new List<LogEventPropertyValue>
			{
				new ScalarValue("a"),
				new ScalarValue("b"),
				new ScalarValue("c"),
				new ScalarValue("d"),
				new ScalarValue("e")
			});

			var sequence0 = new SequenceValue(new List<LogEventPropertyValue>
			{
				sequence1,
				sequence2
			});

			var properties = new List<LogEventProperty> {
				new LogEventProperty("Sequence", sequence0)
			};

			var template = new MessageTemplateParser().Parse(messageTemplate);

			var logEvent = new Events.LogEvent(DateTime.Now, LogEventLevel.Information, null, template, properties);

			var entity = AzureTableStorageEntityFactory.CreateEntityWithProperties(logEvent, null, null);

			Assert.AreEqual(3 + properties.Count, entity.Properties.Count);
			Assert.AreEqual("[[1, 2, 3, 4, 5], [\"a\", \"b\", \"c\", \"d\", \"e\"]]", entity.Properties["Sequence"].StringValue);
		}

	}
}
