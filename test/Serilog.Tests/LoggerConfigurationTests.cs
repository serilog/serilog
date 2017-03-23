using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using Xunit;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Core.Filters;
using Serilog.Events;
using Serilog.Tests.Support;
using System.Threading.Tasks;

namespace Serilog.Tests
{
    public class LoggerConfigurationTests
    {
        class DisposableSink : ILogEventSink, IDisposable
        {
            public bool IsDisposed { get; private set; }

            public Task Emit(LogEvent logEvent)
            {
                return Task.FromResult((object)null);
            }

            public void Dispose()
            {
                IsDisposed = true;
            }
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
        public async Task AFilterPreventsMatchedEventsFromPassingToTheSink()
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
            await logger.Write(included);
            await logger.Write(excluded);
            Assert.Equal(1, events.Count);
            Assert.True(events.Contains(included));
        }

        // ReSharper disable UnusedMember.Local, UnusedAutoPropertyAccessor.Local
        class AB
        {
            public int A { get; set; }
            public int B { get; set; }
        }

// ReSharper restore UnusedAutoPropertyAccessor.Local, UnusedMember.Local

        [Fact]
        public async Task SpecifyingThatATypeIsScalarCausesItToBeLoggedAsScalarEvenWhenDestructuring()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .Destructure.AsScalar(typeof(AB))
                .CreateLogger();

            await logger.Information("{@AB}", new AB());

            var ev = events.Single();
            var prop = ev.Properties["AB"];
            Assert.IsType<ScalarValue>(prop);
        }

