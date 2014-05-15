using NUnit.Framework;
using Serilog.Core.Pipeline;

namespace Serilog.Tests
{
    [TestFixture]
    public class LogTests
    {
        [Test]
        public void TheUninitializedLoggerIsSilent()
        {
            Assert.IsInstanceOf<SilentLogger>(Log.Logger);
        }
    }
}
