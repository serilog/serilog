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

using System.Diagnostics.CodeAnalysis;
using Serilog.Templates.Ast;

namespace Serilog.Templates.Parsing;

class TemplateParser
{
    readonly TemplateTokenizer _tokenizer = new();
    readonly TemplateTokenParsers _templateTokenParsers = new();

    public bool TryParse(
        string template,
        [MaybeNullWhen(false)] out Template parsed,
        [MaybeNullWhen(true)] out string error)
    {
        if (template == null) throw new ArgumentNullException(nameof(template));

        var tokenList = _tokenizer.TryTokenize(template);
        if (!tokenList.HasValue)
        {
            error = tokenList.ToString();
            parsed = null;
            return false;
        }

        var result = _templateTokenParsers.TryParse(tokenList.Value);
        if (!result.HasValue)
        {
            error = result.ToString();
            parsed = null;
            return false;
        }

        parsed = result.Value;
        error = null;
        return true;
    }
}