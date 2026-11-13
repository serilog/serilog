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

using System.Globalization;
using Serilog.Events;
using Serilog.Expressions.Ast;
using Serilog.ParserConstruction;
using Serilog.ParserConstruction.Model;
using Serilog.ParserConstruction.Parsers;

namespace Serilog.Expressions.Parsing;

static class ExpressionTokenParsers
{
    public static TokenListParserResult<ExpressionToken, Expression> TryParse(
        TokenList<ExpressionToken> input)
    {
        return Expr.AtEnd().TryParse(input);
    }

    public static TokenListParserResult<ExpressionToken, Expression> TryPartialParse(
        TokenList<ExpressionToken> input)
    {
        return Expr.TryParse(input);
    }

    static readonly TokenListParser<ExpressionToken, string> Add = Token.EqualTo(ExpressionToken.Plus).Value(Operators.RuntimeOpAdd);
    static readonly TokenListParser<ExpressionToken, string> Subtract = Token.EqualTo(ExpressionToken.Minus).Value(Operators.RuntimeOpSubtract);
    static readonly TokenListParser<ExpressionToken, string> Multiply = Token.EqualTo(ExpressionToken.Asterisk).Value(Operators.RuntimeOpMultiply);
    static readonly TokenListParser<ExpressionToken, string> Divide = Token.EqualTo(ExpressionToken.ForwardSlash).Value(Operators.RuntimeOpDivide);
    static readonly TokenListParser<ExpressionToken, string> Modulo = Token.EqualTo(ExpressionToken.Percent).Value(Operators.RuntimeOpModulo);
    static readonly TokenListParser<ExpressionToken, string> Power = Token.EqualTo(ExpressionToken.Caret).Value(Operators.RuntimeOpPower);
    static readonly TokenListParser<ExpressionToken, string> And = Token.EqualTo(ExpressionToken.And).Value(Operators.RuntimeOpAnd);
    static readonly TokenListParser<ExpressionToken, string> Or = Token.EqualTo(ExpressionToken.Or).Value(Operators.RuntimeOpOr);
    static readonly TokenListParser<ExpressionToken, string> Lte = Token.EqualTo(ExpressionToken.LessThanOrEqual).Value(Operators.RuntimeOpLessThanOrEqual);
    static readonly TokenListParser<ExpressionToken, string> Lt = Token.EqualTo(ExpressionToken.LessThan).Value(Operators.RuntimeOpLessThan);
    static readonly TokenListParser<ExpressionToken, string> Gt = Token.EqualTo(ExpressionToken.GreaterThan).Value(Operators.RuntimeOpGreaterThan);
    static readonly TokenListParser<ExpressionToken, string> Gte = Token.EqualTo(ExpressionToken.GreaterThanOrEqual).Value(Operators.RuntimeOpGreaterThanOrEqual);
    static readonly TokenListParser<ExpressionToken, string> Eq = Token.EqualTo(ExpressionToken.Equal).Value(Operators.RuntimeOpEqual);
    static readonly TokenListParser<ExpressionToken, string> Neq = Token.EqualTo(ExpressionToken.NotEqual).Value(Operators.RuntimeOpNotEqual);
    static readonly TokenListParser<ExpressionToken, string> Negate = Token.EqualTo(ExpressionToken.Minus).Value(Operators.RuntimeOpNegate);
    static readonly TokenListParser<ExpressionToken, string> Not = Token.EqualTo(ExpressionToken.Not).Value(Operators.RuntimeOpNot);

    static readonly TokenListParser<ExpressionToken, string> Like = Token.EqualTo(ExpressionToken.Like).Value(Operators.IntermediateOpLike);

    static readonly TokenListParser<ExpressionToken, string> NotLike =
        Token.EqualTo(ExpressionToken.Not)
            .IgnoreThen(Token.EqualTo(ExpressionToken.Like))
            .Value(Operators.IntermediateOpNotLike);

    static readonly TokenListParser<ExpressionToken, string> In = Token.EqualTo(ExpressionToken.In).Value(Operators.RuntimeOpIn);

    static readonly TokenListParser<ExpressionToken, string> NotIn =
        Token.EqualTo(ExpressionToken.Not)
            .IgnoreThen(Token.EqualTo(ExpressionToken.In))
            .Value(Operators.RuntimeOpNotIn);

    static readonly TokenListParser<ExpressionToken, Func<Expression, Expression>> PropertyPathStep =
        Token.EqualTo(ExpressionToken.Period)
            .IgnoreThen(Token.EqualTo(ExpressionToken.Identifier))
            .Then(n => Parse.Return<ExpressionToken, Func<Expression, Expression>>(r => new AccessorExpression(r, n.ToStringValue())));

