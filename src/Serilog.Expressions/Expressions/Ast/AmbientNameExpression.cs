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
/// An ambient name is generally a property name or built-in that appears standalone in an expression. For example,
/// in <code>Headers.ContentType</code>, <code>Headers</code> is an ambient name that produces an
/// <see cref="AmbientNameExpression"/>. Built-ins like <code>@Level</code> are also parsed as ambient names.
/// </summary>
class AmbientNameExpression : Expression
{
    readonly bool _requiresEscape;

    public AmbientNameExpression(string name, bool isBuiltIn)
    {
        PropertyName = name ?? throw new ArgumentNullException(nameof(name));
        IsBuiltIn = isBuiltIn;
        _requiresEscape = !SerilogExpression.IsValidIdentifier(name);
    }

    public string PropertyName { get; }

    public bool IsBuiltIn { get; }

    public override string ToString()
    {
        if (_requiresEscape)
            return $"@Properties['{SerilogExpression.EscapeStringContent(PropertyName)}']";

        return (IsBuiltIn ? "@" : "") + PropertyName;
    }
}