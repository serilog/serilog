using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Settings.KeyValuePairs;
using Serilog.Tests.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestDummies;
using TestDummies.Console;
using TestDummies.Console.Themes;
using Xunit;

namespace Serilog.Tests.Settings
{
    public class KeyValuePairSettingsTests
    {
        [Fact]
        public void LastValueIsTakenWhenKeysAreDuplicate()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("enrich:with-property:App", "InitialValue"),
                    new KeyValuePair<string, string>("enrich:with-property:App", "OverridenValue"),
                    new KeyValuePair<string, string>("enrich:with-property:App", "FinalValue")
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Has a test property");

            Assert.NotNull(evt);
            Assert.Equal("FinalValue", evt.Properties["App"].LiteralValue());
        }

        [Fact]
        public void FindsConfigurationAssemblies()
        {
            var configurationAssemblies = KeyValuePairSettings.LoadConfigurationAssemblies(new Dictionary<string, string>()).ToList();

            // The core Serilog assembly is always considered
            Assert.Single(configurationAssemblies);
        }

        [Fact]
        public void PropertyEnrichmentIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    { "enrich:with-property:App", "Test" }
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Has a test property");

            Assert.NotNull(evt);
            Assert.Equal("Test", evt.Properties["App"].LiteralValue());
        }

        [Fact]
        public void CallableMethodsAreSelected()
        {
            var options = GetDummyRollingFileConfigurationMethods(typeof(LoggerSinkConfiguration));
            Assert.Equal(2, options.Count(mi => mi.Name == "DummyRollingFile"));
            var suppliedArguments = new[]
            {
                new KeyValuePairSettings.ConfigurationMethodCall { MethodName = "DummyRollingFile", ArgumentName = "pathFormat", Value = "C:\\" },
            };

            var selected = KeyValuePairSettings.SelectConfigurationMethod(options, "DummyRollingFile", suppliedArguments);
            Assert.Equal(typeof(string), selected.GetParameters()[1].ParameterType);
        }

        [Fact]
        public void MethodsAreSelectedBasedOnCountOfMatchedArguments()
        {
            var options = GetDummyRollingFileConfigurationMethods(typeof(LoggerSinkConfiguration));
            Assert.Equal(2, options.Count(mi => mi.Name == "DummyRollingFile"));
            var suppliedArguments = new[]
            {
                new KeyValuePairSettings.ConfigurationMethodCall { MethodName = "DummyRollingFile", ArgumentName = "pathFormat", Value = "C:\\" },
                new KeyValuePairSettings.ConfigurationMethodCall { MethodName = "DummyRollingFile", ArgumentName = "formatter", Value = "SomeFormatter, SomeAssembly" }
            };

            var selected = KeyValuePairSettings.SelectConfigurationMethod(options, "DummyRollingFile", suppliedArguments);
            Assert.Equal(typeof(ITextFormatter), selected.GetParameters()[1].ParameterType);
        }

        static List<MethodInfo> GetDummyRollingFileConfigurationMethods(Type receiverType)
        {
            return typeof(DummyLoggerConfigurationExtensions)
                .GetTypeInfo()
                .DeclaredMethods
                .Where(m => m.GetParameters()[0].ParameterType == receiverType)
                .ToList();
        }

        [Fact]
        public void SinksAreConfigured()
        {
            var settings = new Dictionary<string, string>
            {
                ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                ["write-to:DummyRollingFile.pathFormat"] = "C:\\"
            };

            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings)
                .CreateLogger();

            DummyRollingFileSink.Reset();
            DummyRollingFileAuditSink.Reset();

            log.Write(Some.InformationEvent());

            Assert.Single(DummyRollingFileSink.Emitted);
            Assert.Empty(DummyRollingFileAuditSink.Emitted);
        }

        [Fact]
        public void AuditSinksAreConfigured()
        {
            var settings = new Dictionary<string, string>
            {
                ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                ["audit-to:DummyRollingFile.pathFormat"] = "C:\\"
            };

            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings)
                .CreateLogger();

            DummyRollingFileSink.Reset();
            DummyRollingFileAuditSink.Reset();

            log.Write(Some.InformationEvent());

            Assert.Empty(DummyRollingFileSink.Emitted);
            Assert.Single(DummyRollingFileAuditSink.Emitted);
        }

        [Fact]
        public void TestMinimumLevelOverrides()
        {
            var settings = new Dictionary<string, string>
            {
                ["minimum-level:override:System"] = "Warning",
            };

            LogEvent evt = null;

            var log = new LoggerConfiguration()
                 .ReadFrom.KeyValuePairs(settings)
                 .WriteTo.Sink(new DelegatingSink(e => evt = e))
                 .CreateLogger();

            var systemLogger = log.ForContext<WeakReference>();
            systemLogger.Write(Some.InformationEvent());

            Assert.Null(evt);

            systemLogger.Warning("Bad things");
            Assert.NotNull(evt);

            evt = null;
            log.Write(Some.InformationEvent());
            Assert.NotNull(evt);
        }

        [Theory]
        [InlineData("$switchName", true)]
        [InlineData("$SwitchName", true)]
        [InlineData("$switch1", true)]
        [InlineData("$sw1tch0", true)]
        [InlineData("$SWITCHNAME", true)]
        [InlineData("$$switchname", false)]
        [InlineData("$switchname$", false)]
        [InlineData("switch$name", false)]
        [InlineData("$", false)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("$1switch", false)]
        [InlineData("$switch_name", false)]
        public void LoggingLevelSwitchNameValidityScenarios(string switchName, bool expectedValid)
        {
            Assert.True(KeyValuePairSettings.IsValidSwitchName(switchName) == expectedValid,
                $"expected IsValidSwitchName({switchName}) to return {expectedValid} ");
        }

        [Fact]
        public void LoggingLevelSwitchWithInvalidNameThrowsFormatException()
        {
            var settings = new Dictionary<string, string>
            {
                ["level-switch:switchNameNotStartingWithDollar"] = "Warning",
            };

            var ex = Assert.Throws<FormatException>(() => new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings));

            Assert.Contains("\"switchNameNotStartingWithDollar\"", ex.Message);
            Assert.Contains("'$' sign", ex.Message);
            Assert.Contains("\"level-switch:$switchName\"", ex.Message);
        }

        [Fact]
        public void LoggingLevelSwitchIsConfigured()
        {
            var settings = new Dictionary<string, string>
            {
                ["level-switch:$switch1"] = "Warning",
                ["minimum-level:controlled-by"] = "$switch1",
            };

            LogEvent evt = null;

            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings)
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Write(Some.DebugEvent());
            Assert.True(evt is null, "LoggingLevelSwitch initial level was Warning. It should not log Debug messages");
            log.Write(Some.InformationEvent());
            Assert.True(evt is null, "LoggingLevelSwitch initial level was Warning. It should not log Information messages");
            log.Write(Some.WarningEvent());
            Assert.True(evt != null, "LoggingLevelSwitch initial level was Warning. It should log Warning messages");
        }

        [Fact]
        public void SettingMinimumLevelControlledByToAnUndeclaredSwitchThrows()
        {
            var settings = new Dictionary<string, string>
            {
                ["level-switch:$switch1"] = "Information",
                ["minimum-level:controlled-by"] = "$switch2",
            };

            var ex = Assert.Throws<InvalidOperationException>(() =>
                new LoggerConfiguration()
                    .ReadFrom.KeyValuePairs(settings)
                    .CreateLogger());
            Assert.Contains("$switch2", ex.Message);
            Assert.Contains("level-switch:$switch2", ex.Message);
        }

        [Fact]
        public void LoggingLevelSwitchIsPassedToSinks()
        {
            var settings = new Dictionary<string, string>
            {
                ["level-switch:$switch1"] = "Information",
                ["minimum-level:controlled-by"] = "$switch1",
                ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                ["write-to:DummyWithLevelSwitch.controlLevelSwitch"] = "$switch1"
            };

            LogEvent evt = null;

            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings)
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            Assert.False(DummyWithLevelSwitchSink.ControlLevelSwitch == null, "Sink ControlLevelSwitch should have been initialized");

            var controlSwitch = DummyWithLevelSwitchSink.ControlLevelSwitch;
            Assert.NotNull(controlSwitch);

            log.Write(Some.DebugEvent());
            Assert.True(evt is null, "LoggingLevelSwitch initial level was information. It should not log Debug messages");

            controlSwitch.MinimumLevel = LogEventLevel.Debug;
            log.Write(Some.DebugEvent());
            Assert.True(evt != null, "LoggingLevelSwitch level was changed to Debug. It should log Debug messages");
        }

        [Fact]
        public void ReferencingAnUndeclaredSwitchInSinkThrows()
        {
            var settings = new Dictionary<string, string>
            {
                ["level-switch:$switch1"] = "Information",
                ["minimum-level:controlled-by"] = "$switch1",
                ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                ["write-to:DummyWithLevelSwitch.controlLevelSwitch"] = "$switch2"
            };

            var ex = Assert.Throws<InvalidOperationException>(() =>
                new LoggerConfiguration()
                    .ReadFrom.KeyValuePairs(settings)
                    .CreateLogger());
            Assert.Contains("$switch2", ex.Message);
            Assert.Contains("level-switch:", ex.Message);
        }

        [Fact]
        public void LoggingLevelSwitchCanBeUsedForMinimumLevelOverrides()
        {
            var settings = new Dictionary<string, string>
            {
                ["minimum-level"] = "Debug",
                ["level-switch:$specificSwitch"] = "Warning",
                ["minimum-level:override:System"] = "$specificSwitch",
                ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                ["write-to:DummyWithLevelSwitch.controlLevelSwitch"] = "$specificSwitch"
            };

            LogEvent evt = null;

            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings)
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            var systemLogger = log.ForContext(Constants.SourceContextPropertyName, "System.Bar");

            log.Write(Some.InformationEvent());
            Assert.False(evt is null, "Minimum level is Debug. It should log Information messages");

            evt = null;
            // ReSharper disable HeuristicUnreachableCode
            systemLogger.Write(Some.InformationEvent());
            Assert.True(evt is null, "LoggingLevelSwitch initial level was Warning for logger System.*. It should not log Information messages for SourceContext System.Bar");

            systemLogger.Write(Some.WarningEvent());
            Assert.False(evt is null, "LoggingLevelSwitch initial level was Warning for logger System.*. It should log Warning messages for SourceContext System.Bar");

            evt = null;
            var controlSwitch = DummyWithLevelSwitchSink.ControlLevelSwitch;

            controlSwitch.MinimumLevel = LogEventLevel.Information;
            systemLogger.Write(Some.InformationEvent());
            Assert.False(evt is null, "LoggingLevelSwitch level was changed to Information for logger System.*. It should now log Information events for SourceContext System.Bar.");
            // ReSharper restore HeuristicUnreachableCode
        }

        [Fact]
        public void SinksWithAbstractParamsAreConfiguredWithTypeName()
        {
            var settings = new Dictionary<string, string>
            {
                ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                ["write-to:DummyConsole.theme"] = "Serilog.Tests.Support.CustomConsoleTheme, Serilog.Tests"
            };

            DummyConsoleSink.Theme = null;

            new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings)
                .CreateLogger();

            Assert.NotNull(DummyConsoleSink.Theme);
            Assert.IsType<CustomConsoleTheme>(DummyConsoleSink.Theme);
        }

        [Fact]
        public void SinksAreConfiguredWithStaticMember()
        {
            var settings = new Dictionary<string, string>
            {
                ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                ["write-to:DummyConsole.theme"] = "TestDummies.Console.Themes.ConsoleThemes::Theme1, TestDummies"
            };

            DummyConsoleSink.Theme = null;

            new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings)
                .CreateLogger();

            Assert.Equal(ConsoleThemes.Theme1, DummyConsoleSink.Theme);
        }

        [Fact]
        public void DestructuringToMaximumDepthIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["destructure:ToMaximumDepth.maximumDestructuringDepth"] = "3"
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            var nestedObject = new
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

            log.Information("Destructuring a big graph {@DeeplyNested}", nestedObject);
            var formattedProperty = evt.Properties["DeeplyNested"].ToString();

            Assert.Contains("C", formattedProperty);
            Assert.DoesNotContain("D", formattedProperty);
        }

        [Fact]
        public void DestructuringToMaximumStringLengthIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["destructure:ToMaximumStringLength.maximumStringLength"] = "3"
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Destructuring a long string {@LongString}", "ABCDEFGH");
            var formattedProperty = evt.Properties["LongString"].ToString();

            Assert.Equal("\"AB…\"", formattedProperty);
        }

        [Fact]
        public void DestructuringToMaximumCollectionCountIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["destructure:ToMaximumCollectionCount.maximumCollectionCount"] = "3"
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            var collection = new[] { 1, 2, 3, 4, 5, 6 };
            log.Information("Destructuring a big collection {@BigCollection}", collection);
            var formattedProperty = evt.Properties["BigCollection"].ToString();

            Assert.Contains("3", formattedProperty);
            Assert.DoesNotContain("4", formattedProperty);
        }

        [Fact]
        public void DestructuringWithCustomExtensionMethodIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                    ["destructure:WithDummyHardCodedString.hardCodedString"] = "hardcoded"
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Destructuring with hard-coded policy {@Input}", new { Foo = "Bar" });
            var formattedProperty = evt.Properties["Input"].ToString();

            Assert.Equal("\"hardcoded\"", formattedProperty);
        }

        [Fact]
        public void DestructuringAsScalarIsAppliedWithShortTypeName()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["destructure:AsScalar.scalarType"] = "System.Version"
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Destructuring as scalar {@Scalarized}", new Version(2, 3));
            var prop = evt.Properties["Scalarized"];

            Assert.IsAssignableFrom<ScalarValue>(prop);
        }

        [Fact]
        public void DestructuringAsScalarIsAppliedWithAssemblyQualifiedName()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["destructure:AsScalar.scalarType"] = typeof(Version).AssemblyQualifiedName
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Destructuring as scalar {@Scalarized}", new Version(2, 3));
            var prop = evt.Properties["Scalarized"];

            Assert.IsAssignableFrom<ScalarValue>(prop);
        }

        [Fact]
        public void DestructuringWithIsAppliedWithCustomDestructuringPolicy()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                    ["destructure:With.policy"] = typeof(DummyReduceVersionToMajorPolicy).AssemblyQualifiedName
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("Destructuring with policy {@Version}", new Version(2, 3));
            var prop = evt.Properties["Version"];

            Assert.IsType<ScalarValue>(prop);
            Assert.Equal(2, (prop as ScalarValue)?.Value);
        }

        [Fact]
        public void WriteToSinkIsAppliedWithCustomSink()
        {
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                    ["write-to:Sink.sink"] = typeof(DummyRollingFileSink).AssemblyQualifiedName
                })
                .CreateLogger();

            DummyRollingFileSink.Reset();
            log.Write(Some.InformationEvent());

            Assert.Single(DummyRollingFileSink.Emitted);
        }

        [Fact]
        public void WriteToSinkIsAppliedWithCustomSinkAndMinimumLevel()
        {
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                    ["write-to:Sink.sink"] = typeof(DummyRollingFileSink).AssemblyQualifiedName,
                    ["write-to:Sink.restrictedToMinimumLevel"] = "Warning"
                })
                .CreateLogger();

            DummyRollingFileSink.Reset();
            log.Write(Some.InformationEvent());
            log.Write(Some.WarningEvent());

            Assert.Single(DummyRollingFileSink.Emitted);
        }

        [Fact]
        public void WriteToSinkIsAppliedWithCustomSinkAndLevelSwitch()
        {
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                    ["level-switch:$switch1"] = "Warning",
                    ["write-to:Sink.sink"] = typeof(DummyRollingFileSink).AssemblyQualifiedName,
                    ["write-to:Sink.levelSwitch"] = "$switch1"
                })
                .CreateLogger();

            DummyRollingFileSink.Reset();
            log.Write(Some.InformationEvent());
            log.Write(Some.WarningEvent());

            Assert.Single(DummyRollingFileSink.Emitted);
        }

        [Fact]
        public void AuditToSinkIsAppliedWithCustomSink()
        {
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                    ["audit-to:Sink.sink"] = typeof(DummyRollingFileSink).AssemblyQualifiedName
                })
                .CreateLogger();

            DummyRollingFileSink.Reset();
            log.Write(Some.InformationEvent());

            Assert.Single(DummyRollingFileSink.Emitted);
        }

        [Fact]
        public void AuditToSinkIsAppliedWithCustomSinkAndMinimumLevel()
        {
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                    ["audit-to:Sink.sink"] = typeof(DummyRollingFileSink).AssemblyQualifiedName,
                    ["audit-to:Sink.restrictedToMinimumLevel"] = "Warning"
                })
                .CreateLogger();

            DummyRollingFileSink.Reset();
            log.Write(Some.InformationEvent());
            log.Write(Some.WarningEvent());

            Assert.Single(DummyRollingFileSink.Emitted);
        }

        [Fact]
        public void AuditToSinkIsAppliedWithCustomSinkAndLevelSwitch()
        {
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                    ["level-switch:$switch1"] = "Warning",
                    ["audit-to:Sink.sink"] = typeof(DummyRollingFileSink).AssemblyQualifiedName,
                    ["audit-to:Sink.levelSwitch"] = "$switch1"
                })
                .CreateLogger();

            DummyRollingFileSink.Reset();
            log.Write(Some.InformationEvent());
            log.Write(Some.WarningEvent());

            Assert.Single(DummyRollingFileSink.Emitted);
        }

        [Fact]
        public void EnrichWithIsAppliedWithCustomEnricher()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                    ["enrich:With.enricher"] = typeof(DummyThreadIdEnricher).AssemblyQualifiedName
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Write(Some.InformationEvent());

            Assert.NotNull(evt);
            Assert.True(evt.Properties.ContainsKey("ThreadId"), "Event should have enriched property ThreadId");
        }

        [Fact]
        public void FilterWithIsAppliedWithCustomFilter()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                    ["filter:With.filter"] = typeof(DummyAnonymousUserFilter).AssemblyQualifiedName
                })
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.ForContext("User", "anonymous").Write(Some.InformationEvent());
            Assert.Null(evt);
            log.ForContext("User", "the user").Write(Some.InformationEvent());
            Assert.NotNull(evt);
        }
    }
}
