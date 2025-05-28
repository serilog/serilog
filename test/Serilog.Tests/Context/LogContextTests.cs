namespace Serilog.Tests.Context;

public class LogContextTests
{
    [Fact]
    public void PushedSpanPropertiesAreAvailableToLoggers()
    {
        LogEvent? lastEvent = null;

        var log = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
            .CreateLogger();
        ReadOnlySpan<ILogEventEnricher> enrichers = [new PropertyEnricher("A", 1), new PropertyEnricher("B", 2)];
        using (LogContext.Push(enrichers))
        {
            log.Write(Some.InformationEvent());
            Assert.NotNull(lastEvent);
            Assert.Equal(1, lastEvent!.Properties["A"].LiteralValue());
            Assert.Equal(2, lastEvent.Properties["B"].LiteralValue());
        }
    }

    [Fact]
    public void PushedArrayPropertiesAreAvailableToLoggers()
    {
        LogEvent? lastEvent = null;

        var log = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
            .CreateLogger();
        PropertyEnricher[] enrichers = [new("A", 1), new("B", 2)];
        using (LogContext.Push(enrichers))
        {
            log.Write(Some.InformationEvent());
            Assert.NotNull(lastEvent);
            Assert.Equal(1, lastEvent!.Properties["A"].LiteralValue());
            Assert.Equal(2, lastEvent.Properties["B"].LiteralValue());
        }
    }

    [Fact]
    public void PushedEnumerablePropertiesAreAvailableToLoggers()
    {
        LogEvent? lastEvent = null;

        var log = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
            .CreateLogger();
        IEnumerable<PropertyEnricher> enrichers = [new("A", 1), new("B", 2)];
        using (LogContext.Push(enrichers))
        {
            log.Write(Some.InformationEvent());
            Assert.NotNull(lastEvent);
            Assert.Equal(1, lastEvent!.Properties["A"].LiteralValue());
            Assert.Equal(2, lastEvent.Properties["B"].LiteralValue());
        }
    }

    [Fact]
    public void PushedPropertiesAreAvailableToLoggers()
    {
        LogEvent? lastEvent = null;

        var log = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
            .CreateLogger();

        using (LogContext.PushProperty("A", 1))
        using (LogContext.Push(new PropertyEnricher("B", 2)))
        using (LogContext.Push(new PropertyEnricher("C", 3), new PropertyEnricher("D", 4))) // Different overload
        {
            log.Write(Some.InformationEvent());
            Assert.NotNull(lastEvent);
            Assert.Equal(1, lastEvent!.Properties["A"].LiteralValue());
            Assert.Equal(2, lastEvent.Properties["B"].LiteralValue());
            Assert.Equal(3, lastEvent.Properties["C"].LiteralValue());
            Assert.Equal(4, lastEvent.Properties["D"].LiteralValue());
        }
    }

    [Fact]
    public void LogContextCanBeCloned()
    {
        LogEvent? lastEvent = null;

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
            Assert.NotNull(lastEvent);
            Assert.Equal(1, lastEvent!.Properties["A"].LiteralValue());
        }
    }

    [Fact]
    public void ClonedLogContextCanSharedAcrossThreads()
    {
        LogEvent? lastEvent = null;

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

        Assert.NotNull(lastEvent);
        Assert.Equal(1, lastEvent!.Properties["A"].LiteralValue());
    }

    [Fact]
    public void MoreNestedPropertiesOverrideLessNestedOnes()
    {
        LogEvent? lastEvent = null;

        var log = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
            .CreateLogger();

        using (LogContext.PushProperty("A", 1))
        {
            log.Write(Some.InformationEvent());
            Assert.NotNull(lastEvent);
            Assert.Equal(1, lastEvent!.Properties["A"].LiteralValue());

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
        LogEvent? lastEvent = null;

        var log = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
            .CreateLogger();

        using (LogContext.Push(new PropertyEnricher("A1", 1), new PropertyEnricher("A2", 2)))
        {
            log.Write(Some.InformationEvent());
            Assert.NotNull(lastEvent);
            Assert.Equal(1, lastEvent!.Properties["A1"].LiteralValue());
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
                LogEvent? lastEvent = null;

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
                    Assert.NotNull(lastEvent);
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
        LogEvent? lastEvent = null;

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
            }, TestContext.Current.CancellationToken);

            Assert.NotNull(lastEvent);
            Assert.Empty(lastEvent!.Properties);

            // Reset should only work for current async scope, outside of it previous Context
            // instance should be available again.
            log.Write(Some.InformationEvent());
            Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());
        }
    }

    [Fact]
    public async Task ContextEnrichersCanBeTemporarilyCleared()
    {
        LogEvent? lastEvent = null;

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
                }, TestContext.Current.CancellationToken);

                Assert.NotNull(lastEvent);
                Assert.Empty(lastEvent!.Properties);
            }

            // Suspend should only work for scope of using. After calling Dispose all enrichers
            // should be restored.
            log.Write(Some.InformationEvent());
            Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());
        }
    }

#if TEST_FEATURE_APPDOMAIN
    // Must not actually try to pass context across domains,
    // since user property types may not be serializable.
    [Fact]
    public void DoesNotPreventCrossDomainCalls()
    {
        AppDomain? domain = null;
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

#if FEATURE_REMOTING
class InMemoryRemoteObjectTracker : ITrackingHandler
{
    public int DisconnectCount { get; set; }

    public void DisconnectedObject(object obj) => DisconnectCount++;

    public void MarshaledObject(object obj, ObjRef or) { }

    public void UnmarshaledObject(object obj, ObjRef or) { }
}
#endif

#if TEST_FEATURE_APPDOMAIN
public class RemotelyCallable : MarshalByRefObject
{
    public bool IsCallable()
    {
        LogEvent? lastEvent = null;

        var log = new LoggerConfiguration()
            .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
            .Enrich.FromLogContext()
            .CreateLogger();

        using (LogContext.PushProperty("Number", 42))
            log.Information("Hello");

        Assert.NotNull(lastEvent);
        return 42.Equals(lastEvent!.Properties["Number"].LiteralValue());
    }
}
#endif

class ForceNewThreadSyncContext : SynchronizationContext
{
    public override void Post(SendOrPostCallback d, object? state) => new Thread(x => d(x)) { IsBackground = true }.Start(state);
}
