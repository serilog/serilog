// Copyright 2014 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Newtonsoft.Json;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        /// Test that a single <see cref="LogEntryEmittedEvent"/> is readable after it has been written.
        /// </summary>
        [Test]
        public async Task WhenASingleEventIsWrittenToTheSinkItIsRetrievableFromTheEventStore()
        {
            var timestamp = new DateTimeOffset(2013, 05, 28, 22, 10, 20, 666, TimeSpan.FromHours(10));
            var exception = new ArgumentException("Mládek");
            const LogEventLevel level = LogEventLevel.Information;
            const string messageTemplate = "{Song}++";
            var properties = new List<LogEventProperty>
                                 {
                                     new LogEventProperty("Song", new ScalarValue("New Macabre"))
                                 };
            var template = new MessageTemplateParser().Parse(messageTemplate);
            var logEvent = new Events.LogEvent(timestamp, level, exception, template, properties);

            EventReadResult result = null;
            IEventStoreConnection connection = null;
            using (EventStoreRunner runner = new EventStoreRunner(true))
            {
                using (connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113)))
                {
                    await connection.ConnectAsync();
                    using (EventStoreSink sink = new EventStoreSink(connection, "Logs", EventStoreSink.DefaultBatchPostingLimit, EventStoreSink.DefaultPeriod))
                    {
                        sink.Emit(logEvent);
                    }
                }
                using (connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113)))
                {
                    await connection.ConnectAsync();
                    result = await connection.ReadEventAsync("Logs", StreamPosition.End, false);
                }
            }
            Assert.AreEqual(EventReadStatus.Success, result.Status);
            string data = Encoding.UTF8.GetString(result.Event.Value.OriginalEvent.Data);
            LogEntryEmittedEvent ev = JsonConvert.DeserializeObject<LogEntryEmittedEvent>(data);
            Assert.AreEqual(messageTemplate, ev.MessageTemplate);
            Assert.AreEqual("\"New Macabre\"++", ev.RenderedMessage);
            Assert.AreEqual(timestamp, ev.Timestamp);
            Assert.AreEqual(level, ev.Level);
            Assert.AreEqual(1, ev.Properties.Count);
            Assert.AreEqual("New Macabre", ev.Properties["Song"]);
            Assert.AreEqual(exception.Message, ev.Exception.Message);
        }

        /// <summary>
        /// Test that multiple <see cref="LogEntryEmittedEvent"/> is readable after it has been written.
        /// </summary>
        [Test]
        public async Task WhenMultipleEventsAreWrittenToTheSinkTheyAreRetrievableFromTheEventStore()
        {
            var earlierTimestamp = new DateTimeOffset(2013, 05, 28, 22, 10, 20, 400, TimeSpan.FromHours(10));
            var exception = new ArgumentException("Mládek");
            const LogEventLevel level = LogEventLevel.Information;
            const string messageTemplate = "{Song}++";
            var properties = new List<LogEventProperty>
                                 {
                                     new LogEventProperty("Song", new ScalarValue("New Macabre"))
                                 };
            var template = new MessageTemplateParser().Parse(messageTemplate);
            var earlierLogEvent = new Events.LogEvent(earlierTimestamp, level, exception, template, properties);
            //second event.
            var laterTimestamp = new DateTimeOffset(2013, 05, 28, 22, 15, 20, 600, TimeSpan.FromHours(10));
            var laterLogEvent = new Events.LogEvent(laterTimestamp, level, exception, template, properties);

            var actualLogEvents = new List<LogEntryEmittedEvent>()
                                      {
                                          new LogEntryEmittedEvent(earlierLogEvent, earlierLogEvent.RenderMessage(null)),
                                          new LogEntryEmittedEvent(laterLogEvent, laterLogEvent.RenderMessage(null))
                                      };
            IEventStoreConnection connection = null;
            using (EventStoreRunner runner = new EventStoreRunner())
            {
                using (connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113)))
                {
                    await connection.ConnectAsync();
                    using (EventStoreSink sink = new EventStoreSink(connection, "Logs", EventStoreSink.DefaultBatchPostingLimit, EventStoreSink.DefaultPeriod))
                    {
                        sink.Emit(earlierLogEvent);
                    }
                }

                //write a second event, in a different connection. This tests whether the reading/writing of the last event number in the stream metadata works.
                using (connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113)))
                {
                    await connection.ConnectAsync();
                    using (EventStoreSink sink = new EventStoreSink(connection, "Logs", EventStoreSink.DefaultBatchPostingLimit, EventStoreSink.DefaultPeriod))
                    {
                        sink.Emit(laterLogEvent);
                    }
                }
            }

            //Now, try and read both events using a catchup subscription.
            IList<LogEntryEmittedEvent> readEvents = new List<LogEntryEmittedEvent>();
            using (EventStoreRunner runner = new EventStoreRunner(true))
            using (connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113)))
            {
                await connection.ConnectAsync();
                EventStoreCatchUpSubscription sub = connection.SubscribeToStreamFrom("Logs", StreamCheckpoint.StreamStart, true, (es, re) =>
                {
                    string data = Encoding.UTF8.GetString(re.OriginalEvent.Data);
                    LogEntryEmittedEvent lev = JsonConvert.DeserializeObject<LogEntryEmittedEvent>(data);
                    readEvents.Add(lev);
                }, null, null, new UserCredentials("admin", "changeit"));
                Thread.Sleep(TimeSpan.FromSeconds(30));
            }
            Assert.That(readEvents.Count, Is.EqualTo(2));
            //Assert.That(readEvents, Has.All(actualLogEvents));
        }
    }
}
