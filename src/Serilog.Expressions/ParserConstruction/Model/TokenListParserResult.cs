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

namespace Serilog.ParserConstruction.Model;

/// <summary>
/// Helper methods for working with <see cref="TokenListParserResult{TKind,T}"/>.
/// </summary>
static class TokenListParserResult
{
    /// <summary>
    /// Create a token result with no value, indicating a failure to parse any value.
    /// </summary>
    /// <typeparam name="TKind">The kind of token.</typeparam>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="remainder">The start of un-parsed input.</param>
    /// <returns>An empty result.</returns>
    public static TokenListParserResult<TKind, T> Empty<TKind, T>(TokenList<TKind> remainder)
    {
        return new(remainder, Position.Empty, null, null, false);
    }

    /// <summary>
    /// Create a token result with no value, indicating a failure to parse any value.
    /// </summary>
    /// <typeparam name="TKind">The kind of token.</typeparam>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="remainder">The start of un-parsed input.</param>
    /// <param name="expectations">Expectations that could not be fulfilled.</param>
    /// <returns>An empty result.</returns>
    public static TokenListParserResult<TKind, T> Empty<TKind, T>(TokenList<TKind> remainder, string[] expectations)
    {
        return new(remainder, Position.Empty, null, expectations, false);
    }

    /// <summary>
    /// Create a token result with no value, indicating a failure to parse any value.
    /// </summary>
    /// <typeparam name="TKind">The kind of token.</typeparam>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="remainder">The start of un-parsed input.</param>
    /// <param name="errorMessage">An error message describing why the tokens could not be parsed.</param>
    /// <returns>An empty result.</returns>
    public static TokenListParserResult<TKind, T> Empty<TKind, T>(TokenList<TKind> remainder, string errorMessage)
    {
        return new(remainder, Position.Empty, errorMessage, null, false);
    }

    /// <summary>
    /// Create a token result with the provided value.
    /// </summary>
    /// <typeparam name="TKind">The kind of token.</typeparam>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="location">The location where parsing began.</param>
    /// <param name="remainder">The first un-parsed location.</param>
    /// <returns></returns>
    public static TokenListParserResult<TKind, T> Value<TKind, T>(T value, TokenList<TKind> location, TokenList<TKind> remainder)
    {
        return new(value, location, remainder, false);
    }

    /// <summary>
    /// Convert an empty result of one type into another.
    /// </summary>
    /// <typeparam name="TKind">The kind of token.</typeparam>
    /// <typeparam name="T">The source type.</typeparam>
    /// <typeparam name="U">The destination type.</typeparam>
    /// <param name="result">The result to convert.</param>
    /// <returns>The converted result.</returns>
    public static TokenListParserResult<TKind,U> CastEmpty<TKind, T, U>(TokenListParserResult<TKind, T> result)
    {
        return new(result.Remainder, result.SubTokenErrorPosition, result.ErrorMessage, result.Expectations, result.Backtrack);
    }

    /// <summary>
    /// Combine two empty results.
    /// </summary>
    /// <typeparam name="T">The source type.</typeparam>
    /// <typeparam name="TKind">The kind of token.</typeparam>
    /// <param name="first">The first value to combine.</param>
    /// <param name="second">The second value to combine.</param>
    /// <returns>A result of type <typeparamref name="T"/> carrying information from both results.</returns>
    public static TokenListParserResult<TKind, T> CombineEmpty<TKind, T>(TokenListParserResult<TKind, T> first, TokenListParserResult<TKind, T> second)
    {
        if (first.Remainder != second.Remainder)
            return second;

        var expectations = first.Expectations;
        if (expectations == null)
            expectations = second.Expectations;
        else if (second.Expectations != null)
        {
            expectations = new string[first.Expectations!.Length + second.Expectations.Length];
            var i = 0;
            for (; i < first.Expectations!.Length; ++i)
                expectations[i] = first.Expectations![i];
            for (var j = 0; j < second.Expectations.Length; ++i, ++j)
                expectations[i] = second.Expectations[j];
        }

        return new(second.Remainder, second.SubTokenErrorPosition, first.ErrorMessage, expectations, second.Backtrack);
    }
}