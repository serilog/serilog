using System;
using System.Linq;
using System.Threading;

namespace Serilog.Tests.Support
{
    static class Some
    {
        static int Counter;

        public static int Int()
        {
            return Interlocked.Increment(ref Counter);
        }

        public static string String(string tag = null)
        {
            return (tag ?? "") + "__" + Int();
        }

        public static TimeSpan TimeSpan()
        {
            return System.TimeSpan.FromMinutes(Int());
        }

        public static DateTime Instant()
        {
            return new DateTime(2012, 10, 28) + TimeSpan();
        }

        public static DateTimeOffset OffsetInstant()
        {
            return new DateTimeOffset(Instant());
        }

        public static LogEvent LogEvent()
        {
            return new LogEvent(OffsetInstant(), LogEventLevel.Information, null, String(), Enumerable.Empty<LogEventProperty>());
        }
    }
}
