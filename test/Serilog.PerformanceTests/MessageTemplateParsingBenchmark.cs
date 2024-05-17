namespace Serilog.PerformanceTests;

/// <summary>
/// Tests the cost of parsing various message templates.
/// </summary>
[MemoryDiagnoser]
public class MessageTemplateParsingBenchmark
{
    MessageTemplateParser _parser = new();

    [Benchmark(Baseline = true)]
    public void EmptyTemplate()
    {
        _parser.Parse("");
    }

    [Benchmark]
    public void DefaultConsoleOutputTemplate()
    {
        _parser.Parse("{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}");
    }

    [Benchmark]
    public void LiteralTextOnly()
    {
        _parser.Parse("Token validated for claims principal with identifier `HJdfshjka678-HJK68SFHJKhjkfsdhjsd456`");
    }
}
