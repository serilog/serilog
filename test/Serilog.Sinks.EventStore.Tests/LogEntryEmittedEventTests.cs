using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Parsing;

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
            Assert.Throws<ArgumentNullException>(()=>new  LogEntryEmittedEvent(null, "test"));
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
            Assert.Throws<ArgumentNullException>(()=>new LogEntryEmittedEvent(logEvent, null));
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
    }
}
