using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Serilog.Events;
using Serilog.Settings.KeyValuePairs;
using Serilog.Tests.Support;
using TestDummies;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Formatting;

namespace Serilog.Tests.Settings
{
    public class KeyValuePairSettingsTests
    {
        [Fact]
        public void FindsConfigurationAssemblies()
        {
            var configurationAssemblies = KeyValuePairSettings.LoadConfigurationAssemblies(new Dictionary<string, string>()).ToList();

            // The core Serilog assembly is always considered
            Assert.Equal(1, configurationAssemblies.Count);
        }

        [Fact]
        public void PropertyEnrichmentIsApplied()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new Dictionary<string, string>
                {
                    {"enrich:with-property:App", "Test"}
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

            DummyRollingFileSink.Emitted.Clear();
            DummyRollingFileAuditSink.Emitted.Clear();

            log.Write(Some.InformationEvent());

            Assert.Equal(1, DummyRollingFileSink.Emitted.Count);
            Assert.Equal(0, DummyRollingFileAuditSink.Emitted.Count);
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

            DummyRollingFileSink.Emitted.Clear();
            DummyRollingFileAuditSink.Emitted.Clear();

            log.Write(Some.InformationEvent());

            Assert.Equal(0, DummyRollingFileSink.Emitted.Count);
            Assert.Equal(1, DummyRollingFileAuditSink.Emitted.Count);
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
        [InlineData("Debug", LogEventLevel.Debug, "proper parsing")]
        [InlineData("Information", LogEventLevel.Information, "proper parsing")]
        [InlineData("", LogEventLevel.Information, "default value")]
        public void LoggingLevelSwitchIsInstantiatedFromLogLevelAsString3(string paramValue, LogEventLevel expectedSwitchInitialLevel, string reason)
        {
            object actual = null;
            var didSucceed = KeyValuePairSettings.TryInstantiate(typeof(LoggingLevelSwitch), paramValue, out actual);

            Assert.True(didSucceed, "TryInstantiate should successfully instantiate a LoggingLevelSwitch");
            Assert.NotNull(actual);
            Assert.IsType<LoggingLevelSwitch>(actual);
            Assert.Equal(expectedSwitchInitialLevel, ((LoggingLevelSwitch)actual).MinimumLevel);
        }

        [Fact]
        public void LoggingLevelSwitchIsConfigured()
        {
            var settings = new Dictionary<string, string>
            {
                ["level-switch:Switch1"] = "Information",
                ["minimum-level:controlled-by"] = "Switch1",
            };

            LogEvent evt = null;

            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings)
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            // TODO : maybe add Logger.LevelSwitch as an internal readonly property to avoid reflection here ? 
            var privateSwitch = GetPrivateField<Logger, LoggingLevelSwitch>(log, "_levelSwitch");
            Assert.NotNull(privateSwitch);

            log.Write(Some.DebugEvent());
            Assert.True(evt is null, "LoggingLevelSwitch initial level was information. It should not log Debug messages");

            privateSwitch.MinimumLevel = LogEventLevel.Debug;

            log.Write(Some.DebugEvent());
            Assert.True(evt != null, "LoggingLevelSwitch level was changed to Debug. It should log Debug messages");
        }

        static TField GetPrivateField<T, TField>(T item, string fieldName) where T : class
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var fieldInfo = typeof(T).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo == null) throw new InvalidOperationException($"Could not find private instance field {fieldName} for type {typeof(T)}");

            var fieldValue = fieldInfo.GetValue(item);
            return (TField)fieldValue;
        }

        [Fact]
        public void LoggingLevelSwitchIsPassedToSinks()
        {
            var settings = new Dictionary<string, string>
            {
                ["level-switch:Switch1"] = "Information",
                ["minimum-level:controlled-by"] = "Switch1",
                ["using:TestDummies"] = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName,
                ["write-to:DummyWithLevelSwitch.controlLevelSwitch"] = "Switch1"
            };

            LogEvent evt = null;

            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings)
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            Assert.False(DummyWithLevelSwitchSink.ControlLevelSwitch == null, "Sink ControlLevelSwitch should have been initialized");

            var controlSwitch = DummyWithLevelSwitchSink.ControlLevelSwitch;
            log.Write(Some.DebugEvent());
            Assert.True(evt is null, "LoggingLevelSwitch initial level was information. It should not log Debug messages");

            controlSwitch.MinimumLevel = LogEventLevel.Debug;

            log.Write(Some.DebugEvent());
            Assert.True(evt != null, "LoggingLevelSwitch level was changed to Debug. It should log Debug messages");
        }

    }
}
