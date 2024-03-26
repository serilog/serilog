namespace Serilog.Tests.Core;

public class LevelOverrideMapTests
{
    [Theory]
    [InlineData("Serilog", false, LevelAlias.Minimum)]
    [InlineData("MyApp", true, Debug)]
    [InlineData("MyAppSomething", false, LevelAlias.Minimum)]
    [InlineData("MyOtherApp", false, LevelAlias.Minimum)]
    [InlineData("MyApp.Something", true, Debug)]
    [InlineData("MyApp.Api.Models.Person", true, Error)]
    [InlineData("MyApp.Api.Controllers.AboutController", true, Information)]
    [InlineData("MyApp.Api.Controllers.HomeController", true, Warning)]
    [InlineData("Api.Controllers.HomeController", false, LevelAlias.Minimum)]
    public void OverrideScenarios(string context, bool overrideExpected, LogEventLevel expected)
    {
        var overrides = new Dictionary<string, LoggingLevelSwitch>
        {
            ["MyApp"] = new(Debug),
            ["MyApp.Api.Controllers"] = new(Information),
            ["MyApp.Api.Controllers.HomeController"] = new(Warning),
            ["MyApp.Api"] = new(Error)
        };

        var lom = new LevelOverrideMap(overrides, Fatal, null);

        lom.GetEffectiveLevel(context, out var overriddenLevel, out var overriddenSwitch);

        if (overrideExpected)
        {
            Assert.NotNull(overriddenSwitch);
            Assert.Equal(expected, overriddenSwitch.MinimumLevel);
            Assert.Equal(LevelAlias.Minimum, overriddenLevel);
        }
        else
        {
            Assert.Equal(Fatal, overriddenLevel);
            Assert.Null(overriddenSwitch);
        }
    }
}
