using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Serilog.Core;
using Serilog.Events;
using Serilog.Tests.Support;

#pragma warning disable Serilog004 // Constant MessageTemplate verifier
#pragma warning disable Serilog003 // Property binding verifier

namespace Serilog.Tests.Core
{
    public class LoggerTests
    {
        [Fact]
        public async Task AnExceptionThrownByAnEnricherIsNotPropagated()
        {
            var thrown = false;

            var l = new LoggerConfiguration()
                .WriteTo.Sink(new StringSink())
                .Enrich.With(new DelegatingEnricher((le, pf) =>
                {
                    thrown = true;
                    throw new Exception("No go, pal.");
                }))
                .CreateLogger();

            await l.Information(Some.String());

            Assert.True(thrown);
        }

        [Fact]
        public async Task AContextualLoggerAddsTheSourceTypeName()
        {
            var evt = await DelegatingSink.GetLogEvent(l => l.ForContext<LoggerTests>()
                .Information(Some.String()));

            var lv = evt.Properties[Constants.SourceContextPropertyName].LiteralValue();
            Assert.Equal(typeof(LoggerTests).FullName, lv);
        }

        [Fact]
        public async Task PropertiesInANestedContextOverrideParentContextValues()
        {
            var name = Some.String();
            var v1 = Some.Int();
            var v2 = Some.Int();
            var evt = await DelegatingSink.GetLogEvent(l => l.ForContext(name, v1)
                .ForContext(name, v2)
                .Write(Some.InformationEvent()));

            var pActual = evt.Properties[name];
            Assert.Equal(v2, pActual.LiteralValue());
        }

        [Fact]
        public async Task ParametersForAnEmptyTemplateAreIgnored()
        {
            var e = await DelegatingSink.GetLogEvent(l => l.Error("message", new object()));
            Assert.Equal("message", e.RenderMessage());
        }

        [Fact]
        public async Task LoggingLevelSwitchDynamicallyChangesLevel()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Information);

            var log = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.Sink(sink)
                .CreateLogger()
                .ForContext<LoggerTests>();

            await log.Debug("Suppressed");
            await log.Information("Emitted");
            await log.Warning("Emitted");

            // Change the level
            levelSwitch.MinimumLevel = LogEventLevel.Error;

            await log.Warning("Suppressed");
            await log.Error("Emitted");
            await log.Fatal("Emitted");

            Assert.Equal(4, events.Count);
            Assert.True(events.All(evt => evt.RenderMessage() == "Emitted"));
        }

        [Fact]
        public void MessageTemplatesCanBeBound()
        {
            var log = new LoggerConfiguration()
                .CreateLogger();

            MessageTemplate template;
            IEnumerable<LogEventProperty> properties;
            Assert.True(log.BindMessageTemplate("Hello, {Name}!", new object[] { "World" }, out template, out properties));

            Assert.Equal("Hello, {Name}!", template.Text);
            Assert.Equal("World", properties.Single().Value.LiteralValue());
        }

        [Fact]
        public void PropertiesCanBeBound()
        {
            var log = new LoggerConfiguration()
                .CreateLogger();

            LogEventProperty property;
            Assert.True(log.BindProperty("Name", "World", false, out property));

            Assert.Equal("Name", property.Name);
            Assert.Equal("World", property.Value.LiteralValue());
        }
	}
}
