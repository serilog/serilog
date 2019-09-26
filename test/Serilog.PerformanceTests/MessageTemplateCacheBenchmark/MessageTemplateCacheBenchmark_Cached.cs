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
    [ShortRunJob]
    public class MessageTemplateCacheBenchmark_Cached
    {
        const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";

        List<string> _templateList;

        ConcurrentDictionaryMessageTemplateCache _concurrentCache;
        DictionaryMessageTemplateCache _dictionaryCache;
        MessageTemplateCache _hashtableCache;

        [Params(10, 20, 50, 100, 1000)]
        public int Items { get; set; }

        [Params(1, -1)]
        public int MaxDegreeOfParallelism { get; set; }

        public MessageTemplateCacheBenchmark_Cached()
        {
            _templateList = Enumerable.Range(0, Items).Select(x => $"{DefaultOutputTemplate}_{Guid.NewGuid()}").ToList();

            _concurrentCache = new ConcurrentDictionaryMessageTemplateCache(NoOpMessageTemplateParser.Instance);
            _dictionaryCache = new DictionaryMessageTemplateCache(NoOpMessageTemplateParser.Instance);
            _hashtableCache = new MessageTemplateCache(NoOpMessageTemplateParser.Instance);

            foreach (var t in _templateList)
            {
                _concurrentCache.Parse(t);
                _dictionaryCache.Parse(t);
                _hashtableCache.Parse(t);
            }
        }

        [Benchmark(Baseline = true)]
        public void Dictionary()
        {
            Run(() => _dictionaryCache);
        }

        [Benchmark]
        public void Hashtable()
        {
            Run(() => _hashtableCache);
        }

        [Benchmark]
        public void Concurrent()
        {
            Run(() => _concurrentCache);
        }

        void Run<T>(Func<T> cacheFactory)
            where T : IMessageTemplateParser
        {
            var cache = cacheFactory();
            var iterations = Math.Min(10000, Items * 100);

            Parallel.For(
                0,
                iterations,
                new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism },
                idx => cache.Parse(_templateList[idx % Items]));
        }
    }
}