    static readonly TokenListParser<ExpressionToken, Expression> Wildcard =
        Token.EqualTo(ExpressionToken.QuestionMark).Value((Expression)new IndexerWildcardExpression(IndexerWildcard.Any))
            .Or(Token.EqualTo(ExpressionToken.Asterisk).Value((Expression)new IndexerWildcardExpression(IndexerWildcard.All)));

    static readonly TokenListParser<ExpressionToken, Func<Expression, Expression>> PropertyPathIndexerStep =
        from open in Token.EqualTo(ExpressionToken.LBracket)
        from indexer in Wildcard.Or(Parse.Ref(() => Expr!))
        from close in Token.EqualTo(ExpressionToken.RBracket)
        select new Func<Expression, Expression>(r => new IndexerExpression(r, indexer));

    static readonly TokenListParser<ExpressionToken, Expression> Function =
        (from name in Token.EqualTo(ExpressionToken.Identifier)
            from lparen in Token.EqualTo(ExpressionToken.LParen)
            from expr in Parse.Ref(() => Expr!).ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma))
            from rparen in Token.EqualTo(ExpressionToken.RParen)
            from ci in Token.EqualTo(ExpressionToken.CI).Value(true).OptionalOrDefault()
            select (Expression)new CallExpression(ci, name.ToStringValue(), expr)).Named("function");

    static readonly TokenListParser<ExpressionToken, Element> ArrayElement =
        Token.EqualTo(ExpressionToken.Spread)
            .IgnoreThen(Parse.Ref(() => Expr!))
            .Select(content => (Element)new SpreadElement(content))
            .Or(Parse.Ref(() => Expr!).Select(item => (Element) new ItemElement(item)));

    static readonly TokenListParser<ExpressionToken, Expression> ArrayLiteral =
        (from lbracket in Token.EqualTo(ExpressionToken.LBracket)
            from elements in ArrayElement.ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma))
            from rbracket in Token.EqualTo(ExpressionToken.RBracket)
            select (Expression)new ArrayExpression(elements)).Named("array");

    static readonly TokenListParser<ExpressionToken, Member> IdentifierMember =
        from key in Token.EqualTo(ExpressionToken.Identifier).Or(Token.EqualTo(ExpressionToken.BuiltInIdentifier))
        from value in Token.EqualTo(ExpressionToken.Colon)
            .IgnoreThen(Parse.Ref(() => Expr!))
            .Cast<ExpressionToken, Expression, Expression?>()
            .OptionalOrDefault()
        select (Member) new PropertyMember(
            key.ToStringValue(),
            value ?? (key.Kind == ExpressionToken.BuiltInIdentifier ?
                new AmbientNameExpression(key.ToStringValue().Substring(1), true) :
                new AmbientNameExpression(key.ToStringValue(), false)));

    static readonly TokenListParser<ExpressionToken, Member> StringMember =
        from key in Token.EqualTo(ExpressionToken.String).Apply(ExpressionTextParsers.String)
        from colon in Token.EqualTo(ExpressionToken.Colon)
        from value in Parse.Ref(() => Expr!)
        select (Member)new PropertyMember(key, value);

    static readonly TokenListParser<ExpressionToken, Member> SpreadMember =
        from spread in Token.EqualTo(ExpressionToken.Spread)
        from content in Parse.Ref(() => Expr!)
        select (Member) new SpreadMember(content);

    static readonly TokenListParser<ExpressionToken, Member> ObjectMember =
        IdentifierMember.Or(StringMember).Or(SpreadMember).Named("object member");

    static readonly TokenListParser<ExpressionToken, Expression> ObjectLiteral =
        (from lbrace in Token.EqualTo(ExpressionToken.LBrace)
            from members in ObjectMember.ManyDelimitedBy(Token.EqualTo(ExpressionToken.Comma))
            from rbrace in Token.EqualTo(ExpressionToken.RBrace)
            select (Expression)new ObjectExpression(members)).Named("object");

    static readonly TokenListParser<ExpressionToken, Expression> RootProperty =
        (from notFunction in Parse.Not(Token.EqualTo(ExpressionToken.Identifier).IgnoreThen(Token.EqualTo(ExpressionToken.LParen)))
            from p in Token.EqualTo(ExpressionToken.BuiltInIdentifier).Select(b => (Expression) new AmbientNameExpression(b.ToStringValue().Substring(1), true))
                .Or(Token.EqualTo(ExpressionToken.Identifier).Select(t => (Expression) new AmbientNameExpression(t.ToStringValue(), false)))
            select p).Named("property");

    static readonly TokenListParser<ExpressionToken, Expression> String =
        Token.EqualTo(ExpressionToken.String)
            .Apply(ExpressionTextParsers.String)
            .Select(s => (Expression)new ConstantExpression(new ScalarValue(s)));

    static readonly TokenListParser<ExpressionToken, Expression> HexNumber =
        Token.EqualTo(ExpressionToken.HexNumber)
            .Apply(ExpressionTextParsers.HexInteger)
            .SelectCatch(n => ulong.Parse(n, NumberStyles.HexNumber, CultureInfo.InvariantCulture), "the numeric literal is too large")
            .Select(u => (Expression)new ConstantExpression(new ScalarValue((decimal)u)));

    static readonly TokenListParser<ExpressionToken, Expression> Number =
        Token.EqualTo(ExpressionToken.Number)
            .Apply(ExpressionTextParsers.Real)
            .SelectCatch(n => decimal.Parse(n.ToStringValue(), CultureInfo.InvariantCulture), "the numeric literal is too large")
            .Select(d => (Expression)new ConstantExpression(new ScalarValue(d)));

    static readonly TokenListParser<ExpressionToken, Expression> Conditional =
        from _ in Token.EqualTo(ExpressionToken.If)
        from condition in Parse.Ref(() => Expr!)
        from __ in Token.EqualTo(ExpressionToken.Then)
        from consequent in Parse.Ref(() => Expr!)
        from ___ in Token.EqualTo(ExpressionToken.Else)
        from alternative in Parse.Ref(() => Expr!)
        select (Expression)new CallExpression(false, Operators.RuntimeOpIfThenElse, condition, consequent, alternative);

    static readonly TokenListParser<ExpressionToken, Expression> Literal =
        String
            .Or(Number)
            .Or(HexNumber)
            .Or(Token.EqualTo(ExpressionToken.True).Value((Expression)new ConstantExpression(new ScalarValue(true))))
            .Or(Token.EqualTo(ExpressionToken.False).Value((Expression)new ConstantExpression(new ScalarValue(false))))
            .Or(Token.EqualTo(ExpressionToken.Null).Value((Expression)new ConstantExpression(new ScalarValue(null))))
            .Named("literal");

    static readonly TokenListParser<ExpressionToken, Expression> Item =
        Literal
            .Or(RootProperty)
            .Or(Function)
            .Or(ArrayLiteral)
            .Or(ObjectLiteral)
            .Or(Conditional);

    static readonly TokenListParser<ExpressionToken, Expression> Factor =
        (from lparen in Token.EqualTo(ExpressionToken.LParen)
            from expr in Parse.Ref(() => Expr!)
            from rparen in Token.EqualTo(ExpressionToken.RParen)
            select expr)
        .Or(Item);

    static readonly TokenListParser<ExpressionToken, Expression> Path =
        from root in Factor
        from path in PropertyPathStep.Or(PropertyPathIndexerStep).Many()
        select path.Aggregate(root, (o, f) => f(o));

    static readonly TokenListParser<ExpressionToken, Expression> Operand =
        (from op in Negate.Or(Not)
            from path in Path
            select MakeUnary(op, path))
        .Or(Path)
        .Then(operand => Token.EqualTo(ExpressionToken.Is).Try()
            .IgnoreThen(
                Token.EqualTo(ExpressionToken.Null).Value(Operators.RuntimeOpIsNull)
                    .Or(Token.EqualTo(ExpressionToken.Not).IgnoreThen(Token.EqualTo(ExpressionToken.Null)).Value(Operators.RuntimeOpIsNotNull)))
            .Select(op => (Expression)new CallExpression(false, op, operand))
            .OptionalOrDefault(operand))
        .Named("expression");

    static readonly TokenListParser<ExpressionToken, Expression> InnerTerm = Parse.Chain(Power, Operand, MakeBinary);

    static readonly TokenListParser<ExpressionToken, Expression> Term = Parse.Chain(Multiply.Or(Divide).Or(Modulo), InnerTerm, MakeBinary);

    static readonly TokenListParser<ExpressionToken, Expression> Comparand = Parse.Chain(Add.Or(Subtract), Term, MakeBinary);

    static readonly TokenListParser<ExpressionToken, Expression> Comparison = Combinators.ChainModified(
        NotLike.Try().Or(Like)
            .Or(NotIn.Try().Or(In))
            .Or(Lte.Or(Neq).Or(Lt))
            .Or(Gte.Or(Gt))
            .Or(Eq),
        Comparand,
        Token.EqualTo(ExpressionToken.CI).Value(true).OptionalOrDefault(),
        (o, l, r, ci) => new CallExpression(ci, o, l, r));

    static readonly TokenListParser<ExpressionToken, Expression> Conjunction = Parse.Chain(And, Comparison, MakeBinary);

    static readonly TokenListParser<ExpressionToken, Expression> Disjunction = Parse.Chain(Or, Conjunction, MakeBinary);

    public static readonly TokenListParser<ExpressionToken, Expression> Expr = Disjunction;

    static Expression MakeBinary(string operatorName, Expression leftOperand, Expression rightOperand)
    {
        return new CallExpression(false, operatorName, leftOperand, rightOperand);
    }

    static Expression MakeUnary(string operatorName, Expression operand)
    {
        return new CallExpression(false, operatorName, operand);
    }
}