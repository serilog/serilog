// ReSharper disable PossibleNullReferenceException

namespace Serilog.Tests.Configuration;

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
        var (logger, wr) = CreateLogger();

        GC.Collect();

        Assert.False(wr.IsAlive);
        GC.KeepAlive(logger);

        static (ILogger, WeakReference) CreateLogger()
        {
            var loggerConfiguration = new LoggerConfiguration();
            return (loggerConfiguration.CreateLogger(), new(loggerConfiguration));
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
            _canApply = Guard.AgainstNull(canApply);
            _projection = Guard.AgainstNull(projection);
        }

        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, [NotNullWhen(true)] out LogEventPropertyValue? result)
        {
            Guard.AgainstNull(value);

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
                canApply: t => typeof(Type).IsAssignableFrom(t),
                projection: o => ((Type)o).AssemblyQualifiedName!))
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
                .WriteTo.Sink(new DelegatingSink(_ => eventReceived = true)))
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
                .WriteTo.Sink(new DelegatingSink(_ => eventReceived = true)))
            .CreateLogger();

        logger.Write(Some.InformationEvent());

        Assert.False(eventReceived);
    }

    [Fact]
    public void EnrichersExecuteInConfigurationOrder()
    {
        var property = Some.LogEventProperty();
        var enrichedPropertySeen = false;

        var logger = new LoggerConfiguration()
            .WriteTo.Sink(new StringSink())
            .Enrich.With(new DelegatingEnricher((e, _) => e.AddPropertyIfAbsent(property)))
            .Enrich.With(new DelegatingEnricher((e, _) => enrichedPropertySeen = e.Properties.ContainsKey(property.Name)))
            .CreateLogger();

        logger.Write(Some.InformationEvent());

        Assert.True(enrichedPropertySeen);
    }

    [Fact]
    public void EnrichersWithPropertyFactoryExecuteInConfigurationOrder()
    {
        var propertyName = Some.String();
        var propertyValue = Some.String();
        var enrichedPropertySeen = false;

        var logger = new LoggerConfiguration()
            .WriteTo.Sink(new StringSink())
            .Enrich.With(new DelegatingEnricher((e, factory) => e.AddPropertyIfAbsent(factory, propertyName, propertyValue)))
            .Enrich.With(new DelegatingEnricher((e, _) => enrichedPropertySeen = e.Properties.ContainsKey(propertyName)))
            .CreateLogger();

        logger.Write(Some.InformationEvent());

        Assert.True(enrichedPropertySeen);
    }

    [Fact]
    public void MaximumDestructuringDepthDefaultIsEffective()
    {
        var x = new
        {
            Lvl01 = new
            {
                Lvl02 = new
                {
                    Lvl03 = new
                    {
                        Lvl04 = new
                        {
                            Lvl05 = new
                            {
                                Lvl06 = new { Lvl07 = new { Lvl08 = new { Lvl09 = new { Lvl10 = "Lvl11" } } } }
                            }
                        }
                    }
                }
            }
        };

        var xs = LogAndGetAsString(x, conf => conf, "@");

        Assert.Contains("Lvl10", xs);
        Assert.DoesNotContain("Lvl11", xs);
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
            return new('#', _toStringOfLength);
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
        LogEvent? evt = null;
        var logConf = new LoggerConfiguration()
            .WriteTo.Sink(new DelegatingSink(e => evt = e));
        logConf = conf(logConf);
        var log = logConf.CreateLogger();

        log.Information($"{{{destructuringSymbol}X}}", x);
        Assert.NotNull(evt);
        return evt!.Properties["X"].ToString();
    }

    [Fact]
    public void DynamicallySwitchingSinkRestrictsOutput()
    {
        var eventsReceived = 0;
        var levelSwitch = new LoggingLevelSwitch();

        var logger = new LoggerConfiguration()
            .WriteTo.Sink(
                new DelegatingSink(_ => eventsReceived++),
                levelSwitch: levelSwitch)
            .CreateLogger();

        logger.Write(Some.InformationEvent());
        levelSwitch.MinimumLevel = Warning;
        logger.Write(Some.InformationEvent());
        levelSwitch.MinimumLevel = Verbose;
        logger.Write(Some.InformationEvent());

        Assert.Equal(2, eventsReceived);
    }

    [Fact]
    public void LevelSwitchTakesPrecedenceOverMinimumLevel()
    {
        var sink = new CollectingSink();

        var logger = new LoggerConfiguration()
            .WriteTo.Sink(sink, Fatal, new())
            .CreateLogger();

        logger.Write(Some.InformationEvent());

        Assert.Single(sink.Events);
    }

    [Fact]
    public void LastMinimumLevelConfigurationWins()
    {
        var sink = new CollectingSink();

        var logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(new(Fatal))
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
            .MinimumLevel.Override("Microsoft", Error)
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
            .MinimumLevel.Override("Microsoft", Debug)
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
            .WriteTo.Sink(new DelegatingSink(_ => throw new("Boom!")))
            .CreateLogger();

        logger.Write(Some.InformationEvent());

        Assert.True(true, "No exception reached the caller");
    }

    [Fact]
    public void ExceptionsThrownBySinksAreNotPropagatedEvenWhenAuditingIsPresent()
    {
        var logger = new LoggerConfiguration()
            .AuditTo.Sink(new CollectingSink())
            .WriteTo.Sink(new DelegatingSink(_ => throw new("Boom!")))
            .CreateLogger();

        logger.Write(Some.InformationEvent());

        Assert.True(true, "No exception reached the caller");
    }

    [Fact]
    public void ExceptionsThrownByFiltersAreNotPropagated()
    {
        var logger = new LoggerConfiguration()
            .Filter.ByExcluding(_ => throw new("Boom!"))
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
            .Destructure.ByTransforming<Value>(_ => throw new("Boom!"))
            .CreateLogger();

        logger.Information("{@Value}", new Value());

        Assert.True(true, "No exception reached the caller");
    }

    [Fact]
    public void ConditionalSinksReceiveEventsMatchingCondition()
    {
        var matching = new CollectingSink();
        var logger = new LoggerConfiguration()
            .WriteTo.Conditional(
                le => le.Level == Warning,
                w => w.Sink(matching))
            .CreateLogger();

        logger.Information("Information");
        logger.Warning("Warning");
        logger.Error("Error");

        var evt = Assert.Single(matching.Events);
        Assert.Equal(Warning, evt.Level);
    }

