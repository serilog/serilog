#if FEATURE_SPAN
using System.Runtime.InteropServices;

namespace Serilog.Core;

[StructLayout(LayoutKind.Sequential)]
struct PropertiesInlineArray1
{
    object? Object;

    public PropertiesInlineArray1(object? @object) => Object = @object;

    public Span<object?> AsSpan() => MemoryMarshal.CreateSpan(ref Object, 1);

    public static implicit operator Span<object?>(PropertiesInlineArray1 arr) => arr.AsSpan();
}

[StructLayout(LayoutKind.Sequential)]
struct PropertiesInlineArray2
{
    object? Object1;
    object? Object2;

    public PropertiesInlineArray2(object? object1, object? object2)
    {
        Object1 = object1;
        Object2 = object2;
    }

    public Span<object?> AsSpan() => MemoryMarshal.CreateSpan(ref Object1, 2);

    public static implicit operator Span<object?>(PropertiesInlineArray2 arr) => arr.AsSpan();
}

struct PropertiesInlineArray3
{
    object? Object1;
    object? Object2;
    object? Object3;

    public PropertiesInlineArray3(object? object1, object? object2, object? object3)
    {
        Object1 = object1;
        Object2 = object2;
        Object3 = object3;
    }

    public Span<object?> AsSpan() => MemoryMarshal.CreateSpan(ref Object1, 3);

    public static implicit operator Span<object?>(PropertiesInlineArray3 arr) => arr.AsSpan();
}

#endif
