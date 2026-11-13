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

using Serilog.ParserConstruction.Display;

namespace Serilog.Expressions.Parsing;

enum ExpressionToken
{
    None,

    Identifier,

    [Token(Description = "built-in identifier")]
    BuiltInIdentifier,

    String,

    Number,

    [Token(Description = "hexadecimal number")]
    HexNumber,

    [Token(Example = ",")]
    Comma,

    [Token(Example = ".")]
    Period,

    [Token(Example = "..")]
    Spread,

    [Token(Example = "[")]
    LBracket,

    [Token(Example = "]")]
    RBracket,

    [Token(Example = "(")]
    LParen,

    [Token(Example = ")")]
    RParen,

    [Token(Example = "{")]
    LBrace,

    [Token(Example = "}")]
    RBrace,

    [Token(Example = ":")]
    Colon,

    [Token(Example = "?")]
    QuestionMark,

    [Token(Category = "operator", Example = "+")]
    Plus,

    [Token(Category = "operator", Example = "-")]
    Minus,

    [Token(Example = "*")]
    Asterisk,

    [Token(Category = "operator", Example = "/")]
    ForwardSlash,

    [Token(Category = "operator", Example = "%")]
    Percent,

    [Token(Category = "operator", Example = "^")]
    Caret,

    [Token(Category = "operator", Example = "<")]
    LessThan,

    [Token(Category = "operator", Example = "<=")]
    LessThanOrEqual,

    [Token(Category = "operator", Example = ">")]
    GreaterThan,

    [Token(Category = "operator", Example = ">=")]
    GreaterThanOrEqual,

    [Token(Category = "operator", Example = "=")]
    Equal,

    [Token(Category = "operator", Example = "<>")]
    NotEqual,

    [Token(Category = "keyword", Example = "and")]
    And,

    [Token(Category = "keyword", Example = "in")]
    In,

    [Token(Category = "keyword", Example = "is")]
    Is,

    [Token(Category = "keyword", Example = "like")]
    Like,

    [Token(Category = "keyword", Example = "not")]
    Not,

    [Token(Category = "keyword", Example = "or")]
    Or,

    [Token(Category = "keyword", Example = "true")]
    True,

    [Token(Category = "keyword", Example = "false")]
    False,

    [Token(Category = "keyword", Example = "null")]
    Null,

    [Token(Category = "keyword", Example = "if")]
    If,

    [Token(Category = "keyword", Example = "then")]
    Then,

    [Token(Category = "keyword", Example = "else")]
    Else,

    [Token(Category = "keyword", Example = "ci")]
    CI,

    // Template syntax

    [Token(Description = "text")]
    Text,

    [Token(Example = "{{")]
    DoubleLBrace,

    [Token(Example = "}}")]
    DoubleRBrace,

    [Token(Example = "{#")]
    LBraceHash,

    [Token(Description = "format specifier")]
    Format,

    [Token(Category = "keyword", Example = "end")]
    End,

    [Token(Category = "keyword", Example = "each")]
    Each,

    [Token(Category = "keyword", Example = "delimit")]
    Delimit,
}