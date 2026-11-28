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

namespace Serilog.Expressions.Ast;

/// <summary>
/// A <see cref="CallExpression"/> is a function call made up of the function name, parenthesised argument
/// list, and optional postfix <code>ci</code> modifier. For example, <code>Substring(RequestPath, 0, 5)</code>.
/// </summary>
class CallExpression : Expression
{
    public CallExpression(bool ignoreCase, string operatorName, params Expression[] operands)
    {
        IgnoreCase = ignoreCase;
        OperatorName = operatorName ?? throw new ArgumentNullException(nameof(operatorName));
        Operands = operands ?? throw new ArgumentNullException(nameof(operands));
    }

    public bool IgnoreCase { get; }

    public string OperatorName { get; }

    public Expression[] Operands { get; }

    public override string ToString()
    {
        return OperatorName
               + "(" + string.Join(", ", Operands.Select(o => o.ToString())) + ")"
               + (IgnoreCase ? " ci" : "");
    }
}