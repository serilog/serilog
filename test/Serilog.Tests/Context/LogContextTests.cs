#if !DNXCORE50
using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using Xunit;
using Serilog.Context;
using Serilog.Events;
using Serilog.Core.Enrichers;
using Serilog.Tests.Support;
using System.Threading;
using System.Threading.Tasks;

namespace Serilog.Tests.Context
{
    public class LogContextTests
    {
        public LogContextTests()
        {
            LogContext.PermitCrossAppDomainCalls = false;
            CallContext.LogicalSetData(typeof(LogContext).FullName, null);
        }

        [Fact]
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
                Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());

                using (LogContext.PushProperty("A", 2))
                {
                    log.Write(Some.InformationEvent());
                    Assert.Equal(2, lastEvent.Properties["A"].LiteralValue());
                }

                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());
            }

            log.Write(Some.InformationEvent());
            Assert.False(lastEvent.Properties.ContainsKey("A"));
        }

        [Fact]
        public void MultipleNestedPropertiesOverrideLessNestedOnes()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .CreateLogger();

            using (LogContext.PushProperties(new PropertyEnricher("A1", 1), new PropertyEnricher("A2", 2)))
            {
                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A1"].LiteralValue());
                Assert.Equal(2, lastEvent.Properties["A2"].LiteralValue());

                using (LogContext.PushProperties(new PropertyEnricher("A1", 10), new PropertyEnricher("A2", 20)))
                {
                    log.Write(Some.InformationEvent());
                    Assert.Equal(10, lastEvent.Properties["A1"].LiteralValue());
                    Assert.Equal(20, lastEvent.Properties["A2"].LiteralValue());
                }

                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A1"].LiteralValue());
                Assert.Equal(2, lastEvent.Properties["A2"].LiteralValue());
            }

            log.Write(Some.InformationEvent());
            Assert.False(lastEvent.Properties.ContainsKey("A1"));
            Assert.False(lastEvent.Properties.ContainsKey("A2"));
        }

        [Fact]
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
                Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());

                // No problem if this happens occasionally.
                // TODO: xUnit inconclusive?
                //if (pre == post)
                //    Assert.Inconclusive("The test was marshalled back to the same thread after awaiting");
            }
        }

        [Fact]
        public async Task ContextPropertiesPersistWhenCrossAppDomainCallsAreEnabled()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .CreateLogger();

            LogContext.PermitCrossAppDomainCalls = true;

            using (LogContext.PushProperty("A", 1))
            {
                var pre = Thread.CurrentThread.ManagedThreadId;

                await Task.Delay(1000);

                var post = Thread.CurrentThread.ManagedThreadId;

                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());

                // No problem if this happens occasionally.
                // TODO: xUnit inconclusive?
                //if (pre == post)
                //    Assert.Inconclusive("The test was marshalled back to the same thread after awaiting");
            }
        }

        // Must not actually try to pass context across domains,
        // since user property types may not be serializable.
        // Fails if the Serilog assemblies cannot be loaded in the
        // remote domain. See also LogContext.Suspend()
        [Fact(Skip = "Fails on DNX451")]
        public void DoesNotPreventCrossDomainCalls()
        {
            AppDomain domain = null;
            try
            {
                var domaininfo = new AppDomainSetup { ApplicationBase = Path.GetDirectoryName(GetType().Assembly.CodeBase.Replace("file:///", "")) };
                var evidence = AppDomain.CurrentDomain.Evidence;
                domain = AppDomain.CreateDomain("LogContextTest", evidence, domaininfo);

                var callable = (RemotelyCallable)domain.CreateInstanceAndUnwrap(typeof(RemotelyCallable).Assembly.FullName, typeof(RemotelyCallable).FullName);

                using (LogContext.PushProperty("Anything", 1001))
                    Assert.True(callable.IsCallable());
            }
            finally
            {
                if (domain != null)
                    AppDomain.Unload(domain);
            }
        }

        [Fact]
        public void WhenSuspendedAllPropertiesAreRemovedFromTheContext()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .CreateLogger();

            using (LogContext.PushProperty("A1", 1))
            {
                using (LogContext.Suspend())
                {
                    log.Write(Some.InformationEvent());
                    Assert.False(lastEvent.Properties.ContainsKey("A1"));
                }

                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A1"].LiteralValue());
            }
        }
    }

    public class RemotelyCallable : MarshalByRefObject
    {
        public bool IsCallable()
        {
            var sw = new StringWriter();

            var log = new LoggerConfiguration()
                .WriteTo.TextWriter(sw, outputTemplate: "{Anything}{Number}")
                .Enrich.FromLogContext()
                .CreateLogger();

            using (LogContext.PushProperty("Number", 42))
                log.Information("Hello");

            var s = sw.ToString();
            return s == "42";
        }
    }
}
#endif
