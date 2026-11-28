using Serilog.Events;
using Serilog.Expressions.Tests.Support;
using Serilog.Formatting;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Parsing;
using Serilog.Templates;
using Xunit;

namespace Serilog.Expressions.Tests;

/// <summary>
/// These tests track the ability of Serilog.Expressions to faithfully reproduce the JSON formats implemented in
/// Serilog and Serilog.Formatting.Compact. The tests jump through a few hoops to achieve byte-for-byte correctness;
/// in practice, valid JSON in these formats can be constructed with simpler templates.
/// </summary>
public class FormatParityTests
{
    // Implements CLEF-style `@@` escaping of property names that begin with `@`.
    // ReSharper disable once UnusedMember.Global
    public static LogEventPropertyValue? ClefEscape(LogEventPropertyValue? logEventProperties)
    {
        if (logEventProperties is not StructureValue st)
            return null;

        foreach (var check in st.Properties)
        {
            if (check.Name.Length > 0 && check.Name[0] == '@')
            {
                var properties = new List<LogEventProperty>();

                foreach (var member in st.Properties)
                {
                    var property = new LogEventProperty(
                        member.Name.Length > 0 && member.Name[0] == '@' ? "@" + member.Name : member.Name,
                        member.Value);

                    properties.Add(property);
                }

                return new StructureValue(properties, st.TypeTag);
            }
        }

        return logEventProperties;
    }

    // Renders a message template with old-style "quoted" strings (expression templates use the newer :lj formatting always).
    // ReSharper disable once UnusedMember.Global
    public static LogEventPropertyValue? ClassicRender(LogEventPropertyValue? messageTemplate, LogEventPropertyValue? properties)
    {
        if (messageTemplate is not ScalarValue {Value: string smt} ||
            properties is not StructureValue stp)
        {
            return null;
        }

        var mt = new MessageTemplateParser().Parse(smt);
        var space = new StringWriter();
        mt.Render(stp.Properties.ToDictionary(p => p.Name, p => p.Value), space);
        return new ScalarValue(space.ToString());
    }

    // Constructs the Renderings property used in the old JSON format.
    // ReSharper disable once UnusedMember.Global
    public static LogEventPropertyValue? ClassicRenderings(LogEventPropertyValue? messageTemplate, LogEventPropertyValue? properties)
    {
        if (messageTemplate is not ScalarValue {Value: string smt} ||
            properties is not StructureValue stp)
        {
            return null;
        }

        var mt = new MessageTemplateParser().Parse(smt);
        var tokensWithFormat = mt.Tokens
            .OfType<PropertyToken>()
            .Where(pt => pt.Format != null)
            .GroupBy(pt => pt.PropertyName);

        // ReSharper disable once PossibleMultipleEnumeration
        if (!tokensWithFormat.Any())
            return null;

        var propertiesByName = stp.Properties.ToDictionary(p => p.Name, p => p.Value);

        var renderings = new List<LogEventProperty>();
        // ReSharper disable once PossibleMultipleEnumeration
        foreach (var propertyFormats in tokensWithFormat)
        {
            var values = new List<LogEventPropertyValue>();

            foreach (var format in propertyFormats)
            {
                var sw = new StringWriter();

                format.Render(propertiesByName, sw);

                values.Add(new StructureValue(new []
                {
                    new LogEventProperty("Format", new ScalarValue(format.Format)),
                    new LogEventProperty("Rendering", new ScalarValue(sw.ToString())),
                }));
            }

            renderings.Add(new(propertyFormats.Key, new SequenceValue(values)));
        }

        return new StructureValue(renderings);
    }

    readonly ITextFormatter
        _clef = new CompactJsonFormatter(),
        _renderedClef = new RenderedCompactJsonFormatter(),
        _classic = new JsonFormatter(),
        _clefExpression = new ExpressionTemplate(
            "{ {@t: UtcDateTime(@t), @mt, @r, @l: if @l = 'Information' then undefined() else @l, @x, ..ClefEscape(@p)} }" + Environment.NewLine,
            null, new StaticMemberNameResolver(typeof(FormatParityTests))),
        _renderedClefExpression = new ExpressionTemplate(
            "{ {@t: UtcDateTime(@t), @m: ClassicRender(@mt, @p), @i: ToString(@i, 'x8'), @l: if @l = 'Information' then undefined() else @l, @x, ..ClefEscape(@p)} }" + Environment.NewLine,
            null, new StaticMemberNameResolver(typeof(FormatParityTests))),
        _classicExpression = new ExpressionTemplate(
            "{ {Timestamp: @t, Level: @l, MessageTemplate: @mt, Exception: @x, Properties: if IsDefined(@p[?]) then @p else undefined(), Renderings: ClassicRenderings(@mt, @p)} }" + Environment.NewLine,
            null, new StaticMemberNameResolver(typeof(FormatParityTests)));

    static string Render(
        ITextFormatter formatter,
        LogEvent logEvent)
    {
        var space = new StringWriter();
        formatter.Format(logEvent, space);
        return space.ToString();
    }

    void AssertWriteParity(
        LogEventLevel level,
        Exception? exception,
        string messageTemplate,
        params object[] propertyValues)
    {
        var sink = new CollectingSink();
        using (var log = new LoggerConfiguration()
                   .MinimumLevel.Is(LevelAlias.Minimum)
                   .WriteTo.Sink(sink)
                   .CreateLogger())
        {
            log.Write(level, exception, messageTemplate, propertyValues);
        }

        var clef = Render(_clef, sink.SingleEvent);
        var clefExpression = Render(_clefExpression, sink.SingleEvent);
        Assert.Equal(clef, clefExpression);

        var renderedClef = Render(_renderedClef, sink.SingleEvent);
        var renderedClefExpression = Render(_renderedClefExpression, sink.SingleEvent);
        Assert.Equal(renderedClef, renderedClefExpression);

        var renderedClassic = Render(_classic, sink.SingleEvent);
        var renderedClassicExpression = Render(_classicExpression, sink.SingleEvent);
        Assert.Equal(renderedClassic, renderedClassicExpression);
    }

    [Fact]
    public void ParityIsMaintained()
    {
        AssertWriteParity(LogEventLevel.Information, null, "Hello, world!");
        AssertWriteParity(LogEventLevel.Debug, new(), "Hello, {Name}, {Number:000}, {Another:#.00}", "world", 42, 3.1);
    }
}