using System;
using System.Reactive.Linq;
using NUnit.Framework;
using Serilog.Tests.Support;

namespace Serilog.Tests.Sinks.Observable
{
    [TestFixture]
    public class ObservableSinkTests
    {
        [Test]
        public void EventsAreWrittenToObservers()
        {
            var eventSeen = false;

            var log = new LoggerConfiguration()
                .WriteTo.Observers(events => events
                    .Do(evt => { eventSeen = true; })
                    .Subscribe())
                .CreateLogger();

            log.Write(Some.InformationEvent());
            Assert.That(eventSeen);
        }
    }
}
