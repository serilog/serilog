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

using Serilog.ParserConstruction;
using Serilog.ParserConstruction.Model;

namespace Serilog.Expressions.Parsing;

static class ParserExtensions
{
    public static TokenListParser<TTokenKind, TResult> SelectCatch<TTokenKind, TArg, TResult>(this TokenListParser<TTokenKind, TArg> parser, Func<TArg, TResult> trySelector, string errorMessage)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        if (trySelector == null) throw new ArgumentNullException(nameof(trySelector));
        if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));

        return input =>
        {
            var t = parser(input);
            if (!t.HasValue)
                return TokenListParserResult.CastEmpty<TTokenKind, TArg, TResult>(t);

            try
            {
                var u = trySelector(t.Value);
                return TokenListParserResult.Value(u, input, t.Remainder);
            }
            catch
            {
                return TokenListParserResult.Empty<TTokenKind, TResult>(input, errorMessage);
            }
        };
    }
}