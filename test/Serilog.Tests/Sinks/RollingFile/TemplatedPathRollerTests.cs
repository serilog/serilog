using System;
using System.IO;
using System.Linq;
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
            AssertAreEqualAbsolute("Logs\\log.20130714.txt", path);
            Assert.AreEqual(new DateTime(2013, 7, 15), nextCheckpoint);
        }

        static void AssertAreEqualAbsolute(string path1, string path2)
        {
            var abs1 = Path.GetFullPath(path1);
            var abs2 = Path.GetFullPath(path2);
            Assert.AreEqual(abs1, abs2);
        }

        [Test]
        public void TheRollerReturnsTheLogFileDirectory()
        {
            var roller = new TemplatedPathRoller("Logs\\log.{Date}.txt");
            AssertAreEqualAbsolute("Logs", roller.LogFileDirectory);
        }

        [Test]
        public void IfNoTokenIsSpecifiedDashFollowedByTheDateIsImplied()
        {
            var roller = new TemplatedPathRoller("Logs\\log.txt");
            var now = new DateTime(2013, 7, 14, 3, 24, 9, 980);
            string path;
            DateTime nextCheckpoint;
            roller.GetLogFilePath(now, out path, out nextCheckpoint);
            AssertAreEqualAbsolute("Logs\\log-20130714.txt", path);
        }

        [Test]
        public void TheLogFileIsNotRequiredToIncludeAnExtension()
        {
            var roller = new TemplatedPathRoller("Logs\\log-{Date}");
            var now = new DateTime(2013, 7, 14, 3, 24, 9, 980);
            string path;
            DateTime nextCheckpoint;
            roller.GetLogFilePath(now, out path, out nextCheckpoint);
            AssertAreEqualAbsolute("Logs\\log-20130714", path);
        }

        [Test]
        public void TheLogFileIsNotRequiredToIncludeADirectory()
        {
            var roller = new TemplatedPathRoller("log-{Date}");
            var now = new DateTime(2013, 7, 14, 3, 24, 9, 980);
            string path;
            DateTime nextCheckpoint;
            roller.GetLogFilePath(now, out path, out nextCheckpoint);
            AssertAreEqualAbsolute("log-20130714", path);
        }

        [Test]
        public void TheDirectorSearchPatternUsesWildcardInPlaceOfDate()
        {
            var roller = new TemplatedPathRoller("Logs\\log-{Date}.txt");
            Assert.AreEqual("log-*.txt", roller.DirectorySearchPattern);
        }

        [Test]
        public void OrderingMatchesFiles()
        {
            var roller = new TemplatedPathRoller("log-{Date}.txt");
            const string example = "log-20131210.txt";
            var matched = roller.OrderMatchingByAge(new[] { example });
            Assert.AreEqual(1, matched.Count());
        }

        [Test]
        public void OrderingExcludesSimilarButNonmatchingFiles()
        {
            var roller = new TemplatedPathRoller("log-{Date}.txt");
            const string similar1 = "log-0.txt";
            const string similar2 = "log-helloyou.txt";
            var matched = roller.OrderMatchingByAge(new[] { similar1, similar2 });
            Assert.AreEqual(0, matched.Count());
        }

        [Test]
        public void OrderingPresentsNewerFilesFirst()
        {
            var roller = new TemplatedPathRoller("log-{Date}.txt");
            const string newer = "log-20150101.txt";
            const string older = "log-20141231.txt";
            var matched = roller.OrderMatchingByAge(new[] { older, newer });
            CollectionAssert.AreEqual(new[] { newer, older }, matched);
        }
    }
}
