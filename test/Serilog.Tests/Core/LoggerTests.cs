using System;
using NUnit.Framework;
using Serilog.Core;
using Serilog.Tests.Support;

namespace Serilog.Tests.Core
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public void AnExceptionThrownByAnEnricherIsNotPropagated()
        {
            var thrown = false;

            var l = (Logger) new LoggerConfiguration()
                .EnrichedBy(new DelegatingEnricher(le => {
                    thrown = true;
                    throw new Exception("No go, pal."); }))
                .CreateLogger();

            l.Write(Some.LogEvent());

            Assert.IsTrue(thrown);
        }
    }
}
