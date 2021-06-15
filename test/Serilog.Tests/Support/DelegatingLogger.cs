#if FEATURE_DEFAULT_INTERFACE

#nullable enable
using System;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    public class DelegatingLogger : ILogger, IDisposable
    {
        readonly ILogger _inner;

        public DelegatingLogger(ILogger logger)
        {
            _inner = logger;
        }

        public bool Disposed { get; private set; }

        public void Dispose() => Disposed = true;

        public void Write(LogEvent? logEvent) => _inner.Write(logEvent);
    }
}

#endif
