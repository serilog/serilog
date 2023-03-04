// Copyright 2013-2015 Serilog Contributors
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

namespace Serilog.Configuration;

/// <summary>
/// Controls enrichment configuration.
/// </summary>
public class LoggerEnrichmentConfiguration
{
    readonly LoggerConfiguration _loggerConfiguration;
    readonly Action<ILogEventEnricher> _addEnricher;

    internal LoggerEnrichmentConfiguration(
        LoggerConfiguration loggerConfiguration,
        Action<ILogEventEnricher> addEnricher)
    {
        _loggerConfiguration = Guard.AgainstNull(loggerConfiguration);
        _addEnricher = Guard.AgainstNull(addEnricher);
    }

    /// <summary>
    /// Specifies one or more enrichers that may add properties dynamically to
    /// log events.
    /// </summary>
    /// <param name="enrichers">Enrichers to apply to all events passing through
    /// the logger.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="enrichers"/> is <code>null</code></exception>
    /// <exception cref="ArgumentException">When any element of <paramref name="enrichers"/> is <code>null</code></exception>
    public LoggerConfiguration With(params ILogEventEnricher[] enrichers)
    {
        Guard.AgainstNull(enrichers);

        foreach (var logEventEnricher in enrichers)
        {
            Guard.AgainstNull(logEventEnricher);

            _addEnricher(logEventEnricher);
        }
        return _loggerConfiguration;
    }

    /// <summary>
    /// Specifies an enricher that may add properties dynamically to
    /// log events.
    /// </summary>
    /// <typeparam name="TEnricher">Enricher type to apply to all events passing through
    /// the logger.</typeparam>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration With<TEnricher>()
        where TEnricher : ILogEventEnricher, new()
    {
        return With(new TEnricher());
    }

    /// <summary>
    /// Include the specified property value in all events logged to the logger.
    /// </summary>
    /// <param name="name">The name of the property to add.</param>
    /// <param name="value">The property value to add.</param>
    /// <param name="destructureObjects">If <see langword="true"/>, objects of unknown type will be logged as structures; otherwise they will be converted using <see cref="object.ToString"/>.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration WithProperty(string name, object value, bool destructureObjects = false)
    {
        return With(new PropertyEnricher(name, value, destructureObjects));
    }

    /// <summary>
    /// Enrich log events with properties from <see cref="Context.LogContext"/>.
    /// </summary>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration FromLogContext() => With<LogContextEnricher>();

    /// <summary>
    /// Apply an enricher only when <paramref name="condition"/> evaluates to <c>true</c>.
    /// </summary>
    /// <param name="condition">A predicate that evaluates to <c>true</c> when the supplied <see cref="LogEvent"/>
    /// should be enriched.</param>
    /// <param name="configureEnricher">An action that configures the wrapped enricher.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="condition"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="configureEnricher"/> is <code>null</code></exception>
    public LoggerConfiguration When(Func<LogEvent, bool> condition, Action<LoggerEnrichmentConfiguration> configureEnricher)
    {
        Guard.AgainstNull(condition);
        Guard.AgainstNull(configureEnricher);

        return Wrap(this, e => new ConditionalEnricher(e, condition), configureEnricher);
    }

    /// <summary>
    /// Apply an enricher only to events with a <see cref="LogEventLevel"/> greater than or equal to <paramref name="enrichFromLevel"/>.
    /// </summary>
    /// <param name="enrichFromLevel">The level from which the enricher will be applied.</param>
    /// <param name="configureEnricher">An action that configures the wrapped enricher.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <remarks>This method permits additional information to be attached to e.g. warnings and errors, that might be too expensive
    /// to collect or store at lower levels.</remarks>
    /// <exception cref="ArgumentNullException">When <paramref name="configureEnricher"/> is <code>null</code></exception>
    public LoggerConfiguration AtLevel(LogEventLevel enrichFromLevel, Action<LoggerEnrichmentConfiguration> configureEnricher)
    {
        Guard.AgainstNull(configureEnricher);

        return Wrap(this, e => new ConditionalEnricher(e, le => le.Level >= enrichFromLevel), configureEnricher);
    }

