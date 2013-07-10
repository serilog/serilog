using System;
using NUnit.Framework;
using Serilog.Sinks.RollingFile;

namespace Serilog.Tests.Sinks.RollingFile
{
    [TestFixture]
    public class TemplatedPathRollerTests
    {
        [Test]
        public void WhenOldStyleSpecifierIsSuppliedTheExceptionIsInformative()
        {
            var ex = Assert.Throws<ArgumentException>(() => new TemplatedPathRoller("log-{0}.txt"));
            Assert.That(ex.Message.Contains("{Date}"));
        }

        [Test]
        public void NewStyleSpecifierCannotBeProvidedInDirectory()
        {
            var ex = Assert.Throws<ArgumentException>(() => new TemplatedPathRoller("{Date}\\log.txt"));
            Assert.That(ex.Message.Contains("directory"));
        }
        
        [Test]
        public void TheLogFileIncludesDateTokenAndSetsCheckpointToNextDay()
        {
            var roller = new TemplatedPathRoller("Logs\\log.{Date}.txt");
            var now = new DateTime(2013, 7, 14, 3, 24, 9, 980);
            string path;
            DateTime nextCheckpoint;
            roller.GetLogFilePath(now, out path, out nextCheckpoint);
            Assert.AreEqual("Logs\\log.20130714.txt", path);
            Assert.AreEqual(new DateTime(2013, 7, 15), nextCheckpoint);
        }
    }
}
