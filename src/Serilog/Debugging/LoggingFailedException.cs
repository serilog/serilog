// Copyright 2013-2015 Serilog Contributors
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

namespace Serilog.Debugging
{
    /// <summary>
    /// May be thrown by log event sinks when a failure occurs. Should not be used in cases
    /// where the exception would propagate out to callers.
    /// </summary>
    public class LoggingFailedException : Exception
    {
        /// <summary>
        /// Construct a <see cref="LoggingFailedException"/> to communicate a logging failure.
        /// </summary>
        /// <param name="message">A message describing the logging failure.</param>
        public LoggingFailedException(string message)
            : base(message)
        {
        }
    }
}
