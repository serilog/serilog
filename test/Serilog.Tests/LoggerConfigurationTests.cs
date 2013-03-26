using System;
using NUnit.Framework;
using Serilog.Core;
using Serilog.Events;

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
    }
}
