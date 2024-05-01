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

// ReSharper disable MergeCastWithTypeCheck

using Serilog.Core.Sinks.Batching;

namespace Serilog.Configuration;

/// <summary>
/// Controls sink configuration.
/// </summary>
public class LoggerSinkConfiguration
{
    readonly LoggerConfiguration _loggerConfiguration;
    readonly Action<ILogEventSink> _addSink;

    internal LoggerSinkConfiguration(LoggerConfiguration loggerConfiguration, Action<ILogEventSink> addSink)
    {
        _loggerConfiguration = Guard.AgainstNull(loggerConfiguration);
        _addSink = Guard.AgainstNull(addSink);
    }

    /// <summary>
    /// Write log events to an <see cref="ILogEventSink"/>.
    /// </summary>
    /// <param name="logEventSink">The sink.</param>
    /// <param name="restrictedToMinimumLevel">The minimum level for
    /// events passed through the sink.</param>
    /// <seealso cref="Sink(ILogEventSink, LogEventLevel, LoggingLevelSwitch?)"/>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <remarks>Sink configuration methods that specify <paramref name="restrictedToMinimumLevel"/> should also specify <see cref="LoggingLevelSwitch"/>.</remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public LoggerConfiguration Sink(
        ILogEventSink logEventSink,
        LogEventLevel restrictedToMinimumLevel)
    {
        return Sink(logEventSink, restrictedToMinimumLevel, null);
    }

    /// <summary>
    /// Write log events to an <see cref="ILogEventSink"/>.
    /// </summary>
    /// <param name="logEventSink">The sink.</param>
    /// <param name="restrictedToMinimumLevel">The minimum level for
    /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
    /// <param name="levelSwitch">A switch allowing the pass-through minimum level
    /// to be changed at runtime.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration Sink(
        ILogEventSink logEventSink,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        // The warning here is redundant; the optional parameter allows `WriteTo.Sink(s)` usage. We would obsolete the two-argument
        // version above for purposes of economy, but end-user `WriteTo.Sink(s, level)` is valid and shouldn't result in a warning.
        // ReSharper disable once MethodOverloadWithOptionalParameter
        LoggingLevelSwitch? levelSwitch = null)
    {
        Guard.AgainstNull(logEventSink);

        var sink = logEventSink;
        if (levelSwitch != null)
        {
            if (restrictedToMinimumLevel != LevelAlias.Minimum)
                SelfLog.WriteLine("Sink {0} was configured with both a level switch and minimum level '{1}'; the minimum level will be ignored and the switch level used", sink, restrictedToMinimumLevel);

            sink = new RestrictedSink(sink, levelSwitch);
        }
        else if (restrictedToMinimumLevel > LevelAlias.Minimum)
        {
            sink = new RestrictedSink(sink, new(restrictedToMinimumLevel));
        }

        _addSink(sink);
        return _loggerConfiguration;
    }

    /// <summary>
    /// Write log events to the specified <see cref="ILogEventSink"/>.
    /// </summary>
    /// <typeparam name="TSink">The sink.</typeparam>
    /// <param name="restrictedToMinimumLevel">The minimum level for
    /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
    /// <param name="levelSwitch">A switch allowing the pass-through minimum level
    /// to be changed at runtime.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration Sink<TSink>(
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch? levelSwitch = null)
        where TSink : ILogEventSink, new()
    {
        return Sink(new TSink(), restrictedToMinimumLevel, levelSwitch);
    }

    /// <summary>
    /// Write log events to an <see cref="IBatchedLogEventSink"/>. Events will be internally buffered, and
    /// written to the sink in batches.
    /// </summary>
    /// <param name="batchedLogEventSink">The batched sink to receive events.</param>
    /// <param name="batchingOptions">Options that control batch sizes, buffering time, and backpressure.</param>
    /// <param name="restrictedToMinimumLevel">The minimum level for
    /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
    /// <param name="levelSwitch">A switch allowing the pass-through minimum level
    /// to be changed at runtime.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration Sink(
        IBatchedLogEventSink batchedLogEventSink,
        BatchingOptions batchingOptions,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch? levelSwitch = null)
    {
        Guard.AgainstNull(batchedLogEventSink);
        Guard.AgainstNull(batchingOptions);

        return Sink(new BatchingSink(batchedLogEventSink, batchingOptions), restrictedToMinimumLevel, levelSwitch);
    }

    /// <summary>
    /// Write log events to an <see cref="IBatchedLogEventSink"/>. Events will be internally buffered, and
    /// written to the sink in batches.
    /// </summary>
    /// <typeparam name="TSink">The type of a batched sink to receive events. The sink must provide a public,
    /// parameterless constructor.</param>
    /// <param name="batchingOptions">Options that control batch sizes, buffering time, and backpressure.</param>
    /// <param name="restrictedToMinimumLevel">The minimum level for
    /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
    /// <param name="levelSwitch">A switch allowing the pass-through minimum level
    /// to be changed at runtime.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration Sink<TSink>(
        BatchingOptions batchingOptions,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch? levelSwitch = null)
        where TSink : IBatchedLogEventSink, new()
    {
        return Sink(new TSink(), batchingOptions, restrictedToMinimumLevel, levelSwitch);
    }

    /// <summary>
    /// Write log events to a sub-logger, where further processing may occur. Events through
    /// the sub-logger will be constrained by filters and enriched by enrichers that are
    /// active in the parent. A sub-logger cannot be used to log at a more verbose level, but
    /// a less verbose level is possible.
    /// </summary>
    /// <param name="configureLogger">An action that configures the sub-logger.</param>
    /// <param name="restrictedToMinimumLevel">The minimum level for
    /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
    /// <param name="levelSwitch">A switch allowing the pass-through minimum level
    /// to be changed at runtime. Can be <code>null</code></param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="configureLogger"/> is <code>null</code></exception>
    public LoggerConfiguration Logger(
        Action<LoggerConfiguration> configureLogger,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch? levelSwitch = null)
    {
        Guard.AgainstNull(configureLogger);

        var lc = new LoggerConfiguration().MinimumLevel.Is(LevelAlias.Minimum);
        configureLogger(lc);

        var subLogger = lc.CreateLogger();
        if (subLogger.HasOverrideMap)
        {
            SelfLog.WriteLine("Minimum level overrides are not supported on sub-loggers " +
                              "and may be removed completely in a future version.");
        }

        var secondarySink = new SecondaryLoggerSink(subLogger, attemptDispose: true);
        return Sink(secondarySink, restrictedToMinimumLevel, levelSwitch);
    }

    /// <summary>
    /// Write log events to a sub-logger, where further processing may occur. Events through
    /// the sub-logger will be constrained by filters and enriched by enrichers that are
    /// active in the parent. A sub-logger cannot be used to log at a more verbose level, but
    /// a less verbose level is possible.
    /// </summary>
    /// <param name="logger">The sub-logger. This will <em>not</em> be shut down automatically when the
    /// parent logger is disposed.</param>
    /// <param name="restrictedToMinimumLevel">The minimum level for
    /// events passed through the sink.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="logger"/> is <code>null</code></exception>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public LoggerConfiguration Logger(
        ILogger logger,
        LogEventLevel restrictedToMinimumLevel)
        => Logger(logger, attemptDispose: false, restrictedToMinimumLevel);

    /// <summary>
    /// Write log events to a sub-logger, where further processing may occur. Events through
    /// the sub-logger will be constrained by filters and enriched by enrichers that are
    /// active in the parent. A sub-logger cannot be used to log at a more verbose level, but
    /// a less verbose level is possible.
    /// </summary>
    /// <param name="logger">The sub-logger.</param>
    /// <param name="attemptDispose">Whether to shut down automatically the sub-logger
    /// when the parent logger is disposed.</param>
    /// <param name="restrictedToMinimumLevel">The minimum level for
    /// events passed through the sink.</param>
    /// <param name="levelSwitch">A switch allowing the pass-through minimum level
    /// to be changed at runtime. Can be <code>null</code></param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="logger"/> is <code>null</code></exception>
    public LoggerConfiguration Logger(
        ILogger logger,
        bool attemptDispose = false,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch? levelSwitch = null)
    {
        Guard.AgainstNull(logger);

        if (logger is Logger { HasOverrideMap: true })
        {
            SelfLog.WriteLine("Minimum level overrides are not supported on sub-loggers " +
                              "and may be removed completely in a future version.");
        }

        var secondarySink = new SecondaryLoggerSink(logger, attemptDispose: attemptDispose);
        return Sink(secondarySink, restrictedToMinimumLevel, levelSwitch);
    }

    /// <summary>
    /// Write to a sink only when <paramref name="condition"/> evaluates to <c>true</c>.
    /// </summary>
    /// <param name="condition">A predicate that evaluates to <c>true</c> when the supplied <see cref="LogEvent"/>
    /// should be written to the configured sink.</param>
    /// <param name="configureSink">An action that configures the wrapped sink.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="condition"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="configureSink"/> is <code>null</code></exception>
    public LoggerConfiguration Conditional(Func<LogEvent, bool> condition, Action<LoggerSinkConfiguration> configureSink)
    {
        Guard.AgainstNull(condition);
        Guard.AgainstNull(configureSink);

        // Level aliases and so on don't need to be accepted here; if the user wants both a condition and leveling, they
        // can specify `restrictedToMinimumLevel` etc in the wrapped sink configuration.
        return Wrap(this, s => new ConditionalSink(s, condition), configureSink);
    }

    /// <summary>
    /// Helper method for wrapping sinks.
    /// </summary>
    /// <param name="loggerSinkConfiguration">The parent sink configuration.</param>
    /// <param name="wrapSink">A function that allows for wrapping <see cref="ILogEventSink"/>s
    /// added in <paramref name="configureWrappedSink"/>.</param>
    /// <param name="configureWrappedSink">An action that configures sinks to be wrapped in <paramref name="wrapSink"/>.</param>
    /// <param name="restrictedToMinimumLevel">The minimum level for
    /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
    /// <param name="levelSwitch">A switch allowing the pass-through minimum level
    /// to be changed at runtime. Can be <code>null</code></param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="loggerSinkConfiguration"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="wrapSink"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="configureWrappedSink"/> is <code>null</code></exception>
    public static LoggerConfiguration Wrap(
        LoggerSinkConfiguration loggerSinkConfiguration,
        Func<ILogEventSink, ILogEventSink> wrapSink,
        Action<LoggerSinkConfiguration> configureWrappedSink,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch? levelSwitch = null)
    {
        Guard.AgainstNull(loggerSinkConfiguration);
        Guard.AgainstNull(wrapSink);
        Guard.AgainstNull(configureWrappedSink);

        var sinksToWrap = new List<ILogEventSink>();

        var capturingConfiguration = new LoggerConfiguration();
        var capturingLoggerSinkConfiguration = new LoggerSinkConfiguration(
            capturingConfiguration,
            sinksToWrap.Add);

        // `WriteTo.Sink()` will return the capturing configuration; this ensures chained `WriteTo` gets back
        // to the capturing sink configuration, enabling `WriteTo.X().WriteTo.Y()`.
        capturingConfiguration.WriteTo = capturingLoggerSinkConfiguration;

        configureWrappedSink(capturingLoggerSinkConfiguration);

        if (sinksToWrap.Count == 0)
            return loggerSinkConfiguration._loggerConfiguration;

        var enclosed = sinksToWrap.Count == 1 ?
            sinksToWrap.Single() :
            new DisposingAggregateSink(sinksToWrap);

        var wrapper = wrapSink(enclosed);
        if (wrapper is not IDisposable && enclosed is IDisposable
#if FEATURE_ASYNCDISPOSABLE
                or IAsyncDisposable
#endif
           )
        {
            wrapper = new DisposeDelegatingSink(wrapper, enclosed as IDisposable
#if FEATURE_ASYNCDISPOSABLE
                , enclosed as IAsyncDisposable
#endif
                );
        }

        return loggerSinkConfiguration.Sink(wrapper, restrictedToMinimumLevel, levelSwitch);
    }
}
