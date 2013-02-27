using System;
using NUnit.Framework;
using Serilog.Core;
using Serilog.Tests.Support;

namespace Serilog.Tests.Core
{
    [TestFixture]
    public class SafeAggregateSinkTests
    {
        [Test]
        public void AnExceptionThrownByASinkIsNotPropagated()
        {
            var thrown = false;

            var s = new SafeAggregateSink(new[] { new DelegatingSink(le => {
                thrown = true;
                throw new Exception("No go, pal.");
            }) }); 

            s.Write(Some.LogEvent());

            Assert.IsTrue(thrown);
        }
    }
}
