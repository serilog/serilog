using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Tests.Support;

namespace Serilog.Tests.Formatting.Json
{
    [TestFixture]
    public class JsonFormatterTests
    {
        [Test]
        public void JsonFormattedEventsIncludeTimestamp()
        {
            var @event = new LogEvent(
                new DateTimeOffset(2013, 3, 11, 15, 59, 0, 123, TimeSpan.FromHours(10)),
                LogEventLevel.Information,
                null,
                Some.MessageTemplate(),
                new LogEventProperty[0]);

            var formatted = FormatJson(@event);
            
            Assert.AreEqual(
                "2013-03-11T15:59:00.1230000+10:00",
                (string)formatted.Timestamp);
        }

        static dynamic FormatJson(LogEvent @event)
        {
            var formatter = new JsonFormatter();
            var output = new StringWriter();            
            formatter.Format(@event, output);

            var serializer = new JsonSerializer { DateParseHandling = DateParseHandling.None };
            return serializer.Deserialize(new JsonTextReader(new StringReader(output.ToString())));
        }

        [Test]
        public void AnIntegerPropertySerializesAsIntegerValue()
        {
            var name = Some.String();
            var value = Some.Int();
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(new LogEventProperty(name, new ScalarValue(value)));

            var formatted = FormatJson(@event);

            Assert.AreEqual(value, (int)formatted.Properties[name]);
        }

        [Test]
        public void ASequencePropertySerializesAsArrayValue()
        {
            var name = Some.String();
            var ints = new[]{ Some.Int(), Some.Int() };
            var value = new SequenceValue(ints.Select(i => new ScalarValue(i)));
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(new LogEventProperty(name, value));

            var formatted = FormatJson(@event);
            var result = new List<int>();
            foreach (var el in formatted.Properties[name])
                result.Add((int)el);

            CollectionAssert.AreEqual(ints, result);
        }

        [Test]
        public void AStructureSerializesAsAnObject()
        {
            var value = Some.Int();
            var memberProp = new LogEventProperty(Some.String(), new ScalarValue(value));
            var structure = new StructureValue(new[] { memberProp });
            var structureProp = new LogEventProperty(Some.String(), structure);
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(structureProp);

            var formatted = FormatJson(@event);
            var result = (int)formatted.Properties[structureProp.Name][memberProp.Name];
            Assert.AreEqual(value, result);
        }

        [Test]
        public void WellKnownSpecialCharactersAreEscapedAsNormal()
        {
            const string s = "\\\"\t\r\n\f";
            var escaped = JsonFormatter.Escape(s);
            Assert.AreEqual("\\\\\\\"\\t\\r\\n\\f", escaped);
        }

        [Test]
        public void StringsWithoutSpecialCharactersAreUnchanged()
        {
            const string s = "Hello, world!";
            var escaped = JsonFormatter.Escape(s);
            Assert.AreSame(s, escaped);
        }

        [Test]
        public void UnprintableCharactersAreEscapedAsUnicodeSequences()
        {
            const string s = "\u0001";
            var escaped = JsonFormatter.Escape(s);
            Assert.AreEqual("\\u0001", escaped);
        }

        [Test]
        public void EmbeddedEscapesPreservePrefixAndSuffix()
        {
            const string s = "a\nb";
            var escaped = JsonFormatter.Escape(s);
            Assert.AreEqual("a\\nb", escaped);
        }

        [Test]
        public void DictionariesAreSerialisedAsObjects()
        {
            var dict = new DictionaryValue(new[] {
                new KeyValuePair<ScalarValue, LogEventPropertyValue>(
                    new ScalarValue(1), new ScalarValue("hello")),
                new KeyValuePair<ScalarValue, LogEventPropertyValue>(
                    new ScalarValue("world"), new SequenceValue(new [] { new ScalarValue(1.2)  }))
            });

            var e = DelegatingSink.GetLogEvent(l => l.Information("Value is {ADictionary}", dict));
            var f = FormatJson(e);

            Assert.AreEqual("hello", (string)f.Properties.ADictionary["1"]);
            Assert.AreEqual(1.2, (double)f.Properties.ADictionary.world[0]);
        }
    }
}
