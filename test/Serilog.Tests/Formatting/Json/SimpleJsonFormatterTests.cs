using System;
using System.IO;
using NUnit.Framework;
using Newtonsoft.Json;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Tests.Support;

namespace Serilog.Tests.Formatting.Json
{
    [TestFixture]
    public class SimpleJsonFormatterTests
    {
        [Test]
        public void JsonFormattedEventsIncludeTimeStamp()
        {
            var @event = new LogEvent(
                new DateTimeOffset(2013, 3, 11, 15, 59, 0, 123, TimeSpan.FromHours(10)),
                LogEventLevel.Information,
                null,
                Some.String(),
                new LogEventProperty[0]);

            var formatted = FormatJson(@event);
            
            Assert.AreEqual(
                "2013-03-11T15:59:00.1230000+10:00",
                (string)formatted.TimeStamp);
        }

        static dynamic FormatJson(LogEvent @event)
        {
            var formatter = new SimpleJsonFormatter();
            var output = new StringWriter();            
            formatter.Format(@event, output);

            var serializer = new JsonSerializer { DateParseHandling = DateParseHandling.None };
            return serializer.Deserialize(new JsonTextReader(new StringReader(output.ToString())));
        }
    }
}