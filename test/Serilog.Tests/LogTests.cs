using Xunit;
using Serilog.Core.Pipeline;

namespace Serilog.Tests
{
    public class LogTests
    {
        [Test]
        public void TheUninitializedLoggerIsSilent()
        {
            Assert.IsInstanceOf<SilentLogger>(Log.Logger);
        }
    }
}
