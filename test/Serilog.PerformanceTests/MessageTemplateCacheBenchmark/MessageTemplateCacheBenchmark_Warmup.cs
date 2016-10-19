using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Serilog.Core;
using Serilog.Core.Pipeline;
using Serilog.PerformanceTests.Support;

namespace Serilog.PerformanceTests
{
    public class MessageTemplateCacheBenchmark_Warmup
    {
        const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";

        List<string> _templateList;

        [Params(10, 100, 1000, 10000)]
        public int Items { get; set; }

        [Params(1, -1)]
        public int MaxDegreeOfParallelism { get; set; }

        [Setup]
        public void Setup()
        {
            _templateList = Enumerable.Range(0, Items).Select(x => $"{DefaultOutputTemplate}_{Guid.NewGuid()}").ToList();
        }

        [Benchmark(Baseline = true)]
        public void Dictionary()
        {
            Run(() => new DictionaryMessageTemplateCache(NoOpMessageTemplateParser.Instance));
        }

        [Benchmark]
        public void Hashtable()
        {
            Run(() => new MessageTemplateCache(NoOpMessageTemplateParser.Instance));
        }

        [Benchmark]
        public void Concurrent()
        {
            Run(() => new ConcurrentDictionaryMessageTemplateCache(NoOpMessageTemplateParser.Instance));
        }

        void Run<T>(Func<T> cacheFactory) where T : IMessageTemplateParser
        {
            var cache = cacheFactory();

            Parallel.ForEach(
                _templateList,
                new ParallelOptions() { MaxDegreeOfParallelism = MaxDegreeOfParallelism },
                t => cache.Parse(t));
        }
    }
}