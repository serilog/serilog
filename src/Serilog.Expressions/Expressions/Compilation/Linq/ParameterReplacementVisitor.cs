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

using System.Linq.Expressions;

namespace Serilog.Expressions.Compilation.Linq;

class ParameterReplacementVisitor : ExpressionVisitor
{
    readonly ParameterExpression[] _from, _to;

    public static Expression ReplaceParameters(LambdaExpression lambda, params ParameterExpression[] newParameters)
    {
        var v = new ParameterReplacementVisitor(lambda.Parameters.ToArray(), newParameters);
        return v.Visit(lambda.Body)!;
    }

    ParameterReplacementVisitor(ParameterExpression[] from, ParameterExpression[] to)
    {
        if (from == null) throw new ArgumentNullException(nameof(from));
        if (to == null) throw new ArgumentNullException(nameof(to));
        if (from.Length != to.Length) throw new InvalidOperationException("Mismatched parameter lists");
        _from = from;
        _to = to;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        for (var i = 0; i < _from.Length; i++)
        {
            if (node == _from[i]) return _to[i];
        }
        return node;
    }
}