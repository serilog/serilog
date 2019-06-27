using Serilog.Core;
using Serilog.Core.Filters;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Tests.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serilog.Configuration;
using Serilog.Core.Enrichers;
using TestDummies;
using Xunit;
// ReSharper disable PossibleNullReferenceException

namespace Serilog.Tests
{
    public class LoggerConfigurationTests
    {
        class DisposableSink : ILogEventSink, IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Emit(LogEvent logEvent)
            {
            }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        [Fact]
        public void LoggerShouldNotReferenceToItsConfigurationAfterBeingCreated()
        {
            var loggerConfiguration = new LoggerConfiguration();
            var wr = new WeakReference(loggerConfiguration);
            var logger = loggerConfiguration.CreateLogger();

            GC.Collect();

            Assert.False(wr.IsAlive);
            GC.KeepAlive(logger);
        }

        [Fact]
        public void CreateLoggerThrowsIfCalledMoreThanOnce()
        {
            var loggerConfiguration = new LoggerConfiguration();
            loggerConfiguration.CreateLogger();
            Assert.Throws<InvalidOperationException>(() => loggerConfiguration.CreateLogger());
        }

        [Fact]
        public void DisposableSinksAreDisposedAlongWithRootLogger()
        {
            var sink = new DisposableSink();
            var logger = (IDisposable)new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Dispose();
            Assert.True(sink.IsDisposed);
        }

        [Fact]
        public void DisposableSinksAreNotDisposedAlongWithContextualLoggers()
        {
            var sink = new DisposableSink();
            var logger = (IDisposable)new LoggerConfiguration()
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
            Assert.Single(events);
            Assert.Contains(included, events);
        }

        // ReSharper disable UnusedMember.Local, UnusedAutoPropertyAccessor.Local
        class AB
        {
            public int A { get; set; }
            public int B { get; set; }
        }

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
        public void DestructuringSystemTypeGivesScalarByDefault()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .CreateLogger();

            var thisType = GetType();
            logger.Information("{@thisType}", thisType);

            var ev = events.Single();
            var prop = ev.Properties["thisType"];
            var sv = Assert.IsAssignableFrom<ScalarValue>(prop);
            Assert.Equal(thisType, sv.LiteralValue());
        }

        class ProjectedDestructuringPolicy : IDestructuringPolicy
        {
            readonly Func<Type, bool> _canApply;
            readonly Func<object, object> _projection;

            public ProjectedDestructuringPolicy(Func<Type, bool> canApply, Func<object, object> projection)
            {
                _canApply = canApply ?? throw new ArgumentNullException(nameof(canApply));
                _projection = projection ?? throw new ArgumentNullException(nameof(projection));
            }

            public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
            {
                if (value == null) throw new ArgumentNullException(nameof(value));

                if (!_canApply(value.GetType()))
                {
                    result = null;
                    return false;
                }

                var projected = _projection(value);
                result = propertyValueFactory.CreatePropertyValue(projected, true);
                return true;
            }
        }

