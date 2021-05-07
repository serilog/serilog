using Serilog.Core.Sinks;
using Serilog.Tests.Support;
using System;
using Xunit;

namespace Serilog.Tests.Core
{
    public class SafeAggregateSinkTests
    {
        [Fact]
        public void AnExceptionThrownByASinkIsNotPropagated()
        {
            var thrown = false;

            SafeAggregateSink s = new(new[] { new DelegatingSink(_ => {
                thrown = true;
                throw new Exception("No go, pal.");
            }) });

            s.Emit(Some.InformationEvent());

            Assert.True(thrown);
        }

        [Fact]
        public void WhenASinkThrowsOtherSinksAreStillInvoked()
        {
            bool called1 = false, called2 = false;

            SafeAggregateSink s = new(new[] {
                new DelegatingSink(_ => called1 = true),
                new DelegatingSink(_ => { throw new Exception("No go, pal."); }),
                new DelegatingSink(_ => called2 = true)
            });

            s.Emit(Some.InformationEvent());

            Assert.True(called1 && called2);
        }
    }
}
