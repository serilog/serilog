// Copyright 2014 Serilog Contributors
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

using System;
using Splunk;

namespace Serilog.Sinks.Splunk
{
    // No XML documentation
    #pragma warning disable 1591

    /// <summary>
    /// An interface isn't required for this use case; by using the concrete type
    /// only, we gain the ability to add parameters without breaking consumers.
    /// </summary>
    [Obsolete("Please use the concrete SplunkConnectionInfo class instead.")]
    public interface ISplunkConnectionInfo
    {
        ServiceArgs ServiceArgs { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string SplunkSource { get; set; }
        string SplunkEventType { get; set; }
    }

    #pragma warning restore 1591
}