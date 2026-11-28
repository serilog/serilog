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

using Serilog.Expressions.Ast;
using Serilog.Expressions.Compilation.Arrays;
using Serilog.Expressions.Compilation.Linq;
using Serilog.Expressions.Compilation.Properties;
using Serilog.Expressions.Compilation.Text;
using Serilog.Expressions.Compilation.Variadics;
using Serilog.Expressions.Compilation.Wildcards;

namespace Serilog.Expressions.Compilation;

static class ExpressionCompiler
{
    public static Expression Translate(Expression expression)
    {
        var actual = expression;
        actual = VariadicCallRewriter.Rewrite(actual);
        actual = TextMatchingTransformer.Rewrite(actual);
        actual = LikeSyntaxTransformer.Rewrite(actual);
        actual = PropertiesObjectAccessorTransformer.Rewrite(actual);
        actual = ConstantArrayEvaluator.Rewrite(actual);
        actual = WildcardComprehensionTransformer.Rewrite(actual);
        return actual;
    }

    public static Evaluatable Compile(Expression expression, IFormatProvider? formatProvider, NameResolver nameResolver)
    {
        var actual = Translate(expression);
        return LinqExpressionCompiler.Compile(actual, formatProvider, nameResolver);
    }
}