// Copyright © Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Serilog.Core.Sinks.Batching;

/// <summary>
/// Manages reconnection period and transient fault response for <see cref="BatchingSink"/>.
/// During normal operation an object of this type will simply echo the configured batch transmission
/// period. When availability fluctuates, the class tracks the number of failed attempts, each time
/// increasing the interval before reconnection is attempted (up to a set maximum) and at predefined
/// points indicating that either the current batch, or entire waiting queue, should be dropped. This
/// Serves two purposes - first, a loaded receiver may need a temporary reduction in traffic while coming
/// back online. Second, the sender needs to account for both bad batches (the first fault response) and
/// also overproduction (the second, queue-dropping response). In combination these should provide a
/// reasonable delivery effort but ultimately protect the sender from memory exhaustion.
/// </summary>
class FailureAwareBatchScheduler
{
    static readonly TimeSpan MinimumBackoffPeriod = TimeSpan.FromSeconds(5);
    static readonly TimeSpan MaximumBackoffInterval = TimeSpan.FromMinutes(1);
    const int DroppedBatchesBeforeDroppingQueue = 10;

    readonly TimeSpan _bufferingTimeLimit, _retryTimeLimit;
    readonly TimeProvider _timeProvider;

    long? _firstFailureTimestamp;
    int _failuresSinceSuccessfulBatch, _consecutiveDroppedBatches;

    public FailureAwareBatchScheduler(TimeSpan bufferingTimeLimit, TimeSpan retryTimeLimit)
        : this(bufferingTimeLimit, retryTimeLimit, TimeProvider.System)
    {
    }

    internal FailureAwareBatchScheduler(TimeSpan bufferingTimeLimit, TimeSpan retryTimeLimit, TimeProvider timeProvider)
    {
        if (bufferingTimeLimit < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(bufferingTimeLimit), "The batching period must be a positive timespan.");

        if (retryTimeLimit < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(retryTimeLimit), "The retry time limit must be a positive timespan.");

        _bufferingTimeLimit = bufferingTimeLimit;
        _retryTimeLimit = retryTimeLimit;
        _timeProvider = timeProvider;
    }

    public void MarkSuccess()
    {
        _failuresSinceSuccessfulBatch = 0;
        _consecutiveDroppedBatches = 0;
        _firstFailureTimestamp = null;
    }

    public void MarkFailure(out bool shouldDropBatch, out bool shouldDropQueue)
    {
        ++_failuresSinceSuccessfulBatch;
        _firstFailureTimestamp ??= _timeProvider.GetTimestamp();

        // Once we're up against the time limit, we'll try each subsequent batch once and then drop it.
        var now = _timeProvider.GetElapsedTime(_firstFailureTimestamp.Value);
        var wouldRetryAt = now.Add(NextInterval);
        shouldDropBatch = wouldRetryAt >= _retryTimeLimit;

        if (shouldDropBatch)
        {
            ++_consecutiveDroppedBatches;
        }

        // After trying and dropping enough batches consecutively, we'll try to get out of the way and just drop
        // everything after each subsequent failure. Each time a batch is tried and fails, we'll drop it and
        // drain the whole queue.
        shouldDropQueue = _consecutiveDroppedBatches >= DroppedBatchesBeforeDroppingQueue;
    }

    public TimeSpan NextInterval
    {
        get
        {
            // Available, and first failure, just try the batch interval
            if (_failuresSinceSuccessfulBatch <= 1) return _bufferingTimeLimit;

            // Second failure, start ramping up the interval - first 2x, then 4x, ...
            var backoffFactor = Math.Pow(2, _failuresSinceSuccessfulBatch - 1);

            // If the period is ridiculously short, give it a boost so we get some
            // visible backoff.
            var backoffPeriod = Math.Max(_bufferingTimeLimit.Ticks, MinimumBackoffPeriod.Ticks);

            // The "ideal" interval
            var backedOff = (long) (backoffPeriod * backoffFactor);

            // Capped to the maximum interval
            var cappedBackoff = Math.Min(MaximumBackoffInterval.Ticks, backedOff);

            // Unless that's shorter than the period, in which case we'll just apply the period
            var actual = Math.Max(_bufferingTimeLimit.Ticks, cappedBackoff);

            return TimeSpan.FromTicks(actual);
        }
    }
}
