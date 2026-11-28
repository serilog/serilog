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

namespace Serilog.Expressions.Compilation.Variadics;

// Handles variadic `coalesce()` and `concat()`, as well as optional arguments for other functions.
class VariadicCallRewriter : IdentityTransformer
{
    static readonly VariadicCallRewriter Instance = new();

    public static Expression Rewrite(Expression expression)
    {
        return Instance.Transform(expression);
    }

    protected override Expression Transform(CallExpression call)
    {
        if (Operators.SameOperator(call.OperatorName, Operators.OpCoalesce) ||
            Operators.SameOperator(call.OperatorName, Operators.OpConcat))
        {
            if (call.Operands.Length == 0)
                return new CallExpression(false, Operators.OpUndefined);
            if (call.Operands.Length == 1)
                return Transform(call.Operands.Single());
            if (call.Operands.Length > 2)
            {
                var first = Transform(call.Operands.First());
                return new CallExpression(call.IgnoreCase, call.OperatorName, first,
                    Transform(new CallExpression(call.IgnoreCase, call.OperatorName, call.Operands.Skip(1).ToArray())));
            }
        }

        return base.Transform(call);
    }
}