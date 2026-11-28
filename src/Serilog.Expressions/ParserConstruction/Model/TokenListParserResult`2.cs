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
using Serilog.ParserConstruction.Util;

namespace Serilog.ParserConstruction.Model;

/// <summary>
/// The result of parsing from a token list.
/// </summary>
/// <typeparam name="T">The type of the value being parsed.</typeparam>
/// <typeparam name="TKind">The kind of token being parsed.</typeparam>
struct TokenListParserResult<TKind, T>
{
    readonly T _value;

    /// <summary>
    /// If the result has a value, this carries the location of the value in the token
    /// list. If the result is an error, it's the location of the error.
    /// </summary>
    public TokenList<TKind> Location { get; }

    /// <summary>
    /// The first un-parsed location in the list.
    /// </summary>
    public TokenList<TKind> Remainder { get; }

    /// <summary>
    /// True if the result carries a successfully-parsed value; otherwise, false.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// If the result is an error, the source-level position of the error; otherwise, <see cref="Position.Empty"/>.
    /// </summary>
    public Position ErrorPosition
    {
        get
        {
            if (HasValue)
                return Position.Empty;

            if (SubTokenErrorPosition.HasValue)
                return SubTokenErrorPosition;

            if (!Remainder.IsAtEnd)
                return Remainder.ConsumeToken().Value.Position;

            return Location.ComputeEndOfInputPosition();
        }
    }

    /// <summary>
    /// If the result is an error, the source-level position of the error; otherwise, <see cref="Position.Empty"/>.
    /// </summary>
    public Position SubTokenErrorPosition { get; }

    /// <summary>
    /// A provided error message, or null.
    /// </summary>
    public string? ErrorMessage { get; }

    /// <summary>
    /// A list of expectations that were unmet, or null.
    /// </summary>
    public string[]? Expectations { get; }

    /// <summary>
    /// The parsed value.
    /// </summary>
    public T Value
    {
        get
        {
            if (!HasValue)
                throw new InvalidOperationException($"{nameof(TokenListParserResult)} has no value.");
            return _value;
        }
    }

    internal bool IsPartial(TokenList<TKind> from) => SubTokenErrorPosition.HasValue || from != Remainder;

    internal bool Backtrack { get; set; }

    internal TokenListParserResult(T value, TokenList<TKind> location, TokenList<TKind> remainder, bool backtrack)
    {
        Location = location;
        Remainder = remainder;
        _value = value;
        HasValue = true;
        SubTokenErrorPosition = Position.Empty;
        ErrorMessage = null;
        Expectations = null;
        Backtrack = backtrack;
    }

    internal TokenListParserResult(TokenList<TKind> location, TokenList<TKind> remainder, Position errorPosition, string? errorMessage, string[]? expectations, bool backtrack)
    {
        Location = location;
        Remainder = remainder;
        _value = default!; // Default value is not observable.
        HasValue = false;
        SubTokenErrorPosition = errorPosition;
        ErrorMessage = errorMessage;
        Expectations = expectations;
        Backtrack = backtrack;
    }

    internal TokenListParserResult(TokenList<TKind> remainder, Position errorPosition, string? errorMessage, string[]? expectations, bool backtrack)
    {
        Location = Remainder = remainder;
        _value = default!; // Default value is not observable.
        HasValue = false;
        SubTokenErrorPosition = errorPosition;
        ErrorMessage = errorMessage;
        Expectations = expectations;
        Backtrack = backtrack;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (Remainder == TokenList<TKind>.Empty)
            return "(Empty result.)";

        if (HasValue)
            return $"Successful parsing of {Value}.";

        var message = FormatErrorMessageFragment();
        var location = "";
        if (!Remainder.IsAtEnd)
        {
            // Since the message notes `end of input`, don't report line/column here.
            var sourcePosition = SubTokenErrorPosition.HasValue ? SubTokenErrorPosition : Remainder.ConsumeToken().Value.Position;
            location = $" (line {sourcePosition.Line}, column {sourcePosition.Column})";
        }

        return $"Syntax error{location}: {message}.";
    }

    /// <summary>
    /// If the result is empty, format the fragment of text describing the error.
    /// </summary>
    /// <returns>The error fragment.</returns>
    string FormatErrorMessageFragment()
    {
        if (ErrorMessage != null)
            return ErrorMessage;

        string message;
        if (Remainder.IsAtEnd)
        {
            message = "unexpected end of input";
        }
        else
        {
            var next = Remainder.ConsumeToken().Value;
            var appearance = Presentation.FormatAppearance(next.Kind, next.ToStringValue());
            message = $"unexpected {appearance}";
        }

        if (Expectations != null)
        {
            var expected = Friendly.List(Expectations);
            message += $", expected {expected}";
        }

        return message;
    }
}