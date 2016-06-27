using System;

namespace Serilog.Tests.Support
{
    public class DelegateDisposable : IDisposable
    {
        private readonly Action _disposeAction;
        private bool _disposed;

        public DelegateDisposable(Action disposeAction)
        {
            _disposeAction = disposeAction;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposeAction();
            _disposed = true;
        }
    }
}