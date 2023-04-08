namespace Serilog.Tests.Core;

public class ChildLoggerDisposalTests
{
    [Fact]
    public void ByDefaultChildLoggerIsNotDisposedOnOuterDisposal()
    {
        var child = new DisposableLogger();
        var outer = new LoggerConfiguration()
            .WriteTo.Logger(child)
            .CreateLogger();
        outer.Dispose();
        Assert.False(child.IsDisposed);
    }

    [Fact]
    public void ViaLegacyOverloadChildLoggerIsNotDisposedOnOuterDisposal()
    {
        var child = new DisposableLogger();
        var outer = new LoggerConfiguration()
            .WriteTo.Logger(child, Information)
            .CreateLogger();
        outer.Dispose();
        Assert.False(child.IsDisposed);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void WhenExplicitlyConfiguredChildLoggerShouldBeDisposedAccordingToConfiguration(bool attemptDispose)
    {
        var child = new DisposableLogger();
        var outer = new LoggerConfiguration()
            .WriteTo.Logger(child, attemptDispose)
            .CreateLogger();
        outer.Dispose();
        Assert.Equal(attemptDispose, child.IsDisposed);
    }

#if FEATURE_ASYNCDISPOSABLE
    [Fact]
    public async Task ByDefaultChildLoggerIsNotAsyncDisposedOnOuterDisposal()
    {
        var child = new DisposableLogger();
        var outer = new LoggerConfiguration()
            .WriteTo.Logger(child)
            .CreateLogger();
        await outer.DisposeAsync();
        Assert.False(child.IsDisposedAsync);
    }

    [Fact]
    public async Task ViaLegacyOverloadChildLoggerIsNotAsyncDisposedOnOuterDisposal()
    {
        var child = new DisposableLogger();
        var outer = new LoggerConfiguration()
            .WriteTo.Logger(child, Information)
            .CreateLogger();
        await outer.DisposeAsync();
        Assert.False(child.IsDisposedAsync);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WhenExplicitlyConfiguredChildLoggerShouldBeAsyncDisposedAccordingToConfiguration(bool attemptDispose)
    {
        var child = new DisposableLogger();
        var outer = new LoggerConfiguration()
            .WriteTo.Logger(child, attemptDispose)
            .CreateLogger();
        await outer.DisposeAsync();
        Assert.Equal(attemptDispose, child.IsDisposedAsync);
    }
#endif
}
