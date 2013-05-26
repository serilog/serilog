using System.Linq;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Tests.Support;
using Serilog.Core;

namespace Serilog.Tests.Core
{
    [TestFixture]
    public class CopyingSinkTests
    {
        [Test]
        public void ModifyingCopiesPassedThroughTheSinkPreservesOriginal()
        {
            var e = Some.InformationEvent();
            LogEvent copy = null;
            new LoggerConfiguration()
                .WriteTo.Sink(new CopyingSink(new DelegatingSink(le => copy = le)))
                .CreateLogger()
                .Write(e);
            
            Assert.AreNotSame(e, copy);
            var p = Some.LogEventProperty();
            copy.AddPropertyIfAbsent(p);
            Assert.IsTrue(copy.Properties.ContainsKey(p.Name));
            Assert.IsFalse(e.Properties.ContainsKey(p.Name));
        }
    }
}
