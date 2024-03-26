namespace Serilog.Tests.Rendering;

public class MessageTemplateRendererTests
{
    readonly MessageTemplateParser _messageTemplateParser = new();

    [Theory]
    [InlineData("{Number}", null, "16")]
    [InlineData("{Number:X8}", null, "00000010")]
    [InlineData("{Number}", "j", "16")]
    [InlineData("{Number:X8}", "j", "00000010")]
    public void PropertyTokenFormatsAreApplied(string template, string appliedFormat, string expected)
    {
        var eventTemplate = _messageTemplateParser.Parse(template);
        var properties = new Dictionary<string, LogEventPropertyValue> { ["Number"] = new ScalarValue(16) };

        var output = new StringWriter();
        MessageTemplateRenderer.Render(eventTemplate, properties, output, appliedFormat, CultureInfo.InvariantCulture);

        Assert.Equal(expected, output.ToString());
    }
}
