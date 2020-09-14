// Copyright 2013-2017 Serilog Contributors
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

using System.Runtime.CompilerServices;

namespace Serilog.Rendering
{
    static class Casing
    {
        /// <summary>
        /// Apply upper or lower casing to <paramref name="value"/> when <paramref name="format"/> is provided.
        /// Returns <paramref name="value"/> when no or invalid format provided
        /// </summary>
        /// <returns>The provided <paramref name="value"/> with formatting applied</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Format(string value, string format = null)
        {
            return format switch
            {
                "u" => value.ToUpperInvariant(),
                "w" => value.ToLowerInvariant(),
                _ => value
            };
        }
    }
}
