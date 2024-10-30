using System.Diagnostics;

#pragma warning disable Serilog004 // Constant MessageTemplate verifier
#pragma warning disable Serilog003 // Property binding verifier

namespace Serilog.Tests.Core;

public class LoggerTests
{
    static LoggerTests()
    {
        // This is necessary to force activity id allocation on .NET Framework and early .NET Core versions. When this isn't
        // done, log events end up carrying null trace and span ids (which is fine).
        Activity.DefaultIdFormat = ActivityIdFormat.W3C;
        Activity.ForceDefaultIdFormat = true;
    }

    [Theory]
    [InlineData(typeof(Logger))]
#if FEATURE_DEFAULT_INTERFACE
    [InlineData(typeof(DelegatingLogger))]
#endif
    public void AnExceptionThrownByAnEnricherIsNotPropagated(Type loggerType)
    {
        var thrown = false;

        var l = CreateLogger(loggerType, lc => lc
            .WriteTo.Sink(new StringSink())
            .Enrich.With(new DelegatingEnricher((_, _) =>
            {
                thrown = true;
                throw new Exception("No go, pal.");
            })));

        l.Information(Some.String());

        Assert.True(thrown);
    }

    [Fact]
    public void AContextualLoggerAddsTheSourceTypeName()
    {
        var evt = DelegatingSink.GetLogEvent(l => l.ForContext<LoggerTests>()
            .Information(Some.String()));

        var lv = evt.Properties[Constants.SourceContextPropertyName].LiteralValue();
        Assert.Equal(typeof(LoggerTests).FullName, lv);
    }

    [Fact]
    public void PropertiesInANestedContextOverrideParentContextValues()
    {
        var name = Some.String();
        var v1 = Some.Int();
        var v2 = Some.Int();
        var evt = DelegatingSink.GetLogEvent(l => l.ForContext(name, v1)
            .ForContext(name, v2)
            .Write(Some.InformationEvent()));

        var pActual = evt.Properties[name];
        Assert.Equal(v2, pActual.LiteralValue());
    }

    [Fact]
    public void ParametersForAnEmptyTemplateAreIgnored()
    {
        // ReSharper disable StructuredMessageTemplateProblem
        var e = DelegatingSink.GetLogEvent(l => l.Error("message", new object()));
        // ReSharper restore StructuredMessageTemplateProblem
        Assert.Equal("message", e.RenderMessage());
    }

    [Theory]
    [InlineData(typeof(Logger))]
#if FEATURE_DEFAULT_INTERFACE
    [InlineData(typeof(DelegatingLogger))]
#endif
    public void LoggingLevelSwitchDynamicallyChangesLevel(Type loggerType)
    {
        var events = new List<LogEvent>();
        var sink = new DelegatingSink(events.Add);

        var levelSwitch = new LoggingLevelSwitch();

        var log = CreateLogger(loggerType, lc => lc
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.Sink(sink))
            .ForContext<LoggerTests>();

        log.Debug("Suppressed");
        log.Information("Emitted");
        log.Warning("Emitted");

        // Change the level
        levelSwitch.MinimumLevel = Error;

        log.Warning("Suppressed");
        log.Error("Emitted");
        log.Fatal("Emitted");

        Assert.Equal(4, events.Count);
        Assert.True(events.All(evt => evt.RenderMessage() == "Emitted"));
    }

    [Theory]
    [InlineData(typeof(Logger))]
#if FEATURE_DEFAULT_INTERFACE
    [InlineData(typeof(DelegatingLogger))]
#endif
    public void MessageTemplatesCanBeBound(Type loggerType)
    {
        var log = CreateLogger(loggerType, lc => lc);

        // ReSharper disable StructuredMessageTemplateProblem
        Assert.True(log.BindMessageTemplate("Hello, {Name}!", new object[] { "World" }, out var template, out var properties));
        // ReSharper restore StructuredMessageTemplateProblem

        Assert.Equal("Hello, {Name}!", template.Text);
        Assert.Equal("World", properties.Single().Value.LiteralValue());
    }

