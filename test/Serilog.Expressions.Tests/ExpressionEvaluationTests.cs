using System.Diagnostics;
using System.Globalization;
using Serilog.Events;
using Serilog.Expressions.Runtime;
using Serilog.Expressions.Tests.Support;
using Serilog.Parsing;
using Xunit;

namespace Serilog.Expressions.Tests;

public class ExpressionEvaluationTests
{
    public static IEnumerable<object[]> ExpressionEvaluationCases =>
        AsvCases.ReadCases("expression-evaluation-cases.asv");

    [Theory]
    [MemberData(nameof(ExpressionEvaluationCases))]
    public void ExpressionsAreCorrectlyEvaluated(string expr, string result)
    {
        var evt = new LogEvent(
            new DateTimeOffset(2025, 5, 15, 13, 12, 11, 789, TimeSpan.FromHours(10)),
            LogEventLevel.Warning,
            new DivideByZeroException(),
            new MessageTemplateParser().Parse("Hello, {Name}!"),
            new []
            {
                new LogEventProperty("Name", new ScalarValue("World")),
                new LogEventProperty("User", new StructureValue(new[]
                {
                    new LogEventProperty("Id", new ScalarValue(42)),
                    new LogEventProperty("Name", new ScalarValue("nblumhardt")),
                }))
            },
            ActivityTraceId.CreateFromString("1befc31e94b01d1a473f63a7905f6c9b"),
            ActivitySpanId.CreateFromString("bb1111820570b80e"));

        var frFr = CultureInfo.GetCultureInfoByIetfLanguageTag("fr-FR");
        var testHelpers = new TestHelperNameResolver();
        var actual = SerilogExpression.Compile(expr, formatProvider: frFr, testHelpers)(evt);
        var expected = SerilogExpression.Compile(result, nameResolver: testHelpers)(evt);

        if (expected is null)
        {
            Assert.True(actual is null, $"Expected value: undefined{Environment.NewLine}Actual value: {Display(actual)}");
        }
        else
        {
            Assert.True(Coerce.IsTrue(RuntimeOperators._Internal_Equal(StringComparison.OrdinalIgnoreCase, actual, expected)), $"Expected value: {Display(expected)}{Environment.NewLine}Actual value: {Display(actual)}");
        }
    }

    static string Display(LogEventPropertyValue? value)
    {
        if (value == null)
            return "undefined";

        return value.ToString();
    }
}