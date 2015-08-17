#if NET40
using Xunit;
using Serilog.Context;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Tests.Context
{
    public class LogContextTests
    {
        [Fact]
        public void MoreNestedPropertiesOverrideLessNestedOnes()
        {
            LogEvent lastEvent = null;

            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new DelegatingSink(e => lastEvent = e))
                .CreateLogger();

            using (LogContext.PushProperty("A", 1))
            {
                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());

                using (LogContext.PushProperty("A", 2))
                {
                    log.Write(Some.InformationEvent());
                    Assert.Equal(2, lastEvent.Properties["A"].LiteralValue());
                }

                log.Write(Some.InformationEvent());
                Assert.Equal(1, lastEvent.Properties["A"].LiteralValue());
            }

            log.Write(Some.InformationEvent());
            Assert.False(lastEvent.Properties.ContainsKey("A"));
        }
    }
}
#endif