using Serilog.Core.Enrichers;
using System.Numerics;

namespace Serilog.PerformanceTests;

[MemoryDiagnoser]
public abstract class AllocationsBaseBenchmark
{
    readonly ILogger _logger;
    readonly ILogger _enrichedLogger;

    readonly LogEvent _emptyEvent;
    readonly object _dictionaryValue;
    readonly object _anonymousObject;
    readonly object _sequence;
    readonly BigTestStruct _bigStruct;
    readonly Exception _exception;
    readonly MessageTemplateTextFormatter formatter;
    readonly LogEvent evt;
    readonly StringWriter stringWriter;

    protected AllocationsBaseBenchmark(ILogger logger)
    {
        _logger = logger;
        _enrichedLogger = _logger.ForContext(new PropertyEnricher("Prop", "Value"));

        _emptyEvent = new(
            DateTimeOffset.Now,
            LogEventLevel.Information,
            null,
            new(Enumerable.Empty<MessageTemplateToken>()),
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
        _bigStruct = new BigTestStruct()
        {
            Active = true,
            Count = 918217217261726172L,
            Lat = -42,
            Long = 42,
            Points = [
                        [0,1,2,3,4,5,6,7,8,9,],
                        [1,2,3,4,5,6,7,8,9,0,],
                        [2,3,4,5,6,7,8,9,0,1,],
                        [3,4,5,6,7,8,9,0,1,2,],
                        [4,5,6,7,8,9,0,1,2,3,],
                        [5,6,7,8,9,0,1,2,3,4,],
                        [6,7,8,9,0,1,2,3,4,5,],
                        [7,8,9,0,1,2,3,4,5,6,],
                        [8,9,0,1,2,3,4,5,6,7,],
                        [9,0,1,2,3,4,5,6,7,8,],
            ],
        };
        _exception = new ApplicationException("An Error");
        formatter = new MessageTemplateTextFormatter("{ThreadId,5}");
        evt = Some.InformationEvent();
        evt.AddOrUpdateProperty(new LogEventProperty("ThreadId", new ScalarValue(15)));
        stringWriter = new StringWriter();
    }

    public struct BigTestStruct
    {
        public bool Active { get; set; }
        public BigInteger Count { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public float[][] Points { get; set; }
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
    public void FormatMessage()
    {
        stringWriter.GetStringBuilder().Clear();
        formatter.Format(evt, stringWriter);
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
}

public class AllocationsBenchmark : AllocationsBaseBenchmark
{
    private static readonly ILogger _logger = new LoggerConfiguration()
        .CreateLogger();

    public AllocationsBenchmark() : base(_logger)
    {
    }
}

public class AllocationsIgnoringEventsBenchmark : AllocationsBaseBenchmark
{
    private static readonly ILogger _logger = new LoggerConfiguration()
        .MinimumLevel.Fatal()
        .CreateLogger();

    public AllocationsIgnoringEventsBenchmark() : base(_logger)
    {
    }
}
