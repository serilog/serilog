using System.Diagnostics.Metrics;

namespace Serilog.Debugging;

static class SelfMetrics
{
    static readonly Meter Meter = new("Serilog", typeof(Log).Assembly.GetName().Version?.ToString());

    public static class TagNames
    {
        public const string LoggingFailureKind = "serilog.logging_failure_kind";
    }

    // Most applications should create only a single pipeline; creating and disposing multiple pipelines isn't a
    // typical usage pattern so we don't try to record disposal. In general, applications that see more than a handful
    // of increments probably have logger lifecycle bugs to resolve.
    public static readonly Counter<long> PipelineCreated = Meter.CreateCounter<long>(
        "serilog.pipeline.created",
        unit: "{pipeline}",
        description: "The number of full logging pipelines constructed.");

    public static readonly Counter<long> PipelineEventEmitted = Meter.CreateCounter<long>(
        "serilog.pipeline.event_emitted",
        unit: "{event}",
        description: "The number of events dispatched to sinks through the logging pipeline.");

    public static readonly Counter<long> DiagnosticsSelfLogWrites = Meter.CreateCounter<long>(
        "serilog.diagnostics.self_log_writes",
        unit: "{write}",
        description: "The number of calls made to write lines to Serilog's internal diagnostic log.");

    public static readonly Counter<long> DiagnosticsDefaultFailureListenerLoggingFailures = Meter.CreateCounter<long>(
        "serilog.diagnostics.default_failure_listener.logging_failures",
        unit: "{failure}",
        description: "The number of logging failures to reach the default failure listener.");
}
