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

    }
}
