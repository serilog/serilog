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
using Serilog.Templates.Themes;

namespace Serilog.Templates.Compilation;

class CompiledExceptionToken : CompiledTemplate
{
    const string StackFrameLinePrefix = "   ";

    readonly Style _text, _secondaryText;

    public CompiledExceptionToken(TemplateTheme theme)
    {
        _text = theme.GetStyle(TemplateThemeStyle.Text);
        _secondaryText = theme.GetStyle(TemplateThemeStyle.SecondaryText);
    }

    public override void Evaluate(EvaluationContext ctx, TextWriter output)
    {
        // Padding and alignment are not applied by this renderer.

        if (ctx.LogEvent.Exception is null)
            return;

        StringReader lines;
        try
        {
            lines = new StringReader(ctx.LogEvent.Exception.ToString());
        }
        catch (Exception ex)
        {
            lines = new StringReader(
                $"[Exception.ToString() failed: {ex.Message}] Original exception type: {ctx.LogEvent.Exception.GetType().FullName}{Environment.NewLine}");
        }

        while (lines.ReadLine() is { } nextLine)
        {
            var style = nextLine.StartsWith(StackFrameLinePrefix) ? _secondaryText : _text;
            var _ = 0;
            using (style.Set(output, ref _))
                output.WriteLine(nextLine);
        }
    }
}