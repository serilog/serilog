using System.Collections.Generic;
using System.Linq;
using Serilog.Settings.KeyValuePairs;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Settings
{
    public class CombinedSettingsSourceTests
    {
        [Fact]
        public void BuilderCombinesDifferentSources()
        {
            var source1 = new ConstantSettingsSource(new Dictionary<string, string>
            {
                { "minimum-level", "Error"},
                { "enrich:with-property:Enriched1", "Enrichement1"}
            });

            var source2 = new ConstantSettingsSource(new Dictionary<string, string>
            {
                { "minimum-level", "Information"}
            });

            var source3 = new ConstantSettingsSource(new Dictionary<string, string>
            {
                { "enrich:with-property:Enriched2", "Enrichement2"}
            });

            ISettingsSource combinedSource = new CombinedSettingsSource(new[] { source1, source2, source3 });

            var keyValuePairs = combinedSource.GetKeyValuePairs().ToList();

            var expected = new Dictionary<string, string>()
            {
                { "minimum-level", "Information"},
                { "enrich:with-property:Enriched1", "Enrichement1"},
                { "enrich:with-property:Enriched2", "Enrichement2"},
            }.ToList();

            Assert.Equal(expected, keyValuePairs, new KeyValuePairComparer<string, string>());
        }
    }
}
