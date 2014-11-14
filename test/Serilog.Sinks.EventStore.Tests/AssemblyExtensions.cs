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

namespace Serilog.Sinks.EventStore.Tests
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Class containing methids extending the <see cref="Assembly"/> class.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Retrieve the executing filder of the <see cref="Assembly"/> passed in.
        /// </summary>
        /// <param name="asm">The assembly who's folder you wish to retrieve.</param>
        /// <returns>The assemblies executing folder.</returns>
        public static string GetExecutingFolder(this Assembly asm)
        {
            if (asm == null)
            {
                throw new ArgumentNullException("The assembly passed in is null.");
            }
            
            return Path.GetDirectoryName(new Uri(asm.CodeBase).LocalPath);
        }
    }
}
