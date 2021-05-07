using Newtonsoft.Json;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Parsing;
using Serilog.Tests.Support;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Serilog.Tests.Formatting.Json
{
    public class JsonFormatterTests
    {
        [Fact]
        public void JsonFormattedEventsIncludeTimestamp()
        {
            LogEvent @event = new(
                new DateTimeOffset(2013, 3, 11, 15, 59, 0, 123, TimeSpan.FromHours(10)),
                LogEventLevel.Information,
                null,
                Some.MessageTemplate(),
                new LogEventProperty[0]);

            var formatted = FormatJson(@event);

            Assert.Equal(
                "2013-03-11T15:59:00.1230000+10:00",
                (string)formatted.Timestamp);
        }

        static string FormatToJson(LogEvent @event)
        {
            JsonFormatter formatter = new();
            StringWriter output = new();
            formatter.Format(@event, output);
            return output.ToString();
        }

        static dynamic FormatJson(LogEvent @event)
        {
            var output = FormatToJson(@event);
            JsonSerializer serializer = new() { DateParseHandling = DateParseHandling.None };
            return serializer.Deserialize(new JsonTextReader(new StringReader(output)));
        }

        [Fact]
        public void AnIntegerPropertySerializesAsIntegerValue()
        {
            var name = Some.String();
            var value = Some.Int();
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(new LogEventProperty(name, new ScalarValue(value)));

            var formatted = FormatJson(@event);

            Assert.Equal(value, (int)formatted.Properties[name]);
        }

        [Fact]
        public void ABooleanPropertySerializesAsBooleanValue()
        {
            var name = Some.String();
            const bool value = true;
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(new LogEventProperty(name, new ScalarValue(value)));

            var formatted = FormatJson(@event);

            Assert.Equal(value, (bool)formatted.Properties[name]);
        }

        [Fact]
        public void ACharPropertySerializesAsStringValue()
        {
            var name = Some.String();
            const char value = 'c';
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(new LogEventProperty(name, new ScalarValue(value)));

            var formatted = FormatJson(@event);

            Assert.Equal(value.ToString(), (string)formatted.Properties[name]);
        }

        [Fact]
        public void ADecimalSerializesAsNumericValue()
        {
            var name = Some.String();
            const decimal value = 123.45m;
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(new LogEventProperty(name, new ScalarValue(value)));

            var formatted = FormatJson(@event);

            Assert.Equal(value, (decimal)formatted.Properties[name]);
        }

        [Fact]
        public void ASequencePropertySerializesAsArrayValue()
        {
            var name = Some.String();
            var ints = new[]{ Some.Int(), Some.Int() };
            SequenceValue value = new(ints.Select(i => new ScalarValue(i)));
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(new LogEventProperty(name, value));

            var formatted = FormatJson(@event);
            List<int> result = new();
            foreach (var el in formatted.Properties[name])
                result.Add((int)el);

            Assert.Equal(ints, result);
        }

        [Fact]
        public void AStructureSerializesAsAnObject()
        {
            var value = Some.Int();
            LogEventProperty memberProp = new(Some.String(), new ScalarValue(value));
            StructureValue structure = new(new[] { memberProp });
            LogEventProperty structureProp = new(Some.String(), structure);
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(structureProp);

            var formatted = FormatJson(@event);
            var result = (int)formatted.Properties[structureProp.Name][memberProp.Name];
            Assert.Equal(value, result);
        }

        [Fact]
        public void ADictionaryWithScalarKeySerializesAsAnObject()
        {
            var dictKey = Some.Int();
            var dictValue = Some.Int();
            DictionaryValue dict = new(new Dictionary<ScalarValue, LogEventPropertyValue> {
                { new ScalarValue(dictKey), new ScalarValue(dictValue) }
            });
            LogEventProperty dictProp = new(Some.String(), dict);
            var @event = Some.InformationEvent();
            @event.AddOrUpdateProperty(dictProp);

            var formatted = FormatToJson(@event);
            var expected = $"{{\"{dictKey}\":{dictValue}}}";
            Assert.Contains(expected, formatted);
        }

        [Fact]
        public void LegacyEscapeMethodDelegatesCorrectly()
        {
            const string s = "\\\"\t\r\n\f";
#pragma warning disable CS0618 // Type or member is obsolete
            var escaped = JsonFormatter.Escape(s);
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.Equal("\\\\\\\"\\t\\r\\n\\f", escaped);
        }

        [Fact]
        public void DictionariesAreDestructuredViaDictionaryValue()
        {
            Dictionary<string, object> dict = new()
            {
                { "hello", "world" },
                { "nums", new[] { 1.2 } }
            };

            var e = DelegatingSink.GetLogEvent(l => l.Information("Value is {ADictionary}", dict));
            var f = FormatJson(e);

            Assert.Equal("world", (string)f.Properties.ADictionary["hello"]);
            Assert.Equal(1.2, (double)f.Properties.ADictionary.nums[0]);
        }

        [Fact]
        public void PropertyTokensWithFormatStringsAreIncludedAsRenderings()
        {
            MessageTemplateParser p = new();
            LogEvent e = new(Some.OffsetInstant(), LogEventLevel.Information, null,
                p.Parse("{AProperty:000}"), new[] { new LogEventProperty("AProperty", new ScalarValue(12)) });

            var d = FormatEvent(e);

            var rs = ((IEnumerable)d.Renderings).Cast<dynamic>().ToArray();
            Assert.Single(rs);
            var ap = d.Renderings.AProperty;
            var fs = ((IEnumerable)ap).Cast<dynamic>().ToArray();
            Assert.Single(fs);
            Assert.Equal("000", (string)fs.Single().Format);
            Assert.Equal("012", (string)fs.Single().Rendering);
        }

        static dynamic FormatEvent(LogEvent e)
        {
            JsonFormatter j = new();

            StringWriter f = new();
            j.Format(e, f);

            return JsonConvert.DeserializeObject<dynamic>(f.ToString());
        }

        [Fact]
        public void PropertyTokensWithoutFormatStringsAreNotIncludedAsRenderings()
        {
            MessageTemplateParser p = new();
            LogEvent e = new(Some.OffsetInstant(), LogEventLevel.Information, null,
                p.Parse("{AProperty}"), new[] { new LogEventProperty("AProperty", new ScalarValue(12)) });

            var d = FormatEvent(e);

            var rs = (IEnumerable)d.Renderings;
            Assert.Null(rs);
        }

        [Fact]
        public void SequencesOfSequencesAreSerialized()
        {
            MessageTemplateParser p = new();
            LogEvent e = new(Some.OffsetInstant(), LogEventLevel.Information, null,
                p.Parse("{@AProperty}"), new[] { new LogEventProperty("AProperty", new SequenceValue(new[] { new SequenceValue(new[] { new ScalarValue("Hello") }) })) });

            var d = FormatEvent(e);

            var h = (string)d.Properties.AProperty[0][0];
            Assert.Equal("Hello", h);
        }
    }
}
