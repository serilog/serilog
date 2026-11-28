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

using Serilog.Expressions.Ast;
using Serilog.Expressions.Compilation.Transformations;

namespace Serilog.Templates.Compilation.NameResolution;

class ExpressionLocalNameBinder : IdentityTransformer
{
    readonly IReadOnlyCollection<string> _localNames;

    public static Expression BindLocalValueNames(Expression expression, IReadOnlyCollection<string> locals)
    {
        var expressionLocalNameBinder = new ExpressionLocalNameBinder(locals);
        return expressionLocalNameBinder.Transform(expression);
    }

    ExpressionLocalNameBinder(IReadOnlyCollection<string> localNames)
    {
        _localNames = localNames ?? throw new ArgumentNullException(nameof(localNames));
    }

    protected override Expression Transform(AmbientNameExpression px)
    {
        if (!px.IsBuiltIn && _localNames.Contains(px.PropertyName))
            return new LocalNameExpression(px.PropertyName);

        return base.Transform(px);
    }
}