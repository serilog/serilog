using System;
using Serilog.Core;
using Serilog.Events;
using System.Threading.Tasks;

namespace Serilog.Tests.Support
{
    public class DelegatingSink : ILogEventSink
    {
        readonly Action<LogEvent> _write;

        public DelegatingSink(Action<LogEvent> write)
        {
            if (write == null) throw new ArgumentNullException(nameof(write));
            _write = write;
        }

        public Task Emit(LogEvent logEvent)
        {
            _write(logEvent);
            return Task.FromResult((object)null);
        }

        public static async Task<LogEvent> GetLogEvent(Func<ILogger, Task> writeAction)
        {
            LogEvent result = null;
            var l = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(new DelegatingSink(le => result = le))
                .CreateLogger();

            await writeAction(l);
            return result;
        }
    }
}
