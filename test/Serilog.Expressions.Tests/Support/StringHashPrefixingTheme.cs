using Serilog.Templates.Themes;

namespace Serilog.Expressions.Tests.Support;

static class StringHashPrefixingTheme
{
    public static readonly TemplateTheme Instance = new(new Dictionary<TemplateThemeStyle, string>
    {
        [TemplateThemeStyle.String] = "#"
    });
}