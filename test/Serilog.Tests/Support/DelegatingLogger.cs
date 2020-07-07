namespace Serilog.Tests.Support
{
#if FEATURE_DEFAULT_INTERFACE
    using System;
    using Serilog.Events;

    public class DelegatingLogger : ILogger, IDisposable
    {
        readonly ILogger _inner;

        public DelegatingLogger(ILogger logger)
        {
            _inner = logger;
        }

        public bool Disposed { get; private set; }

        public void Dispose() => Disposed = true;

        public void Write(LogEvent logEvent) => _inner.Write(logEvent);
    }
#endif
}
