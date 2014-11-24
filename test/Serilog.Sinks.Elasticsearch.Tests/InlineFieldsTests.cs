using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.ElasticSearch;
using NUnit.Framework;

namespace Serilog.Sinks.Elasticsearch.Tests
{
    [TestFixture]
    public class InlineFieldsTests : ElasticsearchSinkTestsBase
    {

        [Test]
        public async Task UsesCustomPropertyNames()
        {
            try
            {
                await new HttpClient().GetStringAsync("http://i.do.not.exist");
            }
            catch (Exception e)
            {
                var timestamp = new DateTimeOffset(2013, 05, 28, 22, 10, 20, 666, TimeSpan.FromHours(10));
                var messageTemplate = "{Song}++";
                var template = new MessageTemplateParser().Parse(messageTemplate);
                _options.InlineFields = true;
                using (var sink = new ElasticsearchSink(_options))
                {
                    var properties = new List<LogEventProperty>
					{
						new LogEventProperty("Song", new ScalarValue("New Macabre")),
					};
                    var logEvent = new LogEvent(timestamp, LogEventLevel.Information, e, template, properties);
                    sink.Emit(logEvent);
                    logEvent = new LogEvent(timestamp.AddDays(2), LogEventLevel.Information, e, template, properties);
                    sink.Emit(logEvent);
                }
                _seenHttpPosts.Should().NotBeEmpty().And.HaveCount(1);
                var json = _seenHttpPosts.First();
                var bulkJsonPieces = json.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                bulkJsonPieces.Should().HaveCount(4);
                bulkJsonPieces[0].Should().Contain(@"""_index"":""logstash-2013.05.28");
                bulkJsonPieces[1].Should().Contain("New Macabre");
                bulkJsonPieces[1].Should().NotContain("Properties\"");
                bulkJsonPieces[1].Should().NotContain("fields\":{");
                bulkJsonPieces[1].Should().Contain("@timestamp");
                bulkJsonPieces[1].Should().Contain("Song\":");
                bulkJsonPieces[1].Should().EndWith("New Macabre\"}");
                bulkJsonPieces[2].Should().Contain(@"""_index"":""logstash-2013.05.30");
            }
        }
    }

}
