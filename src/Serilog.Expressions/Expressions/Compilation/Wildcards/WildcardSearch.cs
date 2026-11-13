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

class WildcardSearch : SerilogExpressionTransformer<IndexerExpression?>
{
    static readonly WildcardSearch Instance = new();

    public static IndexerExpression? FindWildcardIndexer(Expression fx)
    {
        return Instance.Transform(fx);
    }

    protected override IndexerExpression? Transform(IndexerExpression ix)
    {
        if (ix.Index is IndexerWildcardExpression)
            return ix;

        return Transform(ix.Receiver);
    }

    protected override IndexerExpression? Transform(ConstantExpression cx)
    {
        return null;
    }

    protected override IndexerExpression? Transform(AmbientNameExpression px)
    {
        return null;
    }

    protected override IndexerExpression? Transform(LocalNameExpression nlx)
    {
        return null;
    }

    protected override IndexerExpression? Transform(AccessorExpression spx)
    {
        return Transform(spx.Receiver);
    }

    protected override IndexerExpression? Transform(LambdaExpression lmx)
    {
        return null;
    }

    protected override IndexerExpression? Transform(ParameterExpression prx)
    {
        return null;
    }

    protected override IndexerExpression? Transform(IndexerWildcardExpression wx)
    {
        // Must be within an indexer
        return null;
    }

    protected override IndexerExpression? Transform(ArrayExpression ax)
    {
        return null;
    }

    protected override IndexerExpression? Transform(CallExpression call)
    {
        // If we hit a wildcard-compatible operation, then any wildcards within its operands "belong" to
        // it and can't be the result of this search.
        if (Operators.WildcardComparators.Contains(call.OperatorName))
            return null;

        return call.Operands.Select(Transform).FirstOrDefault(e => e != null);
    }

    protected override IndexerExpression? Transform(IndexOfMatchExpression mx)
    {
        return Transform(mx.Corpus);
    }

    protected override IndexerExpression? Transform(ObjectExpression ox)
    {
        return null;
    }
}