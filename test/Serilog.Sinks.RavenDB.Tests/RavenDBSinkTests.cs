using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public void WhenAnEventIsEnqueuedItIsWrittenToABatch_OnTimer()
        {
            using (var documentStore = new EmbeddableDocumentStore {RunInMemory = true}.Initialize())
            {
                var timestamp = new DateTimeOffset(2013, 05, 28, 22, 10, 20, 666, new TimeSpan(0));
                var ravenSink = new RavenDBSink(documentStore, 2, TinyWait, null);
                var logEvent = new LogEvent(timestamp, LogEventLevel.Information, new ArgumentNullException("Mládek"), new MessageTemplateParser().Parse("Geneva"), new List<LogEventProperty> { new LogEventProperty("Youngblood", new ScalarValue("New Macabre")) });
                ravenSink.Emit(logEvent);
                Thread.Sleep(TinyWait + TinyWait);
                ravenSink.Dispose();

                using (var session = documentStore.OpenSession())
                {
                    var events = session.Query<LogEventEntity>().Customize(x => x.WaitForNonStaleResults()).ToList();
                    Assert.AreEqual(1, events.Count);
                    Assert.AreEqual(timestamp, events.First().Timestamp);
                    Assert.AreEqual(LogEventLevel.Information, events.First().Level);
                    Assert.AreEqual(1, events.First().Properties.Count);
                }
            }
        }
    }
}
