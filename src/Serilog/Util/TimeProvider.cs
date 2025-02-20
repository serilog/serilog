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

#if !NET8_0_OR_GREATER

using System.Diagnostics;

namespace System;

/// <summary>
/// A super-simple, cut-down subset of `System.TimeProvider` which we use internally to avoid a package dependency
/// on platforms without it.
/// </summary>
abstract class TimeProvider
{
    public static TimeProvider System { get; } = new SystemTimeProvider();

    public DateTimeOffset GetLocalNow() => DateTimeOffset.Now;

    public virtual DateTimeOffset GetUtcNow() => DateTimeOffset.UtcNow;

    public virtual long TimestampFrequency => Stopwatch.Frequency;

    public virtual long GetTimestamp() => Stopwatch.GetTimestamp();

    public TimeSpan GetElapsedTime(long startingTimestamp, long endingTimestamp)
    {
        // Assumes Stopwatch.Frequency is never zero, safe for our internal usage.
        return new TimeSpan((long)((endingTimestamp - startingTimestamp) * ((double)TimeSpan.TicksPerSecond / TimestampFrequency)));
    }

    public TimeSpan GetElapsedTime(long startingTimestamp) => GetElapsedTime(startingTimestamp, GetTimestamp());

    sealed class SystemTimeProvider : TimeProvider;
}

#endif