    [Theory]
    [InlineData(typeof(Logger))]
#if FEATURE_DEFAULT_INTERFACE
    [InlineData(typeof(DelegatingLogger))]
#endif
    public void PropertiesCanBeBound(Type loggerType)
    {
        var log = CreateLogger(loggerType, lc => lc);

        Assert.True(log.BindProperty("Name", "World", false, out var property));

        Assert.Equal("Name", property.Name);
        Assert.Equal("World", property.Value.LiteralValue());
    }

    [Fact]
    public void TheNoneLoggerIsSilent()
    {
        Assert.IsType<SilentLogger>(Logger.None);
    }

    [Fact]
    public void TheNoneLoggerIsAConstant()
    {
        var firstCall = Logger.None;
        var secondCall = Logger.None;
        Assert.Equal(firstCall, secondCall);
    }

    [Fact]
    public void TheNoneLoggerIsSingleton()
    {
        lock (this)
        {
            Log.CloseAndFlush();
            Assert.Same(Log.Logger, Logger.None);
        }
    }

    static ILogger CreateLogger(Type loggerType, Func<LoggerConfiguration, LoggerConfiguration> configureLogger)
    {
        var lc = new LoggerConfiguration();

        return loggerType switch
        {
            _ when loggerType == typeof(Logger) => configureLogger(lc).CreateLogger(),
#if FEATURE_DEFAULT_INTERFACE
            _ when loggerType == typeof(DelegatingLogger) => new DelegatingLogger(configureLogger(lc).CreateLogger()),
#endif
            _ => throw new NotSupportedException()
        };
    }

#if FEATURE_DEFAULT_INTERFACE
    [Fact]
    public void DelegatingLoggerShouldDelegateCallsToInnerLogger()
    {
        var collectingSink = new CollectingSink();
        var levelSwitch = new LoggingLevelSwitch();

        var innerLogger =
            new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.Sink(collectingSink)
                .CreateLogger();

        var delegatingLogger = new DelegatingLogger(innerLogger);

        var log = ((ILogger)delegatingLogger).ForContext("number", 42)
                                             .ForContext(new PropertyEnricher("type", "string"));

        log.Debug("suppressed");
        Assert.Empty(collectingSink.Events);

        log.Write(Warning, new Exception("warn"), "emit some {prop} with {values}", "message", new[] { 1, 2, 3 });

        Assert.Single(collectingSink.Events);
        Assert.Equal(Warning, collectingSink.SingleEvent.Level);
        Assert.Equal("warn", collectingSink.SingleEvent.Exception?.Message);
        Assert.Equal("string", collectingSink.SingleEvent.Properties["type"].LiteralValue());
        Assert.Equal("message", collectingSink.SingleEvent.Properties["prop"].LiteralValue());
        Assert.Equal(42, collectingSink.SingleEvent.Properties["number"].LiteralValue());
        Assert.Equal(
            // ReSharper disable once CoVariantArrayConversion
            expected: new SequenceValue(new[] { new ScalarValue(1), new ScalarValue(2), new ScalarValue(3) }),
            actual: (SequenceValue)collectingSink.SingleEvent.Properties["values"],
            comparer: new LogEventPropertyValueComparer());

        levelSwitch.MinimumLevel = Fatal;
        collectingSink.Events.Clear();

        log.Error("error");
        Assert.Empty(collectingSink.Events);

        innerLogger.Dispose();
        Assert.False(delegatingLogger.Disposed);
    }
#endif

    [Fact]
    public void ASingleSinkIsDisposedWhenLoggerIsDisposed()
    {
        var sink = new DisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Sink(sink)
            .CreateLogger();

        log.Dispose();

        Assert.True(sink.IsDisposed);
    }

