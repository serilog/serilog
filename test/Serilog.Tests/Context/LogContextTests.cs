using Xunit;
using Serilog.Context;
using Serilog.Events;
using Serilog.Core.Enrichers;
using Serilog.Tests.Support;
#if APPDOMAIN
using System;
using System.IO;
#endif
#if REMOTING
using System.Runtime.Remoting.Messaging;
#endif
using System.Threading;
using System.Threading.Tasks;
using Serilog.Core;

namespace Serilog.Tests.Context
{
    public class LogContextTests
    {
        public LogContextTests()
        {
#if REMOTING
            CallContext.LogicalSetData(typeof(LogContext).FullName, null);
#endif
        }

        [Fact]
        public void PushedPropertiesAreAvailableToLoggers()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .CreateLogger();

            using (LogContext.PushProperty("A", 1))
            using (LogContext.Push(new PropertyEnricher("B", 2)))
            using (LogContext.Push(new PropertyEnricher("C", 3), new PropertyEnricher("D", 4))) // Different overload
            {
                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());
                Assert.Equal(2, lastEvent.Properties["B"].LiteralValue());
                Assert.Equal(3, lastEvent.Properties["C"].LiteralValue());
                Assert.Equal(4, lastEvent.Properties["D"].LiteralValue());
            }
        }

        [Fact]
        public void LogContextCanBeCloned()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .CreateLogger();

            ILogEventEnricher clonedContext;
            using (LogContext.PushProperty("A", 1))
            {
                clonedContext = LogContext.Clone();
            }

            using (LogContext.Push(clonedContext))
            {
                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());
            }
        }

        [Fact]
        public void ClonedLogContextCanSharedAcrossThreads()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .CreateLogger();

            ILogEventEnricher clonedContext;
            using (LogContext.PushProperty("A", 1))
            {
                clonedContext = LogContext.Clone();
            }

            var t = new Thread(() =>
            {
                using (LogContext.Push(clonedContext))
                {
                    log.Write(Some.InformationEvent());
                }
            });

            t.Start();
            t.Join();

            Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());
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

            using (LogContext.Push(new PropertyEnricher("A1", 1), new PropertyEnricher("A2", 2)))
            {
                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A1"].LiteralValue());
                Assert.Equal(2, lastEvent.Properties["A2"].LiteralValue());

                using (LogContext.Push(new PropertyEnricher("A1", 10), new PropertyEnricher("A2", 20)))
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

#if APPDOMAIN
        // Must not actually try to pass context across domains,
        // since user property types may not be serializable.
        [Fact(Skip = "Needs to be updated for dotnet runner.")]
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
                    ApplicationBase = projectRoot,
                    PrivateBinPath = @"test\Serilog.Tests\bin\Debug\net452\win7-x64".Replace("Debug", configuration)
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
    }

#if REMOTING || APPDOMAIN
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