#if FEATURE_ASYNCDISPOSABLE
    [Fact]
    public async Task ConditionalSinksAreDisposedAsyncWithLogger()
    {
        var sink = new AsyncDisposeTrackingSink();
        var logger = new LoggerConfiguration()
            .WriteTo.Conditional(_ => true, w => w.Sink(sink))
            .CreateLogger();

        await logger.DisposeAsync();

        Assert.True(sink.IsDisposedAsync);
    }
#endif

    [Fact]
    public void EnrichersCanBeWrapped()
    {
        var enricher = new CollectingEnricher();

        var configuration = new LoggerConfiguration();
        LoggerEnrichmentConfiguration.Wrap(
            configuration.Enrich,
            e => new ConditionalEnricher(e, le => le.Level == Warning),
            enrich => enrich.With(enricher));

        var logger = configuration.CreateLogger();
        logger.Information("Information");
        logger.Warning("Warning");
        logger.Error("Error");

        var evt = Assert.Single(enricher.Events);
        Assert.Equal(Warning, evt.Level);
    }

    [Fact]
    public void ConditionalEnrichersCheckConditions()
    {
        var enricher = new CollectingEnricher();

        var logger = new LoggerConfiguration()
            .Enrich.When(le => le.Level == Warning, enrich => enrich.With(enricher))
            .CreateLogger();

        logger.Information("Information");
        logger.Warning("Warning");
        logger.Error("Error");

        var evt = Assert.Single(enricher.Events);
        Assert.Equal(Warning, evt.Level);
    }

    [Fact]
    public void LeveledEnrichersCheckLevels()
    {
        var enricher = new CollectingEnricher();

        var logger = new LoggerConfiguration()
            .Enrich.AtLevel(Warning, enrich => enrich.With(enricher))
            .CreateLogger();

        logger.Information("Information");
        logger.Warning("Warning");
        logger.Error("Error");

        Assert.Equal(2, enricher.Events.Count);
        Assert.All(enricher.Events, e => Assert.True(e.Level >= Warning));
    }

    [Fact]
    public void LeveledEnrichersCheckLevelSwitch()
    {
        var enricher = new CollectingEnricher();
        var levelSwitch = new LoggingLevelSwitch(Warning);

        var logger = new LoggerConfiguration()
            .Enrich.AtLevel(levelSwitch, enrich => enrich.With(enricher))
            .CreateLogger();

        logger.Information("Information");
        logger.Warning("Warning");
        logger.Error("Error");

        Assert.Equal(2, enricher.Events.Count);
        Assert.All(enricher.Events, e => Assert.True(e.Level >= Warning));

        enricher.Events.Clear();
        levelSwitch.MinimumLevel = Error;

        logger.Information("Information");
        logger.Warning("Warning");
        logger.Error("Error");

        var error = Assert.Single(enricher.Events);
        Assert.True(error.Level == Error);
    }

    [Fact]
    public void EventsAreNotEmittedWhenMinimumLevelIsOff()
    {
        var sink = new CollectingSink();

        var logger = new LoggerConfiguration()
            .MinimumLevel.Is(LevelAlias.Off)
            .WriteTo.Sink(sink)
            .CreateLogger();

        logger.Fatal("Not emitted");

        Assert.Empty(sink.Events);
    }

    [Fact]
    public void EventsAreNotEmittedWhenMinimumLevelOverrideIsOff()
    {
        var sink = new CollectingSink();

        var logger = new LoggerConfiguration()
            .MinimumLevel.Override("Test", LevelAlias.Off)
            .WriteTo.Sink(sink)
            .CreateLogger();

        logger.Fatal("Emitted");
        logger.ForContext(Constants.SourceContextPropertyName, "Test").Fatal("Not emitted");
        logger.ForContext(Constants.SourceContextPropertyName, "Another").Fatal("Emitted");

        Assert.Equal(2, sink.Events.Count);
        Assert.All(sink.Events, le => Assert.Equal("Emitted", le.MessageTemplate.Text));
    }
}
