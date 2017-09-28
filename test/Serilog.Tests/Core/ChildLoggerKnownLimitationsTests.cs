using System;
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
    }
}
