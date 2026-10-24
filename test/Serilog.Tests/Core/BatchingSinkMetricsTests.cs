using System.Diagnostics.Metrics;
using Serilog.Core.Sinks.Batching;

namespace Serilog.Core.Tests;

public class BatchingSinkMetricsTests
{
    const string MeterName = "Serilog";
    const string EmitBatchDurationInstrumentName = "serilog.batching.emit_batch.duration";
    const string EmitBatchSizeInstrumentName = "serilog.batching.emit_batch.size";
    const string DroppedEventsInstrumentName = "serilog.batching.dropped_events";
    const string BatchedSinkTypeTagName = "serilog.batched_sink_type";
    const string ErrorTypeTagName = "error.type";

    static readonly TimeSpan EmitDuration = TimeSpan.FromMilliseconds(50);

    [Fact]
    public void TheDurationOfSuccessfulBatchesIsRecorded()
    {
        using var collector = new MeasurementCollector<DelayingBatchedSink>(EmitBatchDurationInstrumentName);

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
        using var collector = new MeasurementCollector<ThrowingBatchedSink>(EmitBatchDurationInstrumentName);

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

    [Fact]
    public void TheSizeOfEmittedBatchesIsRecorded()
    {
        using var collector = new MeasurementCollector<CountingBatchedSink>(EmitBatchSizeInstrumentName);

        const int emitted = 25;

        var bs = new CountingBatchedSink();
        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = 10, BufferingTimeLimit = TimeSpan.FromMilliseconds(100), EagerlyEmitFirstEvent = true });
        for (var i = 0; i < emitted; ++i)
        {
            pbs.Emit(Some.InformationEvent());
        }
        pbs.Dispose();

        Assert.Equal(emitted, bs.Events);
        Assert.Equal(bs.Batches, collector.Measurements.Count);
        Assert.Equal(bs.Events, collector.Measurements.Sum(measurement => measurement.Value));
    }

    [Fact]
    public void EventsDroppedByAFullQueueAreCounted()
    {
        using var collector = new MeasurementCollector<BlockingBatchedSink>(DroppedEventsInstrumentName);

        const int queueLimit = 1;
        const int expectedDropped = 9;

        var bs = new BlockingBatchedSink();
        var pbs = new BatchingSink(bs, new() { BatchSizeLimit = 1, BufferingTimeLimit = TimeSpan.FromMilliseconds(10), QueueLimit = queueLimit });

        pbs.Emit(Some.InformationEvent());
        Assert.True(
            bs.BatchReceived.Wait(TimeSpan.FromSeconds(10)),
            "The target sink did not receive a batch.");

        for (var i = 0; i < queueLimit + expectedDropped; ++i)
        {
            pbs.Emit(Some.InformationEvent());
        }

        bs.Release();
        pbs.Dispose();

        Assert.Equal(expectedDropped, collector.Measurements.Count);
        Assert.All(collector.Measurements, measurement => Assert.Equal(1, measurement.Value));
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

    class CountingBatchedSink : IBatchedLogEventSink
    {
        public int Batches { get; private set; }

        public int Events { get; private set; }

        public Task EmitBatchAsync(IReadOnlyCollection<LogEvent> batch)
        {
            Batches++;
            Events += batch.Count;
            return Task.CompletedTask;
        }

        public Task OnEmptyBatchAsync() => Task.CompletedTask;
    }

    class BlockingBatchedSink : IBatchedLogEventSink
    {
        readonly TaskCompletionSource<object?> _released = new();
        readonly ManualResetEventSlim _batchReceived = new();

        public ManualResetEventSlim BatchReceived => _batchReceived;

        public Task EmitBatchAsync(IReadOnlyCollection<LogEvent> batch)
        {
            _batchReceived.Set();
            return _released.Task;
        }

        public Task OnEmptyBatchAsync() => Task.CompletedTask;

        public void Release() => _released.TrySetResult(null);
    }

    // The meter is static and shared, and test classes run in parallel, so measurements are matched on the
    // tag identifying the target sink: a test that uses a sink type of its own will see only its own batches.
    sealed class MeasurementCollector<TBatchedSink> : IDisposable
        where TBatchedSink : IBatchedLogEventSink
    {
        readonly MeterListener _listener = new();
        readonly object _sync = new();
        readonly List<Measurement> _measurements = [];

        public MeasurementCollector(string instrumentName)
        {
            _listener.InstrumentPublished = (instrument, listener) =>
            {
                if (instrument.Meter.Name == MeterName && instrument.Name == instrumentName)
                {
                    listener.EnableMeasurementEvents(instrument);
                }
            };

            _listener.SetMeasurementEventCallback<double>((_, value, tags, _) => Record(value, tags));
            _listener.SetMeasurementEventCallback<long>((_, value, tags, _) => Record(value, tags));
            _listener.SetMeasurementEventCallback<int>((_, value, tags, _) => Record(value, tags));

            _listener.Start();
        }

        void Record(double value, ReadOnlySpan<KeyValuePair<string, object?>> tags)
        {
            var recorded = tags.ToArray();
            if (!recorded.Any(tag => tag.Key == BatchedSinkTypeTagName &&
                                     (string?)tag.Value == typeof(TBatchedSink).FullName))
                return;

            lock (_sync)
            {
                _measurements.Add(new(value, recorded));
            }
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
