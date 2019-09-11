using BenchmarkDotNet.Attributes;
using Serilog.Core.Enrichers;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Serilog.PerformanceTests
{
    [MemoryDiagnoser]
    public abstract class AllocationsBaseBenchmark
    {
        protected ILogger _logger;
        protected ILogger _enrichedLogger;

        readonly LogEvent _emptyEvent = new LogEvent(
                DateTimeOffset.Now,
                LogEventLevel.Information,
                null,
                new MessageTemplate(Enumerable.Empty<MessageTemplateToken>()),
                Enumerable.Empty<LogEventProperty>());

        readonly object _dictionaryValue = new Dictionary<string, object>
            {
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

        readonly object _anonymousObject = new
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

        readonly object _sequence = new List<object> { "1", 2, (int?)3, "4", (short)5 };

        readonly BigTestStruct _bigStruct = new BigTestStruct() //don't cast to object to not have any boxing here.
        {
            Active = true,
            Count = 918217217261726172L,
            Lat = -42,
            Long = 42,
            Points = new float[10][]
            {
                new float[10] {0,1,2,3,4,5,6,7,8,9,},
                new float[10] {1,2,3,4,5,6,7,8,9,0,},
                new float[10] {2,3,4,5,6,7,8,9,0,1,},
                new float[10] {3,4,5,6,7,8,9,0,1,2,},
                new float[10] {4,5,6,7,8,9,0,1,2,3,},
                new float[10] {5,6,7,8,9,0,1,2,3,4,},
                new float[10] {6,7,8,9,0,1,2,3,4,5,},
                new float[10] {7,8,9,0,1,2,3,4,5,6,},
                new float[10] {8,9,0,1,2,3,4,5,6,7,},
                new float[10] {9,0,1,2,3,4,5,6,7,8,},
            },
        };

        readonly Exception _exception = new ApplicationException("An Error");

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
        public void LogMsg()
        {
            _logger.Information("Template:");
        }

        [Benchmark]
        public void LogMsgWithEx()
        {
            _logger.Information(_exception, "Template:");
        }

        [Benchmark]
        public void LogScalar1()
        {
            _logger.Information("Template: {ScalarValue}", "42");
        }
        [Benchmark]
        public void LogScalar2()
        {
            _logger.Information("Template: {ScalarValue1},{ScalarValue2}", "42", "7");
        }
        [Benchmark]
        public void LogScalar3()
        {
            _logger.Information("Template: {ScalarValue1},{ScalarValue2},{ScalarValue3}", "42", "7", "108");
        }
        [Benchmark]
        public void LogScalarMany()
        {
            _logger.Information("Template: {ScalarValue1},{ScalarValue2},{ScalarValue3},{ScalarValue4}", "42", "7", "108", "1024");
        }
        
        [Benchmark]
        public void LogScalarStruct1()
        {
            _logger.Information("Template: {ScalarStructValue}", 42);
        }
        [Benchmark]
        public void LogScalarStruct2()
        {
            _logger.Information("Template: {ScalarStructValue1},{ScalarStructValue2}", 42, 7);
        }
        [Benchmark]
        public void LogScalarStruct3()
        {
            _logger.Information("Template: {ScalarStructValue1},{ScalarStructValue2},{ScalarStructValue3}", 42, 7, 108);
        }
        [Benchmark]
        public void LogScalarStructMany()
        {
            _logger.Information("Template: {ScalarStructValue1},{ScalarStructValue2},{ScalarStructValue3},{ScalarStructValue4}", 42, 7, 108, 1024);
        }
        
        [Benchmark]
        public void LogScalarBigStruct()
        {
            _logger.Information("Template: {ScalarBigStructValue}", _bigStruct);
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
        
        [Benchmark]
        public void LogMix2()
        {
            _logger.Information("Template: {Value1},{Value2}", "42", 7);
        }
        [Benchmark]
        public void LogMix3()
        {
            _logger.Information("Template: {Value1},{Value2},{Value3}", "42", 7, 108L);
        }
        [Benchmark]
        public void LogMix4()
        {
            _logger.Information("Template: {Value1},{Value2},{Value3},{Value4}", "42", 7, 108L, 1024M);
        }
        [Benchmark]
        public void LogMix5()
        {
            _logger.Information("Template: {Value1},{Value2},{Value3},{Value4},{Value5}", "42", 7, 108L, 1024M, (short?)-11);
        }
        [Benchmark]
        public void LogMixMany()
        {
            _logger.Information("Template: {Value1},{Value2},{Value3},{Value4},{Value5},{Value6},{Value7},{Value8},{@Value9}", "42", 7, 108L, 1024M, (short?)-11, _bigStruct, _dictionaryValue, _sequence, _anonymousObject);
        }

        [Benchmark]
        public bool LogAll()
        {
            var x = _logger.IsEnabled(LogEventLevel.Debug);
            _logger.Write(_emptyEvent);
            _logger.Information("Template:");
            _logger.Information("Template: {ScalarStructValue}", 42);
            _logger.Information("Template: {ScalarStructValue1},{ScalarStructValue2},{ScalarStructValue3}", 42, 7, 108);
            _logger.Information("Template: {ScalarBigStructValue}", _bigStruct);
            _logger.Information("Template: {ScalarValue}", "42");
            _logger.Information("Template: {ScalarValue1},{ScalarValue2},{ScalarValue3}", "42", "7", "108");
            _logger.Information("Template: {DictionaryValue}", _dictionaryValue);
            _logger.Information("Template: {SequenceValue}", _sequence);
            _logger.Information("Template: {@AnonymousObject}.", _anonymousObject);
            _logger.Information(_exception, "Hello, {Name}!", "World");

            var y = _enrichedLogger.IsEnabled(LogEventLevel.Debug);
            _enrichedLogger.Write(_emptyEvent);
            _enrichedLogger.Information("Template:");
            _enrichedLogger.Information("Template: {ScalarStructValue}", 42);
            _enrichedLogger.Information("Template: {ScalarStructValue1},{ScalarStructValue2},{ScalarStructValue3}", 42, 7, 108);
            _enrichedLogger.Information("Template: {ScalarBigStructValue}", _bigStruct);
            _enrichedLogger.Information("Template: {ScalarValue}", "42");
            _enrichedLogger.Information("Template: {ScalarValue1},{ScalarValue2},{ScalarValue3}", "42", "7", "108");
            _enrichedLogger.Information("Template: {DictionaryValue}", _dictionaryValue);
            _enrichedLogger.Information("Template: {SequenceValue}", _sequence);
            _enrichedLogger.Information("Template: {@AnonymousObject}.", _anonymousObject);
            _enrichedLogger.Information(_exception, "Hello, {Name}!", "World");

            return x && y; //Force the compiler don''t optimize the IsEnabled
        }

        struct BigTestStruct
        {
            public decimal Lat { get; set; }
            public decimal Long { get; set; }
            public float[][] Points { get; set; }
            public BigInteger Count { get; set; }
            public bool Active { get; set; }
        }
    }

    public class AllocationsBenchmark : AllocationsBaseBenchmark
    {
        public AllocationsBenchmark()
        {
            _logger = new LoggerConfiguration()
                .CreateLogger();

            _enrichedLogger = _logger.ForContext(new PropertyEnricher("Prop", "Value"));
        }
    }

    public class AllocationsIgnoringEventsBenchmark : AllocationsBaseBenchmark
    {
        public AllocationsIgnoringEventsBenchmark()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Fatal()
                .CreateLogger();

            _enrichedLogger = _logger.ForContext(new PropertyEnricher("Prop", "Value"));
        }
    }
}
