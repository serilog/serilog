#pragma warning disable Serilog004 // Constant MessageTemplate verifier
#pragma warning disable Serilog003 // Property binding verifier

using System.Linq;
using System.Threading.Tasks;
using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;


namespace Serilog.Tests.Core
{
    public class ScopedLoggerTests
    {
        [Fact]
        public async Task LogLevelAdjustedPerTask()
        {
            // This emulates a dependency-injected object
            async Task RunTask(ILogger serilogger, ILoggingLevelOverrider overrider, LogEventLevel level, string name)
            {
                overrider.OverrideMinimumLevel(level);
                await Task.Delay(50);
                serilogger.Error($"{name} Error");
                serilogger.Warning($"{name} Warning");
                serilogger.Information($"{name} Information");
                serilogger.Debug($"{name} Debug");
                serilogger.Verbose($"{name} Verbose");
            }


            var sink = new CollectingSink();

            // This emulates Log.Logger property, which has type Serilog.ILogger
            ILogger logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(sink)
                .CreateLogger()
                .AsScoped()
                .SetMinimumLevelOverrider(out var overrider);

            var t1 = RunTask(logger, overrider, LogEventLevel.Warning, "Task1");
            var t2 = RunTask(logger, overrider, LogEventLevel.Debug, "Task2");

            await Task.WhenAny(t1, t2);
            Assert.Equal(6, sink.Events.Count);
            Assert.Equal(2, sink.Events.Where(e => e.MessageTemplate.Text.StartsWith("Task1")).Count());
            Assert.Equal(4, sink.Events.Where(e => e.MessageTemplate.Text.StartsWith("Task2")).Count());
        }
    }
}
