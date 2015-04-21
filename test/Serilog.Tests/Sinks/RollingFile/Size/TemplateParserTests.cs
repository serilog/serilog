using System.ComponentModel;
using NUnit.Framework;
using Serilog.Sinks.RollingFile.SizeOnly;

namespace Serilog.Tests.Sinks.RollingFile.Size
{
    [TestFixture]
    public class TemplateParserTests
    {
        [Test]
        public void ParsesFullPathNoExtensionOrSequence()
        {
            const string fullPath = @"C:\logs\applog";
            var result = FileNameParser.ParseLogFileName(fullPath);
            Assert.That(result.Name, Is.EqualTo("applog"));
            Assert.That(result.Sequence, Is.EqualTo(0u));
            Assert.That(result.Extension, Is.EqualTo(string.Empty));
        }

        [Test]
        public void ParsesPartialPathNoExtensionOrSequence()
        {
            const string partialPath = @"logs\applog";
            var result = FileNameParser.ParseLogFileName(partialPath);
            Assert.That(result.Name, Is.EqualTo("applog"));
            Assert.That(result.Sequence, Is.EqualTo(0u));
            Assert.That(result.Extension, Is.EqualTo(string.Empty));
        }

        [Test]
        public void ParsesFullPathWithExtensionNoSequence()
        {
            const string fullPath = @"C:\logs\applog.txt";
            var result = FileNameParser.ParseLogFileName(fullPath);
            Assert.That(result.Name, Is.EqualTo("applog"));
            Assert.That(result.Sequence, Is.EqualTo(0u));
            Assert.That(result.Extension, Is.EqualTo("txt"));
        }

        [Test]
        public void ParsesPartialPathWithExtensionNoSequence()
        {
            const string partialPath = @"logs\applog.txt";
            var result = FileNameParser.ParseLogFileName(partialPath);
            Assert.That(result.Name, Is.EqualTo("applog"));
            Assert.That(result.Sequence, Is.EqualTo(0u));
            Assert.That(result.Extension, Is.EqualTo("txt"));
        }

        [Test]
        public void ParsesFullPathWithExtensionAndSequence()
        {
            const string fullPath = @"C:\logs\applog00000.txt";
            var result = FileNameParser.ParseLogFileName(fullPath);
            Assert.That(result.Name, Is.EqualTo("applog"));
            Assert.That(result.Sequence, Is.EqualTo(0u));
            Assert.That(result.Extension, Is.EqualTo("txt"));
        }

        [Test]
        public void ParsesPartialPathWithExtensionAndSequence()
        {
            const string partialPath = @"logs\applog00000.txt";
            var result = FileNameParser.ParseLogFileName(partialPath);
            Assert.That(result.Name, Is.EqualTo("applog"));
            Assert.That(result.Sequence, Is.EqualTo(0u));
            Assert.That(result.Extension, Is.EqualTo("txt"));
        }
    }
}