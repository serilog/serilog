using Serilog.Events;

namespace Serilog
{
    /// <summary>
    /// Extension method 'ForContext' for ILogger.
    /// </summary>
    public static class ForContextExtension
    {
        /// <summary>
        /// Create a logger that enriches log events with the specified property based on log event level.
        /// </summary>
        /// <typeparam name="TValue"> The type of the property value. </typeparam>
        /// <param name="logger">The logger</param>
        /// <param name="level">The log event level used to determine if log is enriched with property.</param>
        /// <param name="propertyName">The name of the property. Must be non-empty.</param>
        /// <param name="value">The property value.</param>
        /// <param name="destructureObjects">If true, the value will be serialized as a structured
        /// object if possible; if false, the object will be recorded as a scalar or simple array.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        /// <returns></returns>
        public static ILogger ForContext<TValue>(
            this ILogger logger,
            LogEventLevel level,
            string propertyName,
            TValue value,
            bool destructureObjects = false)
        {
            return !logger.IsEnabled(level)
                ? logger
                : logger.ForContext(propertyName, value, destructureObjects);
        }
    }
}
