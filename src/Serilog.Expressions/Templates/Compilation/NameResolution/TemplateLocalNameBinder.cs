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

using Serilog.Templates.Ast;

// ReSharper disable MemberCanBeMadeStatic.Local, SuggestBaseTypeForParameter

namespace Serilog.Templates.Compilation.NameResolution;

class TemplateLocalNameBinder
{
    public static Template BindLocalValueNames(Template template)
    {
        var binder = new TemplateLocalNameBinder();
        return binder.Transform(template, new());
    }

    Template Transform(Template template, Stack<string> locals)
    {
        return template switch
        {
            TemplateBlock block => Transform(block, locals),
            LiteralText text => text,
            FormattedExpression fx => Transform(fx, locals),
            Conditional cond => Transform(cond, locals),
            Repetition rep => Transform(rep, locals),
            _ => throw new NotSupportedException("Unsupported template type.")
        };
    }

    Template Transform(TemplateBlock block, Stack<string> locals)
    {
        return new TemplateBlock(block.Elements
            .Select(e => Transform(e, locals))
            .ToArray());
    }

    Template Transform(FormattedExpression fx, Stack<string> locals)
    {
        if (locals.Count == 0)
            return fx;

        return new FormattedExpression(
            ExpressionLocalNameBinder.BindLocalValueNames(fx.Expression, locals),
            fx.Format,
            fx.Alignment);
    }

    Template Transform(Conditional cond, Stack<string> locals)
    {
        return new Conditional(
            ExpressionLocalNameBinder.BindLocalValueNames(cond.Condition, locals),
            Transform(cond.Consequent, locals),
            cond.Alternative != null ? Transform(cond.Alternative, locals) : null);
    }

    Template Transform(Repetition rep, Stack<string> locals)
    {
        foreach (var name in rep.BindingNames)
            locals.Push(name);

        var body = Transform(rep.Body, locals);

        foreach (var _ in rep.BindingNames)
            locals.Pop();

        return new Repetition(
            ExpressionLocalNameBinder.BindLocalValueNames(rep.Enumerable, locals),
            rep.BindingNames,
            body,
            rep.Delimiter != null ? Transform(rep.Delimiter, locals) : null,
            rep.Alternative != null ? Transform(rep.Alternative, locals) : null);
    }
}