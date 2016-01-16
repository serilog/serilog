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

using Serilog.Events;

namespace Serilog.Core
{
    /// <summary>
    /// Determine how, when destructuring, a supplied value is represented
    /// as a complex log event property.
    /// </summary>
    public interface IDestructuringPolicy
    {
        /// <summary>
        /// If supported, destructure the provided value.
        /// </summary>
        /// <param name="value">The value to destructure.</param>
        /// <param name="propertyValueFactory">Recursively apply policies to destructure additional values.</param>
        /// <param name="result">The destructured value, or null.</param>
        /// <returns>True if the value could be destructured under this policy.</returns>
        bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result);
    }
}
