namespace Serilog.PerformanceTests.Support;

static class Some
{
    public static LogEvent InformationEvent(string messageTemplate = "Hello, world!", params object[] propertyValues)
    {
        var logger = new LoggerConfiguration().CreateLogger();
#pragma warning disable Serilog004 // Constant MessageTemplate verifier
        logger.BindMessageTemplate(messageTemplate, propertyValues, out var parsedTemplate, out var boundProperties);
#pragma warning restore Serilog004 // Constant MessageTemplate verifier
        return LogEvent.GetOrCreate(DateTime.Now, LogEventLevel.Information, null, parsedTemplate!, boundProperties!);
    }
}
