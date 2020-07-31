using Serilog.Context;
using Serilog.Core.Enrichers;
using Serilog.Events;
using Serilog.Tests.Support;
#if REMOTING
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Services;
#endif
using System;
using System.Threading;
using System.Threading.Tasks;
using Serilog.Core;
using Xunit;

namespace Serilog.Tests.Context
{
    public class LogContextTests
    {
        static LogContextTests()
        {
#if REMOTING
            LifetimeServices.LeaseTime = TimeSpan.FromMilliseconds(100);
            LifetimeServices.LeaseManagerPollTime = TimeSpan.FromMilliseconds(10);
#endif
        }

        public LogContextTests()
        {
#if REMOTING
            // ReSharper disable AssignNullToNotNullAttribute
            CallContext.LogicalSetData(typeof(LogContext).FullName, null);
            // ReSharper restore AssignNullToNotNullAttribute
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
            await TestWithSyncContext(async () =>
            {
                LogEvent lastEvent = null;

                var log = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                    .CreateLogger();

                using (LogContext.PushProperty("A", 1))
                {
                    var pre = Thread.CurrentThread.ManagedThreadId;

                    await Task.Yield();

                    var post = Thread.CurrentThread.ManagedThreadId;

                    log.Write(Some.InformationEvent());
                    Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());

                    Assert.False(Thread.CurrentThread.IsThreadPoolThread);
                    Assert.True(Thread.CurrentThread.IsBackground);
                    Assert.NotEqual(pre, post);
                }
            },
            new ForceNewThreadSyncContext());
        }

        [Fact]
        public async Task ContextEnrichersInAsyncScopeCanBeCleared()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .CreateLogger();

            using (LogContext.Push(new PropertyEnricher("A", 1)))
            {
                await Task.Run(() =>
                {
                    LogContext.Reset();
                    log.Write(Some.InformationEvent());
                });

                Assert.Empty(lastEvent.Properties);

                // Reset should only work for current async scope, outside of it previous Context
                // instance should be available again.
                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());
            }
        }

        [Fact]
        public async Task ContextEnrichersCanBeTemporarilyCleared()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .CreateLogger();

            using (LogContext.Push(new PropertyEnricher("A", 1)))
            {
                using (LogContext.Suspend())
                {
                    await Task.Run(() =>
                    {
                        log.Write(Some.InformationEvent());
                    });

                    Assert.Empty(lastEvent.Properties);
                }

                // Suspend should only work for scope of using. After calling Dispose all enrichers
                // should be restored.
                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());
            }
        }

#if APPDOMAIN
        // Must not actually try to pass context across domains,
        // since user property types may not be serializable.
        [Fact]
        public void DoesNotPreventCrossDomainCalls()
        {
            AppDomain domain = null;
            try
            {
                domain = AppDomain.CreateDomain("LogContextTests", null, AppDomain.CurrentDomain.SetupInformation);

                // ReSharper disable AssignNullToNotNullAttribute
                var callable = (RemotelyCallable)domain.CreateInstanceAndUnwrap(typeof(RemotelyCallable).Assembly.FullName, typeof(RemotelyCallable).FullName);
                // ReSharper restore AssignNullToNotNullAttribute

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

#if APPDOMAIN && REMOTING
        [Fact]
        public void DoesNotThrowOnCrossDomainCallsWhenLeaseExpired()
        {
            RemotingException remotingException = null;

            AppDomain.CurrentDomain.FirstChanceException +=
                (_, e) => remotingException = e.Exception is RemotingException re ? re : remotingException;

            var logger = new LoggerConfiguration().Enrich.FromLogContext().CreateLogger();
            var remote = AppDomain.CreateDomain("Remote", null, AppDomain.CurrentDomain.SetupInformation);

            try
            {
                using (LogContext.PushProperty("Prop", 42))
                {
                    remote.DoCallBack(CallFromRemote);
#pragma warning disable Serilog003
                    logger.Information("Prop = {Prop}");
#pragma warning restore Serilog003
                }
            }
            finally
            {
                AppDomain.Unload(remote);
            }

            Assert.Null(remotingException);

            void CallFromRemote() => Thread.Sleep(200);
        }

        [Fact]
        public async Task DisconnectRemoteObjectsAfterCrossDomainCallsOnDispose()
        {
            var tracker = new InMemoryRemoteObjectTracker();
            TrackingServices.RegisterTrackingHandler(tracker);

            var remote = AppDomain.CreateDomain("Remote", null, AppDomain.CurrentDomain.SetupInformation);

            try
            {
                using (LogContext.PushProperty("Prop1", 42))
                {
                    remote.DoCallBack(CallFromRemote);

                    using (LogContext.PushProperty("Prop2", 24))
                    {
                        remote.DoCallBack(CallFromRemote);
                    }
                }
            }
            finally
            {
                AppDomain.Unload(remote);
            }

            await Task.Delay(200);

            // This is intermittently 2 or 3 (now, 4), depending on the moods of the test runner;
            // I think "at least two" is what we're concerned about, here.
            Assert.InRange(tracker.DisconnectCount, 2, 4);

            void CallFromRemote() { }
        }
#endif

        static async Task TestWithSyncContext(Func<Task> testAction, SynchronizationContext syncContext)
        {
            var prevCtx = SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(syncContext);

            Task t;
            try
            {
                t = testAction();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(prevCtx);
            }

            await t;
        }
    }

#if REMOTING
    class InMemoryRemoteObjectTracker : ITrackingHandler
    {
        public int DisconnectCount { get; set; }

        public void DisconnectedObject(object obj) => DisconnectCount++;

        public void MarshaledObject(object obj, ObjRef or) { }

        public void UnmarshaledObject(object obj, ObjRef or) { }
    }
#endif

#if APPDOMAIN
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

    class ForceNewThreadSyncContext : SynchronizationContext
    {
        public override void Post(SendOrPostCallback d, object state) => new Thread(x => d(x)) { IsBackground = true }.Start(state);
    }
}
