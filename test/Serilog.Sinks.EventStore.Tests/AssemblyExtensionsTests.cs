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

using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace Serilog.Sinks.EventStore.Tests
{
    /// <summary>
    /// Test the <see cref="AssemblyExtensions"/> class.
    /// </summary>
    [TestFixture]
    public class AssemblyExtensionsTests
    {
        /// <summary>
        /// Test that an <see cref="ArgumentNullException"/> is thrown when the <see cref="Assembly"/> passed in is null.
        /// </summary>
        [Test]
        public void GetExecutingFolder_ThrowsAnArgumentNullExceptionWhenTheAssemblyPassedInIsNull()
        {
            Assembly asm = null;
            Assert.Throws<ArgumentNullException>(() => asm.GetExecutingFolder());
        }
        
        /// <summary>
        /// test that the executing folder of the passed in <see cref="Assembly"/> is returned.
        /// </summary>
        [Test]
        public void GetExecutingFolder_ReturnsTheExecutingFolderOfThePassedInAssembly()
        {
            string executingAssemblyDirectory = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            Assert.That(executingAssemblyDirectory, Is.EqualTo(Assembly.GetExecutingAssembly().GetExecutingFolder()));
        }
        }
}