    [Fact]
    public void AggregatedSinksAreDisposedWhenLoggerIsDisposed()
    {
        var sinkA = new DisposeTrackingSink();
        var sinkB = new DisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Sink(sinkA)
            .WriteTo.Sink(sinkB)
            .CreateLogger();

        log.Dispose();

        Assert.True(sinkA.IsDisposed);
        Assert.True(sinkB.IsDisposed);
    }

    [Fact]
    public void WrappedSinksAreDisposedWhenLoggerIsDisposed()
    {
        var sink = new DisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Dummy(wrapped => wrapped.Sink(sink))
            .CreateLogger();

        log.Dispose();

        Assert.True(sink.IsDisposed);
    }

    [Fact]
    public void WrappedAggregatedSinksAreDisposedWhenLoggerIsDisposed()
    {
        var sinkA = new DisposeTrackingSink();
        var sinkB = new DisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Dummy(wrapped => wrapped.Sink(sinkA).WriteTo.Sink(sinkB))
            .CreateLogger();

        log.Dispose();

        Assert.True(sinkA.IsDisposed);
        Assert.True(sinkB.IsDisposed);
    }

    [Fact]
    public void CurrentActivityIsCapturedAtLogEventCreation()
    {
        using var listener = new ActivityListener();
        listener.ShouldListenTo = _ => true;
        listener.SampleUsingParentId = (ref ActivityCreationOptions<string> _) => ActivitySamplingResult.AllData;
        listener.Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData;
        ActivitySource.AddActivityListener(listener);

        using var source = new ActivitySource(Some.String());
        using var activity = source.StartActivity(Some.String());
        Assert.NotNull(activity);
        Assert.NotEqual("00000000000000000000000000000000", activity.TraceId.ToHexString());
        Assert.NotEqual("0000000000000000", activity.SpanId.ToHexString());

        var sink = new CollectingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Sink(sink)
            .CreateLogger();

        log.Information("Hello, world!");

        var single = sink.SingleEvent;

        Assert.Equal(activity.TraceId, single.TraceId);
        Assert.Equal(activity.SpanId, single.SpanId);
    }

    [Fact]
    public void NullMessageTemplateParametersDoNotBreakBinding()
    {
        var log = new LoggerConfiguration().WriteTo.Sink(new CollectingSink()).CreateLogger();

        // ReSharper disable RedundantCast
        // ReSharper disable StructuredMessageTemplateProblem
        log.Information("test", (object?[]?) null);
        log.Write(Warning, (Exception?)null, "test", (object?[]?)null);
        log.BindMessageTemplate("test", (object?[]?)null, out _, out _);
        // ReSharper restore RedundantCast
        // ReSharper restore StructuredMessageTemplateProblem
    }

    // https://github.com/serilog/serilog/issues/2019
    [Fact]
    public void Two_Dimensional_Array_Should_Be_Logger_As_Sequence()
    {
        var evt = DelegatingSink.GetLogEvent(l =>
        {
            var a = new object[3, 2] { { "a", "b" }, { "c", "d" }, { "e", "f" } };
            l.Error("{@Value}", a);
        });

        Assert.Equal(1, evt.Properties.Count);
        var arr = (SequenceValue)evt.Properties["Value"];
        Assert.Equal(3, arr.Elements.Count);
        Assert.Equal("[[a,b],[c,d],[e,f]]", arr.LiteralValue());
    }

    // https://github.com/serilog/serilog/issues/2019
    [Fact]
    public void Three_Dimensional_Array_Should_Be_Logger_As_Sequence()
    {
        var evt = DelegatingSink.GetLogEvent(l =>
        {
            var a = new object[3, 2, 1] { { { "a" }, { "b" } }, { { "c" }, { "d" } }, { { "e" }, { "f" } } };
            l.Error("{@Value}", a);
        });

        Assert.Equal(1, evt.Properties.Count);
        var arr = (SequenceValue)evt.Properties["Value"];
        Assert.Equal(3, arr.Elements.Count);
        Assert.Equal("[[[a],[b]],[[c],[d]],[[e],[f]]]", arr.LiteralValue());
    }

