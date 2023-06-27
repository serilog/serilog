namespace Serilog.Tests.Core;

public class LoggingLevekSwitchTests
{
    [Fact]
    public void MinimumLevelChanged_Should_Work()
    {
        var sw = new LoggingLevelSwitch();
        bool changed = false;
        sw.MinimumLevelChanged += (o, e) =>
        {
            Assert.Equal(sw, o);
            Assert.Equal(Information, e.OldLevel);
            Assert.Equal(Fatal, e.NewLevel);
            changed = true;
        };
        Assert.Equal(Information, sw.MinimumLevel);
        sw.MinimumLevel = Fatal;
        Assert.Equal(Fatal, sw.MinimumLevel);
        Assert.True(changed);
    }
}
