using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Serilog.Core;
using Serilog.Core.Filters;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Tests
{
    [TestFixture]
    public class LoggerConfigurationTests
    {
        class DisposableSink : ILogEventSink, IDisposable
        {
            public bool IsDisposed { get; set; }

            public void Emit(LogEvent logEvent) { }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        [Test]
        public void DisposableSinksAreDisposedAlongWithRootLogger()
        {
            var sink = new DisposableSink();
            var logger = (IDisposable) new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Dispose(); 
            Assert.IsTrue(sink.IsDisposed);
        }

        [Test]
        public void DisposableSinksAreNotDisposedAlongWithContextualLoggers()
        {
            var sink = new DisposableSink();
            var logger = (IDisposable) new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .CreateLogger()
                .ForContext<LoggerConfigurationTests>();

            logger.Dispose();
            Assert.IsFalse(sink.IsDisposed);
        }

        [Test]
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
            Assert.AreEqual(1, events.Count);
            Assert.That(events.Contains(included));
        }

// ReSharper disable UnusedMember.Local, UnusedAutoPropertyAccessor.Local
        class AB { public int A { get; set; } public int B { get; set; } }
// ReSharper restore UnusedAutoPropertyAccessor.Local, UnusedMember.Local

        [Test]
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
            Assert.IsInstanceOf<ScalarValue>(prop);
        }

        [Test]
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
            Assert.AreEqual("C", c.Name);
        }

        [Test]
        public void WritingToALoggerWritesToSubLogger()
        {
            var eventReceived = false;

            var logger = new LoggerConfiguration()
                .WriteTo.Logger(l => l
                    .WriteTo.Sink(new DelegatingSink(e => eventReceived = true)))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.That(eventReceived);
        }

        [Test]
        public void SubLoggerRestrictsFilter()
        {
            var eventReceived = false;

            var logger = new LoggerConfiguration()
                .WriteTo.Logger(l => l
                    .MinimumLevel.Fatal()
                    .WriteTo.Sink(new DelegatingSink(e => eventReceived = true)))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.That(!eventReceived);
        }

        [Test]
        public void EnrichersExecuteInConfigurationOrder()
        {
            var property = Some.LogEventProperty();
            var enrichedPropertySeen = false;

            var logger = new LoggerConfiguration()
                .Enrich.With(new DelegatingEnricher((e, f) => e.AddPropertyIfAbsent(property)))
                .Enrich.With(new DelegatingEnricher((e, f) => enrichedPropertySeen = e.Properties.ContainsKey(property.Name)))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.That(enrichedPropertySeen);
        }

        [Test]
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

            Assert.That(xs, Is.StringContaining("C"));
            Assert.That(xs, Is.Not.StringContaining("D"));
        }

        [Test]
        public void ResetClearsAllConfiguration()
        {
            var enricherUsed = false;
            var filterUsed = false;
            var sink1Used = false;

            var events = new List<LogEvent>();
            var filter = new DelegateFilter(e => { filterUsed = true; return true; });
            var sink1 = new DelegatingSink(e => sink1Used = true);
            var sink2 = new DelegatingSink(events.Add);

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink1)
                .Enrich.With(new DelegatingEnricher((e, f) => enricherUsed = true))
                .Filter.With(filter)
                .Destructure.AsScalar<AB>()
                .MinimumLevel.Error()
                .Reset()
                .WriteTo.Sink(sink2)
                .CreateLogger();

            logger.Information("{@AB}", new AB());

            Assert.AreEqual(1, events.Count);

            var abProp = events.First().Properties["AB"];
            Assert.IsInstanceOf<StructureValue>(abProp);

            Assert.False(enricherUsed);
            Assert.False(filterUsed);
            Assert.False(sink1Used);
        }

        [Test]
        public void ClearingSinksRemovesAllSinks()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .ClearSinks()
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.IsEmpty(events);
        }

        [Test]
        public void ClearingEnrichersRemovesAllEnrichers()
        {
            var enricherUsed = false;

            var logger = new LoggerConfiguration()
                .Enrich.With(new DelegatingEnricher((e, f) => enricherUsed = true))
                .ClearEnrichers()
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.False(enricherUsed);
        }

        [Test]
        public void ClearingFiltersRemovesAllFilters()
        {
            var @event = Some.InformationEvent();

            var filter = new DelegateFilter(e => false);
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .Filter.With(filter)
                .ClearFilters()
                .CreateLogger();

            logger.Write(@event);

            Assert.AreEqual(1, events.Count);
            Assert.That(events.Contains(@event));
        }

        // ReSharper disable UnusedMember.Local, UnusedAutoPropertyAccessor.Local
        class CD { public int C { get; set; } public int D { get; set; } }
        // ReSharper restore UnusedAutoPropertyAccessor.Local, UnusedMember.Local

        [Test]
        public void ClearingDesctructureRemovesAllConfig()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .Destructure.AsScalar<AB>()
                .Destructure.ByTransforming<CD>(cd => new { E = cd.D })
                .ClearDestructureConfig()
                .CreateLogger();

            logger.Information("{@AB} {@CD}", new AB(), new CD());

            var ev = events.Single();
            var abProp = ev.Properties["AB"];
            Assert.IsInstanceOf<StructureValue>(abProp);

            var cdProp = (StructureValue) ev.Properties["CD"];
            Assert.AreEqual(2, cdProp.Properties.Count);
            Assert.AreEqual("C", cdProp.Properties[0].Name);
            Assert.AreEqual("D", cdProp.Properties[1].Name);
        }
    }
}