        [Fact]
        public void DestructuringIsPossibleForSystemTypeDerivedProperties()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            var logger = new LoggerConfiguration()
                .Destructure.With(new ProjectedDestructuringPolicy(
                    canApply: t => typeof(Type).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()),
                    projection: o => ((Type)o).AssemblyQualifiedName))
                .WriteTo.Sink(sink)
                .CreateLogger();

            var thisType = GetType();
            logger.Information("{@thisType}", thisType);

            var ev = events.Single();
            var prop = ev.Properties["thisType"];
            var sv = Assert.IsAssignableFrom<ScalarValue>(prop);
            Assert.Equal(thisType.AssemblyQualifiedName, sv.LiteralValue());
        }

        [Fact]
        public void TransformationsAreAppliedToEventProperties()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .Destructure.ByTransforming<AB>(ab => new
                {
                    C = ab.B
                })
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
                .WriteTo.Sink(new StringSink())
                .Enrich.With(new DelegatingEnricher((e, f) => e.AddPropertyIfAbsent(property)))
                .Enrich.With(new DelegatingEnricher((e, f) => enrichedPropertySeen = e.Properties.ContainsKey(property.Name)))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.True(enrichedPropertySeen);
        }

        [Fact]
        public void MaximumDestructuringDepthIsEffective()
        {
            var x = new
            {
                A = new
                {
                    B = new
                    {
                        C = new
                        {
                            D = "F"
                        }
                    }
                }
            };

            var xs = LogAndGetAsString(x, conf => conf.Destructure.ToMaximumDepth(3), "@");

            Assert.Contains("C", xs);
            Assert.DoesNotContain("D", xs);
        }

        [Fact]
        public void MaximumStringLengthThrowsForLimitLowerThan2()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => new LoggerConfiguration().Destructure.ToMaximumStringLength(1));
            Assert.Equal(1, ex.ActualValue);
        }

        [Fact]
        public void MaximumStringLengthNOTEffectiveForString()
        {
            var x = "ABCD";
            var limitedText = LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(3));

            Assert.Equal("\"ABCD\"", limitedText);
        }

        [Fact]
        public void MaximumStringLengthEffectiveForCapturedString()
        {
            var x = "ABCD";

            var limitedText = LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(3), "@");

            Assert.Equal("\"AB…\"", limitedText);
        }

        [Fact]
        public void MaximumStringLengthEffectiveForStringifiedString()
        {
            var x = "ABCD";

            var limitedText = LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(3), "$");

            Assert.Equal("\"AB…\"", limitedText);
        }

        [Theory]
        [InlineData("1234", "12…", 3)]
        [InlineData("123", "123", 3)]
        public void MaximumStringLengthEffectiveForCapturedObject(string text, string textAfter, int limit)
        {
            var x = new
            {
                TooLongText = text
            };

            var limitedText = LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(limit), "@");

            Assert.Contains(textAfter, limitedText);
        }

        [Fact]
        public void MaximumStringLengthEffectiveForStringifiedObject()
        {
            var x = new ToStringOfLength(4);

            var limitedText = LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(3), "$");
            Assert.Contains("##…", limitedText);
        }

        [Fact]
        public void MaximumStringLengthNOTEffectiveForObject()
        {
            var x = new ToStringOfLength(4);

            var limitedText = LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(3));

            Assert.Contains("####", limitedText);
        }

        class ToStringOfLength
        {
            readonly int _toStringOfLength;

            public ToStringOfLength(int toStringOfLength)
            {
                _toStringOfLength = toStringOfLength;
            }

            public override string ToString()
            {
                return new string('#', _toStringOfLength);
            }
        }

        [Fact]
        public void MaximumStringCollectionThrowsForLimitLowerThan1()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => new LoggerConfiguration().Destructure.ToMaximumCollectionCount(0));
            Assert.Equal(0, ex.ActualValue);
        }

        [Fact]
        public void MaximumCollectionCountNotEffectiveForArrayAsLongAsLimit()
        {
            var x = new[] { 1, 2, 3 };

            var limitedCollection = LogAndGetAsString(x, conf => conf.Destructure.ToMaximumCollectionCount(3));

            Assert.Contains("3", limitedCollection);
        }

        [Fact]
        public void MaximumCollectionCountEffectiveForArrayThanLimit()
        {
            var x = new[] { 1, 2, 3, 4 };

            var limitedCollection = LogAndGetAsString(x, conf => conf.Destructure.ToMaximumCollectionCount(3));

            Assert.Contains("3", limitedCollection);
            Assert.DoesNotContain("4", limitedCollection);
        }

        [Fact]
        public void MaximumCollectionCountEffectiveForDictionaryWithMoreKeysThanLimit()
        {
            var x = new Dictionary<string, int>
            {
                { "1", 1 },
                { "2", 2 },
                { "3", 3 }
            };

            var limitedCollection = LogAndGetAsString(x, conf => conf.Destructure.ToMaximumCollectionCount(2));

            Assert.Contains("2", limitedCollection);
            Assert.DoesNotContain("3", limitedCollection);
        }

        [Fact]
        public void MaximumCollectionCountNotEffectiveForDictionaryWithAsManyKeysAsLimit()
        {
            var x = new Dictionary<string, int>
            {
                { "1", 1 },
                { "2", 2 },
            };

            var limitedCollection = LogAndGetAsString(x, conf => conf.Destructure.ToMaximumCollectionCount(2));

            Assert.Contains("2", limitedCollection);
        }

        static string LogAndGetAsString(object x, Func<LoggerConfiguration, LoggerConfiguration> conf, string destructuringSymbol = "")
        {
            LogEvent evt = null;
            var logConf = new LoggerConfiguration()
                .WriteTo.Sink(new DelegatingSink(e => evt = e));
            logConf = conf(logConf);
            var log = logConf.CreateLogger();

            log.Information($"{{{destructuringSymbol}X}}", x);
            return evt.Properties["X"].ToString();
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

            Assert.Single(sink.Events);
        }

        [Fact]
        public void LastMinimumLevelConfigurationWins()
        {
            var sink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(new LoggingLevelSwitch(LogEventLevel.Fatal))
                .MinimumLevel.Debug()
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.Single(sink.Events);
        }

        [Fact]
        public void HigherMinimumLevelOverridesArePropagated()
        {
            var sink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Write(Some.InformationEvent());
            logger.ForContext(Constants.SourceContextPropertyName, "Microsoft.AspNet.Something").Write(Some.InformationEvent());
            logger.ForContext<LoggerConfigurationTests>().Write(Some.InformationEvent());

            Assert.Equal(2, sink.Events.Count);
        }

        [Fact]
        public void LowerMinimumLevelOverridesArePropagated()
        {
            var sink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                .WriteTo.Sink(sink)
                .CreateLogger();

            logger.Write(Some.InformationEvent());
            logger.ForContext(Constants.SourceContextPropertyName, "Microsoft.AspNet.Something").Write(Some.InformationEvent());
            logger.ForContext<LoggerConfigurationTests>().Write(Some.InformationEvent());

            Assert.Single(sink.Events);
        }

        [Fact]
        public void ExceptionsThrownBySinksAreNotPropagated()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sink(new DelegatingSink(e => throw new Exception("Boom!")))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.True(true, "No exception reached the caller");
        }

        [Fact]
        public void ExceptionsThrownBySinksAreNotPropagatedEvenWhenAuditingIsPresent()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new CollectingSink())
                .WriteTo.Sink(new DelegatingSink(e => throw new Exception("Boom!")))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.True(true, "No exception reached the caller");
        }

        [Fact]
        public void ExceptionsThrownByFiltersAreNotPropagated()
        {
            var logger = new LoggerConfiguration()
                .Filter.ByExcluding(e => throw new Exception("Boom!"))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.True(true, "No exception reached the caller");
        }

        class Value { }

        [Fact]
        public void ExceptionsThrownByDestructuringPoliciesAreNotPropagated()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sink(new CollectingSink())
                .Destructure.ByTransforming<Value>(v => throw new Exception("Boom!"))
                .CreateLogger();

            logger.Information("{@Value}", new Value());

            Assert.True(true, "No exception reached the caller");
        }

        class ThrowingProperty
        {
            // ReSharper disable once UnusedMember.Local
            public string Property => throw new Exception("Boom!");
        }

        [Fact]
        public void WrappingDecoratesTheConfiguredSink()
        {
            DummyWrappingSink.Reset();
            var sink = new CollectingSink();
            var logger = new LoggerConfiguration()
                .WriteTo.Dummy(w => w.Sink(sink))
                .CreateLogger();

            var evt = Some.InformationEvent();
            logger.Write(evt);

            Assert.Same(evt, DummyWrappingSink.Emitted.Single());
            Assert.Same(evt, sink.SingleEvent);
        }

        [Fact]
        public void WrappingDoesNotPermitEnrichment()
        {
            var sink = new CollectingSink();
            var propertyName = Some.String();
            var logger = new LoggerConfiguration()
                .WriteTo.Dummy(w => w.Sink(sink)
                    .Enrich.WithProperty(propertyName, 1))
                .CreateLogger();

            var evt = Some.InformationEvent();
            logger.Write(evt);

            Assert.Same(evt, sink.SingleEvent);
            Assert.False(evt.Properties.ContainsKey(propertyName));
        }

        [Fact]
        public void WrappingIsAppliedWhenChaining()
        {
            DummyWrappingSink.Reset();
            var sink1 = new CollectingSink();
            var sink2 = new CollectingSink();
            var logger = new LoggerConfiguration()
                .WriteTo.Dummy(w => w.Sink(sink1)
                    .WriteTo.Sink(sink2))
                .CreateLogger();

            var evt = Some.InformationEvent();
            logger.Write(evt);

            Assert.Same(evt, DummyWrappingSink.Emitted.Single());
            Assert.Same(evt, sink1.SingleEvent);
            Assert.Same(evt, sink2.SingleEvent);
        }

        [Fact]
        public void WrappingIsAppliedWhenCallingMultipleTimes()
        {
            DummyWrappingSink.Reset();
            var sink1 = new CollectingSink();
            var sink2 = new CollectingSink();
            var logger = new LoggerConfiguration()
                .WriteTo.Dummy(w =>
                {
                    w.Sink(sink1);
                    w.Sink(sink2);
                })
                .CreateLogger();

            var evt = Some.InformationEvent();
            logger.Write(evt);

            Assert.Same(evt, DummyWrappingSink.Emitted.Single());
            Assert.Same(evt, sink1.SingleEvent);
            Assert.Same(evt, sink2.SingleEvent);
        }

        [Fact]
        public void WrappingWarnsAboutNonDisposableWrapper()
        {
            var messages = new List<string>();
            SelfLog.Enable(s => messages.Add(s));

            new LoggerConfiguration()
                .WriteTo.Dummy(w => w.Sink<DisposeTrackingSink>())
                .CreateLogger();

            SelfLog.Disable();
            Assert.NotEmpty(messages);
        }

        [Fact]
        public void WrappingSinkRespectsLogEventLevelSetting()
        {
            DummyWrappingSink.Reset();
            var sink = new CollectingSink();
            var logger = new LoggerConfiguration()
                .WriteTo.DummyWrap(w => w.Sink(sink), LogEventLevel.Error, null)
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.Empty(DummyWrappingSink.Emitted);
            Assert.Empty(sink.Events);
        }

        [Fact]
        public void WrappingSinkRespectsLevelSwitchSetting()
        {
            DummyWrappingSink.Reset();
            var sink = new CollectingSink();
            var logger = new LoggerConfiguration()
                .WriteTo.DummyWrap(
                    w => w.Sink(sink), LogEventLevel.Verbose,
                    new LoggingLevelSwitch(LogEventLevel.Error))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.Empty(DummyWrappingSink.Emitted);
            Assert.Empty(sink.Events);
        }
        
        [Fact]
        public void WrappingSinkReceivesEventsWhenLevelIsAppropriate()
        {
            DummyWrappingSink.Reset();
            var sink = new CollectingSink();
            var logger = new LoggerConfiguration()
                .WriteTo.DummyWrap(
                    w => w.Sink(sink), LogEventLevel.Error,
                    new LoggingLevelSwitch(LogEventLevel.Verbose))
                .CreateLogger();

            logger.Write(Some.InformationEvent());

            Assert.NotEmpty(DummyWrappingSink.Emitted);
            Assert.NotEmpty(sink.Events);
        }

        [Fact]
        public void ConditionalSinksReceiveEventsMatchingCondition()
        {
            var matching = new CollectingSink();
            var logger = new LoggerConfiguration()
                .WriteTo.Conditional(
                    le => le.Level == LogEventLevel.Warning,
                    w => w.Sink(matching))
                .CreateLogger();

            logger.Information("Information");
            logger.Warning("Warning");
            logger.Error("Error");

            var evt = Assert.Single(matching.Events);
            Assert.Equal(LogEventLevel.Warning, evt.Level);
        }

        [Fact]
        public void EnrichersCanBeWrapped()
        {
            var enricher = new CollectingEnricher();

            var configuration = new LoggerConfiguration();
            LoggerEnrichmentConfiguration.Wrap(
                configuration.Enrich,
                e => new ConditionalEnricher(e, le => le.Level == LogEventLevel.Warning),
                enrich => enrich.With(enricher));

            var logger = configuration.CreateLogger();
            logger.Information("Information");
            logger.Warning("Warning");
            logger.Error("Error");

            var evt = Assert.Single(enricher.Events);
            Assert.Equal(LogEventLevel.Warning, evt.Level);
        }

        [Fact]
        public void ConditionalEnrichersCheckConditions()
        {
            var enricher = new CollectingEnricher();

            var logger = new LoggerConfiguration()
                .Enrich.When(le => le.Level == LogEventLevel.Warning, enrich => enrich.With(enricher))
                .CreateLogger();

            logger.Information("Information");
            logger.Warning("Warning");
            logger.Error("Error");

            var evt = Assert.Single(enricher.Events);
            Assert.Equal(LogEventLevel.Warning, evt.Level);
        }
        
        [Fact]
        public void LeveledEnrichersCheckLevels()
        {
            var enricher = new CollectingEnricher();

            var logger = new LoggerConfiguration()
                .Enrich.AtLevel(LogEventLevel.Warning, enrich => enrich.With(enricher))
                .CreateLogger();

            logger.Information("Information");
            logger.Warning("Warning");
            logger.Error("Error");

            Assert.Equal(2, enricher.Events.Count);
            Assert.All(enricher.Events, e => Assert.True(e.Level >= LogEventLevel.Warning));
        }

        [Fact]
        public void LeveledEnrichersCheckLevelSwitch()
        {
            var enricher = new CollectingEnricher();
            var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Warning);

            var logger = new LoggerConfiguration()
                .Enrich.AtLevel(levelSwitch, enrich => enrich.With(enricher))
                .CreateLogger();

            logger.Information("Information");
            logger.Warning("Warning");
            logger.Error("Error");

            Assert.Equal(2, enricher.Events.Count);
            Assert.All(enricher.Events, e => Assert.True(e.Level >= LogEventLevel.Warning));

            enricher.Events.Clear();
            levelSwitch.MinimumLevel = LogEventLevel.Error;

            logger.Information("Information");
            logger.Warning("Warning");
            logger.Error("Error");

            var error = Assert.Single(enricher.Events);
            Assert.True(error.Level == LogEventLevel.Error);
        }
    }
}
