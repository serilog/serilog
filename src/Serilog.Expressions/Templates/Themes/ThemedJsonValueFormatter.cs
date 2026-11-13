// Copyright © Serilog Contributors
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

using System.Globalization;
using Serilog.Data;
using Serilog.Events;
using Serilog.Formatting.Json;

// ReSharper disable ForCanBeConvertedToForeach

namespace Serilog.Templates.Themes;

class ThemedJsonValueFormatter : LogEventPropertyValueVisitor<TextWriter, int>
{
    const string TypeTagPropertyName = "$type";

    readonly Style _null, _bool, _num, _string, _scalar, _tertiary, _name;

    public ThemedJsonValueFormatter(TemplateTheme theme)
    {
        _null = theme.GetStyle(TemplateThemeStyle.Null);
        _bool = theme.GetStyle(TemplateThemeStyle.Boolean);
        _num = theme.GetStyle(TemplateThemeStyle.Number);
        _string = theme.GetStyle(TemplateThemeStyle.String);
        _scalar = theme.GetStyle(TemplateThemeStyle.Scalar);
        _tertiary = theme.GetStyle(TemplateThemeStyle.TertiaryText);
        _name = theme.GetStyle(TemplateThemeStyle.Name);
    }

    public int Format(LogEventPropertyValue value, TextWriter output)
    {
        return Visit(output, value);
    }

    protected override int VisitScalarValue(TextWriter state, ScalarValue scalar)
    {
        return FormatLiteralValue(scalar, state);
    }

    protected override int VisitSequenceValue(TextWriter state, SequenceValue sequence)
    {
        var count = 0;

        using (_tertiary.Set(state, ref count))
            state.Write('[');

        var delim = string.Empty;
        for (var index = 0; index < sequence.Elements.Count; ++index)
        {
            if (delim.Length != 0)
            {
                using (_tertiary.Set(state, ref count))
                    state.Write(delim);
            }

            delim = ",";
            count += Visit(state, sequence.Elements[index]);
        }

        using (_tertiary.Set(state, ref count))
            state.Write(']');

        return count;

    }

    protected override int VisitStructureValue(TextWriter state, StructureValue structure)
    {
        var count = 0;

        using (_tertiary.Set(state, ref count))
            state.Write('{');

        var delim = string.Empty;
        for (var index = 0; index < structure.Properties.Count; ++index)
        {
            if (delim.Length != 0)
            {
                using (_tertiary.Set(state, ref count))
                    state.Write(delim);
            }

            delim = ",";

            var property = structure.Properties[index];

            using (_name.Set(state, ref count))
                JsonValueFormatter.WriteQuotedJsonString(property.Name, state);

            using (_tertiary.Set(state, ref count))
                state.Write(":");

            count += Visit(state, property.Value);
        }

        if (structure.TypeTag != null)
        {
            using (_tertiary.Set(state, ref count))
                state.Write(delim);

            using (_name.Set(state, ref count))
                JsonValueFormatter.WriteQuotedJsonString(TypeTagPropertyName, state);

            using (_tertiary.Set(state, ref count))
                state.Write(":");

            using (_string.Set(state, ref count))
                JsonValueFormatter.WriteQuotedJsonString(structure.TypeTag, state);
        }

        using (_tertiary.Set(state, ref count))
            state.Write('}');

        return count;
    }

    protected override int VisitDictionaryValue(TextWriter state, DictionaryValue dictionary)
    {
        var count = 0;

        using (_tertiary.Set(state, ref count))
            state.Write('{');

        var delim = string.Empty;
        foreach (var element in dictionary.Elements)
        {
            if (delim.Length != 0)
            {
                using (_tertiary.Set(state, ref count))
                    state.Write(delim);
            }

            delim = ",";

            var style = element.Key.Value == null
                ? _null
                : element.Key.Value is string
                    ? _string
                    : _scalar;

            using (style.Set(state, ref count))
                JsonValueFormatter.WriteQuotedJsonString(element.Key.Value?.ToString() ?? "null", state);

            using (_tertiary.Set(state, ref count))
                state.Write(":");

            count += Visit(state, element.Value);
        }

        using (_tertiary.Set(state, ref count))
            state.Write('}');

        return count;
    }

    int FormatLiteralValue(ScalarValue scalar, TextWriter output)
    {
        var value = scalar.Value;
        var count = 0;

        if (value == null)
        {
            using (_null.Set(output, ref count))
                output.Write("null");
            return count;
        }

        if (value is string str)
        {
            using (_string.Set(output, ref count))
                JsonValueFormatter.WriteQuotedJsonString(str, output);
            return count;
        }

        if (value is ValueType)
        {
            if (value is int or uint or long or ulong or decimal or byte or sbyte or short or ushort)
            {
                using (_num.Set(output, ref count))
                    output.Write(((IFormattable)value).ToString(null, CultureInfo.InvariantCulture));
                return count;
            }

            if (value is double d)
            {
                using (_num.Set(output, ref count))
                {
                    if (double.IsNaN(d) || double.IsInfinity(d))
                        JsonValueFormatter.WriteQuotedJsonString(d.ToString(CultureInfo.InvariantCulture), output);
                    else
                        output.Write(d.ToString("R", CultureInfo.InvariantCulture));
                }
                return count;
            }

            if (value is float f)
            {
                using (_num.Set(output, ref count))
                {
                    if (double.IsNaN(f) || double.IsInfinity(f))
                        JsonValueFormatter.WriteQuotedJsonString(f.ToString(CultureInfo.InvariantCulture), output);
                    else
                        output.Write(f.ToString("R", CultureInfo.InvariantCulture));
                }
                return count;
            }

            if (value is bool b)
            {
                using (_bool.Set(output, ref count))
                    output.Write(b ? "true" : "false");

                return count;
            }

            if (value is char ch)
            {
                using (_scalar.Set(output, ref count))
                    JsonValueFormatter.WriteQuotedJsonString(ch.ToString(), output);
                return count;
            }

            if (value is DateTime or DateTimeOffset)
            {
                using (_scalar.Set(output, ref count))
                {
                    output.Write('"');
                    output.Write(((IFormattable)value).ToString("O", CultureInfo.InvariantCulture));
                    output.Write('"');
                }
                return count;
            }
        }

        using (_scalar.Set(output, ref count))
            JsonValueFormatter.WriteQuotedJsonString(value.ToString() ?? "", output);

        return count;
    }
}