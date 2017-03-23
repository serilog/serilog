using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    public class DisposableLogger : Serilog.ILogger, IDisposable
    {
        public bool Disposed { get; set; }

        public void Dispose()
        {
            Disposed = true;
        }

        public ILogger ForContext(ILogEventEnricher enricher)
        {
            throw new NotImplementedException();
        }

        public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
        {
            throw new NotImplementedException();
        }

        public ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
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

        public Task Write(LogEvent logEvent)
        {
            throw new NotImplementedException();
        }

        public Task Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogEventLevel level)
        {
            throw new NotImplementedException();
        }

        public Task Verbose(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Debug(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Information(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Warning(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Error(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Fatal(string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            throw new NotImplementedException();
        }

        public Task Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Verbose<T>(string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Debug<T>(string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Information<T>(string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Warning<T>(string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Error<T>(string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Fatal<T>(string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            throw new NotImplementedException();
        }

        public Task Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            throw new NotImplementedException();
        }

        public Task Write(LogEventLevel level, string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Write(LogEventLevel level, Exception exception, string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Verbose(string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Verbose(Exception exception, string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Debug(string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Debug(Exception exception, string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Information(string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Information(Exception exception, string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Warning(string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Warning(Exception exception, string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Error(string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Error(Exception exception, string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Fatal(string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task Fatal(Exception exception, string messageTemplate)
        {
            throw new NotImplementedException();
        }

        public bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate parsedTemplate, out IEnumerable<LogEventProperty> boundProperties)
        {
            throw new NotImplementedException();
        }

        public bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty property)
        {
            throw new NotImplementedException();
        }
    }
}