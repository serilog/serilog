namespace Serilog;

/// <summary>
/// Class that provides reusable StringWriters to reduce memory allocations
/// </summary>
public class ReusableStringWriter: StringWriter
{
    [ThreadStatic]
    static ReusableStringWriter? _pooledWriter;

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
            writer = formatProvider == null ? new ReusableStringWriter() : new ReusableStringWriter(formatProvider);
        }

        return writer;
    }

    ReusableStringWriter()
    {
    }

    ReusableStringWriter(IFormatProvider formatProvider) : base(formatProvider)
    {
    }

    /// <summary>
    /// Clear this instance and prepare it for reuse in the future.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        // We don't call base.Dispose because all it does is mark the writer as closed so it can't be
        // written to and we want to keep it open as reusable writer.
        GetStringBuilder().Clear();
        _pooledWriter = this;
    }
}
