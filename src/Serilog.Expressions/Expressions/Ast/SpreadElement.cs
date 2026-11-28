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
/// An element in an <see cref="ArrayExpression"/> that describes zero or more items to include in the array.
/// Spread elements are written with two dots preceding an expression that evaluates to an array of elements to
/// insert into the result array at the position of the spread element, for example, in <code>[1, 2, ..Others]</code>,
/// the <code>..Others</code> expression is a spread element. If the value of the array in the spread is
/// undefined, no items will be added to the list.
/// </summary>
class SpreadElement : Element
{
    public Expression Content { get; }

    public SpreadElement(Expression content)
    {
        Content = content;
    }

    public override string ToString()
    {
        return $"..{Content}";
    }
}