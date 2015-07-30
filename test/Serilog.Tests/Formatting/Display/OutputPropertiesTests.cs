using System;
using System.IO;
using NUnit.Framework;
using Serilog.Debugging;
using Serilog.Formatting.Display;
using Serilog.Tests.Support;

namespace Serilog.Tests.Formatting.Display
{
    [TestFixture]
    public class OutputPropertiesTests
    {
        [Test]
        public void BadExceptionRendersIntoOutput()
        {
            const string exceptionMessage = "Test Message";
            Exception exception = new BadException(exceptionMessage);
            var evt = DelegatingSink.GetLogEvent(l => l.Error(exception, "Something went wrong"));

            var outputProperties = OutputProperties.GetOutputProperties(evt);
            Assert.IsTrue(outputProperties.ContainsKey(OutputProperties.ExceptionPropertyName));
            Assert.That(outputProperties[OutputProperties.ExceptionPropertyName].ToString(), Is.StringContaining(exceptionMessage));
        }

        [Serial]
        [Test]
        public void ExceptionRenderingExceptionLogsToSelfLog()
        {
            StringWriter writer = new StringWriter();
            SelfLog.Out = writer;

            const string exceptionMessage = "Test Message";
            Exception exception = new BadException(exceptionMessage);
            var evt = DelegatingSink.GetLogEvent(l => l.Error(exception, "Something went wrong"));

            var outputProperties = OutputProperties.GetOutputProperties(evt);
            Assert.IsNotEmpty(writer.ToString());
        }

        [Test]
        public void NoExceptionRendersEmptyStringForException()
        {
            var evt = DelegatingSink.GetLogEvent(l => l.Error("No Exception Logged"));

            var outputProperties = OutputProperties.GetOutputProperties(evt);
            Assert.IsTrue(outputProperties.ContainsKey(OutputProperties.ExceptionPropertyName));
            Assert.IsEmpty(outputProperties[OutputProperties.ExceptionPropertyName].ToString());
        }

        [Test]
        public void RendersExceptionIntoOutput()
        {
            const string exceptionMessage = "Test Message";
            Exception exception = new Exception(exceptionMessage);
            var evt = DelegatingSink.GetLogEvent(l => l.Error(exception, "Something went wrong"));

            var outputProperties = OutputProperties.GetOutputProperties(evt);
            Assert.IsTrue(outputProperties.ContainsKey(OutputProperties.ExceptionPropertyName));
            Assert.That(outputProperties[OutputProperties.ExceptionPropertyName].ToString(), Is.StringContaining(exceptionMessage));
        }

        [Test]
        public void VeryBadExceptionRendersFailureMessageIntoOutput()
        {
            const string exceptionMessage = "Test Message";
            Exception exception = new VeryBadException(exceptionMessage);
            var evt = DelegatingSink.GetLogEvent(l => l.Error(exception, "Something went wrong"));

            var outputProperties = OutputProperties.GetOutputProperties(evt);
            Assert.IsTrue(outputProperties.ContainsKey(OutputProperties.ExceptionPropertyName));
            Assert.That(outputProperties[OutputProperties.ExceptionPropertyName].ToString(), Is.StringContaining(OutputProperties.FailedToRenderExceptionMessage));
        }

        private class BadException : Exception
        {
            public BadException(string message) : base(message)
            {
            }

            public override string ToString()
            {
                throw new ApplicationException("Bad Exception");
            }
        }

        private class VeryBadException : Exception
        {
            public VeryBadException(string message) : base(message)
            {
            }

            public override string Message
            {
                get { throw new ApplicationException("Very Bad Exception"); }
            }
        }
    }
}