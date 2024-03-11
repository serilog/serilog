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

    [Fact]
    public void UnstableAssembleFromPartsDoesNotAllocateDictionary()
    {
        var properties = new Dictionary<string, LogEventPropertyValue>();

        var timestamp = Some.OffsetInstant();
        var level = Warning;
        var exception = Some.Exception();
        var template = Some.MessageTemplate();
        var traceId = ActivityTraceId.CreateRandom();
        var spanId = ActivitySpanId.CreateRandom();

        var evt = LogEvent.UnstableAssembleFromParts(
            timestamp,
            level,
            exception,
            template,
            properties,
            traceId,
            spanId);

        // Test is named according to this assertion because it's the reason for the existence of
        // the UnstableAssembleFromParts() method.
        Assert.Same(properties, evt.Properties);

        Assert.Equal(timestamp, evt.Timestamp);
        Assert.Equal(level, evt.Level);
        Assert.Same(exception, evt.Exception);
        Assert.Same(template, evt.MessageTemplate);
        Assert.Equal(traceId, evt.TraceId!.Value);
        Assert.Equal(spanId, evt.SpanId!.Value);
    }
}
