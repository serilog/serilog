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

using Serilog.Events;
using Serilog.Expressions;
using Serilog.Parsing;
using Serilog.Templates.Rendering;
using Serilog.Templates.Themes;

namespace Serilog.Templates.Compilation;

class CompiledMessageToken : CompiledTemplate
{
    readonly IFormatProvider? _formatProvider;
    readonly Alignment? _alignment;
    readonly Style _text, _invalid, _null, _bool, _string, _num, _scalar;
    readonly ThemedJsonValueFormatter _jsonFormatter;

    public CompiledMessageToken(IFormatProvider? formatProvider, Alignment? alignment, TemplateTheme theme)
    {
        _formatProvider = formatProvider;
        _alignment = alignment;
        _text = theme.GetStyle(TemplateThemeStyle.Text);
        _null = theme.GetStyle(TemplateThemeStyle.Null);
        _bool = theme.GetStyle(TemplateThemeStyle.Boolean);
        _num = theme.GetStyle(TemplateThemeStyle.Number);
        _string = theme.GetStyle(TemplateThemeStyle.String);
        _scalar = theme.GetStyle(TemplateThemeStyle.Scalar);
        _invalid = theme.GetStyle(TemplateThemeStyle.Invalid);
        _jsonFormatter = new(theme);
    }

    public override void Evaluate(EvaluationContext ctx, TextWriter output)
    {
        var invisibleCharacterCount = 0;

        if (_alignment == null)
        {
            EvaluateUnaligned(ctx, output, ref invisibleCharacterCount);
        }
        else
        {
            var writer = new StringWriter();
            EvaluateUnaligned(ctx, writer, ref invisibleCharacterCount);
            Padding.Apply(output, writer.ToString(), _alignment.Value.Widen(invisibleCharacterCount));
        }
    }

    void EvaluateUnaligned(EvaluationContext ctx, TextWriter output, ref int invisibleCharacterCount)
    {
        foreach (var token in ctx.LogEvent.MessageTemplate.Tokens)
        {
            switch (token)
            {
                case TextToken tt:
                {
                    using var _ = _text.Set(output, ref invisibleCharacterCount);
                    output.Write(tt.Text);
                    break;
                }
                case PropertyToken pt:
                {
                    EvaluateProperty(ctx.LogEvent.Properties, pt, output, ref invisibleCharacterCount);
                    break;
                }
                default:
                {
                    output.Write(token);
                    break;
                }
            }
        }
    }

    void EvaluateProperty(IReadOnlyDictionary<string,LogEventPropertyValue> properties, PropertyToken pt, TextWriter output, ref int invisibleCharacterCount)
    {
        if (!properties.TryGetValue(pt.PropertyName, out var value))
        {
            using var _ = _invalid.Set(output, ref invisibleCharacterCount);
            output.Write(pt.ToString());
            return;
        }

        if (pt.Alignment is null)
        {
            EvaluatePropertyUnaligned(value, output, pt.Format, ref invisibleCharacterCount);
            return;
        }

        var buffer = new StringWriter();
        var resultInvisibleCharacters = 0;

        EvaluatePropertyUnaligned(value, buffer, pt.Format, ref resultInvisibleCharacters);

        var result = buffer.ToString();
        invisibleCharacterCount += resultInvisibleCharacters;

        if (result.Length - resultInvisibleCharacters >= pt.Alignment.Value.Width)
            output.Write(result);
        else
            Padding.Apply(output, result, pt.Alignment.Value.Widen(resultInvisibleCharacters));
    }

    void EvaluatePropertyUnaligned(LogEventPropertyValue propertyValue, TextWriter output, string? format, ref int invisibleCharacterCount)
    {
        if (propertyValue is not ScalarValue scalar)
        {
            invisibleCharacterCount += _jsonFormatter.Format(propertyValue, output);
            return;
        }

        var value = scalar.Value;

        if (value == null)
        {
            using (_null.Set(output, ref invisibleCharacterCount))
                output.Write("null");
            return;
        }

        if (value is string str)
        {
            using (_string.Set(output, ref invisibleCharacterCount))
                output.Write(str);
            return;
        }

        if (value is ValueType)
        {
            if (value is int or uint or long or ulong or decimal or byte or sbyte or short or ushort)
            {
                using (_num.Set(output, ref invisibleCharacterCount))
                    output.Write(((IFormattable)value).ToString(format, _formatProvider));
                return;
            }

            if (value is double d)
            {
                using (_num.Set(output, ref invisibleCharacterCount))
                    output.Write(d.ToString(format, _formatProvider));
                return;
            }

            if (value is float f)
            {
                using (_num.Set(output, ref invisibleCharacterCount))
                    output.Write(f.ToString(format, _formatProvider));
                return;
            }

            if (value is bool b)
            {
                using (_bool.Set(output, ref invisibleCharacterCount))
                    output.Write(b);
                return;
            }
        }

        if (value is IFormattable formattable)
        {
            using (_scalar.Set(output, ref invisibleCharacterCount))
                output.Write(formattable.ToString(format, _formatProvider));
            return;
        }

        using (_scalar.Set(output, ref invisibleCharacterCount))
            output.Write(value);
    }
}