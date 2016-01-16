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
    /// Creates log event properties from regular .NET objects, applying policies as
    /// required.
    /// </summary>
    public interface ILogEventPropertyFactory
    {
        /// <summary>
        /// Construct a <see cref="LogEventProperty"/> with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        /// <param name="destructureObjects">If true, and the value is a non-primitive, non-array type,
        /// then the value will be converted to a structure; otherwise, unknown types will
        /// be converted to scalars, which are generally stored as strings.</param>
        /// <returns></returns>
        LogEventProperty CreateProperty(string name, object value, bool destructureObjects = false);
    }
}