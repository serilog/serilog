using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace Serilog.Extras.TraceListener.Tests
{
    [TestFixture]
    public class SerilogTraceListenerSeverityConversionTests
    {
        static IEnumerable<TraceEventType> allTraceEventTypes = Enum.GetValues(typeof(TraceEventType)).Cast<TraceEventType>();

        [Test]
        public void CanConvertAnyTraceEventType([ValueSource("allTraceEventTypes")] TraceEventType sourceType)
        {
            TestDelegate act = () => SerilogTraceListener.ToLogEventLevel(sourceType);

            Assert.DoesNotThrow(act);
        }
    }
}