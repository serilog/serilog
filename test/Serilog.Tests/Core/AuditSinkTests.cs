using System;
using System.Reflection;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Core
{
    public class AuditSinkTests
    {
        [Fact]
        public void ExceptionsThrownByAuditSinksArePropagated()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new DelegatingSink(e => { throw new Exception("Boom!"); }))
                .CreateLogger();

            Assert.Throws<AggregateException>(() => logger.Write(Some.InformationEvent()));
        }

        [Fact]
        public void ExceptionsThrownByFiltersArePropagatedIfAuditingEnabled()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new DelegatingSink(e => { }))
                .Filter.ByExcluding(e => { throw new Exception("Boom!"); })
                .CreateLogger();

            Assert.Throws<Exception>(() => logger.Write(Some.InformationEvent()));
        }

        [Fact]
        public void ExceptionsThrownByAuditSinksArePropagatedFromChildLoggers()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new DelegatingSink(e => { throw new Exception("Boom!"); }))
                .CreateLogger();

            Assert.Throws<AggregateException>(() => logger
                .ForContext<LoggerConfigurationTests>()
                .Write(Some.InformationEvent()));
        }

        [Fact]
        public void ExceptionsThrownByDestructuringPoliciesArePropagatedIfAuditingEnabled()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new CollectingSink())
                .Destructure.ByTransforming<Value>(v => { throw new Exception("Boom!"); })
                .CreateLogger();

            Assert.Throws<Exception>(() => logger.Information("{@Value}", new Value()));
        }

        [Fact]
        public void ExceptionsThrownByPropertyAccessorsAreNotPropagated()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sink(new CollectingSink())
                .CreateLogger();

            logger.Information("{@Value}", new ThrowingProperty());

            Assert.True(true, "No exception reached the caller");
        }

        [Fact]
        public void ExceptionsThrownByPropertyAccessorsArePropagatedIfAuditingEnabled()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new CollectingSink())
                .CreateLogger();

            Assert.Throws<TargetInvocationException>(() => logger.Information("{@Value}", new ThrowingProperty()));
        }

        [Fact]
        public void SinkIsDisposedWhenLoggerDisposed()
        {
            var tracker = new DisposeTrackingSink();
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(tracker)
                .CreateLogger();

            logger.Dispose();

            Assert.True(tracker.IsDisposed);
        }

        class Value { }

        class ThrowingProperty
        {
            // ReSharper disable once UnusedMember.Local
            public string Property
            {
                get { throw new Exception("Boom!"); }
            }
        }
    }
}