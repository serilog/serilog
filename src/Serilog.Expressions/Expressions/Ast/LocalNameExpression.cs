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
/// Represents the iteration variable in template <code>#each</code> directives.
/// </summary>
class LocalNameExpression : Expression
{
    public LocalNameExpression(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name { get; }

    public override string ToString()
    {
        // No unambiguous syntax for this right now, `$` will do to make these stand out when debugging,
        // but the result won't round-trip parse.
        return $"${Name}";
    }
}