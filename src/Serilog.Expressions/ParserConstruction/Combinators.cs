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

// ReSharper disable MemberCanBePrivate.Global

namespace Serilog.ParserConstruction;

/// <summary>
/// Functions that construct more complex parsers by combining simpler ones.
/// </summary>
static class Combinators
{
    /// <summary>
    /// Apply the text parser <paramref name="valueParser"/> to the span represented by the parsed token.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="valueParser">A text parser to apply.</param>
    /// <returns>A parser that returns the result of parsing the token value.</returns>
    public static TokenListParser<TKind, U> Apply<TKind, U>(this TokenListParser<TKind, Token<TKind>> parser, TextParser<U> valueParser)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        if (valueParser == null) throw new ArgumentNullException(nameof(valueParser));

        var valueParserAtEnd = valueParser.AtEnd();
        return input =>
        {
            var rt = parser(input);
            if (!rt.HasValue)
                return TokenListParserResult.CastEmpty<TKind, Token<TKind>, U>(rt);

            var uResult = valueParserAtEnd(rt.Value.Span);
            if (uResult.HasValue)
                return TokenListParserResult.Value(uResult.Value, rt.Location, rt.Remainder);

            var problem = uResult.Remainder.IsAtEnd ? "incomplete" : "invalid";
            var textError = uResult.Remainder.IsAtEnd ? uResult.Expectations != null ? $", expected {Friendly.List(uResult.Expectations)}" : "" : $", {uResult.FormatErrorMessageFragment()}";
            var message = $"{problem} {Presentation.FormatExpectation(rt.Value.Kind)}{textError}";
            return new(input, rt.Remainder, uResult.Remainder.Position, message, null, uResult.Backtrack);
        };
    }

    /// <summary>
    /// Construct a parser that succeeds only if the source is at the end of input.
    /// </summary>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <returns>The resulting parser.</returns>
    public static TextParser<T> AtEnd<T>(this TextParser<T> parser)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return input =>
        {
            var result = parser(input);
            if (!result.HasValue)
                return result;

            if (result.Remainder.IsAtEnd)
                return result;

            return Result.Empty<T>(result.Remainder);
        };
    }

    /// <summary>
    /// Construct a parser that succeeds only if the source is at the end of input.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, T> AtEnd<TKind, T>(this TokenListParser<TKind, T> parser)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return input =>
        {
            var result = parser(input);
            if (!result.HasValue)
                return result;

            if (result.Remainder.IsAtEnd)
                return result;

            return TokenListParserResult.Empty<TKind, T>(result.Remainder);
        };
    }

    /// <summary>
    /// Construct a parser that matches one or more instances of applying <paramref name="parser"/>.
    /// </summary>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <returns>The resulting parser.</returns>
    public static TextParser<T[]> AtLeastOnce<T>(this TextParser<T> parser)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        return parser.Then(first => parser.Many().Select(rest => (T[])[first, ..rest]));
    }

    /// <summary>
    /// Construct a parser that matches one or more instances of applying <paramref name="parser"/>, delimited by <paramref name="delimiter"/>.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="delimiter">The parser that matches the delimiters.</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, T[]> AtLeastOnceDelimitedBy<TKind, T, U>(this TokenListParser<TKind, T> parser, TokenListParser<TKind, U> delimiter)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        if (delimiter == null) throw new ArgumentNullException(nameof(delimiter));

        return parser.Then(first => delimiter.IgnoreThen(parser).Many().Select(rest => (T[])[first, ..rest]));
    }

    /// <summary>
    /// Construct a parser that matches <paramref name="first"/>, discards the resulting value, then returns the result of <paramref name="second"/>.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="first">The first parser.</param>
    /// <param name="second">The second parser.</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, U> IgnoreThen<TKind, T, U>(this TokenListParser<TKind, T> first, TokenListParser<TKind, U> second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));

        return input =>
        {
            var rt = first(input);
            if (!rt.HasValue)
                return TokenListParserResult.CastEmpty<TKind, T, U>(rt);

            var ru = second(rt.Remainder);
            if (!ru.HasValue)
                return ru;

            return TokenListParserResult.Value(ru.Value, input, ru.Remainder);
        };
    }

    /// <summary>
    /// Construct a parser that matches <paramref name="first"/>, discards the resulting value, then returns the result of <paramref name="second"/>.
    /// </summary>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="first">The first parser.</param>
    /// <param name="second">The second parser.</param>
    /// <returns>The resulting parser.</returns>
    public static TextParser<U> IgnoreThen<T, U>(this TextParser<T> first, TextParser<U> second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));

        return input =>
        {
            var rt = first(input);
            if (!rt.HasValue)
                return Result.CastEmpty<T, U>(rt);

            var ru = second(rt.Remainder);
            if (!ru.HasValue)
                return ru;

            return Result.Value(ru.Value, input, ru.Remainder);
        };
    }

    /// <summary>
    /// Construct a parser that matches <paramref name="parser"/> zero or more times.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <returns>The resulting parser.</returns>
    /// <remarks>Many will fail if any item partially matches this. To modify this behavior use <see cref="Try{TKind,T}(TokenListParser{TKind,T})"/>.</remarks>
    public static TokenListParser<TKind, T[]> Many<TKind, T>(this TokenListParser<TKind, T> parser)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return input =>
        {
            var result = new List<T>();
            var from = input;
            var r = parser(input);
            while (r.HasValue)
            {
                if (from == r.Remainder) // Broken parser, not a failed parsing.
                    throw new ParseException($"Many() cannot be applied to zero-width parsers; value {r.Value} at position {r.Location.Position}.", r.ErrorPosition);

                result.Add(r.Value);
                from = r.Remainder;
                r = parser(r.Remainder);
            }

            if (!r.Backtrack && r.IsPartial(from))
                return TokenListParserResult.CastEmpty<TKind, T, T[]>(r);

            return TokenListParserResult.Value(result.ToArray(), input, from);
        };
    }

    /// <summary>
    /// Construct a parser that matches <paramref name="parser"/> zero or more times.
    /// </summary>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <returns>The resulting parser.</returns>
    /// <remarks>Many will fail if any item partially matches this. To modify this behavior use <see cref="Try{T}(TextParser{T})"/>.</remarks>
    public static TextParser<T[]> Many<T>(this TextParser<T> parser)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return input =>
        {
            var result = new List<T>();
            var from = input;
            var r = parser(input);
            while (r.HasValue)
            {
                if (from == r.Remainder) // Broken parser, not a failed parsing.
                    throw new ParseException($"Many() cannot be applied to zero-width parsers; value {r.Value} at position {r.Location.Position}.", r.Location.Position);

                result.Add(r.Value);

                from = r.Remainder;
                r = parser(r.Remainder);
            }

            if (!r.Backtrack && r.IsPartial(from))
                return Result.CastEmpty<T, T[]>(r);

            return Result.Value(result.ToArray(), input, from);
        };
    }

    /// <summary>
    /// Construct a parser that matches <paramref name="parser"/> zero or more times, delimited by <paramref name="delimiter"/>.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="delimiter">The parser that matches the delimiters.</param>
    /// <param name="end">A parser to match a final trailing delimiter, if required. Specifying
    /// this can improve error reporting for some lists.</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, T[]> ManyDelimitedBy<TKind, T, U>(
        this TokenListParser<TKind, T> parser,
        TokenListParser<TKind, U> delimiter,
        TokenListParser<TKind, U>? end = null)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        if (delimiter == null) throw new ArgumentNullException(nameof(delimiter));

        // ReSharper disable once ConvertClosureToMethodGroup

        if (end != null)
            return parser
                .AtLeastOnceDelimitedBy(delimiter)
                .Then(p => end.Value(p))
                .Or(end.Value(Array.Empty<T>()));

        return parser
            .Then(first => delimiter.IgnoreThen(parser).Many().Select(rest => (T[])[first, ..rest]))
            .OptionalOrDefault([]);
    }

    /// <summary>
    /// Construct a parser that returns <paramref name="name"/> as its "expectation" if <paramref name="parser"/> fails.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="name">The name given to <paramref name="parser"/>.</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, T> Named<TKind, T>(this TokenListParser<TKind, T> parser, string name)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        if (name == null) throw new ArgumentNullException(nameof(name));

        return input =>
        {
            var result = parser(input);
            if (result.HasValue || result.IsPartial(input))
                return result;

            return TokenListParserResult.Empty<TKind, T>(result.Remainder, [name]);
        };
    }

    /// <summary>
    /// Construct a parser that returns <paramref name="name"/> as its "expectation" if <paramref name="parser"/> fails.
    /// </summary>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="name">The name given to <paramref name="parser"/>.</param>
    /// <returns>The resulting parser.</returns>
    public static TextParser<T> Named<T>(this TextParser<T> parser, string name)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        if (name == null) throw new ArgumentNullException(nameof(name));

        return input =>
        {
            var result = parser(input);
            if (result.HasValue || result.IsPartial(input))
                return result;

            return Result.Empty<T>(result.Remainder, [name]);
        };
    }

    /// <summary>
    /// Construct a parser that matches zero or one instance of <paramref name="parser"/>, returning <paramref name="defaultValue"/> when
    /// no match is possible.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, T> OptionalOrDefault<TKind, T>(this TokenListParser<TKind, T> parser, T defaultValue = default!)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return parser.Or(Parse.Return<TKind, T>(defaultValue!));
    }

    /// <summary>
    /// Construct a parser that matches zero or one instance of <paramref name="parser"/>, returning <paramref name="defaultValue"/> when
    /// no match is possible.
    /// </summary>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The resulting parser.</returns>
    public static TextParser<T> OptionalOrDefault<T>(this TextParser<T> parser, T defaultValue = default!)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return parser.Or(Parse.Return(defaultValue!));
    }

    /// <summary>
    /// Construct a parser that tries first the <paramref name="lhs"/> parser, and if it fails, applies <paramref name="rhs"/>.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="lhs">The first parser to try.</param>
    /// <param name="rhs">The second parser to try.</param>
    /// <returns>The resulting parser.</returns>
    /// <remarks>Or will fail if the first item partially matches this. To modify this behavior use <see cref="Try{TKind,T}(TokenListParser{TKind,T})"/>.</remarks>
    public static TokenListParser<TKind, T> Or<TKind, T>(this TokenListParser<TKind, T> lhs, TokenListParser<TKind, T> rhs)
    {
        if (lhs == null) throw new ArgumentNullException(nameof(lhs));
        if (rhs == null) throw new ArgumentNullException(nameof(rhs));

        return input =>
        {
            var first = lhs(input);
            if (first.HasValue || !first.Backtrack && first.IsPartial(input))
                return first;

            var second = rhs(input);
            if (second.HasValue)
                return second;

            return TokenListParserResult.CombineEmpty(first, second);
        };
    }

    /// <summary>
    /// Construct a parser that tries first the <paramref name="lhs"/> parser, and if it fails, applies <paramref name="rhs"/>.
    /// </summary>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="lhs">The first parser to try.</param>
    /// <param name="rhs">The second parser to try.</param>
    /// <returns>The resulting parser.</returns>
    /// <remarks>Or will fail if the first item partially matches this. To modify this behavior use <see cref="Try{T}(TextParser{T})"/>.</remarks>
    public static TextParser<T> Or<T>(this TextParser<T> lhs, TextParser<T> rhs)
    {
        if (lhs == null) throw new ArgumentNullException(nameof(lhs));
        if (rhs == null) throw new ArgumentNullException(nameof(rhs));

        return input =>
        {
            var first = lhs(input);
            if (first.HasValue || !first.Backtrack && first.IsPartial(input))
                return first;

            var second = rhs(input);
            if (second.HasValue)
                return second;

            return Result.CombineEmpty(first, second);
        };
    }

    /// <summary>
    /// Construct a parser that takes the result of <paramref name="parser"/> and converts it value using <paramref name="selector"/>.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="selector">A mapping from the first result to the second.</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, U> Select<TKind, T, U>(this TokenListParser<TKind, T> parser, Func<T, U> selector)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return input =>
        {
            var rt = parser(input);
            if (!rt.HasValue)
                return TokenListParserResult.CastEmpty<TKind, T, U>(rt);

            var u = selector(rt.Value);

            return TokenListParserResult.Value(u, input, rt.Remainder);
        };
    }

    /// <summary>
    /// Construct a parser that takes the result of <paramref name="parser"/> and converts it value using <paramref name="selector"/>.
    /// </summary>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="selector">A mapping from the first result to the second.</param>
    /// <returns>The resulting parser.</returns>
    public static TextParser<U> Select<T, U>(this TextParser<T> parser, Func<T, U> selector)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return input =>
        {
            var rt = parser(input);
            if (!rt.HasValue)
                return Result.CastEmpty<T, U>(rt);

            var u = selector(rt.Value);

            return Result.Value(u, input, rt.Remainder);
        };
    }

    /// <summary>
    /// Construct a parser that takes the result of <paramref name="parser"/> and casts it to <typeparamref name="U"/>.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, U> Cast<TKind, T, U>(this TokenListParser<TKind, T> parser)
        where T: U
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return parser.Select(rt => (U)rt);
    }

    /// <summary>
    /// The LINQ query comprehension pattern.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="selector">A mapping from the first result to the second parser.</param>
    /// <param name="projector">Function mapping the results of the first two parsers onto the final result.</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, V> SelectMany<TKind, T, U, V>(
        this TokenListParser<TKind, T> parser,
        Func<T, TokenListParser<TKind, U>> selector,
        Func<T, U, V> projector)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (projector == null) throw new ArgumentNullException(nameof(projector));

        return parser.Then(t => selector(t).Select(u => projector(t, u)));
    }

    /// <summary>
    /// Construct a parser that applies <paramref name="first"/>, provides the value to <paramref name="second"/> and returns the result.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="first">The first parser.</param>
    /// <param name="second">The second parser.</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, U> Then<TKind, T, U>(this TokenListParser<TKind, T> first, Func<T, TokenListParser<TKind, U>> second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));

        return input =>
        {
            var rt = first(input);
            if (!rt.HasValue)
                return TokenListParserResult.CastEmpty<TKind, T, U>(rt);

            var ru = second(rt.Value)(rt.Remainder);
            if (!ru.HasValue)
                return ru;

            return TokenListParserResult.Value(ru.Value, input, ru.Remainder);
        };
    }

    /// <summary>
    /// Construct a parser that applies <paramref name="first"/>, provides the value to <paramref name="second"/> and returns the result.
    /// </summary>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="first">The first parser.</param>
    /// <param name="second">The second parser.</param>
    /// <returns>The resulting parser.</returns>
    public static TextParser<U> Then<T, U>(this TextParser<T> first, Func<T, TextParser<U>> second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));

        return input =>
        {
            var rt = first(input);
            if (!rt.HasValue)
                return Result.CastEmpty<T, U>(rt);

            var ru = second(rt.Value)(rt.Remainder);
            if (!ru.HasValue)
                return ru;

            return Result.Value(ru.Value, input, ru.Remainder);
        };
    }

    /// <summary>
    /// Construct a parser that tries one parser, and backtracks if unsuccessful so that no input
    /// appears to have been consumed by subsequent checks against the result.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, T> Try<TKind, T>(this TokenListParser<TKind, T> parser)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return input =>
        {
            var rt = parser(input);
            if (rt.HasValue)
                return rt;

            rt.Backtrack = true;
            return rt;
        };
    }

    /// <summary>
    /// Construct a parser that tries one parser, and backtracks if unsuccessful so that no input
    /// appears to have been consumed by subsequent checks against the result.
    /// </summary>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <returns>The resulting parser.</returns>
    public static TextParser<T> Try<T>(this TextParser<T> parser)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return input =>
        {
            var rt = parser(input);
            if (rt.HasValue)
                return rt;

            rt.Backtrack = true;
            return rt;
        };
    }

    /// <summary>
    /// Construct a parser that applies the first, and returns <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="value">The value to return.</param>
    /// <returns>The resulting parser.</returns>
    public static TokenListParser<TKind, U> Value<TKind, T, U>(this TokenListParser<TKind, T> parser, U value)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return parser.IgnoreThen(Parse.Return<TKind, U>(value));
    }

    /// <summary>
    /// Construct a parser that applies the first, and returns <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">The type of value being parsed.</typeparam>
    /// <typeparam name="U">The type of the resulting value.</typeparam>
    /// <param name="parser">The parser.</param>
    /// <param name="value">The value to return.</param>
    /// <returns>The resulting parser.</returns>
    public static TextParser<U> Value<T, U>(this TextParser<T> parser, U value)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));

        return parser.IgnoreThen(Parse.Return(value));
    }

    /// <summary>
    /// Parse a sequence of operands connected by left-associative operators.
    /// </summary>
    /// <typeparam name="TKind">The kind of the tokens being parsed.</typeparam>
    /// <typeparam name="TResult">The type of the leftmost operand and of the ultimate result.</typeparam>
    /// <typeparam name="TOperator">The type of the operator.</typeparam>
    /// <typeparam name="TOperand">The type of subsequent operands.</typeparam>
    /// <param name="parser">The parser for the leftmost operand.</param>
    /// <param name="operator">A parser matching operators.</param>
    /// <param name="operand">A parser matching operands.</param>
    /// <param name="apply">A function combining the operator, left operand, and right operand, into the result.</param>
    /// <returns>The result of calling <paramref name="apply"/> successively on pairs of operands.</returns>
    public static TokenListParser<TKind, TResult> Chain<TKind, TResult, TOperator, TOperand>(
        this TokenListParser<TKind, TResult> parser,
        TokenListParser<TKind, TOperator> @operator,
        TokenListParser<TKind, TOperand> operand,
        Func<TOperator, TResult, TOperand, TResult> apply)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        if (@operator == null) throw new ArgumentNullException(nameof(@operator));
        if (operand == null) throw new ArgumentNullException(nameof(operand));
        if (apply == null) throw new ArgumentNullException(nameof(apply));

        return input =>
        {
            var parseResult = parser(input);
            if (!parseResult.HasValue )
                return parseResult;

            var result = parseResult.Value;
            var operandRemainder = parseResult.Remainder;

            var operatorResult = @operator(operandRemainder);
            while (operatorResult.HasValue || operatorResult.IsPartial(operandRemainder))
            {
                // If operator read any input, but failed to read complete input, we return error
                if (!operatorResult.HasValue)
                    return TokenListParserResult.CastEmpty<TKind, TOperator, TResult>(operatorResult);

                var operandResult = operand(operatorResult.Remainder);
                operandRemainder = operandResult.Remainder;

                if (!operandResult.HasValue)
                    return TokenListParserResult.CastEmpty<TKind, TOperand, TResult>(operandResult);

                result = apply(operatorResult.Value, result, operandResult.Value);

                operatorResult = @operator(operandRemainder);
            }

            return TokenListParserResult.Value(result, input, operandRemainder);
        };
    }
}