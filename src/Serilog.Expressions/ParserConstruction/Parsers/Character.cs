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
/// Parsers for matching individual characters.
/// </summary>
static class Character
{
    static TextParser<char> Matching(Func<char, bool> predicate, string[] expectations)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        if (expectations == null) throw new ArgumentNullException(nameof(expectations));

        return input =>
        {
            var next = input.ConsumeChar();
            if (!next.HasValue || !predicate(next.Value))
                return Result.Empty<char>(input, expectations);

            return next;
        };
    }

    /// <summary>
    /// Parse a single character matching <paramref name="predicate"/>.
    /// </summary>
    public static TextParser<char> Matching(Func<char, bool> predicate, string name)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        if (name == null) throw new ArgumentNullException(nameof(name));

        return Matching(predicate, [name]);
    }

    /// <summary>
    /// Parse a single character except those matching <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">Characters not to match.</param>
    /// <param name="description">Description of characters that don't match.</param>
    /// <returns>A parser for characters except those matching <paramref name="predicate"/>.</returns>
    static TextParser<char> Except(Func<char, bool> predicate, string description)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        if (description == null) throw new ArgumentNullException(nameof(description));

        return Matching(c => !predicate(c), "any character except " + description);
    }

    /// <summary>
    /// Parse a single specified character.
    /// </summary>
    public static TextParser<char> EqualTo(char ch)
    {
        return Matching(parsed => parsed == ch, Presentation.FormatLiteral(ch));
    }

    /// <summary>
    /// Parse a single character except <paramref name="ch"/>.
    /// </summary>
    public static TextParser<char> Except(char ch)
    {
        return Except(parsed => parsed == ch, Presentation.FormatLiteral(ch));
    }
    /// <summary>
    /// Parse a digit.
    /// </summary>
    public static TextParser<char> Digit { get; } = Matching(char.IsDigit, "digit");
}