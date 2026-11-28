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

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Serilog.Expressions.Compilation;
using Serilog.Expressions.Parsing;

// ReSharper disable MemberCanBePrivate.Global

namespace Serilog.Expressions;

/// <summary>
/// Helper methods to assist with construction of well-formed expressions.
/// </summary>
public static class SerilogExpression
{
    /// <summary>
    /// Create an evaluation function based on the provided expression.
    /// </summary>
    /// <param name="expression">An expression.</param>
    /// <param name="formatProvider">Optionally, a format provider that will be used for culture-specific formatting;
    ///     by default, <see cref="CultureInfo.CurrentCulture"/> is used.</param>
    /// <param name="nameResolver">Optionally, a <see cref="NameResolver"/>
    ///     with which to resolve function names that appear in the template.</param>
    /// <returns>A function that evaluates the expression in the context of a log event.</returns>
    public static CompiledExpression Compile(string expression,
        IFormatProvider? formatProvider = null,
        NameResolver? nameResolver = null)
    {
        if (expression == null) throw new ArgumentNullException(nameof(expression));
        if (!TryCompileImpl(expression, formatProvider, nameResolver, out var filter, out var error))
            throw new ArgumentException(error);

        return filter;
    }

    /// <summary>
    /// Create an evaluation function based on the provided expression.
    /// </summary>
    /// <param name="expression">An expression.</param>
    /// <param name="result">A function that evaluates the expression in the context of a log event.</param>
    /// <param name="error">The reported error, if compilation was unsuccessful.</param>
    /// <returns>True if the function could be created; otherwise, false.</returns>
    /// <remarks>Validation errors including invalid regular expressions and unknown function names are returned
    /// as friendly error messages. Invalid case-insensitive modifiers are ignored with warnings.</remarks>
    public static bool TryCompile(
        string expression,
        [MaybeNullWhen(false)] out CompiledExpression result,
        [MaybeNullWhen(true)] out string error)
    {
        if (expression == null) throw new ArgumentNullException(nameof(expression));
        return TryCompileImpl(expression, null, null, out result, out error);
    }

    /// <summary>
    /// Create an evaluation function based on the provided expression.
    /// </summary>
    /// <param name="expression">An expression.</param>
    /// <param name="formatProvider">Optionally, a format provider that will be used for culture-specific formatting;
    ///     by default, <see cref="CultureInfo.CurrentCulture"/> is used.</param>
    /// <param name="nameResolver">A <see cref="NameResolver"/>
    ///     with which to resolve function names that appear in the template.</param>
    /// <param name="result">A function that evaluates the expression in the context of a log event.</param>
    /// <param name="error">The reported error, if compilation was unsuccessful.</param>
    /// <returns>True if the function could be created; otherwise, false.</returns>
    /// <remarks>Validation errors including invalid regular expressions and unknown function names are returned
    /// as friendly error messages. Invalid case-insensitive modifiers are ignored with warnings.</remarks>
    public static bool TryCompile(string expression,
        IFormatProvider? formatProvider,
        NameResolver nameResolver,
        [MaybeNullWhen(false)] out CompiledExpression result,
        [MaybeNullWhen(true)] out string error)
    {
        if (expression == null) throw new ArgumentNullException(nameof(expression));
        if (nameResolver == null) throw new ArgumentNullException(nameof(nameResolver));
        return TryCompileImpl(expression, formatProvider, nameResolver, out result, out error);
    }

    static bool TryCompileImpl(string expression,
        IFormatProvider? formatProvider,
        NameResolver? nameResolver,
        [MaybeNullWhen(false)] out CompiledExpression result,
        [MaybeNullWhen(true)] out string error)
    {
        var expressionParser = new ExpressionParser();
        if (!expressionParser.TryParse(expression, out var root, out error))
        {
            result = null;
            return false;
        }

        try
        {
            var evaluate = ExpressionCompiler.Compile(root, formatProvider, DefaultFunctionNameResolver.Build(nameResolver));
            result = evt => evaluate(new(evt));
            error = null;
            return true;
        }
        catch (ExpressionValidationException ex)
        {
            // Catch validation errors from compilation
            result = null;
            error = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// Escape a value that is to appear in a `like` expression.
    /// </summary>
    /// <param name="text">The text to escape.</param>
    /// <returns>The text with any special values escaped. Will need to be passed through
    /// <see cref="EscapeStringContent(string)"/> if it is being embedded directly into a filter expression.</returns>
    // ReSharper disable once UnusedMember.Global
    public static string EscapeLikeExpressionContent(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        return EscapeStringContent(text)
            .Replace("%", "%%")
            .Replace("_", "__");
    }

    /// <summary>
    /// Escape a fragment of text that will appear within a string.
    /// </summary>
    /// <param name="text">The text to escape.</param>
    /// <returns>The text with any special values escaped.</returns>
    public static string EscapeStringContent(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        return text.Replace("'", "''");
    }

    /// <summary>
    /// Determine if the specified text is a valid identifier.
    /// </summary>
    /// <param name="identifier">The text to check.</param>
    /// <returns>True if the text can be used verbatim as a property name.</returns>
    public static bool IsValidIdentifier(string identifier)
    {
        return identifier.Length != 0 &&
               !char.IsDigit(identifier[0]) &&
               identifier.All(ch => char.IsLetter(ch) || char.IsDigit(ch) || ch == '_');
    }
}