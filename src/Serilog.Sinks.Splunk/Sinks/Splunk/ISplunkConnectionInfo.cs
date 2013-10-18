using Splunk;

namespace Serilog.Sinks.Splunk
{
    public interface ISplunkConnectionInfo
    {
        ServiceArgs ServiceArgs { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string SplunkSource { get; set; }
        string SplunkEventType { get; set; }
    }
}