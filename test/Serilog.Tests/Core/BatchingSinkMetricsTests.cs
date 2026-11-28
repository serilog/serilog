using System.Diagnostics.Metrics;
using Serilog.Core.Sinks.Batching;

namespace Serilog.Core.Tests;

public class BatchingSinkMetricsTests
{
    const string MeterName = "Serilog";
    const string InstrumentName = "serilog.batching.emit_batch.duration";
    const string BatchedSinkTypeTagName = "serilog.batched_sink_type";
    const string ErrorTypeTagName = "error.type";

    static readonly TimeSpan EmitDuration = TimeSpan.FromMilliseconds(50);

    [Fact]
    public void TheDurationOfSuccessfulBatchesIsRecorded()
    {
        using var collector = new MeasurementCollector<DelayingBatchedSink>();

        var bs = new DelayingBatchedSink();
        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = 10, BufferingTimeLimit = TimeSpan.FromMilliseconds(100), EagerlyEmitFirstEvent = true });
        pbs.Emit(Some.InformationEvent());
        pbs.Dispose();

        var measurement = Assert.Single(collector.Measurements);
        Assert.True(
            measurement.Value >= EmitDuration.TotalMilliseconds / 2,
            $"Expected at least {EmitDuration.TotalMilliseconds / 2} ms to be recorded; the value was {measurement.Value}.");
        Assert.DoesNotContain(measurement.Tags, tag => tag.Key == ErrorTypeTagName);
    }

    [Fact]
    public void FailedBatchesAreRecordedWithTheErrorType()
    {
        using var collector = new MeasurementCollector<ThrowingBatchedSink>();

        var bs = new ThrowingBatchedSink();
        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = 10, BufferingTimeLimit = TimeSpan.FromMilliseconds(100), EagerlyEmitFirstEvent = true });
        pbs.SetFailureListener(new CollectingFailureListener());
        pbs.Emit(Some.InformationEvent());
        pbs.Dispose();

        Assert.NotEmpty(collector.Measurements);
        Assert.All(
            collector.Measurements,
            measurement => Assert.Contains(
                measurement.Tags,
                tag => tag.Key == ErrorTypeTagName && (string?)tag.Value == typeof(InvalidOperationException).FullName));
    }

    class DelayingBatchedSink : IBatchedLogEventSink
    {
        public Task EmitBatchAsync(IReadOnlyCollection<LogEvent> batch) => Task.Delay(EmitDuration);

        public Task OnEmptyBatchAsync() => Task.CompletedTask;
    }

    class ThrowingBatchedSink : IBatchedLogEventSink
    {
        public Task EmitBatchAsync(IReadOnlyCollection<LogEvent> batch) =>
            throw new InvalidOperationException("The batch could not be accepted.");

        public Task OnEmptyBatchAsync() => Task.CompletedTask;
    }

    // The meter is static and shared, and test classes run in parallel, so measurements are matched on the
    // tag identifying the target sink: a test that uses a sink type of its own will see only its own batches.
    sealed class MeasurementCollector<TBatchedSink> : IDisposable
        where TBatchedSink : IBatchedLogEventSink
    {
        readonly MeterListener _listener = new();
        readonly object _sync = new();
        readonly List<Measurement> _measurements = [];

        public MeasurementCollector()
        {
            _listener.InstrumentPublished = (instrument, listener) =>
            {
                if (instrument.Meter.Name == MeterName && instrument.Name == InstrumentName)
                {
                    listener.EnableMeasurementEvents(instrument);
                }
            };

            _listener.SetMeasurementEventCallback<double>((_, value, tags, _) =>
            {
                var recorded = tags.ToArray();
                if (!recorded.Any(tag => tag.Key == BatchedSinkTypeTagName &&
                                         (string?)tag.Value == typeof(TBatchedSink).FullName))
                    return;

                lock (_sync)
                {
                    _measurements.Add(new(value, recorded));
                }
            });

            _listener.Start();
        }

        public IReadOnlyList<Measurement> Measurements
        {
            get
            {
                lock (_sync)
                {
                    return _measurements.ToArray();
                }
            }
        }

        public void Dispose()
        {
            _listener.Dispose();
        }

        public readonly record struct Measurement(double Value, KeyValuePair<string, object?>[] Tags);
    }
}
