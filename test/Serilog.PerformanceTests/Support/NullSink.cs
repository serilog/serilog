using Serilog.Events;
using Serilog.Core;
using System.Threading.Tasks;

namespace Serilog.PerformanceTests.Support
{
    class NullSink : ILogEventSink
    {
        private static readonly Task completedTask = Task.FromResult((object)null);

        public Task Emit(LogEvent logEvent)
        {
            return completedTask;
        }
    }
}
