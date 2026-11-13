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
using Serilog.ParserConstruction.Util;

namespace Serilog.ParserConstruction;

/// <summary>
/// General parsing helper methods.
/// </summary>
static class Parse
{
    /// <summary>
    /// Parse a sequence of similar operands connected by left-associative operators.
    /// </summary>
    /// <typeparam name="T">The type being parsed.</typeparam>
    /// <typeparam name="TOperator">The type of the operator.</typeparam>
    /// <typeparam name="TKind">The kind of token being parsed.</typeparam>
    /// <param name="operator">A parser matching operators.</param>
    /// <param name="operand">A parser matching operands.</param>
    /// <param name="apply">A function combining an operator and two operands into the result.</param>
    /// <returns>The result of calling <paramref name="apply"/> successively on pairs of operands.</returns>
    /// <seealso cref="Combinators.Chain{TKind, TResult,TOperator,TOperand}"/>
    public static TokenListParser<TKind, T> Chain<TKind, T, TOperator>(
        TokenListParser<TKind, TOperator> @operator,
        TokenListParser<TKind, T> operand,
        Func<TOperator, T, T, T> apply)
    {
        return operand.Chain(@operator, operand, apply);
    }

    /// <summary>
    /// Constructs a parser that will fail if the given parser succeeds,
    /// and will succeed if the given parser fails. In any case, it won't
    /// consume any input. It's like a negative look-ahead in a regular expression.
    /// </summary>
    /// <typeparam name="T">The result type of the given parser.</typeparam>
    /// <typeparam name="TKind">The kind of token being parsed.</typeparam>
    /// <param name="parser">The parser to wrap</param>
    /// <returns>A parser that is the negation of the given parser.</returns>
    public static TokenListParser<TKind, Unit> Not<TKind, T>(TokenListParser<TKind, T> parser)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return input =>
        {
            var result = parser(input);

            if (result.HasValue)
            {
                // This is usually a success case for Not(), so the allocations here are a bit of a pity.

                var current = input.ConsumeToken();
                var last = result.Remainder.ConsumeToken();
                if (current.HasValue)
                {
                    var span = last.HasValue ?
                        current.Value.Span.Source!.Substring(current.Value.Position.Absolute, last.Value.Position.Absolute - current.Value.Position.Absolute) :
                        current.Value.Span.Source!.Substring(current.Value.Position.Absolute);
                    return TokenListParserResult.Empty<TKind, Unit>(input, $"unexpected successful parsing of {Presentation.FormatLiteral(Friendly.Clip(span, 12))}");
                }

                return TokenListParserResult.Empty<TKind, Unit>(input, "unexpected successful parsing");
            }

            return TokenListParserResult.Value(Unit.Value, input, input);
        };
    }

    /// <summary>
    /// Lazily construct a parser, so that circular dependencies are possible.
    /// </summary>
    /// <param name="reference">A function creating the parser, when required.</param>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="TKind">The kind of token being parsed.</typeparam>
    /// <returns>A parser that lazily evaluates <paramref name="reference"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="reference"/> is null.</exception>
    public static TokenListParser<TKind, T> Ref<TKind, T>(Func<TokenListParser<TKind, T>> reference)
    {
        if (reference == null) throw new ArgumentNullException(nameof(reference));

        TokenListParser<TKind, T>? parser = null;

        return i =>
        {
            parser ??= reference();

            return parser(i);
        };
    }

    /// <summary>
    /// Construct a parser with a fixed value.
    /// </summary>
    /// <param name="value">The value returned by the parser.</param>
    /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
    /// <returns>The parser.</returns>
    public static TextParser<T> Return<T>(T value)
    {
        return input => Result.Value(value, input, input);
    }

    /// <summary>
    /// Construct a parser with a fixed value.
    /// </summary>
    /// <param name="value">The value returned by the parser.</param>
    /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
    /// <typeparam name="TKind">The kind of token being parsed.</typeparam>
    /// <returns>The parser.</returns>
    public static TokenListParser<TKind, T> Return<TKind, T>(T value)
    {
        return input => TokenListParserResult.Value(value, input, input);
    }
}