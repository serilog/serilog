using NUnit.Framework;
using Serilog.Context;
using Serilog.Events;
using Serilog.Tests.Support;
using System.Threading;
using System.Threading.Tasks;

namespace Serilog.Tests.Context
{
    [TestFixture]
    public class LogContextTests
    {
        [Test]
        public void MoreNestedPropertiesOverrideLessNestedOnes()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .CreateLogger();

            using (LogContext.PushProperty("A", 1))
            {
                log.Write(Some.InformationEvent());
                Assert.AreEqual(1, lastEvent.Properties["A"].LiteralValue());

                using (LogContext.PushProperty("A", 2))
                {
                    log.Write(Some.InformationEvent());
                    Assert.AreEqual(2, lastEvent.Properties["A"].LiteralValue());
                }

                log.Write(Some.InformationEvent());
                Assert.AreEqual(1, lastEvent.Properties["A"].LiteralValue());
            }

            log.Write(Some.InformationEvent());
            Assert.IsFalse(lastEvent.Properties.ContainsKey("A"));
        }

        [Test]
        public async Task ContextPropertiesCrossAsyncCalls()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .CreateLogger();

            using (LogContext.PushProperty("A", 1))
            {
                var pre = Thread.CurrentThread.ManagedThreadId;

                await Task.Delay(1000);

                var post = Thread.CurrentThread.ManagedThreadId;

                log.Write(Some.InformationEvent());
                Assert.AreEqual(1, lastEvent.Properties["A"].LiteralValue());

                // No problem if this happens occasionally.
                if (pre == post)
                    Assert.Inconclusive("The test was marshalled back to the same thread after awaiting");
            }
        }
    }
}
