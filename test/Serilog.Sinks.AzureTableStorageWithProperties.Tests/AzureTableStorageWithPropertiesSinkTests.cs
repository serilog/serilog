using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Linq;
using System.Collections.Generic;
using Serilog.Formatting.Json;
using System.IO;

namespace Serilog.Sinks.AzureTableStorage.Tests
{
	[TestFixture]
	public class AzureTableStorageWithPropertiesSinkTests
	{
		[Test]
		public void WhenALoggerWritesToTheSinkItIsRetrievableFromTheTableWithProperties()
		{
			var storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
			var tableClient = storageAccount.CreateCloudTableClient();
			var table = tableClient.GetTableReference("LogEventEntity");

			table.DeleteIfExists();

			var logger = new LoggerConfiguration()
				.WriteTo.AzureTableStorageWithProperties(storageAccount)
				.CreateLogger();

			var exception = new ArgumentException("Some exception");

			const string messageTemplate = "{Properties} should go in their {Numbered} {Space}";

			logger.Information(exception, messageTemplate, "Properties", 1234, ' ');

			var result = table.ExecuteQuery(new TableQuery().Take(1)).First();
			
			// Check the presence of same properties as in previous version
			Assert.AreEqual(messageTemplate, result.Properties["MessageTemplate"].StringValue);
			Assert.AreEqual("Information", result.Properties["Level"].StringValue);
			Assert.AreEqual("System.ArgumentException: Some exception", result.Properties["Exception"].StringValue);
			Assert.AreEqual("\"Properties\" should go in their 1234  ", result.Properties["RenderedMessage"].StringValue);

			// Check the presence of the new properties.
			Assert.AreEqual("Properties", result.Properties["Properties"].PropertyAsObject);
			Assert.AreEqual(1234, result.Properties["Numbered"].PropertyAsObject);
			Assert.AreEqual(" ", result.Properties["Space"].PropertyAsObject);
		}

		[Test]
		public void WhenALoggerWritesToTheSinkItStoresTheCorrectTypesForScalar()
		{
			var storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
			var tableClient = storageAccount.CreateCloudTableClient();
			var table = tableClient.GetTableReference("LogEventEntity");

			table.DeleteIfExists();

			var logger = new LoggerConfiguration()
				.WriteTo.AzureTableStorageWithProperties(storageAccount)
				.CreateLogger();

			var bytearrayValue = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 250, 251, 252, 253, 254, 255 };
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

			logger.Information("{ByteArray} {Boolean} {DateTime} {DateTimeOffset} {Double} {Guid} {Int} {Long} {String}",
				bytearrayValue,
				booleanValue,
				datetimeValue,
				datetimeoffsetValue,
				doubleValue,
				guidValue,
				intValue,
				longValue,
				stringValue);

			var result = table.ExecuteQuery(new TableQuery().Take(1)).First();

			Assert.AreEqual(bytearrayValue, result.Properties["ByteArray"].BinaryValue);
			Assert.AreEqual(booleanValue, result.Properties["Boolean"].BooleanValue);
			Assert.AreEqual(datetimeValue, result.Properties["DateTime"].DateTime);
			Assert.AreEqual(datetimeoffsetValue, result.Properties["DateTimeOffset"].DateTimeOffsetValue);
			Assert.AreEqual(doubleValue, result.Properties["Double"].DoubleValue);
			Assert.AreEqual(guidValue, result.Properties["Guid"].GuidValue);
			Assert.AreEqual(intValue, result.Properties["Int"].Int32Value);
			Assert.AreEqual(longValue, result.Properties["Long"].Int64Value);
			Assert.AreEqual(stringValue, result.Properties["String"].StringValue);
		}
	}
}
