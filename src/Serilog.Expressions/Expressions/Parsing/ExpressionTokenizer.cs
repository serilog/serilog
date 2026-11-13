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

namespace Serilog.Expressions.Parsing;

class ExpressionTokenizer : Tokenizer<ExpressionToken>
{
    readonly ExpressionToken[] _singleCharOps = new ExpressionToken[128];

    readonly ExpressionKeyword[] _keywords =
    [
        new("and", ExpressionToken.And),
        new("in", ExpressionToken.In),
        new("is", ExpressionToken.Is),
        new("like", ExpressionToken.Like),
        new("not", ExpressionToken.Not),
        new("or", ExpressionToken.Or),
        new("true", ExpressionToken.True),
        new("false", ExpressionToken.False),
        new("null", ExpressionToken.Null),
        new("if", ExpressionToken.If),
        new("then", ExpressionToken.Then),
        new("else", ExpressionToken.Else),
        new("end", ExpressionToken.End),
        new("ci", ExpressionToken.CI),
        new("each", ExpressionToken.Each),
        new("delimit", ExpressionToken.Delimit)
    ];

    public ExpressionTokenizer()
    {
        _singleCharOps['+'] = ExpressionToken.Plus;
        _singleCharOps['-'] = ExpressionToken.Minus;
        _singleCharOps['*'] = ExpressionToken.Asterisk;
        _singleCharOps['/'] = ExpressionToken.ForwardSlash;
        _singleCharOps['%'] = ExpressionToken.Percent;
        _singleCharOps['^'] = ExpressionToken.Caret;
        _singleCharOps['<'] = ExpressionToken.LessThan;
        _singleCharOps['>'] = ExpressionToken.GreaterThan;
        _singleCharOps['='] = ExpressionToken.Equal;
        _singleCharOps[','] = ExpressionToken.Comma;
        _singleCharOps['.'] = ExpressionToken.Period;
        _singleCharOps['('] = ExpressionToken.LParen;
        _singleCharOps[')'] = ExpressionToken.RParen;
        _singleCharOps['{'] = ExpressionToken.LBrace;
        _singleCharOps['}'] = ExpressionToken.RBrace;
        _singleCharOps[':'] = ExpressionToken.Colon;
        _singleCharOps['['] = ExpressionToken.LBracket;
        _singleCharOps[']'] = ExpressionToken.RBracket;
        _singleCharOps['*'] = ExpressionToken.Asterisk;
        _singleCharOps['?'] = ExpressionToken.QuestionMark;
    }

    public TokenList<ExpressionToken> GreedyTokenize(TextSpan textSpan)
    {
        // Dropping error info off for now
        return new(
            Tokenize(textSpan)
                .TakeWhile(r => r.HasValue)
                .Select(r => new Token<ExpressionToken>(r.Value, r.Location.Until(r.Remainder)))
                .ToArray());
    }

    public IEnumerable<Result<ExpressionToken>> LazyTokenize(TextSpan span)
    {
        return Tokenize(span);
    }

    protected override IEnumerable<Result<ExpressionToken>> Tokenize(TextSpan stringSpan)
    {
        var next = SkipWhiteSpace(stringSpan);
        if (!next.HasValue)
            yield break;

        do
        {
            if (char.IsDigit(next.Value))
            {
                var hex = ExpressionTextParsers.HexInteger(next.Location);
                if (hex.HasValue)
                {
                    next = hex.Remainder.ConsumeChar();
                    yield return Result.Value(ExpressionToken.HexNumber, hex.Location, hex.Remainder);
                }
                else
                {
                    var real = ExpressionTextParsers.Real(next.Location);
                    if (!real.HasValue)
                        yield return Result.CastEmpty<TextSpan, ExpressionToken>(real);
                    else
                        yield return Result.Value(ExpressionToken.Number, real.Location, real.Remainder);

                    next = real.Remainder.ConsumeChar();
                }

                if (!IsDelimiter(next))
                {
                    yield return Result.Empty<ExpressionToken>(next.Location, ["digit"]);
                }
            }
            else if (next.Value == '\'')
            {
                var str = ExpressionTextParsers.String(next.Location);
                if (!str.HasValue)
                    yield return Result.CastEmpty<string, ExpressionToken>(str);

                next = str.Remainder.ConsumeChar();

                yield return Result.Value(ExpressionToken.String, str.Location, str.Remainder);
            }
            else if (next.Value == '@')
            {
                var beginIdentifier = next.Location;
                var startOfName = next.Remainder;
                do
                {
                    next = next.Remainder.ConsumeChar();
                }
                while (next.HasValue && char.IsLetterOrDigit(next.Value));

                if (next.Remainder == startOfName)
                {
                    yield return Result.Empty<ExpressionToken>(startOfName, ["built-in identifier name"]);
                }
                else
                {
                    yield return Result.Value(ExpressionToken.BuiltInIdentifier, beginIdentifier, next.Location);
                }
            }
            else if (char.IsLetter(next.Value) || next.Value == '_')
            {
                var beginIdentifier = next.Location;
                do
                {
                    next = next.Remainder.ConsumeChar();
                }
                while (next.HasValue && (char.IsLetterOrDigit(next.Value) || next.Value == '_'));

                if (TryGetKeyword(beginIdentifier.Until(next.Location), out var keyword))
                {
                    yield return Result.Value(keyword, beginIdentifier, next.Location);
                }
                else
                {
                    yield return Result.Value(ExpressionToken.Identifier, beginIdentifier, next.Location);
                }
            }
            else
            {
                var compoundOp = ExpressionTextParsers.CompoundOperator(next.Location);
                if (compoundOp.HasValue)
                {
                    yield return Result.Value(compoundOp.Value, compoundOp.Location, compoundOp.Remainder);
                    next = compoundOp.Remainder.ConsumeChar();
                }
                else if (next.Value < _singleCharOps.Length && _singleCharOps[next.Value] != ExpressionToken.None)
                {
                    yield return Result.Value(_singleCharOps[next.Value], next.Location, next.Remainder);
                    next = next.Remainder.ConsumeChar();
                }
                else
                {
                    yield return Result.Empty<ExpressionToken>(next.Location);
                    next = next.Remainder.ConsumeChar();
                }
            }

            next = SkipWhiteSpace(next.Location);
        } while (next.HasValue);
    }

    bool IsDelimiter(Result<char> next)
    {
        return !next.HasValue ||
               char.IsWhiteSpace(next.Value) ||
               next.Value < _singleCharOps.Length && _singleCharOps[next.Value] != ExpressionToken.None;
    }

    bool TryGetKeyword(TextSpan span, out ExpressionToken keyword)
    {
        foreach (var kw in _keywords)
        {
            if (span.EqualsValueIgnoreCase(kw.Text))
            {
                keyword = kw.Token;
                return true;
            }
        }

        keyword = ExpressionToken.None;
        return false;
    }
}