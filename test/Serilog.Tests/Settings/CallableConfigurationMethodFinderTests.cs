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
        [Fact]
        public void FindsEnricherSpecificConfigurationMethods()
        {
            var eventEnrichers = CallableConfigurationMethodFinder
                .FindConfigurationMethods(new[]
                {
                    typeof(Log).GetTypeInfo().Assembly,
                    typeof(DummyThreadIdEnricher).GetTypeInfo().Assembly
                }, typeof(LoggerEnrichmentConfiguration))
                .Select(m => m.Name)
                .Distinct()
                .ToList();


            Assert.Contains("FromLogContext", eventEnrichers);
            Assert.Contains("WithDummyThreadId", eventEnrichers);
        }

        [Fact]
        public void FindsDestructureSpecificConfigurationMethods()
        {
            var destructuringMethods = CallableConfigurationMethodFinder
                .FindConfigurationMethods(new[]
                {
                    typeof(Log).GetTypeInfo().Assembly,
                    typeof(DummyThreadIdEnricher).GetTypeInfo().Assembly,
                }, typeof(LoggerDestructuringConfiguration))
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
    }
}
