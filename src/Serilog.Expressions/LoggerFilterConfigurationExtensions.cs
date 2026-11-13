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

using Serilog.Configuration;
using Serilog.Expressions;
using Serilog.Expressions.Runtime;

// ReSharper disable UnusedMember.Global

namespace Serilog;

/// <summary>
/// Extends logger filter configuration with methods for filtering with expressions.
/// </summary>
public static class LoggerFilterConfigurationExtensions
{
    /// <summary>
    /// Include only log events that match the provided expression.
    /// </summary>
    /// <param name="loggerFilterConfiguration">Filter configuration.</param>
    /// <param name="expression">The expression to apply.</param>
    /// <returns>The underlying <see cref="LoggerConfiguration"/>.</returns>
    public static LoggerConfiguration ByIncludingOnly(this LoggerFilterConfiguration loggerFilterConfiguration, string expression)
    {
        if (loggerFilterConfiguration == null) throw new ArgumentNullException(nameof(loggerFilterConfiguration));
        if (expression == null) throw new ArgumentNullException(nameof(expression));

        var compiled = SerilogExpression.Compile(expression);
        return loggerFilterConfiguration.ByIncludingOnly(e => Coerce.IsTrue(compiled(e)));
    }

    /// <summary>
    /// Exclude log events that match the provided expression.
    /// </summary>
    /// <param name="loggerFilterConfiguration">Filter configuration.</param>
    /// <param name="expression">The expression to apply.</param>
    /// <returns>The underlying <see cref="LoggerConfiguration"/>.</returns>
    public static LoggerConfiguration ByExcluding(this LoggerFilterConfiguration loggerFilterConfiguration, string expression)
    {
        if (loggerFilterConfiguration == null) throw new ArgumentNullException(nameof(loggerFilterConfiguration));
        if (expression == null) throw new ArgumentNullException(nameof(expression));

        var compiled = SerilogExpression.Compile(expression);
        return loggerFilterConfiguration.ByExcluding(e => Coerce.IsTrue(compiled(e)));
    }

    /// <summary>
    /// Use a <see cref="LoggingFilterSwitch"/> to dynamically control filtering.
    /// </summary>
    /// <param name="loggerFilterConfiguration">Filter configuration.</param>
    /// <param name="switch">A <see cref="LoggingFilterSwitch"/> that can be used to dynamically control
    /// log filtering.</param>
    /// <returns>The underlying <see cref="LoggerConfiguration"/>.</returns>
    public static LoggerConfiguration ControlledBy(this LoggerFilterConfiguration loggerFilterConfiguration, LoggingFilterSwitch @switch)
    {
        if (loggerFilterConfiguration == null) throw new ArgumentNullException(nameof(loggerFilterConfiguration));
        if (@switch == null) throw new ArgumentNullException(nameof(@switch));

        return loggerFilterConfiguration.With(@switch);
    }
}