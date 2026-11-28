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

class CompiledLiteralText : CompiledTemplate
{
    readonly string _text;
    readonly Style _style;

    public CompiledLiteralText(string text, TemplateTheme theme)
    {
        _text = text ?? throw new ArgumentNullException(nameof(text));
        _style = theme.GetStyle(TemplateThemeStyle.TertiaryText);
    }

    public override void Evaluate(EvaluationContext ctx, TextWriter output)
    {
        var _ = 0;
        using (_style.Set(output, ref _))
            output.Write(_text);
    }
}