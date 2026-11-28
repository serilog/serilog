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
/// A span of text within a larger string.
/// </summary>
readonly struct TextSpan : IEquatable<TextSpan>
{
    /// <summary>
    /// The source string containing the span.
    /// </summary>
    public string? Source { get; }

    /// <summary>
    /// The position of the start of the span within the string.
    /// </summary>
    public Position Position { get; }

    /// <summary>
    /// The length of the span.
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// Construct a span encompassing an entire string.
    /// </summary>
    /// <param name="source">The source string.</param>
    public TextSpan(string source)
        : this(source, Position.Zero, source.Length)
    {
    }

    /// <summary>
    /// Construct a string span for a substring of <paramref name="source"/>.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="position">The start of the span.</param>
    /// <param name="length">The length of the span.</param>
    public TextSpan(string source, Position position, int length)
    {
#if CHECKED
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "The length must be non-negative.");
            if (source.Length < position.Absolute + length)
                throw new ArgumentOutOfRangeException(nameof(length), "The token extends beyond the end of the input.");
#endif

        Source = source;
        Position = position;
        Length = length;
    }

    /// <summary>
    /// A span with no value.
    /// </summary>
    public static TextSpan None => default;

    /// <summary>
    /// True if the span has no content.
    /// </summary>
    public bool IsAtEnd
    {
        get
        {
            EnsureHasValue();
            return Length == 0;
        }
    }

    void EnsureHasValue()
    {
        if (Source == null)
            throw new InvalidOperationException("String span has no value.");
    }

    /// <summary>
    /// Consume a character from the start of the span.
    /// </summary>
    /// <returns>A result with the character and remainder.</returns>
    public Result<char> ConsumeChar()
    {
        EnsureHasValue();

        if (IsAtEnd)
            return Result.Empty<char>(this);

        var ch = Source![Position.Absolute];
        return Result.Value(ch, this, new(Source, Position.Advance(ch), Length - 1));
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is not TextSpan other)
            return false;

        return Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            return ((Source?.GetHashCode() ?? 0) * 397) ^ Position.Absolute;
        }
    }

    /// <summary>
    /// Compare a string span with another using source identity
    /// semantics - same source, same position, same length.
    /// </summary>
    /// <param name="other">The other span.</param>
    /// <returns>True if the spans are the same.</returns>
    public bool Equals(TextSpan other)
    {
        return ReferenceEquals(Source, other.Source) &&
               Position.Absolute == other.Position.Absolute &&
               Length == other.Length;
    }

    /// <summary>
    /// Compare two spans using source identity semantics.
    /// </summary>
    /// <param name="lhs">One span.</param>
    /// <param name="rhs">Another span.</param>
    /// <returns>True if the spans are the same.</returns>
    public static bool operator ==(TextSpan lhs, TextSpan rhs)
    {
        return lhs.Equals(rhs);
    }

    /// <summary>
    /// Compare two spans using source identity semantics.
    /// </summary>
    /// <param name="lhs">One span.</param>
    /// <param name="rhs">Another span.</param>
    /// <returns>True if the spans are the different.</returns>
    public static bool operator !=(TextSpan lhs, TextSpan rhs)
    {
        return !(lhs == rhs);
    }

    /// <summary>
    /// Return a new span from the start of this span to the beginning of another.
    /// </summary>
    /// <param name="next">The next span.</param>
    /// <returns>A sub-span.</returns>
    public TextSpan Until(TextSpan next)
    {
#if CHECKED
            next.EnsureHasValue();
            if (next.Source != Source) throw new ArgumentException("The spans are on different source strings.", nameof(next));
#endif
        var charCount = next.Position.Absolute - Position.Absolute;
        return First(charCount);
    }

    /// <summary>
    /// Return a span comprising the first <paramref name="length"/> characters of this span.
    /// </summary>
    /// <param name="length">The number of characters to return.</param>
    /// <returns>The sub-span.</returns>
    TextSpan First(int length)
    {
#if CHECKED
            if (length > Length)
                throw new ArgumentOutOfRangeException(nameof(length), "Length exceeds the source span's length.");
#endif

        return new(Source!, Position, length);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        if (Source == null)
            return "(empty source span)";

        return ToStringValue();
    }

    /// <summary>
    /// Compute the string value of this span.
    /// </summary>
    /// <returns>A string with the value of this span.</returns>
    public string ToStringValue()
    {
        EnsureHasValue();
        return Source!.Substring(Position.Absolute, Length);
    }

    /// <summary>
    /// Compare the contents of this span with <paramref name="otherValue"/>, ignoring invariant character case.
    /// </summary>
    /// <param name="otherValue">The string value to compare.</param>
    /// <returns>True if the values are the same ignoring case.</returns>
    public bool EqualsValueIgnoreCase(string otherValue)
    {
        if (otherValue == null) throw new ArgumentNullException(nameof(otherValue));
        EnsureHasValue();
        if (Length != otherValue.Length)
            return false;
        for (var i = 0; i < Length; ++i)
        {
            if (char.ToUpperInvariant(Source![Position.Absolute + i]) != char.ToUpperInvariant(otherValue[i]))
                return false;
        }
        return true;
    }
}