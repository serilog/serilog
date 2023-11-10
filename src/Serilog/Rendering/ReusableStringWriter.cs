namespace Serilog.Rendering;

/// <summary>
/// Class that provides reusable StringWriters to reduce memory allocations
/// </summary>
class ReusableStringWriter : StringWriter
{
    [ThreadStatic]
    static ReusableStringWriter? _pooledWriter;

    /// <summary>
    /// Max capacity of StringBuilder we keep for next using
    /// </summary>
    const int StringBuilderCapacityThreshold = 32768;

    /// <summary>
    /// Gets already created StringWriter if there is one available or creates a new one.
    /// </summary>
    /// <param name="formatProvider"></param>
    public static StringWriter GetOrCreate(IFormatProvider? formatProvider = null)
    {
        var fmtProvider = formatProvider ?? CultureInfo.CurrentCulture;
        var writer = _pooledWriter;
        _pooledWriter = null;
        if (writer == null || !Equals(writer.FormatProvider, fmtProvider))
        {
            writer = new ReusableStringWriter(formatProvider);
        }

        return writer;
    }

    ReusableStringWriter(IFormatProvider? formatProvider) : base(formatProvider ?? CultureInfo.CurrentCulture)
    {
    }

    /// <summary>
    /// Clear this instance and prepare it for reuse in the future.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        var sb = GetStringBuilder();
        if (sb.Capacity > StringBuilderCapacityThreshold)
        {
            base.Dispose(disposing);
            return;
        }
        // We don't call base.Dispose because all it does is mark the writer as closed so it can't be
        // written to and we want to keep it open as reusable writer.
        sb.Clear();
        _pooledWriter = this;
    }
}