        [Fact]
        public async Task DestructuringSystemTypeGivesScalarByDefault()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink)
                .CreateLogger();

            var thisType = this.GetType();
            await logger.Information("{@thisType}", thisType);

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
                if (canApply == null) throw new ArgumentNullException(nameof(canApply));
                if (projection == null) throw new ArgumentNullException(nameof(projection));
                _canApply = canApply;
                _projection = projection;
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
        public async Task DestructuringIsPossibleForSystemTypeDerivedProperties()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);
            
            var logger = new LoggerConfiguration()
                .Destructure.With(new ProjectedDestructuringPolicy(
                    canApply: t => typeof(Type).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()),
                    projection: o => ((Type)o).AssemblyQualifiedName))
                .WriteTo.Sink(sink)
                .CreateLogger();

            var thisType = this.GetType();
            await logger.Information("{@thisType}", thisType);

            var ev = events.Single();
            var prop = ev.Properties["thisType"];
            var sv = Assert.IsAssignableFrom<ScalarValue>(prop);
            Assert.Equal(thisType.AssemblyQualifiedName, sv.LiteralValue());
        }

        [Fact]
        public async Task TransformationsAreAppliedToEventProperties()
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

            await logger.Information("{@AB}", new AB());

            var ev = events.Single();
            var prop = ev.Properties["AB"];
            var sv = (StructureValue) prop;
            var c = sv.Properties.Single();
            Assert.Equal("C", c.Name);
        }

        [Fact]
        public async Task WritingToALoggerWritesToSubLogger()
        {
            var eventReceived = false;

            var logger = new LoggerConfiguration()
                .WriteTo.Logger(l => l
                    .WriteTo.Sink(new DelegatingSink(e => eventReceived = true)))
                .CreateLogger();

            await logger.Write(Some.InformationEvent());

            Assert.True(eventReceived);
        }

        [Fact]
        public async Task SubLoggerRestrictsFilter()
        {
            var eventReceived = false;

            var logger = new LoggerConfiguration()
                .WriteTo.Logger(l => l
                    .MinimumLevel.Fatal()
                    .WriteTo.Sink(new DelegatingSink(e => eventReceived = true)))
                .CreateLogger();

            await logger.Write(Some.InformationEvent());

            Assert.True(!eventReceived);
        }

        [Fact]
        public async Task EnrichersExecuteInConfigurationOrder()
        {
            var property = Some.LogEventProperty();
            var enrichedPropertySeen = false;

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(new StringSink())
                .Enrich.With(new DelegatingEnricher((e, f) => e.AddPropertyIfAbsent(property)))
                .Enrich.With(new DelegatingEnricher((e, f) => enrichedPropertySeen = e.Properties.ContainsKey(property.Name)))
                .CreateLogger();

            await logger.Write(Some.InformationEvent());

            Assert.True(enrichedPropertySeen);
        }

        [Fact]
        public async Task MaximumDestructuringDepthIsEffective()
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

            var xs = await LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(3), "@");

            Assert.Contains("C", xs);
            Assert.DoesNotContain(xs, "D");
        }

        [Fact]
        public void MaximumStringLengthThrowsForLimitLowerThan2()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => new LoggerConfiguration().Destructure.ToMaximumStringLength(1));
            Assert.Equal(1, ex.ActualValue);
        }

        [Fact]
        public async Task MaximumStringLengthNOTEffectiveForString()
        {
            var x = "ABCD";
            var limitedText = await LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(3));

            Assert.Equal("\"ABCD\"", limitedText);
        }

        [Fact]
        public async Task MaximumStringLengthEffectiveForCapturedString()
        {
            var x = "ABCD";

            var limitedText = await LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(3), "@");

            Assert.Equal("\"AB…\"", limitedText);
        }

        [Fact]
        public async Task MaximumStringLengthEffectiveForStringifiedString()
        {
            var x = "ABCD";

            var limitedText = await LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(3), "$");

            Assert.Equal("\"AB…\"", limitedText);
        }

        [Theory]
        [InlineData("1234", "12…", 3)]
        [InlineData("123", "123", 3)]
        public async Task MaximumStringLengthEffectiveForCapturedObject(string text, string textAfter, int limit)
        {
            var x = new
            {
                TooLongText = text
            };

            var limitedText = await LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(limit), "@");

            Assert.Contains(textAfter, limitedText);
        }

        [Fact]
        public async Task MaximumStringLengthEffectiveForStringifiedObject()
        {
            var x = new ToStringOfLength(4);

            var limitedText = await LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(3), "$");
            Assert.Contains("##…", limitedText);
        }

        [Fact]
        public async Task MaximumStringLengthNOTEffectiveForObject()
        {
            var x = new ToStringOfLength(4);

            var limitedText = await LogAndGetAsString(x, conf => conf.Destructure.ToMaximumStringLength(3));

            Assert.Contains("####", limitedText);
        }

        class ToStringOfLength
        {
            private int _toStringOfLength;

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
        public async Task MaximumCollectionCountNotEffectiveForArrayAsLongAsLimit()
        {
            var x = new[] { 1, 2, 3 };

            var limitedCollection = await LogAndGetAsString(x, conf => conf.Destructure.ToMaximumCollectionCount(3));

            Assert.Contains("3", limitedCollection);
        }

        [Fact]
        public async Task MaximumCollectionCountEffectiveForArrayThanLimit()
        {
            var x = new[] { 1, 2, 3, 4 };

            var limitedCollection = await LogAndGetAsString(x, conf => conf.Destructure.ToMaximumCollectionCount(3));

            Assert.Contains("3", limitedCollection);
            Assert.DoesNotContain("4", limitedCollection);
        }

        [Fact]
        public async Task MaximumCollectionCountEffectiveForDictionaryWithMoreKeysThanLimit()
        {
            var x = new Dictionary<string, int>
            {
                {"1", 1},
                {"2", 2},
                {"3", 3}
            };

            var limitedCollection = await LogAndGetAsString(x, conf => conf.Destructure.ToMaximumCollectionCount(2));

            Assert.Contains("2", limitedCollection);
            Assert.DoesNotContain("3", limitedCollection);
        }

        [Fact]
        public async Task MaximumCollectionCountNotEffectiveForDictionaryWithAsManyKeysAsLimit()
        {
            var x = new Dictionary<string, int>
            {
                {"1", 1},
                {"2", 2},
            };

            var limitedCollection = await LogAndGetAsString(x, conf => conf.Destructure.ToMaximumCollectionCount(2));

            Assert.Contains("2", limitedCollection);
        }

        private async Task<string> LogAndGetAsString(object x, Func<LoggerConfiguration, LoggerConfiguration> conf, string destructuringSymbol = "")
        {
            LogEvent evt = null;
            var logConf = new LoggerConfiguration()
                .WriteTo.Sink(new DelegatingSink(e => evt = e));
            logConf = conf(logConf);
            var log = logConf.CreateLogger();

            await log.Information($"{{{destructuringSymbol}X}}", x);
            var result = evt.Properties["X"].ToString();
            return result;
        }

        [Fact]
        public async Task DynamicallySwitchingSinkRestrictsOutput()
        {
            var eventsReceived = 0;
            var levelSwitch = new LoggingLevelSwitch();

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(
                    new DelegatingSink(e => eventsReceived++),
                    levelSwitch: levelSwitch)
                .CreateLogger();

            await logger.Write(Some.InformationEvent());
            levelSwitch.MinimumLevel = LogEventLevel.Warning;
            await logger.Write(Some.InformationEvent());
            levelSwitch.MinimumLevel = LogEventLevel.Verbose;
            await logger.Write(Some.InformationEvent());

            Assert.Equal(2, eventsReceived);
        }

        [Fact]
        public async Task LevelSwitchTakesPrecedenceOverMinimumLevel()
        {
            var sink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .WriteTo.Sink(sink, LogEventLevel.Fatal, new LoggingLevelSwitch())
                .CreateLogger();

            await logger.Write(Some.InformationEvent());

            Assert.Equal(1, sink.Events.Count);
        }

        [Fact]
        public async Task LastMinimumLevelConfigurationWins()
        {
            var sink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(new LoggingLevelSwitch(LogEventLevel.Fatal))
                .MinimumLevel.Debug()
                .WriteTo.Sink(sink)
                .CreateLogger();

            await logger.Write(Some.InformationEvent());

            Assert.Equal(1, sink.Events.Count);
        }

        [Fact]
        public async Task HigherMinimumLevelOverridesArePropagated()
        {
            var sink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .WriteTo.Sink(sink)
                .CreateLogger();

            await logger.Write(Some.InformationEvent());
            await logger.ForContext(Serilog.Core.Constants.SourceContextPropertyName, "Microsoft.AspNet.Something").Write(Some.InformationEvent());
            await logger.ForContext<LoggerConfigurationTests>().Write(Some.InformationEvent());

            Assert.Equal(2, sink.Events.Count);
        }

        [Fact]
        public async Task LowerMinimumLevelOverridesArePropagated()
        {
            var sink = new CollectingSink();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                .WriteTo.Sink(sink)
                .CreateLogger();

            await logger.Write(Some.InformationEvent());
            await logger.ForContext(Serilog.Core.Constants.SourceContextPropertyName, "Microsoft.AspNet.Something").Write(Some.InformationEvent());
            await logger.ForContext<LoggerConfigurationTests>().Write(Some.InformationEvent());

            Assert.Equal(1, sink.Events.Count);
        }

        [Fact]
        public async Task ExceptionsThrownBySinksAreNotPropagated()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sink(new DelegatingSink(e => { throw new Exception("Boom!"); }))
                .CreateLogger();

            await logger.Write(Some.InformationEvent());

            Assert.True(true, "No exception reached the caller");
        }

        [Fact]
        public async Task ExceptionsThrownBySinksAreNotPropagatedEvenWhenAuditingIsPresent()
        {
            var logger = new LoggerConfiguration()
                .AuditTo.Sink(new CollectingSink())
                .WriteTo.Sink(new DelegatingSink(e => { throw new Exception("Boom!"); }))
                .CreateLogger();

            await logger.Write(Some.InformationEvent());

            Assert.True(true, "No exception reached the caller");
        }

        [Fact]
        public async Task ExceptionsThrownByFiltersAreNotPropagated()
        {
            var logger = new LoggerConfiguration()
                .Filter.ByExcluding(e => { throw new Exception("Boom!"); })
                .CreateLogger();

            await logger.Write(Some.InformationEvent());

            Assert.True(true, "No exception reached the caller");
        }

        class Value { }

        [Fact]
        public async Task ExceptionsThrownByDestructuringPoliciesAreNotPropagated()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sink(new CollectingSink())
                .Destructure.ByTransforming<Value>(v => { throw new Exception("Boom!"); })
                .CreateLogger();

            await logger.Information("{@Value}", new Value());

            Assert.True(true, "No exception reached the caller");
        }

        class ThrowingProperty
        {
            // ReSharper disable once UnusedMember.Local
            public string Property
            {
                get { throw new Exception("Boom!"); }
            }
        }
    }
}
