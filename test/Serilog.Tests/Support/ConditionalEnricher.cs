using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    class ConditionalEnricher : ILogEventEnricher, IDisposable
    {
        readonly ILogEventEnricher _wrapped;
        readonly Func<LogEvent, bool> _condition;

        public ConditionalEnricher(ILogEventEnricher wrapped, Func<LogEvent, bool> condition)
        {
            _wrapped = wrapped;
            _condition = condition;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_condition(logEvent))
                _wrapped.Enrich(logEvent, propertyFactory);
        }

        public void Dispose()
        {
            (_wrapped as IDisposable)?.Dispose();
        }
    }
}