// Copyright 2019 Serilog Contributors
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

using Serilog.Configuration;
using Serilog.Expressions;
using Serilog.Expressions.Runtime;

namespace Serilog;

/// <summary>
/// Extends logger sink configuration with methods for filtering with expressions.
/// </summary>
public static class LoggerSinkConfigurationExtensions
{
    /// <summary>
    /// Write to a sink only when <paramref name="expression" /> evaluates to <c>true</c>.
    /// </summary>
    /// <param name="loggerSinkConfiguration">Sink configuration.</param>
    /// <param name="expression">An expression that evaluates to <c>true</c> when the
    /// supplied <see cref="T:Serilog.Events.LogEvent" />
    /// should be written to the configured sink.</param>
    /// <param name="configureSink">An action that configures the wrapped sink.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <returns>The underlying <see cref="LoggerConfiguration"/>.</returns>
    public static  LoggerConfiguration Conditional(
        this LoggerSinkConfiguration loggerSinkConfiguration,
        string expression,
        Action<LoggerSinkConfiguration> configureSink)
    {
        if (loggerSinkConfiguration == null) throw new ArgumentNullException(nameof(loggerSinkConfiguration));
        if (expression == null) throw new ArgumentNullException(nameof(expression));
        if (configureSink == null) throw new ArgumentNullException(nameof(configureSink));

        var compiled = SerilogExpression.Compile(expression);
        return loggerSinkConfiguration.Conditional(e => Coerce.IsTrue(compiled(e)), configureSink);
    }
}