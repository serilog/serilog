using System.Collections.Generic;
using System.Linq;
using Serilog.Events;
using Serilog.Settings.KeyValuePairs;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Settings
{
    public class SettingsSourceTests
    {
        [Fact]
        public void KeyValuePairsCanReadKeyValuePairsFromASingleSource()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(new ConstantSettingsSource(
                    new Dictionary<string, string>
                    {
                        {"enrich:with-property:SomeProp", "initialValue"},
                    }))
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("a message that should be enriched with properties");

            Assert.NotNull(evt);
            Assert.Equal("initialValue", evt.Properties["SomeProp"].LiteralValue());
        }

        [Fact]
        public void KeyValuePairsCanReadKeyValuePairsFromFromMultipleSources()
        {
            LogEvent evt = null;
            var source1 = new ConstantSettingsSource(
                new Dictionary<string, string>
                {
                    {"enrich:with-property:UntouchedProp", "initialValue"},
                    {"enrich:with-property:OverridenProp", "initialValue"},
                });
            var source2 = new ConstantSettingsSource(
                new Dictionary<string, string>
                {
                    {"enrich:with-property:OverridenProp", "overridenValue"},
                    {"enrich:with-property:NewProp", "value"},
                });
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(source1, source2)
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("a message that should be enriched with properties");

            Assert.NotNull(evt);
            Assert.Equal("initialValue", evt.Properties["UntouchedProp"].LiteralValue());
            Assert.Equal("overridenValue", evt.Properties["OverridenProp"].LiteralValue());
            Assert.Equal("value", evt.Properties["NewProp"].LiteralValue());
        }

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
