using Serilog.Core;
using Serilog.Core.Enrichers;
using Serilog.Core.Pipeline;
using Serilog.Events;
using Serilog.Tests.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using TestDummies;
using Xunit;

#pragma warning disable Serilog004 // Constant MessageTemplate verifier
#pragma warning disable Serilog003 // Property binding verifier

namespace Serilog.Tests.Core
{
    public class LoggerTests
    {
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
                .Enrich.With(new DelegatingEnricher((le, pf) =>
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
            var e = DelegatingSink.GetLogEvent(l => l.Error("message", new object()));
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
            levelSwitch.MinimumLevel = LogEventLevel.Error;

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

            Assert.True(log.BindMessageTemplate("Hello, {Name}!", new object[] { "World" }, out var template, out var properties));

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
            lock (new object())
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

            log.Write(LogEventLevel.Warning, new Exception("warn"), "emit some {prop} with {values}", "message", new[] { 1, 2, 3 });

            Assert.Single(collectingSink.Events);
            Assert.Equal(LogEventLevel.Warning, collectingSink.SingleEvent.Level);
            Assert.Equal("warn", collectingSink.SingleEvent.Exception?.Message);
            Assert.Equal("string", collectingSink.SingleEvent.Properties["type"].LiteralValue());
            Assert.Equal("message", collectingSink.SingleEvent.Properties["prop"].LiteralValue());
            Assert.Equal(42, collectingSink.SingleEvent.Properties["number"].LiteralValue());
            Assert.Equal(
                expected: new SequenceValue(new[] { new ScalarValue(1), new ScalarValue(2), new ScalarValue(3) }),
                actual: (collectingSink.SingleEvent.Properties["values"] as SequenceValue),
                comparer: new LogEventPropertyValueComparer());

            levelSwitch.MinimumLevel = LogEventLevel.Fatal;
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
    }
}
