using Serilog.Expressions.Tests.Support;
using Serilog.Templates;
using Xunit;

namespace Serilog.Expressions.Tests;

public class TemplateEncodingTests
{
    public static IEnumerable<object[]> TemplateEvaluationCases =>
        AsvCases.ReadCases("template-encoding-cases.asv");

    [Theory]
    [MemberData(nameof(TemplateEvaluationCases))]
    public void TemplatesAreCorrectlyEvaluated(string template, string expected)
    {
        var evt = Some.InformationEvent("Hello, {Name}!", "nblumhardt");
        var compiled = new ExpressionTemplate(template, encoder: new ParenthesizingEncoder());
        var output = new StringWriter();
        compiled.Format(evt, output);
        var actual = output.ToString();
        Assert.Equal(expected, actual);
    }

    // Either theme or encoding must be applied first; although it's possible to imagine future scenarios (themes as HTML elements with styles...) that
    // might combine these both in a more sophisticated way, the current implementation chooses to wrap the entire output in the encoder, instead
    // of the other way around, because it's much easier to exclude any possibility of missed encoding using this approach.
    [Fact]
    public void EncodingAppliesToThemedOutput()
    {
        var evt = Some.InformationEvent("Hello, {Name}!", "nblumhardt");
        
        var compiled = new ExpressionTemplate("-{@m}-",
            theme: StringHashPrefixingTheme.Instance,
            encoder: new ParenthesizingEncoder(),
            applyThemeWhenOutputIsRedirected: true);
        
        var output = new StringWriter();
        compiled.Format(evt, output);
        var actual = output.ToString();
        Assert.Equal("-(Hello, #nblumhardt\x1b[0m!)-", actual);
    }
}