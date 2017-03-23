using System;
using System.Threading.Tasks;
using Xunit;
using Serilog.Core.Sinks;
using Serilog.Tests.Support;

namespace Serilog.Tests.Core
{
    public class SafeAggregateSinkTests
    {
        [Fact]
        public async Task AnExceptionThrownByASinkIsNotPropagated()
        {
            var thrown = false;

            var s = new SafeAggregateSink(new[] { new DelegatingSink(le => {
                thrown = true;
                throw new Exception("No go, pal.");
            }) });

            await s.Emit(Some.InformationEvent());

            Assert.True(thrown);
        }

        [Fact]
        public async Task WhenASinkThrowsOtherSinksAreStillInvoked()
        {
            bool called1 = false, called2 = false;

            var s = new SafeAggregateSink(new[] {
                new DelegatingSink(le => called1 = true), 
                new DelegatingSink(le => { throw new Exception("No go, pal."); }),
                new DelegatingSink(le => called2 = true) 
            });

            await s.Emit(Some.InformationEvent());

            Assert.True(called1 && called2);
        }
    }
}
