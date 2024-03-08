// Copyright 2013-2020 Serilog Contributors
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

namespace Serilog;

/// <summary>
/// Configuration object for creating <see cref="ILogger"/> instances.
/// </summary>
public class LoggerConfiguration
{
    readonly List<ILogEventSink> _logEventSinks = new();
    readonly List<ILogEventSink> _auditSinks = new();
    readonly List<ILogEventEnricher> _enrichers = new();
    readonly List<ILogEventFilter> _filters = new();
    readonly List<Type> _additionalScalarTypes = new();
    readonly HashSet<Type> _additionalDictionaryTypes = new();
    readonly List<IDestructuringPolicy> _additionalDestructuringPolicies = new();
    readonly Dictionary<Type, Destructuring> _fallbackDestructuring = new();
    readonly Dictionary<string, LoggingLevelSwitch> _overrides = new();
    LogEventLevel _minimumLevel = LogEventLevel.Information;
    LoggingLevelSwitch? _levelSwitch;
    int _maximumDestructuringDepth = 10;
    int _maximumStringLength = int.MaxValue;
    int _maximumCollectionCount = int.MaxValue;
    bool _loggerCreated;

    /// <summary>
    /// Construct a <see cref="LoggerConfiguration"/>.
    /// </summary>
    public LoggerConfiguration()
    {
        WriteTo = new(this, s => _logEventSinks.Add(s));
        Enrich = new(this, e => _enrichers.Add(e));
    }

    /// <summary>
    /// Configures the sinks that log events will be emitted to.
    /// </summary>
    public LoggerSinkConfiguration WriteTo { get; internal set; }

    /// <summary>
    /// Configures sinks for auditing, instead of regular (safe) logging. When auditing is used,
    /// exceptions from sinks and any intermediate filters propagate back to the caller. Most callers
    /// should use <see cref="WriteTo"/> instead.
    /// </summary>
    /// <remarks>
    /// Not all sinks are compatible with transactional auditing requirements (many will use asynchronous
    /// batching to improve write throughput and latency). Sinks need to opt-in to auditing support by
    /// extending <see cref="LoggerAuditSinkConfiguration"/>, though the generic <see cref="LoggerAuditSinkConfiguration.Sink"/>
    /// method allows any sink class to be adapted for auditing.
    /// </remarks>
    public LoggerAuditSinkConfiguration AuditTo => new(this, s => _auditSinks.Add(s));

    /// <summary>
    /// Configures the minimum level at which events will be passed to sinks. If
    /// not specified, only events at the <see cref="LogEventLevel.Information"/>
    /// level and above will be passed through.
    /// </summary>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerMinimumLevelConfiguration MinimumLevel
    {
        get
        {
            return new(this,
                l =>
                {
                    _minimumLevel = l;
                    _levelSwitch = null;
                },
                sw => _levelSwitch = sw,
                (s, lls) => _overrides[s] = lls);
        }
    }

    /// <summary>
    /// Configures enrichment of <see cref="LogEvent"/>s. Enrichers can add, remove and
    /// modify the properties associated with events.
    /// </summary>
    public LoggerEnrichmentConfiguration Enrich { get; internal set; }

    /// <summary>
    /// Configures global filtering of <see cref="LogEvent"/>s.
    /// </summary>
    public LoggerFilterConfiguration Filter => new(this, f => _filters.Add(f));

    /// <summary>
    /// Configures destructuring of message template parameters.
    /// </summary>
    public LoggerDestructuringConfiguration Destructure
    {
        get
        {
            return new(
                this,
                _additionalScalarTypes.Add,
                type => _additionalDictionaryTypes.Add(type),
                _additionalDestructuringPolicies.Add,
                depth => _maximumDestructuringDepth = depth,
                length => _maximumStringLength = length,
                count => _maximumCollectionCount = count,
                _fallbackDestructuring.Add);
        }
    }

    /// <summary>
    /// Apply external settings to the logger configuration.
    /// </summary>
    public LoggerSettingsConfiguration ReadFrom => new(this);

    /// <summary>
    /// Create a logger using the configured sinks, enrichers and minimum level.
    /// </summary>
    /// <returns>The logger.</returns>
    /// <remarks>To free resources held by sinks ahead of program shutdown,
    /// the returned logger may be cast to <see cref="IDisposable"/> and
    /// disposed.</remarks>
    /// <exception cref="InvalidOperationException">When the logger is already created</exception>
    public Logger CreateLogger()
    {
        if (_loggerCreated) throw new InvalidOperationException("CreateLogger() was previously called and can only be called once.");

        _loggerCreated = true;

        ILogEventSink? sink = null;
        if (_logEventSinks.Count > 0)
            sink = new SafeAggregateSink(_logEventSinks);

        var auditing = _auditSinks.Any();
        if (auditing)
        {
            sink = new AggregateSink(sink == null ? _auditSinks : new[] { sink }.Concat(_auditSinks));
        }

        sink ??= new SafeAggregateSink(Array.Empty<ILogEventSink>());

        if (_filters.Any())
        {
            // A throwing filter could drop an auditable event, so exceptions in filters must be propagated
            // if auditing is used.
            sink = new FilteringSink(sink, _filters, auditing);
        }

        var converter = new PropertyValueConverter(
            _maximumDestructuringDepth,
            _maximumStringLength,
            _maximumCollectionCount,
            _additionalScalarTypes,
            _additionalDictionaryTypes,
            _additionalDestructuringPolicies,
            auditing,
            _fallbackDestructuring);
        var processor = new MessageTemplateProcessor(converter);

        var enricher = _enrichers.Count switch
        {
            // Should be a rare case, so no problem making that extra interface dispatch.
            0 => new EmptyEnricher(),
            1 => _enrichers[0],
            // Enrichment failures are not considered blocking for auditing purposes.
            _ => new SafeAggregateEnricher(_enrichers)
        };

        LevelOverrideMap? overrideMap = null;
        if (_overrides.Count != 0)
        {
            overrideMap = new(_overrides, _minimumLevel, _levelSwitch);
        }

        var disposableSinks = _logEventSinks
            .Concat(_auditSinks)
            .Where(s => s is IDisposable
#if FEATURE_ASYNCDISPOSABLE
                or IAsyncDisposable
#endif
                )
            .ToArray();

        void Dispose()
        {
            foreach (var disposable in disposableSinks)
            {
                (disposable as IDisposable)?.Dispose();
            }
        }

#if FEATURE_ASYNCDISPOSABLE
        async ValueTask DisposeAsync()
        {
            foreach (var disposable in disposableSinks)
            {
                if (disposable is IAsyncDisposable asyncDisposable)
                {
                    await asyncDisposable.DisposeAsync().ConfigureAwait(false);
                }
                else
                {
                    ((IDisposable)disposable).Dispose();
                }
            }
        }
#endif

        return new(
            processor,
            _levelSwitch != null ? LevelAlias.Minimum : _minimumLevel, _levelSwitch,
            sink,
            enricher,
            Dispose,
#if FEATURE_ASYNCDISPOSABLE
            DisposeAsync,
#endif
            overrideMap);
    }
}
