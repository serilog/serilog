namespace Serilog;

/// <summary>
/// Class that provides reusable StringWriters to reduce memory allocations
/// </summary>
public class ReusableStringWriter: StringWriter
{
    static readonly object _poolLock = new();
    static readonly Dictionary<IFormatProvider, Stack<ReusableStringWriter>> _writerPools = new();

    /// <summary>
    /// Gets already created StringWriter if there is one available or creates a new one.
    /// </summary>
    /// <param name="formatProvider"></param>
    public static StringWriter GetOrCreate(IFormatProvider? formatProvider = null)
    {
        formatProvider ??= CultureInfo.CurrentCulture;
        lock (_poolLock)
        {
            if (!_writerPools.TryGetValue(formatProvider, out var writerPool))
            {
                writerPool = new Stack<ReusableStringWriter>();
                _writerPools[formatProvider] = writerPool;
            }

            return writerPool.Count > 0 ? writerPool.Pop() : new ReusableStringWriter(formatProvider);
        }
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
        lock (_poolLock)
        {
            _writerPools[FormatProvider].Push(this);
        }
    }
}
