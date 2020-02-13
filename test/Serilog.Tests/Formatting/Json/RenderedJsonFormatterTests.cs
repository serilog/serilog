using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Compact;
using Xunit;
using Serilog.Formatting.Json;

namespace Serilog.Tests.Formatting.Json
{
    public class RenderedJsonFormatterTests
    {
        JObject AssertValidJson(Action<ILogger> act)
        {
            return AssertValidJson(new RenderedJsonFormatter(), act);
        }

        [Fact]
        public void AnEmptyEventIsValidJson()
        {
            AssertValidJson(log => log.Information("No properties"));
        }

        [Fact]
        public void AMinimalEventIsValidJson()
        {
            var jobject = AssertValidJson(log => log.Information("One {Property}", 42));

            JToken m;
            Assert.True(jobject.TryGetValue("Message", out m));
            Assert.Equal("One 42", m.ToObject<string>());

            JToken i;
            Assert.True(jobject.TryGetValue("EventId", out i));
            Assert.Equal(EventIdHash.Compute("One {Property}").ToString("x8"), i.ToObject<string>());

        }

        [Fact]
        public void MultiplePropertiesAreDelimited()
        {
            AssertValidJson(log => log.Information("Property {First} and {Second}", "One", "Two"));
        }

        [Fact]
        public void ExceptionsAreFormattedToValidJson()
        {
            AssertValidJson(log => log.Information(new DivideByZeroException(), "With exception"));
        }

        [Fact]
        public void ExceptionAndPropertiesAreValidJson()
        {
            AssertValidJson(log => log.Information(new DivideByZeroException(), "With exception and {Property}", 42));
        }

        [Fact]
        public void RenderingsAreValidJson()
        {
            AssertValidJson(log => log.Information("One {Rendering:x8}", 42));
        }

        [Fact]
        public void MultipleRenderingsAreDelimited()
        {
            AssertValidJson(log => log.Information("Rendering {First:x8} and {Second:x8}", 1, 2));
        }

        [Fact]
        public void AtPrefixedPropertyNamesAreEscaped()
        {
            // Not possible in message templates, but accepted this way
            var jobject = AssertValidJson(log => log.ForContext("Mistake", 42)
                                                    .Information("Hello"));

            JToken val;
            Assert.True(jobject.TryGetValue("Mistake", out val));
            Assert.Equal(42, val.ToObject<int>());
        }

        public static JObject AssertValidJson(ITextFormatter formatter, Action<ILogger> act)
        {
            var _settings = new JsonSerializerSettings
            {
                DateParseHandling = DateParseHandling.None
            };

            var output = new StringWriter();
            var log = new LoggerConfiguration()
                .WriteTo.Sink(new TextWriterSink(output, formatter))
                .CreateLogger();

            act(log);

            var json = output.ToString();

            // Unfortunately this will not detect all JSON formatting issues; better than nothing however.
            return JsonConvert.DeserializeObject<JObject>(json, _settings);
        }
    }

    public class TextWriterSink : ILogEventSink
    {
        readonly StringWriter _output;
        readonly ITextFormatter _formatter;

        public TextWriterSink(StringWriter output, ITextFormatter formatter)
        {
            _output = output;
            _formatter = formatter;
        }

        public void Emit(LogEvent logEvent)
        {
            _formatter.Format(logEvent, _output);
        }
    }
}
