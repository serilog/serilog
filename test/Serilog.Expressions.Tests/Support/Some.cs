using Serilog.Events;
using Xunit.Sdk;

namespace Serilog.Expressions.Tests.Support;

static class Some
{
    static int _next;
    
    public static LogEvent InformationEvent(string messageTemplate = "Hello, world!", params object?[] propertyValues)
    {
        return LogEvent(LogEventLevel.Information, messageTemplate, propertyValues);
    }

    public static LogEvent InformationEvent(DateTimeOffset timestamp, string messageTemplate = "Hello, world!", params object?[] propertyValues)
    {
        return LogEvent(timestamp, LogEventLevel.Information, messageTemplate, propertyValues);
    }

    public static LogEvent WarningEvent(string messageTemplate = "Hello, world!", params object?[] propertyValues)
    {
        return LogEvent(LogEventLevel.Warning, messageTemplate, propertyValues);
    }

    public static LogEvent LogEvent(LogEventLevel level, string messageTemplate = "Hello, world!", params object?[] propertyValues)
    {
        return LogEvent(DateTimeOffset.Now, level, messageTemplate, propertyValues);
    }

    public static LogEvent LogEvent(DateTimeOffset timestamp, LogEventLevel level, string messageTemplate = "Hello, world!", params object?[] propertyValues)
    {
        var log = new LoggerConfiguration().CreateLogger();
#pragma warning disable Serilog004 // Constant MessageTemplate verifier
        if (!log.BindMessageTemplate(messageTemplate, propertyValues, out var template, out var properties))
#pragma warning restore Serilog004 // Constant MessageTemplate verifier
        {
            throw new XunitException("Template could not be bound.");
        }
        return new(timestamp, level, null, template, properties);
    }

    public static object AnonymousObject()
    {
        return new {A = Int()};
    }

    public static LogEventPropertyValue LogEventPropertyValue()
    {
        return new ScalarValue(AnonymousObject());
    }

    static int Int()
    {
        return Interlocked.Increment(ref _next);
    }

    public static string String()
    {
        return $"+S_{Int()}";
    }
}
