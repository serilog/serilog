using System;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Observable;

namespace Serilog
{
    public static class ObservableLoggerConfigurationExtensions
    {
        /// <summary>
        /// Write events to Rx observers.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="configureObservers">An action that provides an observable
        /// to which observers can subscribe.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration Observers(
            this LoggerSinkConfiguration sinkConfiguration,
            Action<IObservable<LogEvent>> configureObservers,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
        {
            if (configureObservers == null) throw new ArgumentNullException(nameof(configureObservers));
            var observable = new ObservableSink();
            configureObservers(observable);
            return sinkConfiguration.Sink(observable, restrictedToMinimumLevel, levelSwitch);
        }
    }
}