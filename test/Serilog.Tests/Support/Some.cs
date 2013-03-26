using System;
using System.IO;
using System.Linq;
using System.Threading;
using Serilog.Events;

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

        public static LogEventProperty LogEventProperty()
        {
            return Serilog.Events.LogEventProperty.For(String(), Int());
        }

        public static string NonexistentTempFilePath()
        {
            return Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");
        }

        public static string TempFilePath()
        {
            return Path.GetTempFileName();
        }

        public static string TempFolderPath()
        {
            var dir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(dir);
            return dir;
        }
    }
}
