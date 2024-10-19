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

using Serilog.Core.Sinks.Batching;

namespace Serilog.Configuration;

/// <summary>
/// Initialization options for <see cref="BatchingSink"/>.
/// </summary>
public class BatchingOptions
{
    /// <summary>
    /// Eagerly emit a batch containing the first received event, regardless of
    /// the target batch size or batching time. This helps with perceived "liveness"
    /// when running/debugging applications interactively. The default is <c>true</c>.
    /// </summary>
    public bool EagerlyEmitFirstEvent { get; set; } = true;

    /// <summary>
    /// The maximum number of events to include in a single batch. The default is <c>1000</c>.
    /// </summary>
    public int BatchSizeLimit { get; set; } = 1000;

    /// <summary>
    /// The maximum delay between event batches. The default is two seconds. If a batch can be filled
    /// before the buffering time limit is reached, it will be emitted without waiting.
    /// </summary>
    public TimeSpan BufferingTimeLimit { get; set; } = TimeSpan.FromSeconds(2);

    /// <summary>
    /// Maximum number of events to hold in the sink's internal queue, or <c>null</c>
    /// for an unbounded queue. The default is <c>100000</c>. When the limit is exceeded,
    /// backpressure is applied.
    /// </summary>
    public int? QueueLimit { get; set; } = 100000;

    /// <summary>
    /// The maximum time that the sink will keep retrying failed batches for. The default is ten minutes. Lower
    /// this value to reduce buffering and backpressure in high-load scenarios.
    /// </summary>
    public TimeSpan RetryTimeLimit { get; set; } = TimeSpan.FromMinutes(10);
}
