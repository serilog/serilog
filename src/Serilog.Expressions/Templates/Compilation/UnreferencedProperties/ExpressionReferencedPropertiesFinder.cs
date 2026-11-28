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
using Serilog.Expressions.Compilation.Transformations;

namespace Serilog.Templates.Compilation.UnreferencedProperties;

class ExpressionReferencedPropertiesFinder : SerilogExpressionTransformer<IEnumerable<string>>
{
    public IEnumerable<string> FindReferencedProperties(Expression expression)
    {
        return Transform(expression);
    }

    protected override IEnumerable<string> Transform(CallExpression call)
    {
        return call.Operands.SelectMany(Transform);
    }

    protected override IEnumerable<string> Transform(ConstantExpression cx)
    {
        yield break;
    }

    protected override IEnumerable<string> Transform(AmbientNameExpression px)
    {
        if (!px.IsBuiltIn)
            yield return px.PropertyName;
    }

    protected override IEnumerable<string> Transform(LocalNameExpression nlx)
    {
        yield break;
    }

    protected override IEnumerable<string> Transform(AccessorExpression spx)
    {
        if (Pattern.IsAmbientProperty(spx.Receiver, BuiltInProperty.Properties, true))
            yield return spx.MemberName;
        
        foreach (var nested in Transform(spx.Receiver))
            yield return nested;
    }

    protected override IEnumerable<string> Transform(LambdaExpression lmx)
    {
        return Transform(lmx.Body);
    }

    protected override IEnumerable<string> Transform(ParameterExpression prx)
    {
        yield break;
    }

    protected override IEnumerable<string> Transform(IndexerWildcardExpression wx)
    {
        yield break;
    }

    protected override IEnumerable<string> Transform(ArrayExpression ax)
    {
        return ax.Elements.OfType<ItemElement>().SelectMany(i => Transform(i.Value))
            .Concat(ax.Elements.OfType<SpreadElement>().SelectMany(i => Transform(i.Content)));
    }

    protected override IEnumerable<string> Transform(ObjectExpression ox)
    {
        return ox.Members.OfType<PropertyMember>().SelectMany(m => Transform(m.Value))
            .Concat(ox.Members.OfType<SpreadMember>().SelectMany(m => Transform(m.Content)));
    }

    protected override IEnumerable<string> Transform(IndexerExpression ix)
    {
        if (Pattern.IsAmbientProperty(ix.Receiver, BuiltInProperty.Properties, true) &&
            Pattern.IsStringConstant(ix.Index, out var name))
        {
            yield return name;
        }
        else
        {
            foreach (var nested in Transform(ix.Index).Concat(Transform(ix.Receiver)))
            {
                yield return nested;
            }
        }
    }

    protected override IEnumerable<string> Transform(IndexOfMatchExpression mx)
    {
        return Transform(mx.Corpus);
    }
}