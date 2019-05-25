using BenchmarkDotNet.Attributes;
using Serilog.Core.Enrichers;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serilog.PerformanceTests
{
    [MemoryDiagnoser]
    public class AllocationsBenchmark
    {
        readonly ILogger _logger;
        readonly ILogger _enrichedLogger;
        readonly LogEvent _emptyEvent;
        readonly object _dictionaryValue;
        readonly object _anonymousObject;
        readonly object _sequence;

        public AllocationsBenchmark()
        {
            _logger = new LoggerConfiguration().CreateLogger();

            _enrichedLogger = _logger.ForContext(new PropertyEnricher("Prop", "Value"));

            _emptyEvent = new LogEvent(
                DateTimeOffset.Now,
                LogEventLevel.Information,
                null,
                new MessageTemplate(Enumerable.Empty<MessageTemplateToken>()),
                Enumerable.Empty<LogEventProperty>());

            _anonymousObject = new
            {
                Level11 = "Val1",
                Level12 = new
                {
                    Level21 = (int?)42,
                    Level22 = new
                    {
                        Level31 = System.Reflection.BindingFlags.FlattenHierarchy,
                        Level32 = new
                        {
                            X = 3,
                            Y = "4",
                            Z = (short?)5
                        }
                    }
                }
            };

            _dictionaryValue = new Dictionary<string, object> {
                { "Level11", "Val1" },
                { "Level12", new Dictionary<string, object> {
                        { "Level21", (int?)42 },
                        { "Level22", new Dictionary<string, object> {
                                { "Level31", System.Reflection.BindingFlags.FlattenHierarchy },
                                { "Level32", new { X = 3, Y = "4", Z = (short?)5 } }
                            }
                        }
                    }
                }
            };

            _sequence = new List<object> { "1", 2, (int?)3, "4", (short)5 };
        }

        [Benchmark(Baseline = true)]
        public void LogEmpty()
        {
            _logger.Write(_emptyEvent);
        }

        [Benchmark]
        public void LogEmptyWithEnricher()
        {
            _enrichedLogger.Write(_emptyEvent);
        }

        [Benchmark]
        public void LogScalar()
        {
            _logger.Information("Template: {ScalarValue}", "42");
        }

        [Benchmark]
        public void LogDictionary()
        {
            _logger.Information("Template: {DictionaryValue}", _dictionaryValue);
        }

        [Benchmark]
        public void LogSequence()
        {
            _logger.Information("Template: {SequenceValue}", _sequence);
        }

        [Benchmark]
        public void LogAnonymous()
        {
            _logger.Information("Template: {@AnonymousObject}.", _anonymousObject);
        }
    }
}
