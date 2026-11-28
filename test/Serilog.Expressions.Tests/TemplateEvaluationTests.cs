using System.Globalization;
using Serilog.Events;
using Serilog.Expressions.Tests.Support;
using Serilog.Templates;
using Xunit;

namespace Serilog.Expressions.Tests;

public class TemplateEvaluationTests
{
    static readonly DateTimeOffset TestTimestamp = new(
        2000, 12, 31, 23, 59, 58, 123, TimeSpan.FromHours(10));

    public static IEnumerable<object[]> TemplateEvaluationCases =>
        AsvCases.ReadCases("template-evaluation-cases.asv");

    [Theory]
    [MemberData(nameof(TemplateEvaluationCases))]
    public void TemplatesAreCorrectlyEvaluated(string template, string expected)
    {
        var evt = Some.InformationEvent(TestTimestamp, "Hello, {Name}!", "nblumhardt");
        var frFr = CultureInfo.GetCultureInfoByIetfLanguageTag("fr-FR");
        var compiled = new ExpressionTemplate(template, formatProvider: frFr);
        var output = new StringWriter();
        compiled.Format(evt, output);
        var actual = output.ToString();
        Assert.Equal(expected, actual);
    }
}