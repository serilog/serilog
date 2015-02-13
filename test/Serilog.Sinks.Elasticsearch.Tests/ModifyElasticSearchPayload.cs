using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.ElasticSearch;

namespace Serilog.Sinks.Elasticsearch.Tests
{
    public class ModifyElasticSearchPayload : ElasticsearchSinkTestsBase
    {
        [Test]
        public void IndexDecider_EndsUpInTheOutput()
        {
            //DO NOTE that you cant send objects as scalar values through Logger.*("{Scalar}", {});
            var timestamp = new DateTimeOffset(2013, 05, 28, 22, 10, 20, 666, TimeSpan.FromHours(10));
            const string messageTemplate = "{Song}++ @{Complex}";
            var template = new MessageTemplateParser().Parse(messageTemplate);
            _options.IndexDecider = (l, utcTime) => string.Format("logstash-{1}-{0:yyyy.MM.dd}", utcTime, l.Level.ToString().ToLowerInvariant());
            using (var sink = new ElasticsearchSink(_options))
            {
                var properties = new List<LogEventProperty> { new LogEventProperty("Song", new ScalarValue("New Macabre")) };
                var e = new LogEvent(timestamp, LogEventLevel.Information, null, template, properties);
                sink.Emit(e);
                var exception = new ArgumentException("parameter");
                properties = new List<LogEventProperty>
				{
					new LogEventProperty("Song", new ScalarValue("Old Macabre")),
					new LogEventProperty("Complex", new ScalarValue(new { A  = 1, B = 2}))
				};
                e = new LogEvent(timestamp.AddYears(-2), LogEventLevel.Fatal, exception, template, properties);
                sink.Emit(e);
            }

            _seenHttpPosts.Should().NotBeEmpty().And.HaveCount(1);
            var json = _seenHttpPosts.First();
            var bulkJsonPieces = json.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            bulkJsonPieces.Should().HaveCount(4);
            bulkJsonPieces[0].Should().Contain(@"""_index"":""logstash-information-2013.05.28");
            bulkJsonPieces[1].Should().Contain("New Macabre");
            bulkJsonPieces[2].Should().Contain(@"""_index"":""logstash-fatal-2011.05.28");
            bulkJsonPieces[3].Should().Contain("Old Macabre");

            //serilog by default simpy .ToString()'s unknown objects
            bulkJsonPieces[3].Should().Contain("Complex\":\"{");

        } 
    }
}