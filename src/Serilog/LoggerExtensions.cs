using System;
using Serilog.Events;
using System.Linq;

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="ILogger" /> with additional methods.
    /// </summary>
    public static class LoggerExtensions
    {
        #region Nested Types

        private class LazyPropertyValue
        {
            #region Constructors

            public LazyPropertyValue(Func<object> valueAccessor)
            {
                _valueAccessor = valueAccessor;
            }

            #endregion Constructors

            #region Fields

            private readonly Func<object> _valueAccessor;

            #endregion Fields

            #region Methods

            #region Public Methods

            public override string ToString()
            {
                return _valueAccessor?.Invoke()?.ToString();
            }

            #endregion Public Methods

            #endregion Methods
        }

        #endregion Nested Types

        #region Methods

        #region Public Static Methods

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level and message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValueAccessors">Objects positionally formatted into the message template.</param>
        public static void Debug(this ILogger logger, string messageTemplate, params Func<object>[] propertyValueAccessors)
        {
            logger.Debug(messageTemplate, propertyValueAccessors.Select(x => new LazyPropertyValue(x)));
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValueAccessors">Objects positionally formatted into the message template.</param>
        public static void Error(this ILogger logger, string messageTemplate, params Func<object>[] propertyValueAccessors)
        {
            logger.Error(messageTemplate, propertyValueAccessors.Select(x => new LazyPropertyValue(x)));
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValueAccessors">Objects positionally formatted into the message template.</param>
        public static void Fatal(this ILogger logger, string messageTemplate, params Func<object>[] propertyValueAccessors)
        {
            logger.Fatal(messageTemplate, propertyValueAccessors.Select(x => new LazyPropertyValue(x)));
        }

        /// <summary>
        /// Create a logger that enriches log events when the specified level is enabled.
        /// </summary>
        /// <typeparam name="TValue"> The type of the property value. </typeparam>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The log event level used to determine if log is enriched with property.</param>
        /// <param name="propertyName">The name of the property. Must be non-empty.</param>
        /// <param name="value">The property value.</param>
        /// <param name="destructureObjects">If true, the value will be serialized as a structured
        /// object if possible; if false, the object will be recorded as a scalar or simple array.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="logger"/> is <code>null</code></exception>
        public static ILogger ForContext<TValue>(
            this ILogger logger,
            LogEventLevel level,
            string propertyName,
            TValue value,
            bool destructureObjects = false)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            return logger.IsEnabled(level)
                ? logger.ForContext(propertyName, value, destructureObjects)
                : logger;
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level and message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValueAccessors">Objects positionally formatted into the message template.</param>
        public static void Information(this ILogger logger, string messageTemplate, params Func<object>[] propertyValueAccessors)
        {
            logger.Information(messageTemplate, propertyValueAccessors.Select(x => new LazyPropertyValue(x)));
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValueAccessors">Objects positionally formatted into the message template.</param>
        public static void Verbose(this ILogger logger, string messageTemplate, params Func<object>[] propertyValueAccessors)
        {
            logger.Verbose(messageTemplate, propertyValueAccessors.Select(x => new LazyPropertyValue(x)));
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level and message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValueAccessors">Objects positionally formatted into the message template.</param>
        public static void Warning(this ILogger logger, string messageTemplate, params Func<object>[] propertyValueAccessors)
        {
            logger.Warning(messageTemplate, propertyValueAccessors.Select(x => new LazyPropertyValue(x)));
        }

        /// <summary>
        /// Write a log event with the specified level and message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValueAccessors">Objects positionally formatted into the message template.</param>
        public static void Write(this ILogger logger, LogEventLevel level, string messageTemplate, params Func<object>[] propertyValueAccessors)
        {
            logger.Write(level, messageTemplate, propertyValueAccessors.Select(x => new LazyPropertyValue(x)));
        }

        #endregion Public Static Methods

        #endregion Methods
    }
}
