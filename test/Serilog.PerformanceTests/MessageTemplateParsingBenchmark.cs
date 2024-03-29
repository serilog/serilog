namespace Serilog.PerformanceTests;

/// <summary>
/// Tests the cost of parsing various message templates.
/// </summary>
[MemoryDiagnoser]
public class MessageTemplateParsingBenchmark
{
    MessageTemplateParser _parser = null!;

    const string _SimpleTextTemplate = "Hello, world!";
    const string _SinglePropertyTokenTemplate = "{Name}";
    const string _SingleTextWithPropertyTemplate = "This is a new Log entry with some external {Data}";
    const string _ManyPropertyTokenTemplate = "{Greeting}, {Name}!";
    const string _MultipleTokensTemplate = "Hello, world! {Greeting}, {Name} - {{Escaped}} - {@Hello} {$World}";
    const string _DefaultConsoleOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";
    const string _BigTemplate = "Hello, world! {Greeting}, {Name} - {{Escaped}} - {@Hello} {$World} {Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception} Hello, world! {0} {-1} {2} {Test,-15} {Prop,50} {Qnt,5:000}";

    [GlobalSetup]
    public void Setup()
    {
        _parser = new MessageTemplateParser();
    }

    [Benchmark(Baseline = true)]
    public object EmptyTemplate()
    {
        return _parser.Parse("");
    }

    [Benchmark]
    public object SimpleTextTemplate()
    {
        return _parser.Parse(_SimpleTextTemplate);
    }

    [Benchmark]
    public object SinglePropertyTokenTemplate()
    {
        return _parser.Parse(_SinglePropertyTokenTemplate);
    }

    [Benchmark]
    public object SingleTextWithPropertyTemplate()
    {
        return _parser.Parse(_SingleTextWithPropertyTemplate);
    }

    [Benchmark]
    public object ManyPropertyTokenTemplate()
    {
        return _parser.Parse(_ManyPropertyTokenTemplate);
    }

    [Benchmark]
    public object MultipleTokensTemplate()
    {
        return _parser.Parse(_MultipleTokensTemplate);
    }

    [Benchmark]
    public object DefaultConsoleOutputTemplate()
    {
        return _parser.Parse(_DefaultConsoleOutputTemplate);
    }

    [Benchmark]
    public object BigTemplate()
    {
        return _parser.Parse(_BigTemplate);
    }
}
