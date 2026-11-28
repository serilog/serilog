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

namespace Serilog.Templates.Ast;

class Repetition: Template
{
    public Expression Enumerable { get; }
    public string[] BindingNames { get; }
    public Template Body { get; }
    public Template? Delimiter { get; }
    public Template? Alternative { get; }

    public Repetition(
        Expression enumerable,
        string[] bindingNames,
        Template body,
        Template? delimiter,
        Template? alternative)
    {
        Enumerable = enumerable ?? throw new ArgumentNullException(nameof(enumerable));
        BindingNames = bindingNames;
        Body = body ?? throw new ArgumentNullException(nameof(body));
        Delimiter = delimiter;
        Alternative = alternative;
    }
}