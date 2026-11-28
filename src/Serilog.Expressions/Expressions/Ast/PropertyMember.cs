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

using Serilog.Events;

namespace Serilog.Expressions.Ast;

/// <summary>
/// An <see cref="ObjectExpression"/> member comprising an optionally-quoted name and a value, for example
/// the object <code>{Username: 'alice'}</code> includes a single <see cref="PropertyMember"/> with name
/// <code>Username</code> and value <code>'alice'</code>.
/// </summary>
class PropertyMember : Member
{
    public string Name { get; }
    public Expression Value { get; }

    public PropertyMember(string name, Expression value)
    {
        Name = name;
        Value = value;
    }

    public override string ToString()
    {
        return $"{new ScalarValue(Name)}: {Value}";
    }
}