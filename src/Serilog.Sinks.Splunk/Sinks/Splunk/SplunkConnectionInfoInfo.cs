using Splunk;

namespace Serilog.Sinks.Splunk
{
    public class SplunkConnectionInfoInfo : ISplunkConnectionInfo
    {
        public ServiceArgs ServiceArgs { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SplunkSource { get; set; }
        public string SplunkEventType { get; set; }
    }
}