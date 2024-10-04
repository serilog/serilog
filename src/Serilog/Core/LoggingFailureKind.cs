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
/// Describes the reason for a logging failure.
/// </summary>
public enum LoggingFailureKind
{
    /// <summary>
    /// A failure has occured; the situation may resolve and if any events are associated with the failure,
    /// logging will be retried by the reporting sink.
    /// </summary>
    Temporary,
    /// <summary>
    /// A failure has occurred; any events associated with the failure will not be retried by the reporting sink.
    /// </summary>
    Permanent,
    /// <summary>
    /// A failure has occurred; the reporting sink is going offline and no retries will be attempted.
    /// </summary>
    Final
}