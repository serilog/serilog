using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Xunit;
using Newtonsoft.Json;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Parsing;
using Serilog.Tests.Support;

namespace Serilog.Tests.Formatting.Json
{
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

        static string FormatToJson(LogEvent @event)
        {
            var formatter = new JsonFormatter();
            var output = new StringWriter();
            formatter.Format(@event, output);
            return output.ToString();
        }

        static dynamic FormatJson(LogEvent @event)
        {
            var output = FormatToJson(@event);
            var serializer = new JsonSerializer { DateParseHandling = DateParseHandling.None };
            return serializer.Deserialize(new JsonTextReader(new StringReader(output)));
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
        public void ABooleanPropertySerializesAsBooleanValue()
        {
            var name = Some.String();
            const bool value = true;
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(new LogEventProperty(name, new ScalarValue(value)));

            var formatted = FormatJson(@event);

            Assert.AreEqual(value, (bool)formatted.Properties[name]);
        }

        [Test]
        public void ACharPropertySerializesAsStringValue()
        {
            var name = Some.String();
            const char value = 'c';
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(new LogEventProperty(name, new ScalarValue(value)));

            var formatted = FormatJson(@event);

            Assert.AreEqual(value.ToString(CultureInfo.InvariantCulture), (string)formatted.Properties[name]);
        }

        [Test]
        public void ADecimalSerializesAsNumericValue()
        {
            var name = Some.String();
            const decimal value = 123.45m;
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(new LogEventProperty(name, new ScalarValue(value)));

            var formatted = FormatJson(@event);

            Assert.AreEqual(value, (decimal)formatted.Properties[name]);
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
        public void ADictionaryWithScalarKeySerializesAsAnObject()
        {
            var dictKey = Some.Int();
            var dictValue = Some.Int();
            var dict = new DictionaryValue(new Dictionary<ScalarValue, LogEventPropertyValue> {
                { new ScalarValue(dictKey), new ScalarValue(dictValue) }
            });
            var dictProp = new LogEventProperty(Some.String(), dict);
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(dictProp);

            var formatted = FormatToJson(@event);
            var expected = string.Format("{{\"{0}\":{1}}}", dictKey, dictValue);
            Assert.IsTrue(formatted.Contains(expected));
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
            var dict = new Dictionary<string, object> {
                { "hello", "world" },
                { "nums", new[] { 1.2 } }
            };

            var e = DelegatingSink.GetLogEvent(l => l.Information("Value is {ADictionary}", dict));
            var f = FormatJson(e);

            Assert.AreEqual("world", (string)f.Properties.ADictionary["hello"]);
            Assert.AreEqual(1.2, (double)f.Properties.ADictionary.nums[0]);
        }

        [Test]
        public void PropertyTokensWithFormatStringsAreIncludedAsRenderings()
        {
            var p = new MessageTemplateParser();
            var e = new LogEvent(Some.OffsetInstant(), LogEventLevel.Information, null,
                p.Parse("{AProperty:000}"), new[] { new LogEventProperty("AProperty", new ScalarValue(12)) });

            var d = FormatEvent(e);

            var rs = ((IEnumerable)d.Renderings).Cast<dynamic>().ToArray();
            Assert.AreEqual(1, rs.Count());
            var ap = d.Renderings.AProperty;
            var fs = ((IEnumerable)ap).Cast<dynamic>().ToArray();
            Assert.AreEqual(1, fs.Count());
            Assert.AreEqual("000", (string)fs.Single().Format);
            Assert.AreEqual("012", (string)fs.Single().Rendering);
        }

        static dynamic FormatEvent(LogEvent e)
        {
            var j = new JsonFormatter();

            var f = new StringWriter();
            j.Format(e, f);

            var d = JsonConvert.DeserializeObject<dynamic>(f.ToString());
            return d;
        }

        [Test]
        public void PropertyTokensWithoutFormatStringsAreNotIncludedAsRenderings()
        {
            var p = new MessageTemplateParser();
            var e = new LogEvent(Some.OffsetInstant(), LogEventLevel.Information, null,
                p.Parse("{AProperty}"), new[] { new LogEventProperty("AProperty", new ScalarValue(12)) });

            var d = FormatEvent(e);

            var rs = ((IEnumerable)d.Renderings);
            Assert.IsNull(rs);
        }

        [Test]
        public void SequencesOfSequencesAreSerialized()
        {
            var p = new MessageTemplateParser();
            var e = new LogEvent(Some.OffsetInstant(), LogEventLevel.Information, null,
                p.Parse("{@AProperty}"), new[] { new LogEventProperty("AProperty", new SequenceValue(new[] { new SequenceValue(new[] { new ScalarValue("Hello") }) })) });

            var d = FormatEvent(e);

            var h = (string)d.Properties.AProperty[0][0];
            Assert.AreEqual("Hello", h);
        }
    }
}
