using System;
using Xunit;
using Serilog.Core.Sinks;
using Serilog.Tests.Support;

namespace Serilog.Tests.Core
{
    public class SafeAggregateSinkTests
    {
        [Fact]
        public void AnExceptionThrownByASinkIsNotPropagated()
        {
            var thrown = false;

            var s = new SafeAggregateSink(new[] { new DelegatingSink(le => {
                thrown = true;
                throw new Exception("No go, pal.");
            }) });

            s.Emit(Some.InformationEvent());

            Assert.IsTrue(thrown);
        }

        [Fact]
        public void WhenASinkThrowsOtherSinksAreStillInvoked()
        {
            bool called1 = false, called2 = false;

            var s = new SafeAggregateSink(new[] {
                new DelegatingSink(le => called1 = true), 
                new DelegatingSink(le => { throw new Exception("No go, pal."); }),
                new DelegatingSink(le => called2 = true) 
            });

            s.Emit(Some.InformationEvent());

            Assert.IsTrue(called1 && called2);
        }
    }
}
