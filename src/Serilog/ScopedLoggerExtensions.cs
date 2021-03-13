using Serilog.Core;

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="IScopedLogger" /> with additional methods.
    /// </summary>
    public static class ScopedLoggerExtensions
    {
        /// <summary>
        /// Create a logger that allows adjusting logging levels per Task scope.
        /// </summary>
        public static IScopedLogger AsScoped(this ILogger baseLogger) =>
            new ScopedLogger(baseLogger);
    }
}
