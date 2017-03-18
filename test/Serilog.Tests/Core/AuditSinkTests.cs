using System;
using System.Reflection;
using System.Threading.Tasks;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Core
{
    public class AuditSinkTests
    {
        [Fact]
        public async Task ExceptionsThrownByAuditSinksArePropagated()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new DelegatingSink(e => { throw new Exception("Boom!"); }))
                .CreateLogger();

            await Assert.ThrowsAsync<Exception>(() => logger.Write(Some.InformationEvent()));
        }

        [Fact]
        public async Task ExceptionsThrownByFiltersArePropagatedIfAuditingEnabled()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new DelegatingSink(e => { }))
                .Filter.ByExcluding(e => { throw new Exception("Boom!"); })
                .CreateLogger();

            await Assert.ThrowsAsync<Exception>(() => logger.Write(Some.InformationEvent()));
        }

        [Fact]
        public async Task ExceptionsThrownByAuditSinksArePropagatedFromChildLoggers()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new DelegatingSink(e => { throw new Exception("Boom!"); }))
                .CreateLogger();

            await Assert.ThrowsAsync<Exception>(() => logger
                .ForContext<LoggerConfigurationTests>()
                .Write(Some.InformationEvent()));
        }

        [Fact]
        public async Task ExceptionsThrownByDestructuringPoliciesArePropagatedIfAuditingEnabled()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new CollectingSink())
                .Destructure.ByTransforming<Value>(v => { throw new Exception("Boom!"); })
                .CreateLogger();

            await Assert.ThrowsAsync<Exception>(() => logger.Information("{@Value}", new Value()));
        }

        [Fact]
        public async Task ExceptionsThrownByPropertyAccessorsAreNotPropagated()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sink(new CollectingSink())
                .CreateLogger();

            await logger.Information("{@Value}", new ThrowingProperty());

            Assert.True(true, "No exception reached the caller");
        }

        [Fact]
        public async Task ExceptionsThrownByPropertyAccessorsArePropagatedIfAuditingEnabled()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new CollectingSink())
                .CreateLogger();

            await Assert.ThrowsAsync<TargetInvocationException>(() => logger.Information("{@Value}", new ThrowingProperty()));
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