using System;
using System.Diagnostics;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Extras.TraceListener.Tests
{
    [TestFixture]
    public class SerilogTraceListenerTests
    {
        const TraceEventType WarningEventType = TraceEventType.Warning;
        readonly string _category = Some.String("category");
        readonly int _id = Some.Int();
        readonly string _message = Some.String("message");
        readonly string _source = Some.String("source");
        readonly TraceEventCache _traceEventCache = new TraceEventCache();

        SerilogTraceListener _traceListener;
        LogEvent _loggedEvent;

        [SetUp]
        public void SetUp()
        {
            var delegatingSink = new DelegatingSink(evt => { _loggedEvent = evt; });
            var logger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Sink(delegatingSink).CreateLogger();

            _loggedEvent = null;
            _traceListener = new SerilogTraceListener(logger);
        }

        [TearDown]
        public void TearDown()
        {
            _traceListener.Dispose();
        }

        [Test]
        public void CapturesWrite()
        {
            _traceListener.Write(_message);

            LogEventAssert.HasMessage(_message, _loggedEvent);
            LogEventAssert.HasLevel(LogEventLevel.Debug, _loggedEvent);
            LogEventAssert.HasPropertyValue(typeof(SerilogTraceListener).ToString(), "SourceContext", _loggedEvent);
        }

        [Test]
        public void CapturesWriteWithCategory()
        {
            _traceListener.Write(_message, _category);

            LogEventAssert.HasMessage(string.Format("{0}: {1}", _category, _message), _loggedEvent);
            LogEventAssert.HasLevel(LogEventLevel.Debug, _loggedEvent);
            LogEventAssert.HasPropertyValue(_category, "Category", _loggedEvent);
        }

        [Test]
        public void CapturesWriteOfObjectUsingToString()
        {
            var writtenObject = Tuple.Create(Some.String());
            _traceListener.Write(writtenObject);

            LogEventAssert.HasMessage(writtenObject.ToString(), _loggedEvent);
            LogEventAssert.HasLevel(LogEventLevel.Debug, _loggedEvent);
        }

        [Test]
        public void CapturesWriteOfObjectUsingToStringWithCategory()
        {
            var writtenObject = Tuple.Create(Some.String());
            _traceListener.Write(writtenObject, _category);

            LogEventAssert.HasMessage(string.Format("{0}: {1}", _category, writtenObject), _loggedEvent);
            LogEventAssert.HasLevel(LogEventLevel.Debug, _loggedEvent);
            LogEventAssert.HasPropertyValue(_category, "Category", _loggedEvent);
        }

        [Test]
        public void CapturesWriteLine()
        {
            _traceListener.WriteLine(_message);

            LogEventAssert.HasMessage(_message, _loggedEvent);
            LogEventAssert.HasLevel(LogEventLevel.Debug, _loggedEvent);
        }

        [Test]
        public void CapturesWriteLineWithCategory()
        {
            _traceListener.WriteLine(_message, _category);

            LogEventAssert.HasMessage(string.Format("{0}: {1}", _category, _message), _loggedEvent);
            LogEventAssert.HasLevel(LogEventLevel.Debug, _loggedEvent);
            LogEventAssert.HasPropertyValue(_category, "Category", _loggedEvent);
        }

        [Test]
        public void CapturesWriteLineOfObjectUsingToString()
        {
            var writtenObject = Tuple.Create(Some.String());
            _traceListener.WriteLine(writtenObject);

            LogEventAssert.HasMessage(writtenObject.ToString(), _loggedEvent);
            LogEventAssert.HasLevel(LogEventLevel.Debug, _loggedEvent);
        }

        [Test]
        public void CapturesWriteLineOfObjectUsingToStringWithCategory()
        {
            var writtenObject = Tuple.Create(Some.String());
            _traceListener.WriteLine(writtenObject, _category);

            LogEventAssert.HasMessage(string.Format("{0}: {1}", _category, writtenObject), _loggedEvent);
            LogEventAssert.HasLevel(LogEventLevel.Debug, _loggedEvent);
            LogEventAssert.HasPropertyValue(_category, "Category", _loggedEvent);
        }

        [Test]
        public void CapturesFail()
        {
            _traceListener.Fail(_message);

            LogEventAssert.HasMessage("Fail: " + _message, _loggedEvent);
            LogEventAssert.HasLevel(LogEventLevel.Fatal, _loggedEvent);
        }

        [Test]
        public void CapturesFailWithDetailedDescription()
        {
            var detailMessage = Some.String();

            _traceListener.Fail(_message, detailMessage);

            LogEventAssert.HasMessage(string.Format("Fail: {0} {1}", _message, detailMessage), _loggedEvent);
            LogEventAssert.HasLevel(LogEventLevel.Fatal, _loggedEvent);
            LogEventAssert.HasPropertyValue(detailMessage, "FailDetails", _loggedEvent);
        }

        [Test]
        public void StopsCapturingAfterCloseIsCalled()
        {
            _traceListener.Close();

            _traceListener.Write(_message);

            Assert.That(_loggedEvent, Is.Null);
        }

        [Test]
        public void CapturesTraceEvent()
        {
            _traceListener.TraceEvent(_traceEventCache, _source, WarningEventType, _id);

            LogEventAssert.HasMessage(string.Format("{0} {1}: {2}", _source, WarningEventType, _id), _loggedEvent);

            LogEventAssert.HasLevel(LogEventLevel.Warning, _loggedEvent);

            LogEventAssert.HasPropertyValue(_id, "TraceEventId", _loggedEvent);
            LogEventAssert.HasPropertyValue(_source, "TraceSource", _loggedEvent);
            LogEventAssert.HasPropertyValue(WarningEventType, "TraceEventType", _loggedEvent);
        }

        [Test]
        public void CapturesTraceEventWithMessage()
        {
            _traceListener.TraceEvent(_traceEventCache, _source, WarningEventType, _id, _message);

            LogEventAssert.HasMessage(
                string.Format("{0} {1}: {2} :" + Environment.NewLine + "{3}", _source, WarningEventType, _id, _message),
                _loggedEvent);

            LogEventAssert.HasLevel(LogEventLevel.Warning, _loggedEvent);

            LogEventAssert.HasPropertyValue(_id, "TraceEventId", _loggedEvent);
            LogEventAssert.HasPropertyValue(_source, "TraceSource", _loggedEvent);
            LogEventAssert.HasPropertyValue(WarningEventType, "TraceEventType", _loggedEvent);
        }

        [Test]
        public void CapturesTraceEventWithFormatMessage()
        {
            const string format = "{0}-{1}-{2}";
            var args = new object[]
            {
                Some.String(),
                Some.String(),
                Some.String()
            };

            _traceListener.TraceEvent(_traceEventCache, _source, WarningEventType, _id, format, args);

            LogEventAssert.HasMessage(
                string.Format("{0} {1}: {2} :" + Environment.NewLine + "{3}", _source, WarningEventType, _id, string.Format(format, args)),
                _loggedEvent);

            LogEventAssert.HasLevel(LogEventLevel.Warning, _loggedEvent);

            LogEventAssert.HasPropertyValue(_id, "TraceEventId", _loggedEvent);
            LogEventAssert.HasPropertyValue(_source, "TraceSource", _loggedEvent);
            LogEventAssert.HasPropertyValue(WarningEventType, "TraceEventType", _loggedEvent);
        }

        [Test]
        public void CapturesTraceTransfer()
        {
            var relatedActivityId = Some.Guid();

            _traceListener.TraceTransfer(_traceEventCache, _source, _id, _message, relatedActivityId);

            LogEventAssert.HasMessage(
                string.Format("{0} Transfer: {1} :" + Environment.NewLine + "{2}, relatedActivityId={3}", _source, _id, _message, relatedActivityId),
                _loggedEvent);

            LogEventAssert.HasLevel(LogEventLevel.Debug, _loggedEvent);

            LogEventAssert.HasPropertyValue(_id, "TraceEventId", _loggedEvent);
            LogEventAssert.HasPropertyValue(_source, "TraceSource", _loggedEvent);
            LogEventAssert.HasPropertyValue(relatedActivityId, "RelatedActivityId", _loggedEvent);
        }

        [Test]
        public void CapturesTraceData()
        {
            var data = new
            {
                Info = Some.String()
            };

            _traceListener.TraceData(_traceEventCache, _source, WarningEventType, _id, data);

            LogEventAssert.HasMessage(
                string.Format("{0} {1}: {2} :" + Environment.NewLine + "{3}", _source, WarningEventType, _id, data),
                _loggedEvent);

            LogEventAssert.HasLevel(LogEventLevel.Warning, _loggedEvent);

            LogEventAssert.HasPropertyValue(_id, "TraceEventId", _loggedEvent);
            LogEventAssert.HasPropertyValue(_source, "TraceSource", _loggedEvent);
            LogEventAssert.HasPropertyValue(data.ToString(), "TraceData", _loggedEvent);
        }

        [Test]
        public void CapturesTraceDataWithMultipleData()
        {
            var data1 = new
            {
                Info = Some.String()
            };
            var data2 = new
            {
                Info = Some.String()
            };
            var data3 = new
            {
                Info = Some.Int()
            };

            _traceListener.TraceData(_traceEventCache, _source, WarningEventType, _id, data1, data2, data3);

            // The square-brackets ('[' ,']') are because of serilog behavior and are not how the stock TraceListener would behave.
            LogEventAssert.HasMessage(
                string.Format("{0} {1}: {2} :" + Environment.NewLine + "[{3}]", _source, WarningEventType, _id, string.Join(", ", data1, data2, data3)),
                _loggedEvent);

            LogEventAssert.HasLevel(LogEventLevel.Warning, _loggedEvent);

            LogEventAssert.HasPropertyValue(_id, "TraceEventId", _loggedEvent);
            LogEventAssert.HasPropertyValue(_source, "TraceSource", _loggedEvent);
            LogEventAssert.HasPropertyValueSequenceValue(new object[]
            {
                data1,
                data2,
                data3
            }, "TraceData", _loggedEvent);
        }
    }
}