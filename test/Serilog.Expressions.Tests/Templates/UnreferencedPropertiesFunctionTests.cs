using Serilog.Events;
using Serilog.Parsing;
using Serilog.Templates.Ast;
using Serilog.Templates.Compilation.UnreferencedProperties;
using Serilog.Templates.Parsing;
using Xunit;

namespace Serilog.Expressions.Tests.Templates;

public class UnreferencedPropertiesFunctionTests
{
    [Fact]
    public void UnreferencedPropertiesFunctionIsNamedRest()
    {
        var function = new UnreferencedPropertiesFunction(new LiteralText("test"));
        Assert.True(function.TryResolveFunctionName("Rest", out _));
    }

    [Fact]
    public void UnreferencedPropertiesExcludeThoseInMessageAndTemplate()
    {
        Assert.True(new TemplateParser().TryParse("{@m}{A + 1}{#if true}{B}{@p.C}{@p['D']}{#end}", out var template, out _));

        var function = new UnreferencedPropertiesFunction(template);

        var evt = new LogEvent(
            DateTimeOffset.Now,
            LogEventLevel.Debug,
            null,
            new(new[] {new PropertyToken("E", "{E}")}),
            new[]
            {
                new LogEventProperty("A", new ScalarValue(null)),
                new LogEventProperty("B", new ScalarValue(null)),
                new LogEventProperty("C", new ScalarValue(null)),
                new LogEventProperty("D", new ScalarValue(null)),
                new LogEventProperty("E", new ScalarValue(null)),
                new LogEventProperty("F", new ScalarValue(null)),
            });

        var deep = UnreferencedPropertiesFunction.Implementation(function, evt, new ScalarValue(true));

        var sv = Assert.IsType<StructureValue>(deep);
        var included = Assert.Single(sv.Properties);
        Assert.Equal("F", included.Name);

        var shallow = UnreferencedPropertiesFunction.Implementation(function, evt);
        sv = Assert.IsType<StructureValue>(shallow);
        Assert.Contains(sv.Properties, p => p.Name == "E");
        Assert.Contains(sv.Properties, p => p.Name == "F");
    }
}