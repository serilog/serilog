using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;
using FakeItEasy;
using Serilog.Sinks.ElasticSearch;

namespace Serilog.Sinks.Elasticsearch.Tests
{
    public abstract class ElasticsearchSinkTestsBase
    {
        static readonly TimeSpan TinyWait = TimeSpan.FromMilliseconds(50);
        protected readonly IConnection _connection;
        protected readonly ElasticsearchSinkOptions _options;
        protected readonly List<string> _seenHttpPosts = new List<string>();

        protected ElasticsearchSinkTestsBase()
        {
            _connection = A.Fake<IConnection>();
            _options = new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
            {
                BatchPostingLimit  = 2,
                Period = TinyWait,
                Connection = _connection
            };
            var fixedRespone = new MemoryStream(Encoding.UTF8.GetBytes(@"{ ""ok"": true }"));
            A.CallTo(() => _connection.PostSync(A<Uri>._, A<byte[]>._, A<IRequestConfiguration>._))
                .ReturnsLazily((Uri uri, byte[] postData, IRequestConfiguration requestConfiguration) =>
                {
                    _seenHttpPosts.Add(Encoding.UTF8.GetString(postData));
                    return ElasticsearchResponse<Stream>.Create(new ConnectionConfiguration(), 200, "POST", "/", postData, fixedRespone);
                });
        }
    }
}