// Copyright 2016 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Serilog.Formatting.Json;

/// <summary>
/// Converts Serilog's structured property value format into JSON.
/// </summary>
public class JsonValueFormatter : LogEventPropertyValueVisitor<TextWriter, bool>
{
    readonly string? _typeTagName;

    const string DefaultTypeTagName = "_typeTag";

    /// <summary>
    /// Construct a <see cref="JsonFormatter"/>.
    /// </summary>
    /// <param name="typeTagName">When serializing structured (object) values,
    /// the property name to use for the Serilog <see cref="StructureValue.TypeTag"/> field
    /// in the resulting JSON. If null, no type tag field will be written. The default is
    /// "_typeTag".</param>
    public JsonValueFormatter(string? typeTagName = DefaultTypeTagName)
    {
        _typeTagName = typeTagName;
    }

    /// <summary>
    /// Format <paramref name="value"/> as JSON to <paramref name="output"/>.
    /// </summary>
    /// <param name="value">The value to format</param>
    /// <param name="output">The output</param>
    public void Format(LogEventPropertyValue value, TextWriter output)
    {
        // Parameter order of ITextFormatter is the reverse of the visitor one.
        // In this class, public methods and methods with Format*() names use the
        // (x, output) parameter naming convention.
        Visit(output, value);
    }

    /// <summary>
    /// Visit a <see cref="ScalarValue"/> value.
    /// </summary>
    /// <param name="state">Operation state.</param>
    /// <param name="scalar">The value to visit.</param>
    /// <returns>The result of visiting <paramref name="scalar"/>.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="scalar"/> is <code>null</code></exception>
    protected override bool VisitScalarValue(TextWriter state, ScalarValue scalar)
    {
        Guard.AgainstNull(scalar);

        FormatLiteralValue(scalar.Value, state);
        return false;
    }

    /// <summary>
    /// Visit a <see cref="SequenceValue"/> value.
    /// </summary>
    /// <param name="state">Operation state.</param>
    /// <param name="sequence">The value to visit.</param>
    /// <returns>The result of visiting <paramref name="sequence"/>.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="sequence"/> is <code>null</code></exception>
    protected override bool VisitSequenceValue(TextWriter state, SequenceValue sequence)
    {
        Guard.AgainstNull(sequence);

        state.Write('[');
        char? delim = null;
        for (var i = 0; i < sequence.Elements.Count; i++)
        {
            if (delim != null)
            {
                state.Write(delim.Value);
            }
            delim = ',';
            Visit(state, sequence.Elements[i]);
        }
        state.Write(']');
        return false;
    }

    /// <summary>
    /// Visit a <see cref="StructureValue"/> value.
    /// </summary>
    /// <param name="state">Operation state.</param>
    /// <param name="structure">The value to visit.</param>
    /// <returns>The result of visiting <paramref name="structure"/>.</returns>
    protected override bool VisitStructureValue(TextWriter state, StructureValue structure)
    {
        state.Write('{');

        char? delim = null;

        for (var i = 0; i < structure.Properties.Count; i++)
        {
            if (delim != null)
            {
                state.Write(delim.Value);
            }
            delim = ',';
            var prop = structure.Properties[i];
            WriteQuotedJsonString(prop.Name, state);
            state.Write(':');
            Visit(state, prop.Value);
        }

        if (_typeTagName != null && structure.TypeTag != null)
        {
            state.Write(delim);
            WriteQuotedJsonString(_typeTagName, state);
            state.Write(':');
            WriteQuotedJsonString(structure.TypeTag, state);
        }

        state.Write('}');
        return false;
    }

    /// <summary>
    /// Visit a <see cref="DictionaryValue"/> value.
    /// </summary>
    /// <param name="state">Operation state.</param>
    /// <param name="dictionary">The value to visit.</param>
    /// <returns>The result of visiting <paramref name="dictionary"/>.</returns>
    protected override bool VisitDictionaryValue(TextWriter state, DictionaryValue dictionary)
    {
        state.Write('{');
        char? delim = null;
        foreach (var element in dictionary.Elements)
        {
            if (delim != null)
            {
                state.Write(delim.Value);
            }
            delim = ',';
            WriteQuotedJsonString((element.Key.Value ?? "null").ToString()!, state);
            state.Write(':');
            Visit(state, element.Value);
        }
        state.Write('}');
        return false;
    }

