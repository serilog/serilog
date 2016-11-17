using System;
using Xunit;
using Serilog.Core.Pipeline;
using Serilog.Tests.Support;

namespace Serilog.Tests
{
    [Collection("LogTests")]
    public class LogTests
    {
        [Fact]
        public void TheUninitializedLoggerIsSilent()
        {
            Assert.IsType<SilentLogger>(Log.Logger);
        }

        [Fact]
        public void DisposesTheLogger()
        {
            var disposableLogger = new DisposableLogger();
            using (SwappedLogger(disposableLogger))
            {
                Log.CloseAndFlush();

                Assert.True(disposableLogger.Disposed);
            }
        }

        [Fact]
        public void ResetsLoggerToSilentLogger()
        {
            using (SwappedLogger(new DisposableLogger()))
            {
                Log.CloseAndFlush();

                Assert.IsType<SilentLogger>(Log.Logger);
            }
        }

        private static IDisposable SwappedLogger(ILogger logger)
        {
            ILogger originalLogger = Log.Logger;
            Log.Logger = logger;

            return new DelegateDisposable(() => Log.Logger = originalLogger);
        }
    }
}
