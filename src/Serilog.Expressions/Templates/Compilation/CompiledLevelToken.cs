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

class CompiledLevelToken : CompiledTemplate
{
    readonly string? _format;
    readonly Alignment? _alignment;
    readonly Style[] _levelStyles;

    public CompiledLevelToken(string? format, Alignment? alignment, TemplateTheme theme)
    {
        _format = format;
        _alignment = alignment;
        _levelStyles =
        [
            theme.GetStyle(TemplateThemeStyle.LevelVerbose),
            theme.GetStyle(TemplateThemeStyle.LevelDebug),
            theme.GetStyle(TemplateThemeStyle.LevelInformation),
            theme.GetStyle(TemplateThemeStyle.LevelWarning),
            theme.GetStyle(TemplateThemeStyle.LevelError),
            theme.GetStyle(TemplateThemeStyle.LevelFatal)
        ];
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
        var levelIndex = (int) ctx.LogEvent.Level;
        if (levelIndex < 0 || levelIndex >= _levelStyles.Length)
            return;

        using var _ = _levelStyles[levelIndex].Set(output, ref invisibleCharacterCount);
        output.Write(LevelRenderer.GetLevelMoniker(ctx.LogEvent.Level, _format));
    }
}