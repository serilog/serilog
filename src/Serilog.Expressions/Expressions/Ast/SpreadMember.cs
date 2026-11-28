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
/// An <see cref="ObjectExpression"/> member that designates another object from which to copy members into the
/// current object. Spread member expressions comprise two dots preceding an expression that is expected to
/// evaluate to an object.
/// </summary>
class SpreadMember : Member
{
    public Expression Content { get; }

    public SpreadMember(Expression content)
    {
        Content = content;
    }

    public override string ToString()
    {
        return $"..{Content}";
    }
}