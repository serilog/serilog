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
using Serilog.Pipeline;

namespace Serilog;

/// <summary>
/// Extends logger enrichment configuration with methods for filtering with expressions.
/// </summary>
public static class LoggerEnrichmentConfigurationExtensions
{
    /// <summary>
    /// Write to a sink only when <paramref name="expression" /> evaluates to <c>true</c>.
    /// </summary>
    /// <param name="loggerEnrichmentConfiguration">Enrichment configuration.</param>
    /// <param name="expression">An expression that evaluates to <c>true</c> when the supplied
    /// <see cref="T:Serilog.Events.LogEvent" /> should be enriched.</param>
    /// <param name="configureEnricher">An action that configures the wrapped enricher.</param>
    /// <returns>The underlying <see cref="LoggerConfiguration"/>.</returns>
    public static LoggerConfiguration When(
        this LoggerEnrichmentConfiguration loggerEnrichmentConfiguration,
        string expression,
        Action<LoggerEnrichmentConfiguration> configureEnricher)
    {
        if (loggerEnrichmentConfiguration == null) throw new ArgumentNullException(nameof(loggerEnrichmentConfiguration));
        if (expression == null) throw new ArgumentNullException(nameof(expression));
        if (configureEnricher == null) throw new ArgumentNullException(nameof(configureEnricher));

        var compiled = SerilogExpression.Compile(expression);
        return loggerEnrichmentConfiguration.When(e => Coerce.IsTrue(compiled(e)), configureEnricher);
    }

    /// <summary>
    /// Enrich events with a property <paramref name="propertyName"/> computed by evaluating
    /// <paramref name="expression"/> in the context of the event.
    /// </summary>
    /// <param name="loggerEnrichmentConfiguration">Enrichment configuration.</param>
    /// <param name="propertyName">The name of the property to attach; if the property already
    /// exists, and <paramref name="expression"/> evaluates to a defined value, it will be overwritten.</param>
    /// <param name="expression">An expression to evaluate in the context of each event. If the result of
    /// evaluating the expression is defined, it will be attached to the event as <paramref name="propertyName"/>.</param>
    /// <returns>The underlying <see cref="LoggerConfiguration"/>.</returns>
    public static LoggerConfiguration WithComputed(
        this LoggerEnrichmentConfiguration loggerEnrichmentConfiguration,
        string propertyName,
        string expression)
    {
        if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
        if (expression == null) throw new ArgumentNullException(nameof(expression));
        var compiled = SerilogExpression.Compile(expression);
        return loggerEnrichmentConfiguration.With(new ComputedPropertyEnricher(propertyName, compiled));
    }
}