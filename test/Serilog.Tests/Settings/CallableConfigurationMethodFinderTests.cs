using System.Collections.Generic;
using Serilog.Configuration;
using Serilog.Settings.KeyValuePairs;
using System.Linq;
using System.Reflection;
using TestDummies;
using Xunit;

namespace Serilog.Tests.Settings
{
    public class CallableConfigurationMethodFinderTests
    {
        static readonly Assembly SerilogAssembly = typeof(Log).GetTypeInfo().Assembly;
        static readonly Assembly TestDummiesAssembly = typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly;

        [Fact]
        public void FindsSinkSpecificConfigurationMethods()
        {
            var searchInAssemblies = new[] { SerilogAssembly, TestDummiesAssembly };

            var sinkMethods = CallableConfigurationMethodFinder
                .FindConfigurationMethods(
                    searchInAssemblies,
                    typeof(LoggerSinkConfiguration))
                .Select(m => m.Name)
                .Distinct()
                .ToList();

            Assert.Contains(nameof(LoggerSinkConfiguration.Sink), sinkMethods);
        }

        [Fact]
        public void FindsAuditSinkSpecificConfigurationMethods()
        {
            var searchInAssemblies = new[] { SerilogAssembly, TestDummiesAssembly };

            var auditSinkMethods = CallableConfigurationMethodFinder
                .FindConfigurationMethods(
                    searchInAssemblies,
                    typeof(LoggerAuditSinkConfiguration))
                .Select(m => m.Name)
                .Distinct()
                .ToList();

            Assert.Contains(nameof(LoggerAuditSinkConfiguration.Sink), auditSinkMethods);
        }

        [Fact]
        public void FindsEnricherSpecificConfigurationMethods()
        {
            var searchInAssemblies = new[] { SerilogAssembly, TestDummiesAssembly };

            var enricherMethods = CallableConfigurationMethodFinder
                .FindConfigurationMethods(
                    searchInAssemblies,
                    typeof(LoggerEnrichmentConfiguration))
                .Select(m => m.Name)
                .Distinct()
                .ToList();

            Assert.Contains(nameof(LoggerEnrichmentConfiguration.With), enricherMethods);
            Assert.Contains(nameof(LoggerEnrichmentConfiguration.FromLogContext), enricherMethods);
            Assert.Contains(nameof(DummyLoggerConfigurationExtensions.WithDummyThreadId), enricherMethods);
        }

        [Fact]
        public void FindsDestructureSpecificConfigurationMethods()
        {
            var searchInAssemblies = new[] { SerilogAssembly, TestDummiesAssembly };

            var destructuringMethods = CallableConfigurationMethodFinder
                .FindConfigurationMethods(
                    searchInAssemblies,
                    typeof(LoggerDestructuringConfiguration))
                .Select(m => m.Name)
                .Distinct()
                .ToList();

            Assert.Contains(nameof(LoggerDestructuringConfiguration.AsScalar), destructuringMethods);
            Assert.Contains(nameof(LoggerDestructuringConfiguration.ToMaximumCollectionCount), destructuringMethods);
            Assert.Contains(nameof(LoggerDestructuringConfiguration.ToMaximumDepth), destructuringMethods);
            Assert.Contains(nameof(LoggerDestructuringConfiguration.ToMaximumStringLength), destructuringMethods);
            Assert.Contains(nameof(DummyLoggerConfigurationExtensions.WithDummyHardCodedString), destructuringMethods);
            Assert.Contains(nameof(LoggerDestructuringConfiguration.With), destructuringMethods);
        }

        [Fact]
        public void FindsFilterSpecificConfigurationMethods()
        {
            var searchInAssemblies = new[] { SerilogAssembly, TestDummiesAssembly };

            var filterMethods = CallableConfigurationMethodFinder
                .FindConfigurationMethods(
                    searchInAssemblies,
                    typeof(LoggerFilterConfiguration))
                .Select(m => m.Name)
                .Distinct()
                .ToList();

            Assert.Contains(nameof(LoggerFilterConfiguration.With), filterMethods);
        }
    }
}
