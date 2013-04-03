using System;
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
                .EnrichedBy(new DelegatingEnricher((le, pf) => {
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

        static LogEvent GetLogEvent(Action<ILogger> writeAction)
        {
            LogEvent result = null;
            var l = new LoggerConfiguration()
                .WithSink(new DelegatingSink(le => result = le))
                .CreateLogger();

            writeAction(l);
            return result;
        }
    }
}
