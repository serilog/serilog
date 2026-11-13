// Copyright © Serilog Contributors
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

using Serilog.ParserConstruction;
using Serilog.ParserConstruction.Model;
using Serilog.ParserConstruction.Parsers;

namespace Serilog.Expressions.Parsing;

static class ExpressionTextParsers
{
    static readonly TextParser<ExpressionToken> LessOrEqual = Span.EqualTo("<=").Value(ExpressionToken.LessThanOrEqual);
    static readonly TextParser<ExpressionToken> GreaterOrEqual = Span.EqualTo(">=").Value(ExpressionToken.GreaterThanOrEqual);
    static readonly TextParser<ExpressionToken> NotEqual = Span.EqualTo("<>").Value(ExpressionToken.NotEqual);
    static readonly TextParser<ExpressionToken> Spread = Span.EqualTo("..").Value(ExpressionToken.Spread);

    public static readonly TextParser<ExpressionToken> CompoundOperator = GreaterOrEqual.Or(LessOrEqual.Try().Or(NotEqual)).Or(Spread);

    public static readonly TextParser<string> HexInteger =
        Span.EqualTo("0x")
            .IgnoreThen(Character.Digit.Or(Character.Matching(ch => ch is >= 'a' and <= 'f' or >= 'A' and <= 'F', "a-f"))
                .Named("hex digit")
                .AtLeastOnce())
            .Select(chars => new string(chars));

    static readonly TextParser<char> StringContentChar =
        Span.EqualTo("''").Value('\'').Try().Or(Character.Except('\''));

    public static readonly TextParser<string> String =
        Character.EqualTo('\'')
            .IgnoreThen(StringContentChar.Many())
            .Then(s => Character.EqualTo('\'').Value(new string(s)));

    public static readonly TextParser<TextSpan> Real =
        Numerics.Integer
            .Then(n => Character.EqualTo('.').IgnoreThen(Numerics.Integer).OptionalOrDefault()
                .Select(f => f == TextSpan.None ? n : new(n.Source!, n.Position, n.Length + f.Length + 1)));
}