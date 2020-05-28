using System;

namespace Serilog.Configuration
{
    /// <summary>
    /// Controls time provider configuration.
    /// </summary>
    public class LoggerTimeProviderConfiguration
    {
        readonly LoggerConfiguration _loggerConfiguration;
        readonly Action<Func<DateTimeOffset>> _setTimeProvider;

        internal LoggerTimeProviderConfiguration(LoggerConfiguration loggerConfiguration, Action<Func<DateTimeOffset>> setLevelSwitch)
        {
            _loggerConfiguration = loggerConfiguration ?? throw new ArgumentNullException(nameof(loggerConfiguration));
            _setTimeProvider = setLevelSwitch;
        }

        /// <summary>
        /// Sets the time provider, which will be used for creating the time stamps of the log events.
        /// </summary>
        /// <param name="timeProvider">The time provider.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration SetTo(Func<DateTimeOffset> timeProvider)
        {
            if (timeProvider == null) throw new ArgumentNullException(nameof(timeProvider));
            _setTimeProvider(timeProvider);
            return _loggerConfiguration;
        }
    }
}
