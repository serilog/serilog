using System;
using System.Reactive.Linq;
using Microsoft.Owin.Logging;
using NUnit.Framework;
using Serilog.Events;

namespace Serilog.Extras.MSOwin
{
    [TestFixture]
    public class LoggerFactoryTests
    {
        [Test]
        public void CanCreateLogger()
        {
            var loggerFactory = new LoggerFactory();

            Microsoft.Owin.Logging.ILogger logger = loggerFactory.Create("LoggerFactoryTests");

            Assert.NotNull(logger);
        }

        [Test]
        public void EventsAreWritten()
        {
            var eventSeen = false;
            ILogger log = new LoggerConfiguration()
                .WriteTo
                .Observers(events => events
                    .Do(evt => { eventSeen = true; })
                    .Subscribe())
                .CreateLogger();
            var loggerFactory = new LoggerFactory(log);
            
            loggerFactory
                .Create("LoggerFactoryTests")
                .WriteError("error");

            Assert.True(eventSeen);
        }

        [Test]
        public void CanOverrideTraceEventToLogLevelConversion()
        {
            LogEvent eventSeen = null;
            ILogger log = new LoggerConfiguration()
                .WriteTo
                .Observers(events => events
                    .Do(evt => { eventSeen = evt; })
                    .Subscribe())
                .CreateLogger();
            var loggerFactory = new LoggerFactory(log, traceEventType => LogEventLevel.Fatal);

            loggerFactory
                .Create("LoggerFactoryTests")
                .WriteError("error");

            Assert.AreEqual(eventSeen.Level, LogEventLevel.Fatal);
        }
    }
}