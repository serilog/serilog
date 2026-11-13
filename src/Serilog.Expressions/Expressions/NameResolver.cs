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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Serilog.Events;

namespace Serilog.Expressions;

/// <summary>
/// Looks up the implementations of functions that appear in expressions.
/// </summary>
public abstract class NameResolver
{
    /// <summary>
    /// Match a function name to a method that implements it.
    /// </summary>
    /// <param name="name">The function name as it appears in the expression source. Names are not case-sensitive.</param>
    /// <param name="implementation">A <see cref="MethodInfo"/> implementing the function.</param>
    /// <returns><c>True</c> if the name could be resolved; otherwise, <c>false</c>.</returns>
    /// <remarks>The method implementing a function should be <c>static</c>, return <see cref="LogEventPropertyValue"/>,
    /// and accept parameters of type <see cref="LogEventPropertyValue"/>. If the <c>ci</c> modifier is supported,
    /// a <see cref="StringComparison"/> should be included in the argument list. If the function is culture-specific,
    /// an <see cref="IFormatProvider"/> should be included in the argument list.</remarks>
    public virtual bool TryResolveFunctionName(string name, [MaybeNullWhen(false)] out MethodInfo implementation)
    {
        implementation = null;
        return false;
    }

    /// <summary>
    /// Provide a value for a non-<see cref="LogEventPropertyValue"/> parameter. This allows user-defined state to
    /// be threaded through user-defined functions.
    /// </summary>
    /// <param name="parameter">A parameter of a method implementing a user-defined function, which could not be
    /// bound to any of the standard runtime-provided values or operands.</param>
    /// <param name="boundValue">The value that should be provided when the method is called.</param>
    /// <returns><c>True</c> if the parameter could be bound; otherwise, <c>false</c>.</returns>
    public virtual bool TryBindFunctionParameter(ParameterInfo parameter, [MaybeNullWhen(false)] out object boundValue)
    {
        boundValue = null;
        return false;
    }

    /// <summary>
    /// Map an unrecognized built-in property name to a recognised one.
    /// </summary>
    /// <remarks>Intended predominantly to support migration from <em>Serilog.Filters.Expressions</em>.</remarks>
    /// <param name="alias">The unrecognized name, for example, <code>"Message"</code>; the <code>@</code> prefix is
    /// not included.</param>
    /// <param name="target">If the name could be resolved, the target property name, without any prefix; for
    /// example, <code>"m"</code>.</param>
    /// <returns>True if the alias was mapped to a built-in property; otherwise, false.</returns>
    public virtual bool TryResolveBuiltInPropertyName(string alias, [NotNullWhen(true)] out string? target)
    {
        target = null;
        return false;
    }
}