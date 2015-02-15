using System.Linq;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Tests.Support;
using System.Collections.Generic;


namespace Serilog.Extras.Timing.Tests
{
    [TestFixture]
    public class CounterMeasureTests
    {
        [Test]
        public void CountersIncrementSeperately()
        {
            var events = new List<LogEvent>();

            var log = new LoggerConfiguration()               
                .WriteTo.Sink(new DelegatingSink(e => events.Add(e)))
                .CreateLogger();

            var counter1 = log.CountOperation("TestCounter1", directWrite: false);
            var counter2 = log.CountOperation("TestCounter2", directWrite: false);

            // increment counter1 5 times
            for (int i = 0; i < 5; i++ )
            {
                counter1.Increment();
            }

            // increment counter2 10 times
            for (int i = 0; i < 10; i++)
            {
                counter2.Increment();
            }

            counter1.Write();
            counter2.Write();

            var counter1Literal = (long)events[0].Properties["CounterValue"].LiteralValue();
            var counter2Literal = (long)events[1].Properties["CounterValue"].LiteralValue();

            Assert.AreEqual(5, counter1Literal);
            Assert.AreEqual(10, counter2Literal);           
        }
    }
}
