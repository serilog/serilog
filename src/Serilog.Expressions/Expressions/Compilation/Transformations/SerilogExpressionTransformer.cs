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

abstract class SerilogExpressionTransformer<TResult>
{
    protected virtual TResult Transform(Expression expression)
    {
        return expression switch
        {
            CallExpression call => Transform(call),
            ConstantExpression constant => Transform(constant),
            AccessorExpression accessor => Transform(accessor),
            AmbientNameExpression property => Transform(property),
            LocalNameExpression local => Transform(local),
            LambdaExpression lambda => Transform(lambda),
            ParameterExpression parameter => Transform(parameter),
            IndexerWildcardExpression wildcard => Transform(wildcard),
            ArrayExpression array => Transform(array),
            ObjectExpression obj => Transform(obj),
            IndexerExpression indexer => Transform(indexer),
            IndexOfMatchExpression match => Transform(match),
            null => throw new ArgumentNullException(nameof(expression)),
            // Non-exhaustive because `InternalsVisibleTo` is applied to the assembly.
            _ => throw new NotSupportedException($"{expression} is not supported.")
        };
    }

    protected abstract TResult Transform(CallExpression call);
    protected abstract TResult Transform(ConstantExpression cx);
    protected abstract TResult Transform(AmbientNameExpression px);
    protected abstract TResult Transform(LocalNameExpression nlx);
    protected abstract TResult Transform(AccessorExpression spx);
    protected abstract TResult Transform(LambdaExpression lmx);
    protected abstract TResult Transform(ParameterExpression prx);
    protected abstract TResult Transform(IndexerWildcardExpression wx);
    protected abstract TResult Transform(ArrayExpression ax);
    protected abstract TResult Transform(ObjectExpression ox);
    protected abstract TResult Transform(IndexerExpression ix);
    protected abstract TResult Transform(IndexOfMatchExpression mx);
}