using System.Diagnostics;

namespace Serilog.Tests.Events;

public class LogEventTests
{
    [Fact]
    public void CopiedLogEventsPropagateTraceAndSpan()
    {
        var traceId = ActivityTraceId.CreateRandom();
        var spanId = ActivitySpanId.CreateRandom();
        var evt = Some.LogEvent(traceId: traceId, spanId: spanId);
        var copy = evt.Copy();
        Assert.Equal(traceId, copy.TraceId);
        Assert.Equal(spanId, copy.SpanId);
    }
}
