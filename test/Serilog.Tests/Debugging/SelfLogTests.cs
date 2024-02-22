namespace Serilog.Tests.Debugging;

public class SelfLogTests
{
    [ThreadStatic]
    static List<string>? Messages;

    [Fact]
    public void MessagesAreWrittenWhenOutputIsSet()
    {
        Messages = new();
        SelfLog.Enable(m =>
        {
            Messages.Add(m);
        });

        SelfLog.WriteLine("Hello {0} {1} {2}", 0, 1, 2);
        Assert.Contains(Messages, m => m.EndsWith("Hello 0 1 2"));

        // Better to do this here than in another test, since at this point
        // we've confirmed there's actually something to disable.
        var count = Messages.Count;
        SelfLog.Disable();
        SelfLog.WriteLine("Unwritten");
        Assert.Equal(Messages.Count, count);
    }

    [Fact]
    public void EnsureNoSelfLogForEnriched()
    {
        Messages = new();
        SelfLog.Enable(_ => Messages.Add(_));

        var sink = new CollectingSink();
        var configuration = new LoggerConfiguration();

        var enrich = configuration.Enrich;
        enrich.WithProperty("property", "Value");
        var logger = configuration
            .WriteTo.Dummy(w => w.Sink(sink))
            .CreateLogger();

        logger.Warning("Message: {property}");

        var writer = new StringWriter();
        sink.SingleEvent.RenderMessage(writer);
        Assert.Equal("Message: \"Value\"", writer.ToString());
        Assert.Empty(Messages);
        SelfLog.Disable();
    }

    [Fact]
    public void SelfLogReportsErrorWhenNamedPropertyIsMissing()
    {
        Messages = new();
        SelfLog.Enable(_ => Messages.Add(_));

        var sink = new CollectingSink();
        var configuration = new LoggerConfiguration();

        var logger = configuration
            .WriteTo.Dummy(w => w.Sink(sink))
            .CreateLogger();

        logger.Warning("Message: {property}");

        var writer = new StringWriter();
        sink.SingleEvent.RenderMessage(writer);
        Assert.Equal("Message: {property}", writer.ToString());
        Assert.EndsWith("Could not find named property: property", Messages.Single());
        SelfLog.Disable();
    }

    [Fact]
    public void SelfLogReportsErrorWhenPositionalParameterCountIsMismatched()
    {
        Messages = new();
        SelfLog.Enable(m =>
        {
            Messages.Add(m);
        });

        using var logger = new LoggerConfiguration()
            .WriteTo.Logger(new SilentLogger())
            .CreateLogger();

        // ReSharper disable once StructuredMessageTemplateProblem
        logger.Information("{0}{1}", "hello");

        SelfLog.Disable();

        Assert.Single(Messages);
    }

    [Fact]
    public void SelfLogDoesNotReportErrorWhenPositionalParameterIsRepeated()
    {
        Messages = new();
        SelfLog.Enable(m =>
        {
            Messages.Add(m);
        });

        using var logger = new LoggerConfiguration()
            .WriteTo.Logger(new SilentLogger())
            .CreateLogger();

        logger.Information("{0}{0}", "hello");

        SelfLog.Disable();

        Assert.Empty(Messages);
    }

    [Fact]
    public void WritingToUndeclaredSinkWritesToSelfLog()
    {
        Messages = new();
        SelfLog.Enable(m =>
        {
            Messages.Add(m);
        });

        var settings = new Dictionary<string, string>
        {
            ["write-to:DummyRollingFile.pathFormat"] = "C:\\"
        };

        var log = new LoggerConfiguration()
            .ReadFrom.KeyValuePairs(settings)
            .CreateLogger();

        DummyRollingFileSink.Reset();
        DummyRollingFileAuditSink.Reset();

        log.Write(Some.InformationEvent());

        Assert.Single(Messages);
        Assert.Contains(Messages, m => m.EndsWith("Setting \"DummyRollingFile\" could not be matched to an implementation in any of the loaded assemblies. " +
                                                  "To use settings from additional assemblies, specify them with the \"serilog:using\" key."));
    }
}
