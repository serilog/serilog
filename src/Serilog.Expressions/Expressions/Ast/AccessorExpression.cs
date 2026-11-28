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
/// An accessor retrieves a property from a (structured) object. For example, in the expression
/// <code>Headers.ContentType</code> the <code>.</code> operator forms an accessor expression that
/// retrieves the <code>ContentType</code> property from the <code>Headers</code> object.
/// </summary>
/// <remarks>Note that the AST type can represent accessors that cannot be validly written using
/// <code>.</code> notation. In these cases, if the accessor is formatted back out as an expression,
/// <see cref="IndexerExpression"/> notation will be used.</remarks>
class AccessorExpression : Expression
{
    public AccessorExpression(Expression receiver, string memberName)
    {
        MemberName = memberName ?? throw new ArgumentNullException(nameof(memberName));
        Receiver = receiver;
    }

    public string MemberName { get; }

    public Expression Receiver { get; }

    public override string ToString()
    {
        if (SerilogExpression.IsValidIdentifier(MemberName))
            return Receiver + "." + MemberName;

        return $"{Receiver}['{SerilogExpression.EscapeStringContent(MemberName)}']";
    }
}