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
		public void WhenAnEventIsWrittenToTheSinkItIsRetrievableFromTheTable()
		{
			var storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
			var tableClient = storageAccount.CreateCloudTableClient();
			var table = tableClient.GetTableReference("LogEventEntity");

			table.DeleteIfExists();

			var sink = new AzureTableStorageSink(storageAccount, null);

			var timestamp = new DateTimeOffset(2014, 11, 29, 15, 34, 11, 666, TimeSpan.FromHours(10));
			var level = LogEventLevel.Information;
			var exception = new ArgumentException("Some exception");

			const string messageTemplate = "{Properties} should go in their {Numbered} {Space}";
			var properties = new List<LogEventProperty> {
				new LogEventProperty("Properties", new ScalarValue("Properties")),
				new LogEventProperty("Space", new ScalarValue(' ')),
				new LogEventProperty("Numbered", new ScalarValue(1234))
			};
			var template = new MessageTemplateParser().Parse(messageTemplate);

			var logEvent = new Events.LogEvent(timestamp, level, exception, template, properties);

			sink.Emit(logEvent);


            var dataString = new StringWriter();
            new JsonFormatter(closingDelimiter: "", formatProvider: null).Format(logEvent, dataString);

			var result = table.ExecuteQuery(new TableQuery().Take(1)).First();
			Assert.AreEqual(messageTemplate, result.Properties["MessageTemplate"].StringValue);
			Assert.AreEqual("Information", result.Properties["Level"].StringValue);
			Assert.AreEqual("System.ArgumentException: Some exception", result.Properties["Exception"].StringValue);
			Assert.AreEqual("\"Properties\" should go in their 1234  ", result.Properties["RenderedMessage"].StringValue);
		}

		[Test]
		public void WhenALoggerWritesToTheSinkItIsRetrievableFromTheTable()
		{
			var storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
			var tableClient = storageAccount.CreateCloudTableClient();
			var table = tableClient.GetTableReference("LogEventEntity");

			var logger = new LoggerConfiguration()
				.WriteTo.AzureTableStorage(storageAccount)
				.CreateLogger();

			var exception = new ArgumentException("Some exception");

			const string messageTemplate = "{Properties} should go in their {Numbered} {Space}";

			logger.Information(exception, messageTemplate, "Properties", 1234, ' ');

			var result = table.ExecuteQuery(new TableQuery().Take(1)).First();
			Assert.AreEqual(messageTemplate, result.Properties["MessageTemplate"].StringValue);
			Assert.AreEqual("Information", result.Properties["Level"].StringValue);
			Assert.AreEqual("System.ArgumentException: Some exception", result.Properties["Exception"].StringValue);
			Assert.AreEqual("\"Properties\" should go in their 1234  ", result.Properties["RenderedMessage"].StringValue);
		}

		[Test]
		public void WhenALoggerWritesToTheSinkItIsRetrievableFromTheTableWithProperties()
		{
			var storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
			var tableClient = storageAccount.CreateCloudTableClient();
			var table = tableClient.GetTableReference("SerilogEvents");

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
			Assert.AreEqual("\"Properties\"", result.Properties["Properties"].PropertyAsObject);
			Assert.AreEqual("1234", result.Properties["Numbered"].StringValue);
			Assert.AreEqual(" ", result.Properties["Space"].StringValue);
		}
	}
}
