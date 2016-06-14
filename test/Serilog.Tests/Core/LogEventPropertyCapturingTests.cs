using System;
using System.Linq;
using System.Collections.Generic;

using Serilog.Core;
using Serilog.Events;
using Serilog.Parameters;
using Serilog.Parsing;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Core
{
    public class LogEventPropertyCapturingTests
    {
        [Fact]
        public void CapturingANamedScalarStringWorks()
        {
            Assert.Equal(
                new[] { new LogEventProperty("who", new ScalarValue("world")) },
                Capture("Hello {who}", "world"),
                new LogEventPropertyStructuralEqualityComparer());
        }

        [Fact]
        public void CapturingAPositionalScalarStringWorks()
        {
            Assert.Equal(
                new[] { new LogEventProperty("0", new ScalarValue("world")) },
                Capture("Hello {0}", "world"),
                new LogEventPropertyStructuralEqualityComparer());
        }

        [Fact]
        public void CapturingMixedPositionalAndNamedScalarsWorksUsingNames()
        {
            Assert.Equal(new[]
                {
                    new LogEventProperty("who", new ScalarValue("worldNamed")),
                    new LogEventProperty("0", new ScalarValue("worldPositional")),
                },
                Capture("Hello {who} {0} {0}", "worldNamed", "worldPositional"),
                new LogEventPropertyStructuralEqualityComparer());
        }

        [Fact]
        public void WillCaptureProvidedPositionalValuesEvenIfSomeAreMissing()
        {
            Assert.Equal(new[]
                {
                    new LogEventProperty("0", new ScalarValue(0)),
                    new LogEventProperty("1", new ScalarValue(1)),
                },
                Capture("Hello {3} {2} {1} {0} nothing more", 0, 1), // missing {2} and {3}
                new LogEventPropertyStructuralEqualityComparer());
        }

        [Fact]
        public void WillCaptureProvidedPositionalValuesEvenIfSomeEarlierLowerNumberedAreMissing()
        {
            Assert.Equal(new[]
                {
                    new LogEventProperty("__0", new ScalarValue(0)),
                    new LogEventProperty("1", new ScalarValue(1)),
                    new LogEventProperty("2", new ScalarValue(2)),
                },
                Capture("Hello {1} {2} nothing more", 0, 1, 2), // missing {0}
                new LogEventPropertyStructuralEqualityComparer());
        }

        [Fact]
        public void WillCaptureProvidedNamedValuesEvenIfSomeAreMissing()
        {
            Assert.Equal(new[]
                {
                    new LogEventProperty("who", new ScalarValue("who")),
                    new LogEventProperty("what", new ScalarValue("what")),
                },
                Capture("Hello {who} {what} {where}", "who", "what"), // missing "where"
                new LogEventPropertyStructuralEqualityComparer());
        }

        [Fact]
        public void WillCaptureAdditionalPositionalsNotInTemplate()
        {
            Assert.Equal(new[]
                {
                    new LogEventProperty("0", new ScalarValue(0)),
                    new LogEventProperty("1", new ScalarValue(1)),
                    new LogEventProperty("__2", new ScalarValue("__2")),
                    new LogEventProperty("__3", new ScalarValue("__3")),
                },
                Capture("Hello {0} {1} nothing more", 0, 1, "__2", "__3"),
                new LogEventPropertyStructuralEqualityComparer());
        }

        [Fact]
        public void WillCaptureAdditionalPositionalsNotInTemplatePreservingPositionsInTemplate()
        {
            Assert.Equal(new[]
                {
                    new LogEventProperty("0", new ScalarValue(0)),
                    new LogEventProperty("1", new ScalarValue(1)),
                    new LogEventProperty("__2", new ScalarValue("__2")),
                    new LogEventProperty("__3", new ScalarValue("__3")),
                },
                Capture("Hello {1} {0} nothing more", 0, 1, "__2", "__3"),
                new LogEventPropertyStructuralEqualityComparer());
        }

        [Fact]
        public void WillCaptureAdditionalNamedPropsNotInTemplate()
        {
            Assert.Equal(new[]
                {
                    new LogEventProperty("who", new ScalarValue("who")),
                    new LogEventProperty("what", new ScalarValue("what")),
                    new LogEventProperty("__2", new ScalarValue("__2")),
                    new LogEventProperty("__3", new ScalarValue("__3")),
                },
                Capture("Hello {who} {what} where}", "who", "what", "__2", "__3"),
                new LogEventPropertyStructuralEqualityComparer());
        }

        static IEnumerable<LogEventProperty> Capture(string messageTemplate, params object[] properties)
        {
            var mt = new MessageTemplateParser().Parse(messageTemplate);
            var binder = new PropertyBinder(
                new PropertyValueConverter(10, Enumerable.Empty<Type>(), Enumerable.Empty<IDestructuringPolicy>()));
            return binder.ConstructProperties(mt, properties);
        }

    }
}
