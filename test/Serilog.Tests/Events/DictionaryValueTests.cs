using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Serilog.Events;

namespace Serilog.Tests.Events
{
    [TestFixture]
    public class DictionaryValueTests
    {
        [Test]
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

            Assert.AreEqual("[(1: \"hello\"), (\"world\": [1.2])]", rendered);
        }
    }
}
