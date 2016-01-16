using System.Collections.Generic;
using System.IO;
using Xunit;
using Serilog.Events;

namespace Serilog.Tests.Events
{
    public class DictionaryValueTests
    {
        [Fact]
        public void ADictionaryValueRendersAsMappingOfKeysToValues()
        {
            var dict = new DictionaryValue(new[] {
                new KeyValuePair<ScalarValue, LogEventPropertyValue>(
                    new ScalarValue(1), new ScalarValue("hello")),
                new KeyValuePair<ScalarValue, LogEventPropertyValue>(
                    new ScalarValue("world"), new SequenceValue(new [] { new ScalarValue(1.2)  }))
            });

            var sw = new StringWriter();
            dict.Render(sw);

            var rendered = sw.ToString();

            Assert.Equal("[(1: \"hello\"), (\"world\": [1.2])]", rendered);
        }
    }
}
