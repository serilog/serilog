using XSockets.Core.Common.Utility.Logging;
using XSockets.Core.XSocket;
using XSockets.Plugin.Framework.Attributes;

namespace Serilog.Sinks.XSockets
{
    /// <summary>
    /// The XSockets controller used for dispatching loginformation
    /// </summary>
    [XSocketMetadata(PluginAlias = "log")]
    public class LogController : XSocketController
    {
        /// <summary>
        /// Each client can set the LogEventLevel of interest.
        /// </summary>
        public LogEventLevel LogEventLevel { get; set; }

        /// <summary>
        /// Ctor - By default LogEventLevel will be "Information"
        /// </summary>
        public LogController()
        {
            this.LogEventLevel = LogEventLevel.Information;
        }
    }
}