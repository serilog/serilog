namespace Serilog.Tests;

public class VersioningTests
{
    [Fact]
    public void AssemblyIsExplicitlyVersioned()
    {
        var version = typeof(Log).Assembly.GetName().Version;
        Assert.NotNull(version);
        Assert.False(version.Major is 0 or 1);
    }

    [Fact]
    public void AssemblySubordinateVersionPartsAreZero()
    {
        var version = typeof(Log).Assembly.GetName().Version;
        Assert.True(version?.Build is 0);
        Assert.True(version.Revision is 0);
    }

    [Fact]
    public void AssemblyIsSigned()
    {
        var key = typeof(Log).Assembly.GetName().GetPublicKey();
        Assert.NotNull(key);
    }
}