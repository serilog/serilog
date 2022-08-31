namespace Serilog.Tests.Support;

public class StringSink : ILogEventSink
{
    readonly StringWriter _sw = new();
    readonly ITextFormatter _formatter;

    const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";

    public StringSink(string outputTemplate = DefaultOutputTemplate)
    {
        _formatter = new MessageTemplateTextFormatter(outputTemplate, CultureInfo.InvariantCulture);
    }

    public void Emit(LogEvent logEvent)
    {
        _formatter.Format(logEvent, _sw);
    }

    public override string ToString() => _sw.ToString();
}
