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

using System.Text.RegularExpressions;

namespace Serilog.Expressions.Ast;

/// <summary>
/// A non-syntax expression tree node used when compiling the <see cref="Operators.OpIndexOfMatch"/>,
/// <see cref="Operators.OpIsMatch"/>, and SQL-style <code>like</code> expressions.
/// </summary>
class IndexOfMatchExpression : Expression
{
    public Expression Corpus { get; }
    public Regex Regex { get; }

    public IndexOfMatchExpression(Expression corpus, Regex regex)
    {
        Corpus = corpus ?? throw new ArgumentNullException(nameof(corpus));
        Regex = regex ?? throw new ArgumentNullException(nameof(regex));
    }

    public override string ToString()
    {
        return $"_Internal_IndexOfMatch({Corpus}, '{Regex.ToString().Replace("'", "''")}')";
    }
}