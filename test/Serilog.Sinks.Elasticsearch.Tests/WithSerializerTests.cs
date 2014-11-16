using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.JsonNet;
using FakeItEasy;
using FluentAssertions;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.ElasticSearch;
using NUnit.Framework;

namespace Serilog.Sinks.Elasticsearch.Tests
{
    [TestFixture]
    public class WithSerializerTests
    {
        static readonly TimeSpan TinyWait = TimeSpan.FromMilliseconds(50);
        private readonly IConnection _connection;
        private readonly ConnectionConfiguration _connectionSettings;
        private readonly List<string> _seenHttpPosts = new List<string>();

        public WithSerializerTests()
        {
            _connection = A.Fake<IConnection>();
            _connectionSettings = new ConnectionConfiguration(new Uri("http://localhost:9200"));
            var fixedRespone = new MemoryStream(Encoding.UTF8.GetBytes(@"{ ""ok"": true }"));
            A.CallTo(() => _connection.PostSync(A<Uri>._, A<byte[]>._, A<IRequestConfiguration>._))
                .ReturnsLazily((Uri uri, byte[] postData, IRequestConfiguration requestConfiguration) =>
                {
                    _seenHttpPosts.Add(Encoding.UTF8.GetString(postData));
                    return ElasticsearchResponse<Stream>.Create(_connectionSettings, 200, "POST", "/", postData, fixedRespone);
                });
        }

        [Test]
        public void WhenSendingScalarValue_EnsureItsSendAsJson()
        {
            //DO NOTE that you cant send objects as scalar values through Logger.*("{Scalar}", {});
            var timestamp = new DateTimeOffset(2013, 05, 28, 22, 10, 20, 666, TimeSpan.FromHours(10));
            var messageTemplate = "{Song}++ @{Complex}";
            var template = new MessageTemplateParser().Parse(messageTemplate);
            using (var sink = new ElasticSearchSink(_connectionSettings, ElasticSearchSink.DefaultIndexFormat, 2, TinyWait, null, _connection))
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
            bulkJsonPieces[0].Should().Contain(@"""_index"":""logstash-2013.05.28");
            bulkJsonPieces[1].Should().Contain("New Macabre");
            bulkJsonPieces[2].Should().Contain(@"""_index"":""logstash-2011.05.28");
            bulkJsonPieces[3].Should().Contain("Old Macabre");

            //serilog by default simpy .ToString()'s unknown objects
            bulkJsonPieces[3].Should().Contain("Complex\":\"{");

        }
    }

}
