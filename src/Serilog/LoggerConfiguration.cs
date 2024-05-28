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
    private List<ILogEventSink>? _logEventSinks;
    private List<ILogEventSink>? _auditSinks;
    private List<ILogEventEnricher>? _enrichers;
    private List<ILogEventFilter>? _filters;
    private List<Type>? _additionalScalarTypes;
    private HashSet<Type>? _additionalDictionaryTypes;
    private List<IDestructuringPolicy>? _additionalDestructuringPolicies;
    private Dictionary<string, LoggingLevelSwitch>? _overrides;

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
    }

    private LoggerSinkConfiguration? _writeTo;
    private LoggerAuditSinkConfiguration? _auditTo;
    private LoggerMinimumLevelConfiguration? _minimumLevelConfig;
    private LoggerEnrichmentConfiguration? _enrich;
    private LoggerFilterConfiguration? _filter;
    private LoggerSettingsConfiguration? _readFrom;
    private LoggerDestructuringConfiguration? _destructure;

    /// <summary>
    /// Configures the sinks that log events will be emitted to.
    /// </summary>
    public LoggerSinkConfiguration WriteTo
    {
        get => _writeTo ??= new(this, s => (_logEventSinks ??= []).Add(s));
        internal set => this._writeTo = value;
    }

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
    public LoggerAuditSinkConfiguration AuditTo => _auditTo ??= new(this, s => (_auditSinks ??= []).Add(s));

    /// <summary>
    /// Configures the minimum level at which events will be passed to sinks. If
    /// not specified, only events at the <see cref="LogEventLevel.Information"/>
    /// level and above will be passed through.
    /// </summary>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerMinimumLevelConfiguration MinimumLevel => _minimumLevelConfig ??= new(this,
                l =>
                {
                    _minimumLevel = l;
                    _levelSwitch = null;
                },
                sw => _levelSwitch = sw,
                (s, lls) => (_overrides ??= [])[s] = lls);

    /// <summary>
    /// Configures enrichment of <see cref="LogEvent"/>s. Enrichers can add, remove and
    /// modify the properties associated with events.
    /// </summary>
    public LoggerEnrichmentConfiguration Enrich
    {
        get => _enrich ??= new(this, e => (_enrichers ??= []).Add(e));
        internal set => this._enrich = value;
    }

    /// <summary>
    /// Configures global filtering of <see cref="LogEvent"/>s.
    /// </summary>
    public LoggerFilterConfiguration Filter => _filter ??= new(this, f => (_filters ??= []).Add(f));

    /// <summary>
    /// Configures destructuring of message template parameters.
    /// </summary>
    public LoggerDestructuringConfiguration Destructure => _destructure ??= new(this,
                type => (_additionalScalarTypes ??= []).Add(type),
                type => (_additionalDictionaryTypes ??= []).Add(type),
                type => (_additionalDestructuringPolicies ??= []).Add(type),
                depth => _maximumDestructuringDepth = depth,
                length => _maximumStringLength = length,
                count => _maximumCollectionCount = count)!;

    /// <summary>
    /// Apply external settings to the logger configuration.
    /// </summary>
    public LoggerSettingsConfiguration ReadFrom => _readFrom ??= new(this);

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
        if (_logEventSinks is not null && _logEventSinks.Count != 0)
        {
            sink = new SafeAggregateSink(_logEventSinks);
        }

        var auditing = _auditSinks is not null && _auditSinks.Count != 0;
        if (auditing)
        {
            sink = new AggregateSink(sink == null ? _auditSinks! : [sink, .. _auditSinks!]);
        }

        sink ??= new EmptySink();

        if (_filters is not null && _filters.Count != 0)
        {
            // A throwing filter could drop an auditable event, so exceptions in filters must be propagated
            // if auditing is used.
            sink = new FilteringSink(sink, _filters, auditing);
        }

        var converter = new PropertyValueConverter(
            _maximumDestructuringDepth,
            _maximumStringLength,
            _maximumCollectionCount,
            _additionalScalarTypes ?? [],
            _additionalDictionaryTypes ?? [],
            _additionalDestructuringPolicies ?? [],
            auditing);
        var processor = new MessageTemplateProcessor(converter);

        var enricher = _enrichers?.Count switch
        {
            // Should be a rare case, so no problem making that extra interface dispatch.
            null => new EmptyEnricher(),
            0 => new EmptyEnricher(),
            1 => _enrichers[0],
            // Enrichment failures are not considered blocking for auditing purposes.
            _ => new SafeAggregateEnricher(_enrichers)
        };

        LevelOverrideMap? overrideMap = null;
        if (_overrides is not null && _overrides.Count != 0)
        {
            overrideMap = new(_overrides, _minimumLevel, _levelSwitch);
        }

        var disposableSinks = Util.WhereIsDisposableSinks(_logEventSinks, _auditSinks).ToArray();

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
                    (disposable as IDisposable)?.Dispose();
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

    static class Util
    {
        public static IEnumerable<ILogEventSink> WhereIsDisposableSinks(List<ILogEventSink>? sinks, List<ILogEventSink>? auditSinks)
        {
            foreach (var sink in WhereIsDisposable(sinks))
            {
                yield return sink;
            }
            foreach (var auditSink in WhereIsDisposable(auditSinks))
            {
                yield return auditSink;
            }
        }

        public static IEnumerable<T> WhereIsDisposable<T>(List<T>? items)
        {
            if(items is null)
                yield break;

            foreach (var item in items)
            {
                if (item is IDisposable
#if FEATURE_ASYNCDISPOSABLE
                or IAsyncDisposable
#endif
                )
                    yield return item;
            }
        }
    }
}
