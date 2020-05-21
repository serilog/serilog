using Serilog.Events;

namespace Serilog.Core
{
    /// <summary>
    /// Dynamically controls logging level.
    /// </summary>
    public interface ILoggingLevelSwitch
    {
        /// <summary>
        /// The current minimum level, below which no events
        /// should be generated.
        /// </summary>
        LogEventLevel MinimumLevel { get; }
    }
}
