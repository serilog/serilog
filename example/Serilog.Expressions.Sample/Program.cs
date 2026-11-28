using Serilog;
using Serilog.Debugging;
using Serilog.Templates;
using Serilog.Templates.Themes;

SelfLog.Enable(Console.Error);

TextFormattingExample1();
JsonFormattingExample();
PipelineComponentExample();
TextFormattingExample2();

return;

static void TextFormattingExample1()
{
    using var log = new LoggerConfiguration()
        .Enrich.WithProperty("Application", "Sample")
        .WriteTo.Console(new ExpressionTemplate(
            "[{@t:HH:mm:ss} {@l:u3}" +
            "{#if SourceContext is not null} ({Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)}){#end}] " +
            "{@m} (first item is {coalesce(Items[0], '<empty>')}) {rest()}\n{@x}",
            theme: TemplateTheme.Code))
        .CreateLogger();

    log.Information("Running {Example}", nameof(TextFormattingExample1));

    log.ForContext<Program>()
        .Information("Cart contains {@Items}", new[] { "Tea", "Coffee" });

    log.ForContext<Program>()
        .Information("Cart contains {@Items}", new[] { "Apricots" });
}

static void JsonFormattingExample()
{
    using var log = new LoggerConfiguration()
        .Enrich.WithProperty("Application", "Example")
        .WriteTo.Console(new ExpressionTemplate(
            "{ {@t: UtcDateTime(@t), @mt, @l: if @l = 'Information' then undefined() else @l, @x, ..@p} }\n"))
        .CreateLogger();

    log.Information("Running {Example}", nameof(JsonFormattingExample));

    log.ForContext<Program>()
        .Information("Cart contains {@Items}", new[] { "Tea", "Coffee" });

    log.ForContext<Program>()
        .Warning("Cart is empty");
}

static void PipelineComponentExample()
{
    using var log = new LoggerConfiguration()
        .Enrich.WithProperty("Application", "Example")
        .Enrich.WithComputed("FirstItem", "coalesce(Items[0], '<empty>')")
        .Enrich.WithComputed("SourceContext", "coalesce(Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1), '<no source>')")
        .Filter.ByIncludingOnly("Items is null or Items[?] like 'C%'")
        .WriteTo.Console(outputTemplate:
            "[{Timestamp:HH:mm:ss} {Level:u3} ({SourceContext})] {Message:lj} (first item is {FirstItem}){NewLine}{Exception}")
        .CreateLogger();

    log.Information("Running {Example}", nameof(PipelineComponentExample));

    log.ForContext<Program>()
        .Information("Cart contains {@Items}", new[] { "Tea", "Coffee" });

    log.ForContext<Program>()
        .Information("Cart contains {@Items}", new[] { "Apricots" });
}

static void TextFormattingExample2()
{
    // Emulates `Microsoft.Extensions.Logging`'s `ConsoleLogger`.

    var melon = new TemplateTheme(TemplateTheme.Literate, new Dictionary<TemplateThemeStyle, string>
    {
        // `Information` is dark green in MEL.
        [TemplateThemeStyle.LevelInformation] = "\x1b[38;5;34m",
        [TemplateThemeStyle.String] = "\x1b[38;5;159m",
        [TemplateThemeStyle.Number] = "\x1b[38;5;159m"
    });

    using var log = new LoggerConfiguration()
        .WriteTo.Console(new ExpressionTemplate(
            "{@l:w4}: {SourceContext}\n" +
            "{#if Scope is not null}" +
            "      {#each s in Scope}=> {s}{#delimit} {#end}\n" +
            "{#end}" +
            "      {@m}\n" +
            "{@x}",
            theme: melon))
        .CreateLogger();

    var program = log.ForContext<Program>();
    program.Information("Host listening at {ListenUri}", "https://hello-world.local");

    program
        .ForContext("Scope", new[] {"Main", "TextFormattingExample2()"})
        .Information("HTTP {Method} {Path} responded {StatusCode} in {Elapsed:0.000} ms", "GET", "/api/hello", 200, 1.23);

    program.Warning("We've reached the end of the line");
}
