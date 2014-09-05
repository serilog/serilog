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

using Serilog.Events;

namespace Serilog.Core
{
    /// <summary>
    /// Provides filtering of the properties logged within structured objects.
    /// </summary>
    /// <remarks>
    /// One key use for this kind of filter is to suppress nulls, but other uses
    /// could include suppressing credit card numbers, email addresses, ...
    /// that may have been accidentally included.
    /// </remarks>
    public interface ILogPropertyFilter
    {
        /// <summary>
        /// Returns true if the property should be logged. Otherwise, false.
        /// </summary>
        /// <param name="propertyName">The property being tested.</param>
        /// <param name="value">The value of property being tested.</param>
        /// <returns>True if the property is enabled by this filter. If false
        /// is returned, the property will be suppressed.</returns>
        bool IsAllowed(string propertyName, object value);
    }
}
