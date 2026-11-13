// Copyright 2016 Datalust, Superpower Contributors, Sprache Contributors
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

using Serilog.ParserConstruction.Display;
using Serilog.ParserConstruction.Model;

namespace Serilog.ParserConstruction.Parsers;

/// <summary>
/// Parsers for matching individual tokens.
/// </summary>
static class Token
{
    /// <summary>
    /// Parse a token of the kind <typeparamref name="TKind"/>.
    /// </summary>
    /// <typeparam name="TKind">The type of the token being matched.</typeparam>
    /// <param name="kind">The kind of token to match.</param>
    /// <returns>The matched token.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static TokenListParser<TKind, Token<TKind>> EqualTo<TKind>(TKind kind)
    {
        var expectations = new[] { Presentation.FormatExpectation(kind) };

        return input =>
        {
            var next = input.ConsumeToken();
            if (!next.HasValue || !next.Value.Kind!.Equals(kind))
                return TokenListParserResult.Empty<TKind, Token<TKind>>(input, expectations);

            return next;
        };
    }

    /// <summary>
    /// Parse a sequence of tokens of the kind <typeparamref name="TKind"/>.
    /// </summary>
    /// <typeparam name="TKind">The type of the tokens being matched.</typeparam>
    /// <param name="kinds">The kinds of token to match, once each in order.</param>
    /// <returns>The matched tokens.</returns>
    public static TokenListParser<TKind, Token<TKind>[]> Sequence<TKind>(params TKind[] kinds)
    {
        if (kinds == null) throw new ArgumentNullException(nameof(kinds));

        TokenListParser<TKind, Token<TKind>[]> result = input => TokenListParserResult.Value(new Token<TKind>[kinds.Length], input, input);
        for (var i = 0; i < kinds.Length; ++i)
        {
            var token = EqualTo(kinds[i]);
            var index = i;
            result = result.Then(arr => token.Select(t => { arr[index] = t; return arr; }));
        }
        return result;
    }
}