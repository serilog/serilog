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

using NUnit.Framework;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Collections.Generic;

namespace Serilog.Sinks.EventStore.Tests
{
    /// <summary>
    /// Test the <see cref="LogEntryEmittedEvent"/> class.
    /// </summary>
    [TestFixture]
    public class LogEntryEmittedEventTests
    {
        /// <summary>
        /// Test that the <see cref="LogEntryEmittedEvent"/> constructor throws an <see cref="ArgumentNullException"/> when the <see cref="LogEvent"/> is null.
        /// </summary>
        [Test]
        public void LogEntryEmittedEvent_ThrowsAnArgumentNullExceptionWhenTheLogEventIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new LogEntryEmittedEvent(null, "test"));
        }

        /// <summary>
        /// Test that the <see cref="LogEntryEmittedEvent"/> constructor throws an <see cref="ArgumentNullException"/> when the RenderedMessage is null.
        /// </summary>
        [Test]
        public void LogEntryEmittedEvent_ThrowsAnArgumentNullExceptionWhenTheRenderedMessageIsNull()
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
            var logEvent = new LogEvent(timestamp, level, exception, template, properties);
            Assert.Throws<ArgumentNullException>(() => new LogEntryEmittedEvent(logEvent, null));
        }

        /// <summary>
        /// Test that the <see cref="LogEntryEmittedEvent"/> constructor throws an <see cref="ArgumentException"/> when the RenderedMessage is an empty string.
        /// </summary>
        [Test]
        public void LogEntryEmittedEvent_ThrowsAnArgumentExceptionWhenTheRenderedMessageIsAnEmptyString()
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
            var logEvent = new LogEvent(timestamp, level, exception, template, properties);
            Assert.Throws<ArgumentException>(() => new LogEntryEmittedEvent(logEvent, String.Empty));
        }

        /// <summary>
        /// Test that the object is successfully constructed.
        /// </summary>
        [Test]
        public void LogEntryEmittedEvent_SuccessfullyConstructsTheObject()
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
            var ev = new LogEntryEmittedEvent(logEvent, logEvent.RenderMessage(null));
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