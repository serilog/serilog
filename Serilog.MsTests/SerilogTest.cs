using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog.Context;

namespace Serilog.Tests.Context
{
    [TestClass]
    public class SerilogTest
    {
        [TestMethod]
        public void TestLog()
        {
            var log = Log.ForContext<SerilogTest>();
            log.Information("Test");

            Assert.AreEqual(1, 1);
        }

        // Fails: with the error: An exception occurred while invoking executor 'executor://mstestadapter/v1': Type is not resolved for member 'Serilog.Context.ImmutableStack`1[[Serilog.Core.ILogEventEnricher, Serilog, Version=1.4.0.0, Culture=neutral, PublicKeyToken=24c2f752a8e58a10]],Serilog.FullNetFx, Version=1.4.0.0, Culture=neutral, PublicKeyToken=24c2f752a8e58a10'.
        [TestMethod]
        public void TestPushProperty()
        {
            var log = Log.ForContext<SerilogTest>();
            using (LogContext.PushProperty("Test property", "Test value"))
            {
                log.Information("Test");
            }

            Assert.AreEqual(1, 1);
        }
    }
}