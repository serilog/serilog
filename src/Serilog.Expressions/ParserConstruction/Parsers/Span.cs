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
/// Parsers for spans of characters.
/// </summary>
static class Span
{
    /// <summary>
    /// Match a span equal to <paramref name="text"/>.
    /// </summary>
    /// <param name="text">The text to match.</param>
    /// <returns>The matched text.</returns>
    public static TextParser<TextSpan> EqualTo(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        var expectations = new[] { Presentation.FormatLiteral(text) };
        return input =>
        {
            var remainder = input;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < text.Length; ++i)
            {
                var ch = remainder.ConsumeChar();
                if (!ch.HasValue || ch.Value != text[i])
                {
                    if (ch.Location == input)
                        return Result.Empty<TextSpan>(ch.Location, expectations);

                    return Result.Empty<TextSpan>(ch.Location, [Presentation.FormatLiteral(text[i])]);
                }
                remainder = ch.Remainder;
            }
            return Result.Value(input.Until(remainder), input, remainder);
        };
    }
}