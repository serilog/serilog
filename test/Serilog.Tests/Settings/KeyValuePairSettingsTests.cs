using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Serilog.Events;
using Serilog.Settings.KeyValuePairs;
using Serilog.Tests.Support;
using Serilog.Enrichers;
using TestDummies;
using Serilog.Configuration;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using Serilog.Tests.Formatting.Json;

namespace Serilog.Tests.Settings
{
    public class KeyValuePairSettingsTests
    {
        [Fact]
        public void ConvertibleValuesConvertToTIfTargetIsNullable()
        {
            var result = (int?)KeyValuePairSettings.ConvertToType("3", typeof(int?));
            Assert.True(result == 3);
        }

        [Fact]
        public void NullValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)KeyValuePairSettings.ConvertToType(null, typeof(int?));
            Assert.True(result == null);
        }

        [Fact]
        public void EmptyStringValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)KeyValuePairSettings.ConvertToType("", typeof(int?));
            Assert.True(result == null);
        }

        [Fact]
        public void ValuesConvertToNullableTimeSpan()
        {
            var result = (System.TimeSpan?)KeyValuePairSettings.ConvertToType("00:01:00", typeof(System.TimeSpan?));
            Assert.Equal(System.TimeSpan.FromMinutes(1), result);
        }

        [Fact]
        public void ValuesConvertToEnumMembers()
        {
            var result = (LogEventLevel)KeyValuePairSettings.ConvertToType("Information", typeof(LogEventLevel));
            Assert.Equal(LogEventLevel.Information, result);
        }

        [Fact]
        public void FindsConfigurationAssemblies()
        {
            var configurationAssemblies = KeyValuePairSettings.LoadConfigurationAssemblies(new Dictionary<string, string>()).ToList();

            // The core Serilog assembly is always considered
            Assert.Equal(1, configurationAssemblies.Count);
        } 

        [Fact]
        public void FindsEventEnrichersWithinAnAssembly()
        {
            var eventEnrichers = KeyValuePairSettings
                .FindEventEnricherConfigurationMethods(new[]
                {
                    typeof(Log).GetTypeInfo().Assembly,
                    typeof(DummyThreadIdEnricher).GetTypeInfo().Assembly
                })
                .Select(m => m.Name)
                .Distinct()
                .ToList();

            
            Assert.True(eventEnrichers.Contains("FromLogContext"));
            Assert.True(eventEnrichers.Contains("WithDummyThreadId"));
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
        public void StringValuesConvertToDefaultInstancesIfTargetIsInterface()
        {
            var result = (object)KeyValuePairSettings.ConvertToType("Serilog.Formatting.Json.JsonFormatter", typeof(ITextFormatter));
            Assert.IsType<JsonFormatter>(result);
        }

        [Fact]
        public void CallableMethodsAreSelected()
        {
            var options = GetDummyRollingFileConfigurationMethods(typeof(LoggerSinkConfiguration));
            Assert.Equal(2, options.Count(mi => mi.Name == "DummyRollingFile"));
            var suppliedArguments = new[]
            {
                new KeyValuePairSettings.MethodArgumentValue { Method = "DummyRollingFile", Argument = "pathFormat", Value = "C:\\" },
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
                new KeyValuePairSettings.MethodArgumentValue { Method = "DummyRollingFile", Argument = "pathFormat", Value = "C:\\" },
                new KeyValuePairSettings.MethodArgumentValue { Method = "DummyRollingFile", Argument = "formatter", Value = "SomeFormatter, SomeAssembly" }
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
        public void TestMinimumLevelOverrides() {
            var settings = new Dictionary<string, string>
            {
                ["minimum-level:override:Microsoft"] = "Warning",
            };

            LogEvent evt = null;

            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings)
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            var microsoftLogger = log.ForContext<Microsoft.Extensions.Testing.Abstractions.FullPdbReader>();
            microsoftLogger.Write(Some.InformationEvent());

            Assert.Null(evt);

            microsoftLogger.Warning("Bad things");
            Assert.NotNull(evt);

            evt = null;
            log.Write(Some.InformationEvent());
            Assert.NotNull(evt);
        }
    }
}