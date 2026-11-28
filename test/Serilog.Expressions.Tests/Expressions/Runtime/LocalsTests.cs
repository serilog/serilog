using Serilog.Expressions.Runtime;
using Serilog.Expressions.Tests.Support;
using Xunit;

namespace Serilog.Expressions.Tests.Expressions.Runtime;

public class LocalsTests
{
    [Fact]
    public void NoValueIsDefinedInNoLocals()
    {
        Assert.False(Locals.TryGetValue(null, "A", out _));
    }

    [Fact]
    public void ASetValueIsRetrieved()
    {
        var expected = Some.LogEventPropertyValue();
        var locals = Locals.Set(null, "A", expected);
        Assert.True(Locals.TryGetValue(locals, "A", out var actual));
        Assert.Same(expected, actual);
    }

    [Fact]
    public void ASetValueIsRetrievedFromMany()
    {
        var expected = Some.LogEventPropertyValue();
        var locals = Locals.Set(null, "A", expected);
        locals = Locals.Set(locals, "B", Some.LogEventPropertyValue());
        Assert.True(Locals.TryGetValue(locals, "A", out var actual));
        Assert.Same(expected, actual);
    }

    [Fact]
    public void TheTopmostValueIsRetrievedForAName()
    {
        var expected = Some.LogEventPropertyValue();
        var locals = Locals.Set(null, "A", Some.LogEventPropertyValue());
        locals = Locals.Set(locals, "B", Some.LogEventPropertyValue());
        locals = Locals.Set(locals, "A", expected);
        Assert.True(Locals.TryGetValue(locals, "A", out var actual));
        Assert.Same(expected, actual);
    }
}