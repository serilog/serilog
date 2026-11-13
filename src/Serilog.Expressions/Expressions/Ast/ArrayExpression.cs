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
/// An array expression constructs an array from a list of elements. For example, <code>[1, 2, 3]</code> is an
/// array expression. The items in an array expression can be literal values or expressions, like in the
/// above example, or they can be spread expressions that describe zero or more elements to include in the
/// list. Whether included via regular elements or spread expressions, undefined values are ignored and won't
/// appear in the resulting array value.
/// </summary>
class ArrayExpression : Expression
{
    public ArrayExpression(Element[] elements)
    {
        Elements = elements ?? throw new ArgumentNullException(nameof(elements));
    }

    public Element[] Elements { get; }

    public override string ToString()
    {
        return "[" + string.Join(", ", Elements.Select(o => o.ToString())) + "]";
    }
}