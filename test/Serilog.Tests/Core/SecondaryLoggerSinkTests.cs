using System;
using NUnit.Framework;
using Serilog.Tests.Support;

namespace Serilog.Tests.Core
{
    [TestFixture]
    public class SecondaryLoggerSinkTests
    {
        [Test]
        public void ModifyingCopiesPassedThroughTheSinkPreservesOriginal()
        {
            var secondary = new CollectingSink();
            var secondaryLogger = new LoggerConfiguration()
                .WriteTo.Sink(secondary)
                .CreateLogger();

            var e = Some.InformationEvent();
            new LoggerConfiguration()
                .WriteTo.Logger(secondaryLogger)
                .CreateLogger()
                .Write(e);
            
            Assert.AreNotSame(e, secondary.SingleEvent);
            var p = Some.LogEventProperty();
            secondary.SingleEvent.AddPropertyIfAbsent(p);
            Assert.IsTrue(secondary.SingleEvent.Properties.ContainsKey(p.Name));
            Assert.IsFalse(e.Properties.ContainsKey(p.Name));
        }

        [Test]
        public void WhenOwnedByCallerSecondaryLoggerIsNotDisposed()
        {
            var secondary = new DisposeTrackingSink();
            var secondaryLogger = new LoggerConfiguration()
                .WriteTo.Sink(secondary)
                .CreateLogger();

            ((IDisposable)new LoggerConfiguration()
                .WriteTo.Logger(secondaryLogger)
                .CreateLogger()).Dispose();

            Assert.IsFalse(secondary.IsDisposed);
        }

        [Test]
        public void WhenOwnedByPrimaryLoggerSecondaryIsDisposed()
        {
            var secondary = new DisposeTrackingSink();

            ((IDisposable)new LoggerConfiguration()
                .WriteTo.Logger(lc => lc.WriteTo.Sink(secondary))
                .CreateLogger()).Dispose();

            Assert.That(secondary.IsDisposed);
        }
    }
}