    /// <summary>
    /// Write a literal as a single JSON value, e.g. as a number or string. Override to
    /// support more value types. Don't write arrays/structures through this method - the
    /// active destructuring policies have already indicated the value should be scalar at
    /// this point.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="output">The output</param>
    protected virtual void FormatLiteralValue(object? value, TextWriter output)
    {
        if (value == null)
        {
            FormatNullValue(output);
            return;
        }

        // Although the linear switch-on-type has apparently worse algorithmic performance than the O(1)
        // dictionary lookup alternative, in practice, it's much to make a few equality comparisons
        // than the hash/bucket dictionary lookup, and since most data will be string (one comparison),
        // numeric (a handful) or an object (two comparisons) the real-world performance of the code
        // as written is as fast or faster.

        if (value is string str)
        {
            FormatStringValue(str, output);
            return;
        }

        if (value is ValueType)
        {
            if (value is int i)
            {
                FormatExactNumericValue(i, output);
                return;
            }

            if (value is uint ui)
            {
                FormatExactNumericValue(ui, output);
                return;
            }

            if (value is long l)
            {
                FormatExactNumericValue(l, output);
                return;
            }

            if (value is ulong ul)
            {
                FormatExactNumericValue(ul, output);
                return;
            }

            if (value is decimal dc)
            {
                FormatExactNumericValue(dc, output);
                return;
            }

            if (value is byte bt)
            {
                FormatExactNumericValue(bt, output);
                return;
            }

            if (value is sbyte sb)
            {
                FormatExactNumericValue(sb, output);
                return;
            }

            if (value is short s)
            {
                FormatExactNumericValue(s, output);
                return;
            }

            if (value is ushort us)
            {
                FormatExactNumericValue(us, output);
                return;
            }

            if (value is double d)
            {
                FormatDoubleValue(d, output);
                return;
            }

            if (value is float f)
            {
                FormatFloatValue(f, output);
                return;
            }

            if (value is bool b)
            {
                FormatBooleanValue(b, output);
                return;
            }

            if (value is char)
            {
                FormatStringValue(value.ToString()!, output);
                return;
            }

            if (value is DateTime dt)
            {
                FormatDateTimeValue(dt, output);
                return;
            }

            if (value is DateTimeOffset dto)
            {
                FormatDateTimeOffsetValue(dto, output);
                return;
            }

            if (value is TimeSpan timeSpan)
            {
                FormatTimeSpanValue(timeSpan, output);
                return;
            }

#if FEATURE_DATE_AND_TIME_ONLY
            if (value is DateOnly dateOnly)
            {
                FormatDateOnlyValue(dateOnly, output);
                return;
            }

            if (value is TimeOnly timeOnly)
            {
                FormatTimeOnlyValue(timeOnly, output);
                return;
            }
#endif
        }

        FormatLiteralObjectValue(value, output);
    }

    static void FormatBooleanValue(bool value, TextWriter output)
    {
        output.Write(value ? "true" : "false");
    }

    static void FormatFloatValue(float value, TextWriter output)
    {
        if (float.IsNaN(value) || float.IsInfinity(value))
        {
            FormatStringValue(value.ToString(CultureInfo.InvariantCulture), output);
            return;
        }

#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, "R", CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("R", CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString("R", CultureInfo.InvariantCulture));
#endif
    }

    static void FormatDoubleValue(double value, TextWriter output)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
        {
            FormatStringValue(value.ToString(CultureInfo.InvariantCulture), output);
            return;
        }

#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, "R", CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("R", CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString("R", CultureInfo.InvariantCulture));
#endif
    }

    static void FormatExactNumericValue(int value, TextWriter output)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#endif
    }

    static void FormatExactNumericValue(uint value, TextWriter output)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#endif
    }

    static void FormatExactNumericValue(long value, TextWriter output)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#endif
    }

    static void FormatExactNumericValue(ulong value, TextWriter output)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#endif
    }

    static void FormatExactNumericValue(decimal value, TextWriter output)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#endif
    }

    static void FormatExactNumericValue(byte value, TextWriter output)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#endif
    }

    static void FormatExactNumericValue(sbyte value, TextWriter output)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#endif
    }

    static void FormatExactNumericValue(short value, TextWriter output)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#endif
    }

    static void FormatExactNumericValue(ushort value, TextWriter output)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, provider: CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString(null, CultureInfo.InvariantCulture));
