using System.Collections.Generic;
using System.Linq;
using Serilog.Core;
using Serilog.Tests.Support;
using Xunit;
using static Serilog.Events.LogEventLevel;

namespace Serilog.Tests.Core
{
    public class ChildLoggerKnownLimitationsTests
    {
        [Fact]
        public void ChildLoggerCannotDecreaseRootLoggerOverrideLevel()
        {
            var configCallBackSink = new CollectingSink();
            var loggerSink = new CollectingSink();

            var subLogger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Foo.Bar", Debug)
                .WriteTo.Sink(loggerSink)
                .CreateLogger();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Foo.Bar", Warning)
                .WriteTo.Logger(lc => lc
                    .MinimumLevel.Verbose()
                    .MinimumLevel.Override("Foo.Bar", Debug)
                    .WriteTo.Sink(configCallBackSink))
                .WriteTo.Logger(subLogger)
                .CreateLogger();

            var contextLogger = logger.ForContext(Constants.SourceContextPropertyName, "Foo.Bar");
            contextLogger.Write(Some.InformationEvent());

            // subloggers would have happily accepted Information event
            // but root logger had an override with level Warning
            Assert.Null(loggerSink.Events.FirstOrDefault());
            Assert.Null(configCallBackSink.Events.FirstOrDefault());
        }

        [Fact]
        public void SpecifyingMinimumLevelOverridesInWriteToLoggerWithConfigCallBackWritesWarningToSelfLog()
        {
            var outputs = new List<string>();
            using (TemporarySelfLog.SaveTo(outputs))
            {
                var configCallBackSink = new CollectingSink();

                var logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .MinimumLevel.Override("Foo.Bar", Warning)
                    .WriteTo.Logger(lc => lc
                        .MinimumLevel.Verbose()
                        .MinimumLevel.Override("Foo.Bar", Debug)
                        .WriteTo.Sink(configCallBackSink))
                    .CreateLogger();

                var contextLogger = logger.ForContext(Constants.SourceContextPropertyName, "Foo.Bar");
                contextLogger.Write(Some.InformationEvent());
            }

            Assert.EndsWith("We have detected that you are using .MinimumLevel.Override() from a sub-logger (.WriteTo.Logger(Action<LoggerConfiguration>)). " +
                         "Overrides in a sub-logger cannot decrease the level set by the parent logger. Because of this limitation and possible performance impact, " +
                         "we recommend using .Filter instead in your sub-loggers. " +
                         "Note that support for sub-loggers' minimum level overrides may be removed in the next major version.",
                         outputs.FirstOrDefault() ?? "");
        }


        [Fact]
        public void SpecifyingMinimumLevelOverridesInWriteToLoggerWritesWarningToSelfLog()
        {
            var outputs = new List<string>();
            using (TemporarySelfLog.SaveTo(outputs))
            {
                var subSink = new CollectingSink();

                var subLogger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .MinimumLevel.Override("Foo.Bar", Debug)
                    .WriteTo.Sink(subSink)
                    .CreateLogger();

                var logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .MinimumLevel.Override("Foo.Bar", Warning)
                    .WriteTo.Logger(subLogger)
                    .CreateLogger();

                var contextLogger = logger.ForContext(Constants.SourceContextPropertyName, "Foo.Bar");
                contextLogger.Write(Some.InformationEvent());
            }

            Assert.EndsWith("We have detected that you are using .MinimumLevel.Override() from a sub-logger (.WriteTo.Logger(Logger)). " +
                            "Overrides in a sub-logger cannot decrease the level set by the parent logger. Because of this limitation and possible performance impact, " +
                            "we recommend using .Filter instead in your sub-loggers. " +
                            "Note that support for sub-loggers' minimum level overrides may be removed in the next major version.",
                outputs.FirstOrDefault() ?? "");
        }
    }
}