    // https://github.com/serilog/serilog/issues/2019
    [Fact]
    public void Four_Dimensional_Array_Should_Be_Logged_As_Sequence()
    {
        var evt = DelegatingSink.GetLogEvent(l =>
        {
            var a = new object[2, 2, 2, 2]
            {
                {
                    {
                        { "a", "b" },
                        { "c", "d" }
                    },
                    {
                        { "e", "f" },
                        { "g", "h" }
                    }
                },
                {
                    {
                        { "i", "j" },
                        { "k", "l" }
                    },
                    {
                        { "m", "n" },
                        { "o", "p" }
                    }
                }
            };
            l.Error("{@Value}", a);
        });

        Assert.Equal(1, evt.Properties.Count);
        var arr = (SequenceValue)evt.Properties["Value"];
        Assert.Equal(2, arr.Elements.Count);
        Assert.Equal("[[[[a,b],[c,d]],[[e,f],[g,h]]],[[[i,j],[k,l]],[[m,n],[o,p]]]]", arr.LiteralValue());
    }

    // https://github.com/serilog/serilog/issues/2019
    [Fact]
    public void Empty_Multi_Dimensional_Arrays_Should_Be_Serialized() // Same behaviour as Newtonsoft.Json
    {
        var evt = DelegatingSink.GetLogEvent(l =>
        {
            var a = new int[0, 0];
            var b = new int[0, 1];
            var c = new int[1, 0];
            l.Error("{@Value1} {@Value2} {@Value3}", a, b, c);
        });

        Assert.Equal(3, evt.Properties.Count);
        var arr1 = (SequenceValue)evt.Properties["Value1"];
        Assert.Equal("[]", arr1.LiteralValue());
        var arr2 = (SequenceValue)evt.Properties["Value2"];
        Assert.Equal("[]", arr2.LiteralValue());
        var arr3 = (SequenceValue)evt.Properties["Value3"];
        Assert.Equal("[[]]", arr3.LiteralValue());
    }

    // https://github.com/serilog/serilog/issues/2019
    [Fact]
    public void JaggedArray_Should_Respect_MaximumCollectionCount()
    {
        // Arrange
        var collectingSink = new CollectingSink();
        var log = new LoggerConfiguration()
            .Destructure.ToMaximumCollectionCount(2)
            .WriteTo.Sink(collectingSink)
            .CreateLogger();

        var array = new int[3][]
        {
            new int[] { 1, 2, 3 },
            new int[] { 4, 5, 6 },
            new int[] { 7, 8, 9, 10 }
        };

        // Act
        log.Information("{@Array}", array);

        // Assert
        var logEvent = collectingSink.Events.Single();
        var loggedArray = (SequenceValue)logEvent.Properties["Array"];

        // Outer sequence should have at most 2 elements (rows)
        Assert.Equal(2, loggedArray.Elements.Count);

        // Each inner sequence (row) should have at most 2 elements (columns)
        foreach (var element in loggedArray.Elements)
        {
            var row = (SequenceValue)element;
            Assert.Equal(2, row.Elements.Count);
        }

        // Check the actual logged value
        Assert.Equal("[[1,2],[4,5]]", loggedArray.LiteralValue());
    }

