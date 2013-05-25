using System;
using System.Globalization;
using NUnit.Framework;
using Serilog.Core;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Tests.Core
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public void AnExceptionThrownByAnEnricherIsNotPropagated()
        {
            var thrown = false;

            var l = new LoggerConfiguration()
                .Enrich.With(new DelegatingEnricher((le, pf) => {
                    thrown = true;
                    throw new Exception("No go, pal."); }))
                .CreateLogger();

            l.Information(Some.String());

            Assert.IsTrue(thrown);
        }

        [Test]
        public void AContextualLoggerAddsTheSourceTypeName()
        {
            var evt = GetLogEvent(l => l.ForContext<LoggerTests>()
                                        .Information(Some.String()));

            var lv = evt.Properties[Logger.SourceContextPropertyName].LiteralValue();
            Assert.AreEqual(typeof(LoggerTests).FullName, lv);
        }

        [Test]
        public void PropertiesInANestedContextOverrideParentContextValues()
        {
            var name = Some.String();
            var v1 = Some.Int();
            var v2 = Some.Int();
            var evt = GetLogEvent(l => l.ForContext(name, v1)
                                        .ForContext(name, v2)
                                        .Write(Some.LogEvent()));

            var pActual = evt.Properties[name];
            Assert.AreEqual(v2, pActual.LiteralValue());
        }

        [Test]
        public void UsesFormatProvider()
        {
            var french = CultureInfo.GetCultureInfo("fr-FR");
            var evt = GetLogEventWithFormatProvider(french, l => l.Information("{0}", 12.345));

            Assert.AreEqual("12,345", evt.RenderedMessage);
        }

        [Test]
        public void FormatProviderCanBeOverriden()
        {
            var french = CultureInfo.GetCultureInfo("fr-FR");
            var us = CultureInfo.GetCultureInfo("en-US");
            var evt = GetLogEventWithFormatProvider(french, l => l.Information(us, "{0}", 12.345));

            Assert.AreEqual("12.345", evt.RenderedMessage);
        }

        static LogEvent GetLogEvent(Action<ILogger> writeAction)
        {
            LogEvent result = null;
            var l = new LoggerConfiguration()
                .WriteTo.Sink(new DelegatingSink(le => result = le))
                .CreateLogger();

            writeAction(l);
            return result;
        }

        static LogEvent GetLogEventWithFormatProvider(IFormatProvider formatProvider, Action<ILogger> writeAction)
        {
            LogEvent result = null;
            var l = new LoggerConfiguration()
                .WriteTo.Sink(new DelegatingSink(le => result = le))
                .FormatUsing(formatProvider)
                .CreateLogger();

            writeAction(l);
            return result;
        }
    }
}
