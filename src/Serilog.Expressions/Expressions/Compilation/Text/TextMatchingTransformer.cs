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

using System.Text.RegularExpressions;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Expressions.Ast;
using Serilog.Expressions.Compilation.Transformations;

namespace Serilog.Expressions.Compilation.Text;

class TextMatchingTransformer: IdentityTransformer
{
    static readonly TextMatchingTransformer Instance = new();

    public static Expression Rewrite(Expression expression)
    {
        return Instance.Transform(expression);
    }

    protected override Expression Transform(CallExpression call)
    {
        if (call.Operands.Length != 2)
            return base.Transform(call);

        if (Operators.SameOperator(call.OperatorName, Operators.OpIndexOfMatch))
            return TryCompileIndexOfMatch(call.IgnoreCase, call.Operands[0], call.Operands[1]);

        if (Operators.SameOperator(call.OperatorName, Operators.OpIsMatch))
            return new CallExpression(
                false,
                Operators.RuntimeOpNotEqual,
                TryCompileIndexOfMatch(call.IgnoreCase, call.Operands[0], call.Operands[1]),
                new ConstantExpression(new ScalarValue(-1)));

        return base.Transform(call);
    }

    Expression TryCompileIndexOfMatch(bool ignoreCase, Expression corpus, Expression regex)
    {
        if (regex is ConstantExpression { Constant: ScalarValue { Value: string s } })
        {
            try
            {
                var opts = RegexOptions.Compiled | RegexOptions.ExplicitCapture;
                if (ignoreCase)
                    opts |= RegexOptions.IgnoreCase;
                var compiled = new Regex(s, opts, TimeSpan.FromMilliseconds(100));
                return new IndexOfMatchExpression(Transform(corpus), compiled);
            }
            catch (ArgumentException ex)
            {
                throw new ExpressionValidationException($"Invalid regular expression in IndexOfMatch: {ex.Message}", ex);
            }
        }

        SelfLog.WriteLine($"Serilog.Expressions: `IndexOfMatch()` requires a constant string regular expression argument; found {regex}.");
        return new CallExpression(false, Operators.OpUndefined);
    }
}