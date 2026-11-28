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
using Serilog.Expressions.Ast;
using Serilog.Expressions.Compilation;
using Serilog.Templates.Ast;
using Serilog.Templates.Encoding;
using Serilog.Templates.Themes;

namespace Serilog.Templates.Compilation;

static class TemplateCompiler
{
    public static CompiledTemplate Compile(Template template,
        IFormatProvider? formatProvider, NameResolver nameResolver,
        TemplateTheme theme,
        EncodedTemplateFactory encoder)
    {
        return template switch
        {
            LiteralText text => new CompiledLiteralText(text.Text, theme),
            FormattedExpression { Expression: AmbientNameExpression { IsBuiltIn: true, PropertyName: BuiltInProperty.Level} } level =>
                encoder.Wrap(new CompiledLevelToken(level.Format, level.Alignment, theme)),
            FormattedExpression
            {
                Expression: AmbientNameExpression { IsBuiltIn: true, PropertyName: BuiltInProperty.Exception },
                Alignment: null,
                Format: null
            } => encoder.Wrap(new CompiledExceptionToken(theme)),
            FormattedExpression
            {
                Expression: AmbientNameExpression { IsBuiltIn: true, PropertyName: BuiltInProperty.Message },
                Format: null
            } message => encoder.Wrap(new CompiledMessageToken(formatProvider, message.Alignment, theme)),
            FormattedExpression
            {
                Expression: AmbientNameExpression { IsBuiltIn: true, PropertyName: BuiltInProperty.Timestamp },
            } timestamp => encoder.Wrap(new CompiledTimestampToken(timestamp.Format, timestamp.Alignment, formatProvider, theme)),
            FormattedExpression expression => encoder.MakeCompiledFormattedExpression(
                ExpressionCompiler.Compile(expression.Expression, formatProvider, nameResolver), expression.Format, expression.Alignment, formatProvider, theme),
            TemplateBlock block => new CompiledTemplateBlock(block.Elements.Select(e => Compile(e, formatProvider, nameResolver, theme, encoder)).ToArray()),
            Conditional conditional => new CompiledConditional(
                ExpressionCompiler.Compile(conditional.Condition, formatProvider, nameResolver),
                Compile(conditional.Consequent, formatProvider, nameResolver, theme, encoder),
                conditional.Alternative == null ? null : Compile(conditional.Alternative, formatProvider, nameResolver, theme, encoder)),
            Repetition repetition => new CompiledRepetition(
                ExpressionCompiler.Compile(repetition.Enumerable, formatProvider, nameResolver),
                repetition.BindingNames.Length > 0 ? repetition.BindingNames[0] : null,
                repetition.BindingNames.Length > 1 ? repetition.BindingNames[1] : null,
                Compile(repetition.Body, formatProvider, nameResolver, theme, encoder),
                repetition.Delimiter == null ? null : Compile(repetition.Delimiter, formatProvider, nameResolver, theme, encoder),
                repetition.Alternative == null ? null : Compile(repetition.Alternative, formatProvider, nameResolver, theme, encoder)),
            _ => throw new NotSupportedException()
        };
    }
}
