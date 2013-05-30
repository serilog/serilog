using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Raven.Client.Embedded;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Sinks.RavenDB.Tests
{
    [TestFixture]
    public class RavenDBSinkTests
    {
        static readonly TimeSpan TinyWait = TimeSpan.FromMilliseconds(50);
        
        [Test]
        public void WhenAnEventIsWrittenToTheSinkItIsRetrievableFromTheDocumentStore()
        {
            using (var documentStore = new EmbeddableDocumentStore {RunInMemory = true}.Initialize())
            {
                var timestamp = new DateTimeOffset(2013, 05, 28, 22, 10, 20, 666, TimeSpan.FromHours(10));
                var exception = new ArgumentException("Mládek");
                const LogEventLevel level = LogEventLevel.Information;
                const string messageTemplate = "{Song}";
                var properties = new List<LogEventProperty> { new LogEventProperty("Song", new ScalarValue("New Macabre")) };

                using (var ravenSink = new RavenDBSink(documentStore, 2, TinyWait, null))
                {
                    var template = new MessageTemplateParser().Parse(messageTemplate);
                    var logEvent = new LogEvent(timestamp, level, exception, template, properties);
                    ravenSink.Emit(logEvent);
                }

                using (var session = documentStore.OpenSession())
                {
                    var events = session.Query<LogEventEntity>().Customize(x => x.WaitForNonStaleResults()).ToList();
                    Assert.AreEqual(1, events.Count);
                    var single = events.Single();
                    Assert.AreEqual(messageTemplate, single.MessageTemplate);
                    Assert.AreEqual("\"New Macabre\"", single.RenderedMessage);
                    Assert.AreEqual(timestamp, single.Timestamp);
                    Assert.AreEqual(level, single.Level);
                    Assert.AreEqual(1, single.Properties.Count);
                    Assert.AreEqual(exception.Message, single.Exception.Message);
                }
            }
        }
    }
}
