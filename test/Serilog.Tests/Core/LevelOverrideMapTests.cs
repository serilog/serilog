using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;
using Xunit;

namespace Serilog.Tests.Core
{
    public class LevelOverrideMapTests
    {
        [Theory]
        [InlineData("Serilog", false, LevelAlias.Minimum)]
        [InlineData("MyApp", true, LogEventLevel.Debug)]
        [InlineData("MyAppSomething", false, LevelAlias.Minimum)]
        [InlineData("MyOtherApp", false, LevelAlias.Minimum)]
        [InlineData("MyApp.Something", true, LogEventLevel.Debug)]
        [InlineData("MyApp.Api.Models.Person", true, LogEventLevel.Error)]
        [InlineData("MyApp.Api.Controllers.AboutController", true, LogEventLevel.Information)]
        [InlineData("MyApp.Api.Controllers.HomeController", true, LogEventLevel.Warning)]
        [InlineData("Api.Controllers.HomeController", false, LevelAlias.Minimum)]
        public void OverrideScenarios(string context, bool overrideExpected, LogEventLevel expected)
        {
            var overrides = new Dictionary<string, ILoggingLevelSwitch>
            {
                ["MyApp"] = new LoggingLevelSwitch(LogEventLevel.Debug),
                ["MyApp.Api.Controllers"] = new LoggingLevelSwitch(LogEventLevel.Information),
                ["MyApp.Api.Controllers.HomeController"] = new LoggingLevelSwitch(LogEventLevel.Warning),
                ["MyApp.Api"] = new LoggingLevelSwitch(LogEventLevel.Error)
            };

            var lom = new LevelOverrideMap(overrides, LogEventLevel.Fatal, null);

            lom.GetEffectiveLevel(context, out var overriddenLevel, out var overriddenSwitch);

            if (overrideExpected)
            {
                Assert.NotNull(overriddenSwitch);
                Assert.Equal(expected, overriddenSwitch.MinimumLevel);
                Assert.Equal(LevelAlias.Minimum, overriddenLevel);
            }
            else
            {
                Assert.Equal(LogEventLevel.Fatal, overriddenLevel);
                Assert.Null(overriddenSwitch);
            }
        }
    }
}
