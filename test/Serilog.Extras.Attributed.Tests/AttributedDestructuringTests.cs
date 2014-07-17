using System.Linq;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Extras.Attributed.Tests
{
    [LogAsScalar]
    public class ImmutableScalar { }

    [LogAsScalar(isMutable: true)]
    public class MutableScalar { }

    public class NotAScalar { }

    public class Customized
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public ImmutableScalar ImmutableScalar { get; set; }
        public MutableScalar MutableScalar { get; set; }
        public NotAScalar NotAScalar { get; set; }

        [NotLogged]
        public string Ignored { get; set; }

        [LogAsScalar]
        public NotAScalar ScalarAnyway { get; set; }
    }

    [TestFixture]
    public class AttributedDestructuringTests
    {
        [Test]
        public void AttributesAreConsultedWhenDestructuring()
        {
            LogEvent evt = null;

            var log = new LoggerConfiguration()
                .Destructure.UsingAttributes()
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();

            var customized = new Customized
            {
                ImmutableScalar = new ImmutableScalar(),
                MutableScalar = new MutableScalar(),
                NotAScalar = new NotAScalar(),
                Ignored = "Hello, there",
                ScalarAnyway = new NotAScalar()
            };

            log.Information("Here is {@Customized}", customized);

            var sv = (StructureValue)evt.Properties["Customized"];
            var props = sv.Properties.ToDictionary(p => p.Name, p => p.Value);

            Assert.IsInstanceOf<ImmutableScalar>(props["ImmutableScalar"].LiteralValue());
            Assert.AreEqual(new MutableScalar().ToString(), props["MutableScalar"].LiteralValue());
            Assert.IsInstanceOf<StructureValue>(props["NotAScalar"]);
            Assert.IsFalse(props.ContainsKey("Ignored"));
            Assert.IsInstanceOf<NotAScalar>(props["ScalarAnyway"].LiteralValue());
        }
    }
}
