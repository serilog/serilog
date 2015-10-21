using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Serilog.Core;
using Serilog.Core.Filters;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Tests
{
    public class LoggerConfigurationTests
    {
        class DisposableSink : ILogEventSink, IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Emit(LogEvent logEvent) { }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        [Fact]
        public void DisposableSinksAreDisposedAlongWithRootLogger()
        {
            var sink = new DisposableSink();
            var logger = (IDisposable) new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Dispose(); 
            Assert.True(sink.IsDisposed);
        }

        [Fact]
        public void DisposableSinksAreNotDisposedAlongWithContextualLoggers()
        {
            var sink = new DisposableSink();
            var logger = (IDisposable) new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .CreateLogger()
                .ForContext<LoggerConfigurationTests>();

            logger.Dispose();
            Assert.False(sink.IsDisposed);
        }

        [Fact]
        public void AFilterPreventsMatchedEventsFromPassingToTheSink()
        {
            var excluded = Some.InformationEvent();
            var included = Some.InformationEvent();

            var filter = new DelegateFilter(e => e.MessageTemplate != excluded.MessageTemplate);
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);
            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .Filter.With(filter)
                .CreateLogger();
            logger.Write(included);
            logger.Write(excluded);
            Assert.Equal(1, events.Count);
            Assert.True(events.Contains(included));
        }

// ReSharper disable UnusedMember.Local, UnusedAutoPropertyAccessor.Local
        class AB { public int A { get; set; } public int B { get; set; } }
// ReSharper restore UnusedAutoPropertyAccessor.Local, UnusedMember.Local

        [Fact]
        public void SpecifyingThatATypeIsScalarCausesItToBeLoggedAsScalarEvenWhenDestructuring()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);
            
            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .Destructure.AsScalar(typeof(AB))
                .CreateLogger();

            logger.Information("{@AB}", new AB());

            var ev = events.Single();
            var prop = ev.Properties["AB"];
            Assert.IsType<ScalarValue>(prop);
        }

        [Fact]
        public void TransformationsAreAppliedToEventProperties()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .Destructure.ByTransforming<AB>(ab => new { C = ab.B })
                .CreateLogger();

            logger.Information("{@AB}", new AB());

            var ev = events.Single();
            var prop = ev.Properties["AB"];
            var sv = (StructureValue)prop;
            var c = sv.Properties.Single();
            Assert.Equal("C", c.Name);
        }

        [Fact]
        public void WritingToALoggerWritesToSubLogger()
        {
            var eventReceived = false;

            var logger = new LoggerConfiguration()
                .WriteTo.Logger(l => l
                    .WriteTo.Sink(new DelegatingSink(e => eventReceived = true)))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.True(eventReceived);
        }

        [Fact]
        public void SubLoggerRestrictsFilter()
        {
            var eventReceived = false;

            var logger = new LoggerConfiguration()
                .WriteTo.Logger(l => l
                    .MinimumLevel.Fatal()
                    .WriteTo.Sink(new DelegatingSink(e => eventReceived = true)))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.True(!eventReceived);
        }

        [Fact]
        public void EnrichersExecuteInConfigurationOrder()
        {
            var property = Some.LogEventProperty();
            var enrichedPropertySeen = false;

            var logger = new LoggerConfiguration()
                .Enrich.With(new DelegatingEnricher((e, f) => e.AddPropertyIfAbsent(property)))
                .Enrich.With(new DelegatingEnricher((e, f) => enrichedPropertySeen = e.Properties.ContainsKey(property.Name)))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.True(enrichedPropertySeen);
        }

        [Fact]
        public void MaximumDestructuringDepthIsEffective()
        {
            var x = new { A = new { B = new { C = new { D = "F" } } } };

            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .Destructure.ToMaximumDepth(3)
                .CreateLogger();

            log.Information("{@X}", x);
            var xs = evt.Properties["X"].ToString();

            Assert.Contains("C", xs);
            Assert.DoesNotContain(xs, "D");
        }

        [Fact]
        public void DynamicallySwitchingSinkRestrictsOutput()
        {
            var eventsReceived = 0;
            var levelSwitch = new LoggingLevelSwitch();

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(
                    new DelegatingSink(e => eventsReceived++),
                    levelSwitch: levelSwitch)
                .CreateLogger();

            logger.Write(Some.InformationEvent());
            levelSwitch.MinimumLevel = LogEventLevel.Warning;
            logger.Write(Some.InformationEvent());
            levelSwitch.MinimumLevel = LogEventLevel.Verbose;
            logger.Write(Some.InformationEvent());

            Assert.Equal(2, eventsReceived);
        }

        [Fact]
        public void LevelSwitchTakesPrecedenceOverMinimumLevel()
        {
            var sink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink, LogEventLevel.Fatal, new LoggingLevelSwitch())
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.Equal(1, sink.Events.Count);
        }
    }
}
