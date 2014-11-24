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

using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.IOFile
{
    /// <summary>
    /// An instance of this sink may be substituted when an instance of the
    /// <see cref="FileSink"/> is unable to be constructed.
    /// </summary>
    class NullSink : ILogEventSink
    {
        public void Emit(LogEvent logEvent)
        {
        }
    }
}
