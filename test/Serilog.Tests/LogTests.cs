using Xunit;
using Serilog.Core.Pipeline;
using Serilog.Tests.Support;

namespace Serilog.Tests
{
    [Collection("Log.Logger")]
    public class LogTests
    {
        [Fact]
        public void TheUninitializedLoggerIsSilent()
        {
            // This test depends on being ther first executed from
            // the collection.
            Assert.IsType<SilentLogger>(Log.Logger);
        }
        
        [Fact]
        public void NonePropertyIsNotAffectedByLoggerInitialization()
        {
            Log.Logger = new DisposableLogger();
            Assert.IsType<SilentLogger>(Log.None);
            Log.CloseAndFlush();
        }

        [Fact]
        public void TheNonePropertyIsSilent()
        {
            Assert.IsType<SilentLogger>(Log.None);
        }

        [Fact]
        public void DisposesTheLogger()
        {
            var disposableLogger = new DisposableLogger();
            Log.Logger = disposableLogger;
            Log.CloseAndFlush();
            Assert.True(disposableLogger.Disposed);
        }

        [Fact]
        public void ResetsLoggerToSilentLogger()
        {
            Log.Logger = new DisposableLogger();
            Log.CloseAndFlush();
            Assert.IsType<SilentLogger>(Log.Logger);
        }

        [Fact]
        public void NonePropertyIsNotAffectedByReset()
        {
            Log.Logger = new DisposableLogger();
            Log.CloseAndFlush();
            Assert.IsType<SilentLogger>(Log.None);
            Log.None.Information("Should not throw");
        }

    }
}
