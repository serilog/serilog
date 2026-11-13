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

using Serilog.Templates.Ast;

namespace Serilog.Templates.Compilation.UnreferencedProperties;

class TemplateReferencedPropertiesFinder
{
    readonly ExpressionReferencedPropertiesFinder _rpf = new();

    public IEnumerable<string> FindReferencedProperties(Template template)
    {
        return template switch
        {
            Conditional conditional => _rpf.FindReferencedProperties(conditional.Condition)
                .Concat(FindReferencedProperties(conditional.Consequent))
                .Concat(conditional.Alternative != null
                    ? FindReferencedProperties(conditional.Alternative)
                    : Enumerable.Empty<string>()),
            FormattedExpression formattedExpression =>
                _rpf.FindReferencedProperties(formattedExpression.Expression),
            LiteralText => Enumerable.Empty<string>(),
            Repetition repetition => _rpf.FindReferencedProperties(repetition.Enumerable)
                .Concat(FindReferencedProperties(repetition.Body))
                .Concat(repetition.Alternative != null
                    ? FindReferencedProperties(repetition.Alternative)
                    : Enumerable.Empty<string>())
                .Concat(repetition.Delimiter != null
                    ? FindReferencedProperties(repetition.Delimiter)
                    : Enumerable.Empty<string>()),
            TemplateBlock templateBlock => templateBlock.Elements.SelectMany(FindReferencedProperties),
            _ => throw new ArgumentOutOfRangeException(nameof(template))
        };
    }
}