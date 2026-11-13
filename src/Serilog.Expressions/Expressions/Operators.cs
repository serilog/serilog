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

// ReSharper disable InconsistentNaming, MemberCanBePrivate.Global

namespace Serilog.Expressions;

static class Operators
{
    static StringComparer OperatorComparer { get; } = StringComparer.OrdinalIgnoreCase;

    // Core filter language
    // Op* means usable in expressions _and_ runtime executable.
    // RuntimeOp* means runtime only.

    public const string OpCoalesce = "Coalesce";
    public const string OpConcat = "Concat";
    public const string OpContains = "Contains";
    public const string OpElementAt = "ElementAt";
    public const string OpEndsWith = "EndsWith";
    public const string OpIndexOf = "IndexOf";
    public const string OpIndexOfMatch = "IndexOfMatch";
    public const string OpInspect = "Inspect";
    public const string OpIsMatch = "IsMatch";
    public const string OpIsDefined = "IsDefined";
    public const string OpLastIndexOf = "LastIndexOf";
    public const string OpLength = "Length";
    public const string OpNest = "Nest";
    public const string OpNow = "Now";
    public const string OpReplace = "Replace";
    public const string OpRound = "Round";
    public const string OpStartsWith = "StartsWith";
    public const string OpSubstring = "Substring";
    public const string OpTagOf = "TagOf";
    public const string OpToString = "ToString";
    public const string OpTypeOf = "TypeOf";
    public const string OpUndefined = "Undefined";
    public const string OpUtcDateTime = "UtcDateTime";

    public const string IntermediateOpLike = "_Internal_Like";
    public const string IntermediateOpNotLike = "_Internal_NotLike";

    public const string RuntimeOpAdd = "_Internal_Add";
    public const string RuntimeOpSubtract = "_Internal_Subtract";
    public const string RuntimeOpMultiply = "_Internal_Multiply";
    public const string RuntimeOpDivide = "_Internal_Divide";
    public const string RuntimeOpModulo = "_Internal_Modulo";
    public const string RuntimeOpPower = "_Internal_Power";
    public const string RuntimeOpAnd = "_Internal_And";
    public const string RuntimeOpOr = "_Internal_Or";
    public const string RuntimeOpLessThanOrEqual = "_Internal_LessThanOrEqual";
    public const string RuntimeOpLessThan = "_Internal_LessThan";
    public const string RuntimeOpGreaterThan = "_Internal_GreaterThan";
    public const string RuntimeOpGreaterThanOrEqual = "_Internal_GreaterThanOrEqual";
    public const string RuntimeOpEqual = "_Internal_Equal";
    public const string RuntimeOpNotEqual = "_Internal_NotEqual";
    public const string RuntimeOpNegate = "_Internal_Negate";
    public const string RuntimeOpNot = "_Internal_Not";
    public const string RuntimeOpAny = "_Internal_Any";
    public const string RuntimeOpAll = "_Internal_All";
    public const string RuntimeOpIsNull = "_Internal_IsNull";
    public const string RuntimeOpIsNotNull = "_Internal_IsNotNull";
    public const string RuntimeOpIn = "_Internal_In";
    public const string RuntimeOpNotIn = "_Internal_NotIn";
    public const string RuntimeOpStrictNot = "_Internal_StrictNot";
    public const string RuntimeOpIfThenElse = "_Internal_IfThenElse";

    public static readonly HashSet<string> WildcardComparators = new(OperatorComparer)
    {
        OpContains,
        OpStartsWith,
        OpEndsWith,
        RuntimeOpNotEqual,
        RuntimeOpEqual,
        RuntimeOpLessThan,
        RuntimeOpLessThanOrEqual,
        RuntimeOpGreaterThan,
        RuntimeOpGreaterThanOrEqual,
        IntermediateOpLike,
        IntermediateOpNotLike,
        RuntimeOpIn,
        RuntimeOpNotIn,
        OpIsMatch,
        OpIsDefined,
        RuntimeOpIsNull,
        RuntimeOpIsNotNull,
        RuntimeOpAny,
        RuntimeOpAll
    };

    public static bool SameOperator(string op1, string op2)
    {
        if (op1 == null) throw new ArgumentNullException(nameof(op1));
        if (op2 == null) throw new ArgumentNullException(nameof(op2));

        return OperatorComparer.Equals(op1, op2);
    }

    public static string ToRuntimeWildcardOperator(IndexerWildcard wildcard)
    {
        return wildcard switch
        {
            IndexerWildcard.All => RuntimeOpAll,
            IndexerWildcard.Any => RuntimeOpAny,
            _ => throw new ArgumentException("Unsupported wildcard.")
        };
    }
}