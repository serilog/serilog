using Serilog.Templates.Encoding;

namespace Serilog.Expressions.Tests.Support;

public class ParenthesizingEncoder : TemplateOutputEncoder
{
    public override string Encode(string value)
    {
        return $"({value})";
    }
}
