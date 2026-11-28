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

class CompiledFormattedExpression : CompiledTemplate
{
    readonly ThemedJsonValueFormatter _jsonFormatter;
    readonly Evaluatable _expression;
    readonly string? _format;
    readonly Alignment? _alignment;
    readonly IFormatProvider? _formatProvider;
    readonly Style _secondaryText;

    public CompiledFormattedExpression(Evaluatable expression, string? format, Alignment? alignment, IFormatProvider? formatProvider, TemplateTheme theme)
    {
        _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        _format = format;
        _alignment = alignment;
        _formatProvider = formatProvider;
        _secondaryText = theme.GetStyle(TemplateThemeStyle.SecondaryText);
        _jsonFormatter = new(theme);
    }

    public override void Evaluate(EvaluationContext ctx, TextWriter output)
    {
        var invisibleCharacterCount = 0;

        if (_alignment == null)
        {
            EvaluateUnaligned(ctx, output, _formatProvider, ref invisibleCharacterCount);
        }
        else
        {
            var writer = new StringWriter();
            EvaluateUnaligned(ctx, writer, _formatProvider, ref invisibleCharacterCount);
            Padding.Apply(output, writer.ToString(), _alignment.Value.Widen(invisibleCharacterCount));
        }
    }

    void EvaluateUnaligned(EvaluationContext ctx, TextWriter output, IFormatProvider? formatProvider, ref int invisibleCharacterCount)
    {
        var value = _expression(ctx);
        if (value == null)
            return; // Undefined is empty

        if (value is ScalarValue scalar)
        {
            if (scalar.Value is null)
                return; // Null is empty

            using var style = _secondaryText.Set(output, ref invisibleCharacterCount);

            if (scalar.Value is IFormattable fmt)
                output.Write(fmt.ToString(_format, formatProvider));
            else
                output.Write(scalar.Value.ToString());
        }
        else
        {
            invisibleCharacterCount += _jsonFormatter.Format(value, output);
        }
    }
}