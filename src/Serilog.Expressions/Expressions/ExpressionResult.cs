// Copyright Serilog Contributors
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

using Serilog.Events;
using Serilog.Expressions.Runtime;

namespace Serilog.Expressions;

/// <summary>
/// Helper functions for working with the results of evaluating expressions.
/// </summary>
public static class ExpressionResult
{
    /// <summary>
    /// Test whether <paramref name="value"/> is <c langword="true">true</c>.
    /// </summary>
    /// <param name="value">The value to test, which may be <c langword="null">null</c>
    /// if the result of evaluating an expression was undefined.</param>
    /// <returns>Returns <c langword="true">true</c> if and only if the
    /// <paramref name="value"/> is a scalar Boolean with the
    /// value <c langword="true">true</c>. Returns <c langword="false">false</c>, otherwise.</returns>
    public static bool IsTrue(LogEventPropertyValue? value)
    {
        return Coerce.IsTrue(value);
    }
}