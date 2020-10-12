using BenchmarkDotNet.Attributes;
using Serilog.Core;
using Serilog.Core.Pipeline;
using Serilog.PerformanceTests.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serilog.PerformanceTests
{
    [MyBenchmarkRun(MyConfigs.ShortRun)]
    public class MessageTemplateCacheBenchmark_Leaking : BaseBenchmark
    {
        const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";
        const int MaxCacheItems = 1000;

        List<string> _templateList;

        [Params(10000)]
        public int Items { get; set; }

        [Params(1, 10, 100, 1000)]
        public int OverflowCount { get; set; }

        [Params(1, -1)]
        public int MaxDegreeOfParallelism { get; set; }

        [GlobalSetup]
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

        void Run<T>(Func<T> cacheFactory)
            where T : IMessageTemplateParser
        {
            var cache = cacheFactory();
            var total = MaxCacheItems + OverflowCount;

            Parallel.For(
                0,
                _templateList.Count,
                new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism },
                idx => cache.Parse(_templateList[idx % total]));
        }
    }
}
