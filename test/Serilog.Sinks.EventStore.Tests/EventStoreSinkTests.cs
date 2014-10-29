﻿using EventStore.ClientAPI;
using Newtonsoft.Json;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Serilog.Sinks.EventStore.Tests
{
    /// <summary>
    /// Test the <see cref="EventStoreSink"/>
    /// </summary>
    [TestFixture]
    public class EventStoreSinkTests
    {
        /// <summary>
        /// Test that the constructor throws an <see cref="ArgumentNullException"/> when the <see cref="EventStoreConnection"/> passed in is null.
        /// </summary> 
        [Test]
        public void EventStoreSink_ThrowsAnArgumentNullExceptionWhenThePassedInEventStoreConnectionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new EventStoreSink(null, "test", EventStoreSink.DefaultBatchPostingLimit, EventStoreSink.DefaultPeriod));
        }

        /// <summary>
        /// Test that the constructor throws an <see cref="ArgumentNullException"/> when the stream name passed in is null.
        /// </summary> 
        [Test]
        public void EventStoreSink_ThrowsAnArgumentNullExceptionWhenThePassedInStreamNameIsNull()
        {
            IEventStoreConnection connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            Assert.Throws<ArgumentNullException>(() => new EventStoreSink(connection, null, EventStoreSink.DefaultBatchPostingLimit, EventStoreSink.DefaultPeriod));
        }

        /// <summary>
        /// Test that the constructor throws an <see cref="ArgumentException"/> when the stream name passed in is empty.
        /// </summary> 
        [Test]
        public void EventStoreSink_ThrowsAnArgumentExceptionWhenThePassedInStreamNameIsAnEmptyString()
        {
            IEventStoreConnection connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            Assert.Throws<ArgumentException>(() => new EventStoreSink(connection, String.Empty, EventStoreSink.DefaultBatchPostingLimit, EventStoreSink.DefaultPeriod));
        }
        
        /// <summary>
/// Test that a <see cref="LogEntryEmittedEvent"/> is readable after is has been written.
/// </summary>
        [Test]
        public void WhenAnEventIsWrittenToTheSinkItIsRetrievableFromTheEventStore()
{
    var timestamp = new DateTimeOffset(2013, 05, 28, 22, 10, 20, 666, TimeSpan.FromHours(10));
    var exception = new ArgumentException("Mládek");
    const LogEventLevel level = LogEventLevel.Information;
    const string messageTemplate = "{Song}++";
    var properties = new List<LogEventProperty> { new LogEventProperty("Song", new ScalarValue("New Macabre")) };
    var template = new MessageTemplateParser().Parse(messageTemplate);
    var logEvent = new Events.LogEvent(timestamp, level, exception, template, properties);

            IEventStoreConnection connection= EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            using (EventStoreRunner runner = new EventStoreRunner())
            {
                using (EventStoreSink sink = new EventStoreSink(connection, "Logs", EventStoreSink.DefaultBatchPostingLimit, EventStoreSink.DefaultPeriod))
                {
                    sink.Emit(logEvent);
                }
                EventReadResult result = connection.ReadEventAsync("Logs", StreamPosition.End, false).Result;
            Assert.AreEqual(EventReadStatus.Success, result.Status);
                string data = Encoding.UTF8.GetString(result.Event.Value.OriginalEvent.Data);
                LogEntryEmittedEvent ev = JsonConvert.DeserializeObject(data) as LogEntryEmittedEvent;
                Assert.AreEqual(messageTemplate, ev.MessageTemplate);
                Assert.AreEqual("\"New Macabre\"++", ev.RenderedMessage);
                Assert.AreEqual(timestamp, ev.Timestamp);
                Assert.AreEqual(level, ev.Level);
                Assert.AreEqual(1, ev.Properties.Count);
                Assert.AreEqual("New Macabre", ev.Properties["Song"]);
                Assert.AreEqual(exception.Message, ev.Exception.Message);
            }
}
    }
}