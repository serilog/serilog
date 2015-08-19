using Xunit;
using Serilog.Core.Pipeline;

namespace Serilog.Tests
{
    public class LogTests
    {
        [Fact]
        public void TheUninitializedLoggerIsSilent()
        {
            Assert.IsType<SilentLogger>(Log.Logger);
        }
    }
}
