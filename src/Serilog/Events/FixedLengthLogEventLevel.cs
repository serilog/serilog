namespace Serilog.Events
{
    /// <summary>
    /// A fixed-length representation of <see cref="LogEventLevel"/>
    /// </summary>
    public enum FixedLengthLogEventLevel
    {
        /// <summary>
        /// Shortened version of <see cref="LogEventLevel.Verbose"/>
        /// </summary>
        VRB,

        /// <summary>
        /// Shortened version of <see cref="LogEventLevel.Debug"/>
        /// </summary>
        DBG,

        /// <summary>
        /// Shortened version of <see cref="LogEventLevel.Information"/>
        /// </summary>
        INF,

        /// <summary>
        /// Shortened version of <see cref="LogEventLevel.Warning"/>
        /// </summary>
        WRN,

        /// <summary>
        /// Shortened version of <see cref="LogEventLevel.Error"/>
        /// </summary>
        ERR,

        /// <summary>
        /// Shortened version of <see cref="LogEventLevel.Fatal"/>
        /// </summary>
        FTL
    }
}