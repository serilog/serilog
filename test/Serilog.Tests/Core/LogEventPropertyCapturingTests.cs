namespace Serilog.Tests.Core;

public class LogEventPropertyCapturingTests
{
    [Fact]
    public void CapturingANamedScalarStringWorks()
    {
        Assert.Equal(
            new[] { new LogEventProperty("who", new ScalarValue("world")) },
            Capture("Hello {who}", "world"),
            new LogEventPropertyStructuralEqualityComparer());
    }

    [Fact]
    public void CapturingAPositionalScalarStringWorks()
    {
        Assert.Equal(
            new[] { new LogEventProperty("0", new ScalarValue("world")) },
            Capture("Hello {0}", "world"),
            new LogEventPropertyStructuralEqualityComparer());
    }

    [Fact]
    public void CapturingMixedPositionalAndNamedScalarsWorksUsingNames()
    {
        Assert.Equal(new[]
            {
                new LogEventProperty("who", new ScalarValue("worldNamed")),
                new LogEventProperty("0", new ScalarValue("worldPositional")),
            },
            Capture("Hello {who} {0} {0}", "worldNamed", "worldPositional"),
            new LogEventPropertyStructuralEqualityComparer());
    }

    [Fact]
    public void WillCaptureAnonObjectsAsToStringedScalarsWhenAdditionalNamedPropsNotInTemplate()
    {
        Assert.Equal(new[]
            {
                new LogEventProperty("who", new ScalarValue("who")),
                new LogEventProperty("what", new ScalarValue("what")),
                new LogEventProperty("where", new ScalarValue("{ Num = __2 }")),
                new LogEventProperty("__3", new ScalarValue("{ Num = __3 }")), // because C# anon ToString() does this
            },
            Capture("Hello {who} {what} {where}", "who", "what", new { Num = "__2" }, new { Num = "__3" }),
            new LogEventPropertyStructuralEqualityComparer());
    }

    [Fact]
    public void CapturingTheSameNamedPropertyMatchesValuesCorrectly()
    {
        Assert.Equal(new[]
            {
                new LogEventProperty("who", new ScalarValue("worldNamed")),
                new LogEventProperty("what", new ScalarValue("what1")),
                new LogEventProperty("what", new ScalarValue("what2")),
            },
            Capture("Hello {who} {what} {what}", "worldNamed", "what1", "what2"),
            new LogEventPropertyStructuralEqualityComparer());
    }

    [Fact]
    public void ProvidingLessParametersThanNamedPropertiesInTheTemplateWorksAndSelfLogs()
    {
        var selfLogOutput = new List<string>();
        SelfLog.Enable(selfLogOutput.Add);

        Assert.Equal(new[]
            {
                new LogEventProperty("who", new ScalarValue("who")),
                new LogEventProperty("what", new ScalarValue("what")),
            },
            Capture("Hello {who} {what} {where}", "who", "what"),
            new LogEventPropertyStructuralEqualityComparer());

        Assert.Single(selfLogOutput,
            s => s.EndsWith("Named property count does not match parameter count: Hello {who} {what} {where}"));

        SelfLog.Disable();
    }

    [Fact]
    public void WillCaptureSequenceOfScalarsWhenAdditionalNamedPropsNotInTemplateAreArrays()
    {
        Assert.Equal(new[]
            {
                new LogEventProperty("who", new ScalarValue("who")),
                new LogEventProperty("what", new ScalarValue("what")),
                new LogEventProperty("__2", new SequenceValue(new[] { new ScalarValue("__2") })),
                new LogEventProperty("__3", new SequenceValue(new[] { new ScalarValue("__3") })),
            },
            Capture("Hello {who} {what} where}", "who", "what", new[] { "__2" }, new[] { "__3" }),
            new LogEventPropertyStructuralEqualityComparer());
    }

    [Fact]
    public void WillCaptureAdditionalNamedPropsNotInTemplateUsingUnderscoreIndex()
    {
        Assert.Equal(new[]
            {
                new LogEventProperty("who", new ScalarValue("who")),
                new LogEventProperty("what", new ScalarValue("what")),
                new LogEventProperty("__2", new ScalarValue("__2")),
                new LogEventProperty("__3", new ScalarValue("__3")),
            },
            Capture("Hello {who} {what} where}", "who", "what", "__2", "__3"),
            new LogEventPropertyStructuralEqualityComparer());
    }

    [Fact]
    public void CapturingIntArrayAsScalarSequenceValuesWorks()
    {
        const string template = "Hello {sequence}";
        var templateArguments = new[] { 1, 2, 3, };
        var expected = new[] {
            new LogEventProperty("sequence",
                new SequenceValue(new[] {
                    new ScalarValue(1),
                    new ScalarValue(2),
                    new ScalarValue(3) })) };

        Assert.Equal(expected, Capture(template, templateArguments),
            new LogEventPropertyStructuralEqualityComparer());
    }

    [Fact]
    public void CapturingDestructuredStringArrayAsScalarSequenceValuesWorks()
    {
        const string template = "Hello {@sequence}";
        var templateArguments = new[] { "1", "2", "3", };
        var expected = new[] {
            new LogEventProperty("sequence",
                new SequenceValue(new[] {
                    new ScalarValue("1"),
                    new ScalarValue("2"),
                    new ScalarValue("3") })) };

        Assert.Equal(expected, Capture(template, new object[] { templateArguments }),
            new LogEventPropertyStructuralEqualityComparer());
    }

    [Fact]
    public void WillCaptureProvidedPositionalValuesEvenIfSomeAreMissing()
    {
        Assert.Equal(new[]
            {
                new LogEventProperty("0", new ScalarValue(0)),
                new LogEventProperty("1", new ScalarValue(1)),
            },
            Capture("Hello {3} {2} {1} {0} nothing more", 0, 1), // missing {2} and {3}
            new LogEventPropertyStructuralEqualityComparer());
    }

    [Fact]
    public void WillCaptureProvidedNamedValuesEvenIfSomeAreMissing()
    {
        Assert.Equal(new[]
            {
                new LogEventProperty("who", new ScalarValue("who")),
                new LogEventProperty("what", new ScalarValue("what")),
            },
            Capture("Hello {who} {what} {where}", "who", "what"), // missing "where"
            new LogEventPropertyStructuralEqualityComparer());
    }

    static IEnumerable<LogEventProperty> Capture(string messageTemplate, params object[] properties)
    {
        var mt = new MessageTemplateParser().Parse(messageTemplate);
        var binder = new PropertyBinder(
            new PropertyValueConverter(10, 1000, 1000, Enumerable.Empty<Type>(), Enumerable.Empty<Type>(), Enumerable.Empty<IDestructuringPolicy>(), false, ImmutableDictionary<Type, Destructuring>.Empty));
        return binder.ConstructProperties(mt, properties).Select(p => new LogEventProperty(p.Name, p.Value));
    }
}
