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

namespace Serilog.Expressions.Compilation.Wildcards;

class WildcardComprehensionTransformer : IdentityTransformer
{
    int _nextParameter;

    public static Expression Rewrite(Expression root)
    {
        var wc = new WildcardComprehensionTransformer();
        return wc.Transform(root);
    }

    // This matches expression fragments such as `A[?] = 'test'` and
    // transforms them into `any(A, |p| p = 'test)`.
    //
    // As the comparand in such expressions can be complex, e.g.
    // `Substring(A[?], 0, 4) = 'test')`, the search for `?` and `*` wildcards
    // is deep, but, it terminates upon reaching any other wildcard-compatible
    // comparison. Thus `(A[?] = 'test') = true` will result in `any(A, |p| p = 'test') = true` and
    // not `any(A, |p| (p = 'test') = true)`, which is important because short-circuiting when the first
    // argument to `any()` is undefined will change the semantics of the resulting expression, otherwise.
    protected override Expression Transform(CallExpression lx)
    {
        if (!Operators.WildcardComparators.Contains(lx.OperatorName))
            return base.Transform(lx);

        IndexerExpression? indexer = null;
        Expression? wildcardPath = null;
        var indexerOperand = -1;
        for (var i = 0; i < lx.Operands.Length; ++i)
        {
            indexer = WildcardSearch.FindWildcardIndexer(lx.Operands[i]);
            if (indexer != null)
            {
                indexerOperand = i;
                wildcardPath = lx.Operands[i];
                break;
            }
        }

        if (indexer == null || wildcardPath == null)
            return base.Transform(lx); // N/A, or invalid

        var px = new ParameterExpression("p" + _nextParameter++);
        var nestedComparand = NodeReplacer.Replace(wildcardPath, indexer, px);

        var coll = indexer.Receiver;
        var wc = ((IndexerWildcardExpression)indexer.Index).Wildcard;

        var comparisonArgs = lx.Operands.ToArray();
        comparisonArgs[indexerOperand] = nestedComparand;
        var body = new CallExpression(lx.IgnoreCase, lx.OperatorName, comparisonArgs);

        var lambda = new LambdaExpression([px], body);

        var op = Operators.ToRuntimeWildcardOperator(wc);
        var call = new CallExpression(false, op, coll, lambda);
        return Transform(call);
    }

    // Detects and transforms standalone `A[?]` fragments that are not part of a comparision; these
    // are effectively Boolean tests.
    protected override Expression Transform(IndexerExpression ix)
    {
        if (ix.Index is not IndexerWildcardExpression wx)
            return base.Transform(ix);

        var px = new ParameterExpression("p" + _nextParameter++);
        var coll = Transform(ix.Receiver);
        var lambda = new LambdaExpression([px], px);
        var op = Operators.ToRuntimeWildcardOperator(wx.Wildcard);
        return new CallExpression(false, op, coll, lambda);
    }
}