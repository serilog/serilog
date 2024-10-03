namespace Serilog.Tests.Support;

public static class Extensions
{
    public static object? LiteralValue(this LogEventPropertyValue @this)
    {
        if (@this is ScalarValue scalar)
            return scalar.Value;
        else if (@this is SequenceValue sequence)
            return $"[{string.Join(",", sequence.Elements.Select(e => e.LiteralValue()))}]";
        else
            throw new NotSupportedException(@this.GetType().Name);
    }
}
