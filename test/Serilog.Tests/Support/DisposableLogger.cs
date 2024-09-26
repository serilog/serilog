namespace Serilog.Tests.Support;

public class DisposableLogger : ILogger, IDisposable
#if FEATURE_ASYNCDISPOSABLE
    , IAsyncDisposable
#endif
{
    public bool IsDisposed { get; set; }
    public bool IsDisposedAsync { get; set; }

    public void Dispose()
    {
        IsDisposed = true;
    }

#if FEATURE_ASYNCDISPOSABLE
    public ValueTask DisposeAsync()
    {
        Dispose();
        IsDisposedAsync = true;
        return default;
    }
#endif

    public ILogger ForContext(ILogEventEnricher enricher)
    {
        throw new NotImplementedException();
    }

    public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
    {
        throw new NotImplementedException();
    }

    public ILogger ForContext(string propertyName, object? value, bool destructureObjects = false)
    {
        throw new NotImplementedException();
    }

    public ILogger ForContext<TSource>()
    {
        throw new NotImplementedException();
    }

    public ILogger ForContext(Type source)
    {
        throw new NotImplementedException();
    }

    public void Write(LogEvent logEvent)
    {
        throw new NotImplementedException();
    }

    public void Write(LogEventLevel level, string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Write(LogEventLevel level, Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogEventLevel level)
    {
        throw new NotImplementedException();
    }

    public void Verbose(string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Verbose(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Debug(string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Debug(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Information(string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Information(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Warning(string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Warning(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Error(string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Error(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Fatal(string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Fatal(Exception? exception, string messageTemplate, params object?[]? propertyValues)
    {
        throw new NotImplementedException();
    }

    public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Write<T>(LogEventLevel level, Exception? exception, string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Write<T0, T1>(LogEventLevel level, Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Write<T0, T1, T2>(LogEventLevel level, Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Verbose<T>(string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Verbose<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Verbose<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Verbose<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Debug<T>(string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Debug<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Debug<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Debug<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Information<T>(string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Information<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Information<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Information<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Warning<T>(string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Warning<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Warning<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Warning<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Error<T>(string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Error<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Error<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Error<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Fatal<T>(string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Fatal<T>(Exception? exception, string messageTemplate, T propertyValue)
    {
        throw new NotImplementedException();
    }

    public void Fatal<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        throw new NotImplementedException();
    }

    public void Fatal<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        throw new NotImplementedException();
    }

    public void Write(LogEventLevel level, string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Write(LogEventLevel level, Exception? exception, string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Verbose(string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Verbose(Exception? exception, string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Debug(string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Debug(Exception? exception, string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Information(string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Information(Exception? exception, string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Warning(string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Warning(Exception? exception, string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Error(string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Error(Exception? exception, string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Fatal(string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public void Fatal(Exception? exception, string messageTemplate)
    {
        throw new NotImplementedException();
    }

    public bool BindMessageTemplate(string messageTemplate, object?[]? propertyValues, [NotNullWhen(true)] out MessageTemplate? parsedTemplate, [NotNullWhen(true)] out IEnumerable<LogEventProperty>? boundProperties)
    {
        throw new NotImplementedException();
    }

    public bool BindProperty(string? propertyName, object? value, bool destructureObjects, [NotNullWhen(true)] out LogEventProperty? property)
    {
        throw new NotImplementedException();
    }
}
