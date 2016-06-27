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

namespace Serilog.Core
{
    /// <summary>
    /// Constants used in the core logging pipeline and associated types.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The name of the property included in the emitted log events
        /// when <code>ForContext&lt;T&gt;()</code> and overloads are
        /// applied.
        /// </summary>
        public const string SourceContextPropertyName = "SourceContext";
    }
}
