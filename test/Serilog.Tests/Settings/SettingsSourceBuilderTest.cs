using System.Collections.Generic;
using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Settings
{
    public class SettingsSourceBuilderTest
    {
        [Fact]
        public void CombinedCanMergeMultipleKeyValuePairLists()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.Combined(builder => builder
                    .AddKeyValuePairs(new Dictionary<string, string>
                    {
                        {"enrich:with-property:UntouchedProp", "initialValue"},
                        {"enrich:with-property:OverridenProp", "initialValue"},
                    })
                    .AddKeyValuePairs(new Dictionary<string, string>
                    {
                        {"enrich:with-property:OverridenProp", "overridenValue"},
                        {"enrich:with-property:NewProp", "value"},
                    }))
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("a message that should be enriched with properties");

            Assert.NotNull(evt);
            Assert.Equal("initialValue", evt.Properties["UntouchedProp"].LiteralValue());
            Assert.Equal("overridenValue", evt.Properties["OverridenProp"].LiteralValue());
            Assert.Equal("value", evt.Properties["NewProp"].LiteralValue());
        }

        [Fact]
        public void CombinedCanMergeMultipleKeyValuePairs()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.Combined(builder => builder
                    .AddKeyValuePair("enrich:with-property:UntouchedProp", "initialValue")
                    .AddKeyValuePair("enrich:with-property:OverridenProp", "initialValue")
                    .AddKeyValuePair("enrich:with-property:NewProp", "value")
                    .AddKeyValuePair(new KeyValuePair<string, string>("enrich:with-property:OverridenProp", "overridenValue"))
                    )
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            log.Information("a message that should be enriched with properties");

            Assert.NotNull(evt);
            Assert.Equal("initialValue", evt.Properties["UntouchedProp"].LiteralValue());
            Assert.Equal("overridenValue", evt.Properties["OverridenProp"].LiteralValue());
            Assert.Equal("value", evt.Properties["NewProp"].LiteralValue());
        }
    }
}
