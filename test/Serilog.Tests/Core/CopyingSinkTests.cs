using Xunit;
using Serilog.Core.Sinks;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Tests.Core
{
    public class CopyingSinkTests
    {
        [Fact]
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
