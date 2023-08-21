#if FEATURE_SPAN
using System.Runtime.InteropServices;

namespace Serilog.Core;

[StructLayout(LayoutKind.Sequential)]
struct PropertiesInlineArray
{
    object? Object1;
    object? Object2;
    object? Object3;

    public Span<object?> AsSpan(int count)
    {
        if (count is < 0 or > 3)
            ThrowArgumentOutOfRangeException(nameof(count));

        return MemoryMarshal.CreateSpan(ref Object1, count);
    }

    [DoesNotReturn]
    static void ThrowArgumentOutOfRangeException(string param)
    {
        throw new ArgumentOutOfRangeException(param);
    }
}
#endif
