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
/// A token.
/// </summary>
/// <typeparam name="TKind">The type of the token's kind.</typeparam>
readonly struct Token<TKind>
{
    /// <summary>
    /// The kind of the token.
    /// </summary>
    public TKind Kind { get; }

    /// <summary>
    /// The string span containing the value of the token.
    /// </summary>
    public TextSpan Span { get; }

    /// <summary>
    /// Get the string value of the token.
    /// </summary>
    /// <returns>The token as a string.</returns>
    public string ToStringValue() => Span.ToStringValue();

    /// <summary>
    /// The position of the token within the source string.
    /// </summary>
    public Position Position => Span.Position;

    /// <summary>
    /// True if the token has a value.
    /// </summary>
    bool HasValue => Span != TextSpan.None;

    /// <summary>
    /// Construct a token.
    /// </summary>
    /// <param name="kind">The kind of the token.</param>
    /// <param name="span">The span holding the token's value.</param>
    public Token(TKind kind, TextSpan span)
    {
        Kind = kind;
        Span = span;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        if (!HasValue)
            return "(empty token)";

        return $"{Kind}@{Position}: {Span}";
    }
}