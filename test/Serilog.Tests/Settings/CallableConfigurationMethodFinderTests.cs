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
    }
}
