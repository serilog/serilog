using NUnit.Framework;
using Serilog.Sinks.RollingFile.SizeOnly;

namespace Serilog.Tests.Sinks.RollingFile.Size
{
    [TestFixture]
    public class FileNameComponentsTests
    {
        [Test]
        public void RendersCorrectlyWithExtension()
        {
            var sut = new FileNameComponents("name", 0, "txt");
            Assert.That(sut.FullName, Is.EqualTo("name00000.txt"));
        }

        [Test]
        public void RendersCorrectlyWithoutExtension()
        {
            var sut = new FileNameComponents("name", 1200, null);
            Assert.That(sut.FullName, Is.EqualTo("name01200"));
        }
    }
}