#endif
    }

    static void FormatDateTimeValue(DateTime value, TextWriter output)
    {
        output.Write('\"');

#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, format: "O"))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("O", CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString("O", CultureInfo.InvariantCulture));
#endif

        output.Write('\"');
    }

    static void FormatDateTimeOffsetValue(DateTimeOffset value, TextWriter output)
    {
        output.Write('\"');

#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written, format: "O"))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("O", CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString("O", CultureInfo.InvariantCulture));
#endif

        output.Write('\"');
    }

    static void FormatTimeSpanValue(TimeSpan value, TextWriter output)
    {
        output.Write('\"');
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out var written))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString());
#else
        output.Write(value.ToString());
#endif
        output.Write('\"');
    }

#if FEATURE_DATE_AND_TIME_ONLY

    static void FormatDateOnlyValue(DateOnly value, TextWriter output)
    {
        output.Write('\"');

        Span<char> buffer = stackalloc char[10];
        if (value.TryFormat(buffer, out int written, format: "yyyy-MM-dd"))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("yyyy-MM-dd"));

        output.Write('\"');
    }

    static void FormatTimeOnlyValue(TimeOnly value, TextWriter output)
    {
        output.Write('\"');

        Span<char> buffer = stackalloc char[16];
        if (value.TryFormat(buffer, out int written, format: "O"))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("O"));

        output.Write('\"');
    }

#endif

    static void FormatLiteralObjectValue(object value, TextWriter output)
    {
        Guard.AgainstNull(value);

        FormatStringValue(value.ToString() ?? "", output);
    }

    static void FormatStringValue(string str, TextWriter output)
    {
        WriteQuotedJsonString(str, output);
    }

    static void FormatNullValue(TextWriter output)
    {
        output.Write("null");
    }

    /// <summary>
    /// Write a valid JSON string literal, escaping as necessary.
    /// </summary>
    /// <param name="str">The string value to write.</param>
    /// <param name="output">The output.</param>
    public static void WriteQuotedJsonString(string str, TextWriter output)
    {
        output.Write('\"');

        var cleanSegmentStart = 0;
        var anyEscaped = false;

        for (var i = 0; i < str.Length; ++i)
        {
            var c = str[i];
            if (c is < (char)32 or '\\' or '"')
            {
                anyEscaped = true;

#if FEATURE_SPAN
                output.Write(str.AsSpan().Slice(cleanSegmentStart, i - cleanSegmentStart));
#else
                output.Write(str.Substring(cleanSegmentStart, i - cleanSegmentStart));
#endif
                cleanSegmentStart = i + 1;

                switch (c)
                {
                    case '"':
                    {
                        output.Write("\\\"");
                        break;
                    }
                    case '\\':
                    {
                        output.Write("\\\\");
                        break;
                    }
                    case '\n':
                    {
                        output.Write("\\n");
                        break;
                    }
                    case '\r':
                    {
                        output.Write("\\r");
                        break;
                    }
                    case '\f':
                    {
                        output.Write("\\f");
                        break;
                    }
                    case '\t':
                    {
                        output.Write("\\t");
                        break;
                    }
                    default:
                    {
                        output.Write("\\u");
                        output.Write(((int)c).ToString("X4"));
                        break;
                    }
                }
            }
        }

        if (anyEscaped)
        {
            if (cleanSegmentStart != str.Length)
#if FEATURE_SPAN
                output.Write(str.AsSpan().Slice(cleanSegmentStart));
#else
                output.Write(str.Substring(cleanSegmentStart));
#endif
        }
        else
        {
            output.Write(str);
        }

        output.Write('\"');
    }
}
