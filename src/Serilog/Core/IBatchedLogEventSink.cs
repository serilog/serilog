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

namespace Serilog.Core;

/// <summary>
/// A destination that accepts events in batches. Many sinks gain a performance advantage by handling events in batches,
/// for example to combine multiple events into a single network request to a remote collector. Because the client
/// application cannot wait for every event to be flushed when batching is used, batched sinks normally work
/// asynchronously to conserve local resources while batches are sent.
/// </summary>
/// <seealso cref="ILogEventSink"/>
public interface IBatchedLogEventSink
{
    /// <summary>
    /// Emit a batch of log events, asynchronously.
    /// </summary>
    /// <param name="batch">The batch of events to emit.</param>
    /// <remarks>Implementers should allow exceptions to propagate when batches fail. The batching infrastructure
    /// handles exception handling, diagnostics, and retries.</remarks>
    Task EmitBatchAsync(IReadOnlyCollection<LogEvent> batch);

    /// <summary>
    /// Allows sinks to perform periodic work without requiring additional threads
    /// or timers (thus avoiding additional flush/shut-down complexity).
    /// </summary>
    Task OnEmptyBatchAsync()
#if FEATURE_DEFAULT_INTERFACE
    {
        return Task.CompletedTask;
    }
#else
        ;
#endif
}