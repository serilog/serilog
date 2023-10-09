#if FEATURE_SPAN
namespace Serilog.Rendering;

static class MessageEscaper
{
    internal static void WriteString(TextWriter output, ReadOnlySpan<char> value)
    {
        output.Write('"');
        var firstIndex = value.IndexOf('"');
        if (firstIndex < 0)
        {
            output.Write(value);
        }
        else
        {
            WriteEscapedInner(output, value, firstIndex);
        }

        output.Write('"');
    }

    static void WriteEscapedInner(TextWriter output, ReadOnlySpan<char> value, int index)
    {
        var pos = 0;
        while (index >= 0)
        {
            output.Write(value.Slice(pos, index));
            output.Write("\\\"");
            pos += index + 1;
            index = value.Slice(pos).IndexOf('"');
        }

        if (pos != value.Length)
        {
            output.Write(value.Slice(pos));
        }
    }
}
#endif
