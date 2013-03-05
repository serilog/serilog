using System;
using System.Collections.Generic;
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
                .EnrichedBy(new DelegatingEnricher(le => {
                    thrown = true;
                    throw new Exception("No go, pal."); }))
                .CreateLogger();

            l.Information(Some.String());

            Assert.IsTrue(thrown);
        }

        [Test]
        public void AContextualLoggerAddsTheSourceTypeName()
        {
            IReadOnlyDictionary<string, LogEventProperty> properties = null;
            var l = new LoggerConfiguration()
                .WithSink(new DelegatingSink(le => {
                    properties = le.Properties;
                }))
                .CreateLogger();

            var ctx = l.ForContext<LoggerTests>();
            ctx.Information(Some.String());

            var lv = (LogEventPropertyLiteralValue)properties[Logger.SourceContextPropertyName].Value;
            Assert.AreEqual(typeof(LoggerTests).FullName, lv.Value);
        }
    }
}
