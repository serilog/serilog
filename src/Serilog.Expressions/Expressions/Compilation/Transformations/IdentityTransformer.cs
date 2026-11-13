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

namespace Serilog.Expressions.Compilation.Transformations;

class IdentityTransformer : SerilogExpressionTransformer<Expression>
{
    bool TryTransform(Expression expr, out Expression result)
    {
        result = Transform(expr);
        return !ReferenceEquals(expr, result);
    }

    protected override Expression Transform(CallExpression call)
    {
        var any = false;
        var operands = new List<Expression>();
        foreach (var op in call.Operands)
        {
            if (TryTransform(op, out var result))
                any = true;
            operands.Add(result);
        }

        if (!any)
            return call;

        return new CallExpression(call.IgnoreCase, call.OperatorName, operands.ToArray());
    }

    protected override Expression Transform(ConstantExpression cx)
    {
        return cx;
    }

    protected override Expression Transform(AmbientNameExpression px)
    {
        return px;
    }

    protected override Expression Transform(LocalNameExpression nlx)
    {
        return nlx;
    }

    protected override Expression Transform(AccessorExpression spx)
    {
        if (!TryTransform(spx.Receiver, out var recv))
            return spx;

        return new AccessorExpression(recv, spx.MemberName);
    }

    protected override Expression Transform(LambdaExpression lmx)
    {
        if (!TryTransform(lmx.Body, out var body))
            return lmx;

        // By default we maintain the parameters available in the body
        return new LambdaExpression(lmx.Parameters, body);
    }

    // Only touches uses of the parameters, not decls
    protected override Expression Transform(ParameterExpression prx)
    {
        return prx;
    }

    protected override Expression Transform(IndexerWildcardExpression wx)
    {
        return wx;
    }

    protected override Expression Transform(ArrayExpression ax)
    {
        var any = false;
        var elements = new List<Element>();
        foreach (var el in ax.Elements)
        {
            if (el is ItemElement item)
            {
                if (TryTransform(item.Value, out var result))
                    any = true;
                elements.Add(new ItemElement(result));
            }
            else
            {
                var spread = (SpreadElement) el;
                if (TryTransform(spread.Content, out var result))
                    any = true;
                elements.Add(new SpreadElement(result));
            }
        }

        if (!any)
            return ax;

        return new ArrayExpression(elements.ToArray());
    }

    protected override Expression Transform(ObjectExpression ox)
    {
        var any = false;
        var members = new List<Member>();
        foreach (var m in ox.Members)
        {
            if (m is PropertyMember p)
            {
                if (TryTransform(p.Value, out var result))
                    any = true;
                members.Add(new PropertyMember(p.Name, result));
            }
            else
            {
                var s = (SpreadMember) m;
                if (TryTransform(s.Content, out var result))
                    any = true;
                members.Add(new SpreadMember(result));
            }
        }

        if (!any)
            return ox;

        return new ObjectExpression(members.ToArray());
    }

    protected override Expression Transform(IndexerExpression ix)
    {
        var transformedRecv = TryTransform(ix.Receiver, out var recv);

        if (!TryTransform(ix.Index, out var index) && !transformedRecv)
            return ix;

        return new IndexerExpression(recv, index);
    }

    protected override Expression Transform(IndexOfMatchExpression mx)
    {
        if (!TryTransform(mx.Corpus, out var corpus))
            return mx;

        return new IndexOfMatchExpression(corpus, mx.Regex);
    }
}