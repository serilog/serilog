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

using Serilog.ParserConstruction.Model;

namespace Serilog.ParserConstruction;

/// <summary>
/// Helper methods for working with parsers.
/// </summary>
static class ParserExtensions
{
    /// <summary>
    /// Tries to parse the input without throwing an exception upon failure.
    /// </summary>
    /// <typeparam name="TKind">The type of tokens consumed by the parser.</typeparam>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="input">The input.</param>
    /// <returns>The result of the parser</returns>
    /// <exception cref="ArgumentNullException">The parser or input is null.</exception>
    public static TokenListParserResult<TKind, T> TryParse<TKind, T>(this TokenListParser<TKind, T> parser, TokenList<TKind> input)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return parser(input);
    }
}