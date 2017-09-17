using System.Collections.Generic;
using Serilog.Core;
using Serilog.Core.Sinks;
using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;
using static Serilog.Events.LogEventLevel;

namespace Serilog.Tests.Core
{
    public class OverrideRestrictingSinkTests
    {
        [Theory]
        [InlineData("Whatever", Verbose, true)]
        [InlineData("Whatever.Sub", Verbose, true)]
        [InlineData("Whatever.Sub.Sub", Verbose, true)]
        [InlineData("Fatal", Fatal, true)]
        [InlineData("Fatal", Error, false)]
        [InlineData("Fatal.Sub", Fatal, true)]
        [InlineData("Fatal.Sub", Error, false)]
        [InlineData("Fatal.Error", Error, true)]
        [InlineData("Fatal.Error", Warning, false)]
        [InlineData("Fatal.Error.Sub", Error, true)]
        [InlineData("Fatal.Error.Sub", Warning, false)]
        public void OverridesAreTakenIntoAccount(string context, LogEventLevel level, bool shouldBeEmitted)
        {
            var overides = new Dictionary<string, LoggingLevelSwitch>
            {
                ["Fatal"] = new LoggingLevelSwitch(Fatal),
                ["Fatal.Error"] = new LoggingLevelSwitch(Error),
            };
            LogEvent emitted = null;
            var sut = new OverrideRestrictingSink(
                new DelegatingSink(e => emitted = e),
                overrides: overides);

            var evt = Some.LogEvent(context, level: level);

            sut.Emit(evt);

            if (shouldBeEmitted)
            {
                Assert.NotNull(emitted);
            }
            else
            {
                Assert.Null(emitted);
            }
        }
    }
}
