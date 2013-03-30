using System;
using System.Collections.Generic;
using NUnit.Framework;
using Serilog.Core;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Tests
{
    [TestFixture]
    public class LoggerConfigurationTests
    {
        class DisposableSink : ILogEventSink, IDisposable
        {
            public bool IsDisposed { get; set; }

            public void Emit(LogEvent logEvent) { }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        [Test]
        public void DisposableSinksAreDisposedAlongWithRootLogger()
        {
            var sink = new DisposableSink();
            var logger = (IDisposable) new LoggerConfiguration()
                .WithSink(sink)
                .CreateLogger();

            logger.Dispose(); 
            Assert.IsTrue(sink.IsDisposed);
        }

        [Test]
        public void DisposableSinksAreNotDisposedAlongWithContextualLoggers()
        {
            var sink = new DisposableSink();
            var logger = (IDisposable) new LoggerConfiguration()
                .WithSink(sink)
                .CreateLogger()
                .ForContext<LoggerConfigurationTests>();

            logger.Dispose();
            Assert.IsFalse(sink.IsDisposed);
        }

        [Test]
        public void AFilterPreventsMatchedEventsFromPassingToTheSink()
        {
            var excluded = Some.LogEvent();
            var included = Some.LogEvent();

            var filter = new DelegateFilter(e => e.MessageTemplate != excluded.MessageTemplate);
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);
            var logger = new LoggerConfiguration()
                .WithSink(sink)
                .FilteredBy(filter)
                .CreateLogger();
            logger.Write(included);
            logger.Write(excluded);
            Assert.AreEqual(1, events.Count);
            Assert.That(events.Contains(included));
        }
    }
}
