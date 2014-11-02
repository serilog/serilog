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