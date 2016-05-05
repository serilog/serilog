using Xunit;
using Serilog.Context;
using Serilog.Events;
using Serilog.Core.Enrichers;
using Serilog.Tests.Support;
#if REMOTING
using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
#endif
using System.Threading;
using System.Threading.Tasks;

namespace Serilog.Tests.Context
{
    public class LogContextTests
    {
        public LogContextTests()
        {
#if REMOTING
            LogContext.PermitCrossAppDomainCalls = false;
#endif
#if !ASYNCLOCAL
            CallContext.LogicalSetData(typeof(LogContext).FullName, null);
#endif
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

                // No problem if this happens occasionally; was Assert.Inconclusive().
                // The test was marshalled back to the same thread after awaiting.
                Assert.NotSame(pre, post);
            }
        }

#if REMOTING
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

                // No problem if this happens occasionally; was Assert.Inconclusive().
                // The test was marshalled back to the same thread after awaiting.
                Assert.NotSame(pre, post);
            }
        }
#endif

#if APPDOMAIN
        // Must not actually try to pass context across domains,
        // since user property types may not be serializable.
        // Fails if the Serilog assemblies cannot be loaded in the
        // remote domain. See also LogContext.Suspend()
        [Fact]
        public void DoesNotPreventCrossDomainCalls()
        {
            var projectRoot = Environment.CurrentDirectory;
            while (!File.Exists(Path.Combine(projectRoot, "global.json")))
            {
                projectRoot = Directory.GetParent(projectRoot).FullName;
            }

            AppDomain domain = null;
            try
            {
                const string configuration =
#if DEBUG
                "Debug";
#else
                "Release";
#endif

                var domaininfo = new AppDomainSetup
                {
                    ApplicationBase = Path.Combine(projectRoot, @"artifacts\"),
                    PrivateBinPath = @"Serilog.Tests.xproj\testbin\Debug\dnx452;Serilog.xproj\testbin\Debug\net45;bin\Serilog\Debug\dnx452;bin\Serilog\Debug\net45;bin\Serilog.Tests\Debug\dnx452;".Replace("Debug", configuration)
                };
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
#endif

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

#if REMOTING
    public class RemotelyCallable : MarshalByRefObject
    {
        public bool IsCallable()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .Enrich.FromLogContext()
                .CreateLogger();

            using (LogContext.PushProperty("Number", 42))
                log.Information("Hello");

            return 42.Equals(lastEvent.Properties["Number"].LiteralValue());
        }
    }
#endif
}