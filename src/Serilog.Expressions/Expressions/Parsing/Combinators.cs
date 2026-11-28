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

static class Combinators
{
    public static TokenListParser<TKind, TResult> ChainModified<TKind, TResult, TOperator, TModifier>(
        TokenListParser<TKind, TOperator> @operator,
        TokenListParser<TKind, TResult> operand,
        TokenListParser<TKind, TModifier> modify,
        Func<TOperator, TResult, TResult, TModifier, TResult> apply)
    {
        if (@operator == null)
            throw new ArgumentNullException(nameof (@operator));
        if (operand == null)
            throw new ArgumentNullException(nameof (operand));
        if (modify == null) throw new ArgumentNullException(nameof(modify));
        if (apply == null)
            throw new ArgumentNullException(nameof (apply));

        return input =>
        {
            var parseResult = operand(input);
            if (!parseResult.HasValue )
                return parseResult;

            var result = parseResult.Value;
            var remainder = parseResult.Remainder;

            var operatorResult = @operator(remainder);
            while (operatorResult.HasValue || operatorResult.SubTokenErrorPosition.HasValue || remainder != operatorResult.Remainder)
            {
                // If operator read any input, but failed to read complete input, we return error
                if (!operatorResult.HasValue)
                    return TokenListParserResult.CastEmpty<TKind, TOperator, TResult>(operatorResult);

                var operandResult = operand(operatorResult.Remainder);
                remainder = operandResult.Remainder;

                if (!operandResult.HasValue)
                    return operandResult;

                var modifierResult = modify(remainder);
                remainder = modifierResult.Remainder;

                if (!modifierResult.HasValue)
                    return TokenListParserResult.CastEmpty<TKind, TModifier, TResult>(modifierResult);

                result = apply(operatorResult.Value, result, operandResult.Value, modifierResult.Value);

                operatorResult = @operator(remainder);
            }

            return TokenListParserResult.Value(result, input, remainder);
        };
    }
}