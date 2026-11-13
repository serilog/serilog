// Copyright 2016 Datalust, Superpower Contributors, Sprache Contributors
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

using Serilog.ParserConstruction.Util;

namespace Serilog.ParserConstruction.Model;

/// <summary>
/// Helper methods for working with <see cref="Result{T}"/>.
/// </summary>
static class Result
{
    /// <summary>
    /// An empty result indicating no value could be parsed.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="remainder">The start of un-parsed input.</param>
    /// <returns>A result.</returns>
    public static Result<T> Empty<T>(TextSpan remainder)
    {
        return new(remainder, null, null, false);
    }

    /// <summary>
    /// An empty result indicating no value could be parsed.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="remainder">The start of un-parsed input.</param>
    /// <param name="expectations">Literal descriptions of expectations not met.</param>
    /// <returns>A result.</returns>
    public static Result<T> Empty<T>(TextSpan remainder, string[] expectations)
    {
        return new(remainder, null, expectations, false);
    }

    /// <summary>
    /// A result carrying a successfully-parsed value.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="location">The location corresponding to the beginning of the parsed span.</param>
    /// <param name="remainder">The start of un-parsed input.</param>
    /// <returns>A result.</returns>
    public static Result<T> Value<T>(T value, TextSpan location, TextSpan remainder)
    {
        return new(value, location, remainder, false);
    }

    /// <summary>
    /// Convert an empty result of one type into another.
    /// </summary>
    /// <typeparam name="T">The source type.</typeparam>
    /// <typeparam name="U">The target type.</typeparam>
    /// <param name="result">The value to convert.</param>
    /// <returns>A result of type <typeparamref name="U"/> carrying the same information as <paramref name="result"/>.</returns>
    public static Result<U> CastEmpty<T, U>(Result<T> result)
    {
        return new(result.Remainder, result.ErrorMessage, result.Expectations, result.Backtrack);
    }

    /// <summary>
    /// Combine two empty results.
    /// </summary>
    /// <typeparam name="T">The source type.</typeparam>
    /// <param name="first">The first value to combine.</param>
    /// <param name="second">The second value to combine.</param>
    /// <returns>A result of type <typeparamref name="T"/> carrying information from both results.</returns>
    public static Result<T> CombineEmpty<T>(Result<T> first, Result<T> second)
    {
        if (first.Remainder != second.Remainder)
            return second;

        var expectations = first.Expectations;
        if (expectations == null)
            expectations = second.Expectations;
        else if (second.Expectations != null)
            expectations = [..first.Expectations!, ..second.Expectations];

        return new(second.Remainder, second.ErrorMessage, expectations, second.Backtrack);
    }
}