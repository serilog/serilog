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
/// A position within a stream of character input.
/// </summary>
readonly struct Position
{
    /// <summary>
    /// The zero-based absolute index of the position.
    /// </summary>
    public int Absolute { get; }

    /// <summary>
    /// The one-based line number.
    /// </summary>
    public int Line { get; }

    /// <summary>
    /// The one-based column number.
    /// </summary>
    public int Column { get; }

    /// <summary>
    /// Construct a position.
    /// </summary>
    /// <param name="absolute">The absolute position.</param>
    /// <param name="line">The line number.</param>
    /// <param name="column">The column number.</param>
    Position(int absolute, int line, int column)
    {
#if CHECKED
            if (absolute < 0) throw new ArgumentOutOfRangeException(nameof(line), "Absolute positions start at 0.");
            if (line < 1) throw new ArgumentOutOfRangeException(nameof(line), "Line numbering starts at 1.");
            if (column < 1) throw new ArgumentOutOfRangeException(nameof(column), "Column numbering starts at 1.");
#endif
        Absolute = absolute;
        Line = line;
        Column = column;
    }

    /// <summary>
    /// The position corresponding to the zero index.
    /// </summary>
    public static Position Zero { get; } = new(0, 1, 1);

    /// <summary>
    /// A position with no value.
    /// </summary>
    public static Position Empty => default;

    /// <summary>
    /// True if the position has a value.
    /// </summary>
    public bool HasValue => Line > 0;

    /// <summary>
    /// Advance over <paramref name="overChar"/>, advancing line and column numbers
    /// as appropriate.
    /// </summary>
    /// <param name="overChar">The character being advanced over.</param>
    /// <returns>The updated position.</returns>
    public Position Advance(char overChar)
    {
        if (overChar == '\n')
            return new(Absolute + 1, Line + 1, 1);

        return new(Absolute + 1, Line, Column + 1);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Absolute} (line {Line}, column {Column})";
    }
}