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

using Serilog.Expressions;
using Serilog.Parsing;
using Serilog.Templates.Rendering;
using Serilog.Templates.Themes;

namespace Serilog.Templates.Compilation;

class CompiledTimestampToken : CompiledTemplate
{
    readonly string? _format;
    readonly Alignment? _alignment;
    readonly IFormatProvider? _formatProvider;
    readonly Style _secondaryText;

    public CompiledTimestampToken(string? format, Alignment? alignment, IFormatProvider? formatProvider, TemplateTheme theme)
    {
        _format = format;
        _alignment = alignment;
        _formatProvider = formatProvider;
        _secondaryText = theme.GetStyle(TemplateThemeStyle.SecondaryText);
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
        var value = ctx.LogEvent.Timestamp;

        using var style = _secondaryText.Set(output, ref invisibleCharacterCount);

#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[36];
        if (value.TryFormat(buffer, out int charsWritten, _format, _formatProvider))
        {
            output.Write(buffer[..charsWritten]);
            return;
        }
#endif
        output.Write(value.ToString(_format, formatProvider));
        
    }
}