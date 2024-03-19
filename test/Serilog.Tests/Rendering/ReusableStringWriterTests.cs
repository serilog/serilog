namespace Serilog.Tests.Rendering;

public class ReusableStringWriterTests
{
    [Theory]
    [InlineData(0, true)]
    [InlineData(1, true)]
    [InlineData(ReusableStringWriter.StringBuilderCapacityThreshold - 1, true)]
    [InlineData(ReusableStringWriter.StringBuilderCapacityThreshold, true)]
    [InlineData(ReusableStringWriter.StringBuilderCapacityThreshold + 1, false)]
    public void OnlyBelowMaximumCapacityStringBuilderIsReused(int capacity, bool expectReuse)
    {
        var original = ReusableStringWriter.GetOrCreate();
        original.GetStringBuilder().Capacity = capacity;
        original.Dispose();

        var next = ReusableStringWriter.GetOrCreate();
        var reused = ReferenceEquals(original, next);
        Assert.Equal(expectReuse, reused);
    }

    [Fact]
    public void NoReuseOccursWhenFormatProvidersDiffer()
    {
        var original = ReusableStringWriter.GetOrCreate();
        original.Dispose();

        var next = ReusableStringWriter.GetOrCreate(new NumberFormatInfo());
        Assert.NotSame(original, next);
    }

    [Fact]
    public void NoReuseOccursWhenFormatProvidersDifferCase2()
    {
        var original = ReusableStringWriter.GetOrCreate(new NumberFormatInfo());
        original.Dispose();

        var next = ReusableStringWriter.GetOrCreate();
        Assert.NotSame(original, next);
    }

    [Fact]
    public void ReuseIsPossibleWithIdenticalFormatProvider()
    {
        var formatProvider = new NumberFormatInfo();

        var original = ReusableStringWriter.GetOrCreate(formatProvider);
        original.Dispose();

        var next = ReusableStringWriter.GetOrCreate(formatProvider);
        Assert.Same(original, next);
    }
}
