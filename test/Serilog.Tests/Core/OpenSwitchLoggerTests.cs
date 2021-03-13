#pragma warning disable Serilog004 // Constant MessageTemplate verifier
#pragma warning disable Serilog003 // Property binding verifier

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;
using Serilog.Tests.Support;
using Xunit;


namespace Serilog.Tests.Core
{
    public class OpenSwitchLoggerTests
    {
        [Fact]
        public void LogLevelAdjustedPerTask()
        {
            // Write all 5 level messages.
            // This emulates a dependency-injected object.
            async Task RunTask(
                ILogger serilogger, ILoggingLevelOverrider overrider, LogEventLevel level,
                string name, AutoResetEvent finished)
            {
                overrider.MinimumLevel = level;

                await Task.Delay(50);

                serilogger.Error($"{name} [{Thread.CurrentThread.ManagedThreadId}] Error");
                serilogger.Warning($"{name} Warning");
                serilogger.Information($"{name} Information");
                serilogger.Debug($"{name} Debug");
                serilogger.Verbose($"{name} Verbose");
                finished.Set();
            }


            var sink = new CollectingSink();
            var overrider = new LoggingLevelOverrider();

            // This emulates Log.Logger property, which has type Serilog.ILogger
            ILogger logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(sink)
                .CreateLogger()
                .AsOpenSwitch()
                .MinimumLevelOverride(overrider); // Same level for all log sources

            var ev1 = new AutoResetEvent(false);
            var ev2 = new AutoResetEvent(false);

            // Same overrider, different levels for different tasks
            var t1 = Task.Run(() => RunTask(logger, overrider, LogEventLevel.Warning, "Task1", ev1));
            var t2 = Task.Run(() => RunTask(logger, overrider, LogEventLevel.Debug, "Task2", ev2));

            ev1.WaitOne();
            ev2.WaitOne();

            Assert.Equal(2, sink.Events.Where(e => e.MessageTemplate.Text.StartsWith("Task1")).Count());
            Assert.Equal(4, sink.Events.Where(e => e.MessageTemplate.Text.StartsWith("Task2")).Count());
            Assert.Equal(6, sink.Events.Count);
        }


        [Fact]
        public void LogLevelAdjustedPerSourcePerTask()
        {
            // Write all 5 level messages into the given logger.
            // This emulates a dependency-injected object
            async Task RunTask(
                ILogger serilogger, ILoggingLevelOverrider overrider, LogEventLevel level,
                string name, AutoResetEvent finished)
            {
                overrider.MinimumLevel = level;

                await Task.Delay(50);

                serilogger.Error($"{name} Error");
                serilogger.Warning($"{name} Warning");
                serilogger.Information($"{name} Information");
                serilogger.Debug($"{name} Debug");
                serilogger.Verbose($"{name} Verbose");
                finished.Set();
            }


            var sink = new CollectingSink();
            var sysOverrider = new LoggingLevelOverrider(); // Specific for System.*

            // This emulates Log.Logger property, which has type Serilog.ILogger
            ILogger logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(sink)
                .CreateLogger()
                .AsOpenSwitch()
                .MinimumLevelOverride("System", sysOverrider);

            var ev1 = new AutoResetEvent(false);
            var ev2 = new AutoResetEvent(false);
            var ev3 = new AutoResetEvent(false);

            // Same overrider, different sources, different levels for different tasks
            var l1 = logger.ForContext<System.Object>();
            var l2 = logger.ForContext<Microsoft.DotNet.PlatformAbstractions.Platform>();

            // System.* logger with limit on System.* - 2 messages
            var t1 = Task.Run(() => RunTask(l1, sysOverrider, LogEventLevel.Warning, "Task1", ev1));
            // Another System.* logger with limit on System.* - 3 messages
            var t2 = Task.Run(() => RunTask(l1, sysOverrider, LogEventLevel.Information, "Task2", ev2));
            // Non-System.* logger with limit on System.* - 5 messages
            var t3 = Task.Run(() => RunTask(l2, sysOverrider, LogEventLevel.Fatal, "Task3", ev3));

            ev1.WaitOne();
            ev2.WaitOne();
            ev3.WaitOne();

            Assert.Equal(2, sink.Events.Where(e => e.MessageTemplate.Text.StartsWith("Task1")).Count());
            Assert.Equal(3, sink.Events.Where(e => e.MessageTemplate.Text.StartsWith("Task2")).Count());
            Assert.Equal(5, sink.Events.Where(e => e.MessageTemplate.Text.StartsWith("Task3")).Count());
            Assert.Equal(10, sink.Events.Count);
        }
    }
}
