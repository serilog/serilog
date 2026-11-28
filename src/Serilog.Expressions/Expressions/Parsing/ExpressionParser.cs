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
using Serilog.Expressions.Ast;

namespace Serilog.Expressions.Parsing;

class ExpressionParser
{
    readonly ExpressionTokenizer _tokenizer = new();

    public Expression Parse(string expression)
    {
        if (!TryParse(expression, out var root, out var error))
            throw new ArgumentException(error);

        return root;
    }

    public bool TryParse(string filterExpression,
        [MaybeNullWhen(false)] out Expression root, [MaybeNullWhen(true)] out string error)
    {
        if (filterExpression == null) throw new ArgumentNullException(nameof(filterExpression));

        var tokenList = _tokenizer.TryTokenize(filterExpression);
        if (!tokenList.HasValue)
        {
            error = tokenList.ToString();
            root = null;
            return false;
        }

        var result = ExpressionTokenParsers.TryParse(tokenList.Value);
        if (!result.HasValue)
        {
            error = result.ToString();
            root = null;
            return false;
        }

        root = result.Value;
        error = null;
        return true;
    }
}