    // https://github.com/serilog/serilog/issues/2019
    [Fact]
    public void MultiDimensionalArray_Should_Respect_MaximumCollectionCount()
    {
        // Arrange
        var collectingSink = new CollectingSink();
        var log = new LoggerConfiguration()
            .Destructure.ToMaximumCollectionCount(2)
            .WriteTo.Sink(collectingSink)
            .CreateLogger();

        var array = new int[3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        // Act
        log.Information("{@Array}", array);

        // Assert
        var logEvent = collectingSink.Events.Single();
        var loggedArray = (SequenceValue)logEvent.Properties["Array"];

        // Outer sequence should have at most 2 elements (rows)
        Assert.Equal(2, loggedArray.Elements.Count);

        // Each inner sequence (row) should have at most 2 elements (columns)
        foreach (var element in loggedArray.Elements)
        {
            var row = (SequenceValue)element;
            Assert.Equal(2, row.Elements.Count);
        }

        // Check the actual logged value
        Assert.Equal("[[1,2],[4,5]]", loggedArray.LiteralValue());
    }

#if FEATURE_ASYNCDISPOSABLE

    [Fact]
    public async Task ASingleSinkIsDisposedWhenLoggerIsDisposedAsync()
    {
        var sink = new DisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Sink(sink)
            .CreateLogger();

        await log.DisposeAsync();

        Assert.True(sink.IsDisposed);
    }

    [Fact]
    public void ASingleAsyncSinkIsDisposedWhenLoggerIsDisposed()
    {
        var sink = new DisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Sink(sink)
            .CreateLogger();

        log.Dispose();

        Assert.True(sink.IsDisposed);
    }

    [Fact]
    public async Task ASingleAsyncSinkIsDisposedWhenLoggerIsDisposedAsync()
    {
        var sink = new AsyncDisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Sink(sink)
            .CreateLogger();

        await log.DisposeAsync();

        Assert.True(sink.IsDisposedAsync);
    }

    [Fact]
    public async Task AggregatedSinksAreDisposedWhenLoggerIsDisposedAsync()
    {
        var sinkA = new DisposeTrackingSink();
        var sinkB = new DisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Sink(sinkA)
            .WriteTo.Sink(sinkB)
            .CreateLogger();

        await log.DisposeAsync();

        Assert.True(sinkA.IsDisposed);
        Assert.True(sinkB.IsDisposed);
    }

    [Fact]
    public async Task AggregatedAsyncSinksAreDisposedWhenLoggerIsDisposedAsync()
    {
        var sinkA = new DisposeTrackingSink();
        var sinkB = new AsyncDisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Sink(sinkA)
            .WriteTo.Sink(sinkB)
            .CreateLogger();

        await log.DisposeAsync();

        Assert.True(sinkA.IsDisposed);
        Assert.True(sinkB.IsDisposedAsync);
    }

    [Fact]
    public async Task WrappedSinksAreDisposedWhenLoggerIsDisposedAsync()
    {
        var sink = new DisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Dummy(wrapped => wrapped.Sink(sink))
            .CreateLogger();

        await log.DisposeAsync();

        Assert.True(sink.IsDisposed);
    }

    [Fact]
    public async Task WrappedAsyncSinksAreDisposedWhenLoggerIsDisposedAsync()
    {
        var sink = new AsyncDisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Dummy(wrapped => wrapped.Sink(sink))
            .CreateLogger();

        await log.DisposeAsync();

        Assert.True(sink.IsDisposedAsync);
    }

    [Fact]
    public async Task WrappedAggregatedAsyncSinksAreDisposedWhenLoggerIsDisposedAsync()
    {
        var sinkA = new DisposeTrackingSink();
        var sinkB = new AsyncDisposeTrackingSink();
        var log = new LoggerConfiguration()
            .WriteTo.Dummy(wrapped => wrapped.Sink(sinkA).WriteTo.Sink(sinkB))
            .CreateLogger();

        await log.DisposeAsync();

        Assert.True(sinkA.IsDisposed);
        Assert.True(sinkB.IsDisposedAsync);
    }

    [Fact]
    public async Task RestrictedSinksAreDisposedAsyncWhenLoggerIsDisposedAsync()
    {
        var sink = new AsyncDisposeTrackingSink();
        var logger = new LoggerConfiguration()
            .WriteTo.Sink(sink, Error)
            .CreateLogger();

        await logger.DisposeAsync();

        Assert.True(sink.IsDisposedAsync);
    }

#endif
}
