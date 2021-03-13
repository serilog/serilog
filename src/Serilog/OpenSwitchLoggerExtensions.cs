using Serilog.Core;

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="IOpenSwitchLogger" /> with additional methods.
    /// </summary>
    public static class OpenSwitchLoggerExtensions
    {
        /// <summary>
        /// Create a logger that allows adjusting logging levels via an interface.
        /// </summary>
        public static IOpenSwitchLogger AsOpenSwitch(this ILogger baseLogger) =>
            new OpenSwitchLogger(baseLogger);
    }
}