    /// <summary>
    /// Apply an enricher only to events with a <see cref="LogEventLevel"/> greater than or equal to the level specified by <paramref name="levelSwitch"/>.
    /// </summary>
    /// <param name="levelSwitch">A <see cref="LoggingLevelSwitch"/> that specifies the level from which the enricher will be applied.</param>
    /// <param name="configureEnricher">An action that configures the wrapped enricher.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <remarks>This method permits additional information to be attached to e.g. warnings and errors, that might be too expensive
    /// to collect or store at lower levels.</remarks>
    /// <exception cref="ArgumentNullException">When <paramref name="configureEnricher"/> is <code>null</code></exception>
    public LoggerConfiguration AtLevel(LoggingLevelSwitch levelSwitch, Action<LoggerEnrichmentConfiguration> configureEnricher)
    {
        Guard.AgainstNull(configureEnricher);

        return Wrap(this, e => new ConditionalEnricher(e, le => le.Level >= levelSwitch.MinimumLevel), configureEnricher);
    }

    /// <summary>
    /// Helper method for wrapping sinks.
    /// </summary>
    /// <param name="loggerEnrichmentConfiguration">The parent enrichment configuration.</param>
    /// <param name="wrapEnricher">A function that allows for wrapping <see cref="ILogEventEnricher"/>s
    /// added in <paramref name="configureWrappedEnricher"/>.</param>
    /// <param name="configureWrappedEnricher">An action that configures enrichers to be wrapped in <paramref name="wrapEnricher"/>.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="loggerEnrichmentConfiguration"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="wrapEnricher"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="configureWrappedEnricher"/> is <code>null</code></exception>
    public static LoggerConfiguration Wrap(
        LoggerEnrichmentConfiguration loggerEnrichmentConfiguration,
        Func<ILogEventEnricher, ILogEventEnricher> wrapEnricher,
        Action<LoggerEnrichmentConfiguration> configureWrappedEnricher)
    {
        Guard.AgainstNull(loggerEnrichmentConfiguration);
        Guard.AgainstNull(wrapEnricher);
        Guard.AgainstNull(configureWrappedEnricher);

        var enrichersToWrap = new List<ILogEventEnricher>();

        var capturingConfiguration = new LoggerConfiguration();
        var capturingLoggerEnrichmentConfiguration = new LoggerEnrichmentConfiguration(
            capturingConfiguration,
            enrichersToWrap.Add);

        // `Enrich.With()` will return the capturing configuration; this ensures chained `Enrich` gets back
        // to the capturing enrichment configuration, enabling `Enrich.WithX().Enrich.WithY()`.
        capturingConfiguration.Enrich = capturingLoggerEnrichmentConfiguration;

        configureWrappedEnricher(capturingLoggerEnrichmentConfiguration);

        if (enrichersToWrap.Count == 0)
            return loggerEnrichmentConfiguration._loggerConfiguration;

        var enclosed = enrichersToWrap.Count == 1 ?
            enrichersToWrap.Single() :
            // Enrichment failures are not considered blocking for auditing purposes.
            new SafeAggregateEnricher(enrichersToWrap);

        var wrappedEnricher = wrapEnricher(enclosed);

        // ReSharper disable once SuspiciousTypeConversion.Global
        if (wrappedEnricher is not IDisposable)
        {
            SelfLog.WriteLine("Wrapping enricher {0} does not implement IDisposable; to ensure " +
                              "wrapped enrichers are properly disposed, wrappers should dispose " +
                              "their wrapped contents", wrappedEnricher);
        }

        return loggerEnrichmentConfiguration.With(wrappedEnricher);
    }
}
