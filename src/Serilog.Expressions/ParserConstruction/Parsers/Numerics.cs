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
using Serilog.ParserConstruction.Util;

namespace Serilog.ParserConstruction.Parsers;

/// <summary>
/// Parsers for numeric patterns.
/// </summary>
//* Fairly large amount of duplication/repetition here, due to the lack
//* of generics over numbers in C#.
static class Numerics
{
    static readonly string[] ExpectedDigit = ["digit"];
    static readonly string[] ExpectedSignOrDigit = ["sign", "digit"];

    /// <summary>
    /// A string of digits, converted into a <see cref="uint"/>.
    /// </summary>
    public static TextParser<uint> NaturalUInt32 { get; } = input =>
    {
        var next = input.ConsumeChar();

        if (!next.HasValue || !CharInfo.IsLatinDigit(next.Value))
            return Result.Empty<uint>(input, ExpectedDigit);

        TextSpan remainder;
        var val = 0u;
        do
        {
            val = 10 * val + (uint)(next.Value - '0');
            remainder = next.Remainder;
            next = remainder.ConsumeChar();
        } while (next.HasValue && CharInfo.IsLatinDigit(next.Value));

        return Result.Value(val, input, remainder);
    };

    /// <summary>
    /// A string of digits with an optional +/- sign.
    /// </summary>
    public static TextParser<TextSpan> Integer { get; } = input =>
    {
        var next = input.ConsumeChar();

        if (!next.HasValue)
            return Result.Empty<TextSpan>(input, ExpectedSignOrDigit);

        if (next.Value is '-' or '+')
            next = next.Remainder.ConsumeChar();

        if (!next.HasValue || !CharInfo.IsLatinDigit(next.Value))
            return Result.Empty<TextSpan>(input, ExpectedDigit);

        TextSpan remainder;
        do
        {
            remainder = next.Remainder;
            next = remainder.ConsumeChar();
        } while (next.HasValue && CharInfo.IsLatinDigit(next.Value));

        return Result.Value(input.Until(remainder), input, remainder);
    };
}