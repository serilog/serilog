using System;
using System.IO;
using System.Linq;
using Xunit;
using Serilog.Sinks.RollingFile;

namespace Serilog.Tests.Sinks.RollingFile
{
    public class TemplatedPathRollerTests
    {
        [Fact]
        public void WhenOldStyleSpecifierIsSuppliedTheExceptionIsInformative()
        {
            var ex = Assert.Throws<ArgumentException>(() => new TemplatedPathRoller("log-{0}.txt"));
            Assert.True(ex.Message.Contains("{Date}"));
        }

        [Fact]
        public void NewStyleSpecifierCannotBeProvidedInDirectory()
        {
            var ex = Assert.Throws<ArgumentException>(() => new TemplatedPathRoller("{Date}\\log.txt"));
            Assert.True(ex.Message.Contains("directory"));
        }
        
        [Fact]
        public void TheLogFileIncludesDateToken()
        {
            var roller = new TemplatedPathRoller("Logs\\log.{Date}.txt");
            var now = new DateTime(2013, 7, 14, 3, 24, 9, 980);
            string path;
            roller.GetLogFilePath(now, 0, out path);
            AssertEqualAbsolute("Logs\\log.20130714.txt", path);
        }

        [Fact]
        public void ANonZeroIncrementIsIncludedAndPadded()
        {
            var roller = new TemplatedPathRoller("Logs\\log.{Date}.txt");
            var now = new DateTime(2013, 7, 14, 3, 24, 9, 980);
            string path;
            roller.GetLogFilePath(now, 12, out path);
            AssertEqualAbsolute("Logs\\log.20130714_012.txt", path);
        }

        static void AssertEqualAbsolute(string path1, string path2)
        {
            var abs1 = Path.GetFullPath(path1);
            var abs2 = Path.GetFullPath(path2);
            Assert.Equal(abs1, abs2);
        }

        [Fact]
        public void TheRollerReturnsTheLogFileDirectory()
        {
            var roller = new TemplatedPathRoller("Logs\\log.{Date}.txt");
            AssertEqualAbsolute("Logs", roller.LogFileDirectory);
        }

        [Fact]
        public void IfNoTokenIsSpecifiedDashFollowedByTheDateIsImplied()
        {
            var roller = new TemplatedPathRoller("Logs\\log.txt");
            var now = new DateTime(2013, 7, 14, 3, 24, 9, 980);
            string path;
            roller.GetLogFilePath(now, 0, out path);
            AssertEqualAbsolute("Logs\\log-20130714.txt", path);
        }

        [Fact]
        public void TheLogFileIsNotRequiredToIncludeAnExtension()
        {
            var roller = new TemplatedPathRoller("Logs\\log-{Date}");
            var now = new DateTime(2013, 7, 14, 3, 24, 9, 980);
            string path;
            roller.GetLogFilePath(now, 0, out path);
            AssertEqualAbsolute("Logs\\log-20130714", path);
        }

        [Fact]
        public void TheLogFileIsNotRequiredToIncludeADirectory()
        {
            var roller = new TemplatedPathRoller("log-{Date}");
            var now = new DateTime(2013, 7, 14, 3, 24, 9, 980);
            string path;
            roller.GetLogFilePath(now, 0, out path);
            AssertEqualAbsolute("log-20130714", path);
        }

        [Fact]
        public void TheDirectorSearchPatternUsesWildcardInPlaceOfDate()
        {
            var roller = new TemplatedPathRoller("Logs\\log-{Date}.txt");
            Assert.Equal("log-*.txt", roller.DirectorySearchPattern);
        }

        [Fact]
        public void MatchingSelectsFiles()
        {
            var roller = new TemplatedPathRoller("log-{Date}.txt");
            const string example1 = "log-20131210.txt";
            const string example2 = "log-20131210_031.txt";
            var matched = roller.SelectMatches(new[] { example1, example2 }).ToArray();
            Assert.Equal(2, matched.Count());
            Assert.Equal(0, matched[0].SequenceNumber);
            Assert.Equal(31, matched[1].SequenceNumber);
        }

        [Fact]
        public void MatchingExcludesSimilarButNonmatchingFiles()
        {
            var roller = new TemplatedPathRoller("log-{Date}.txt");
            const string similar1 = "log-0.txt";
            const string similar2 = "log-helloyou.txt";
            var matched = roller.SelectMatches(new[] { similar1, similar2 });
            Assert.Equal(0, matched.Count());
        }

        [Fact]
        public void MatchingParsesDates()
        {
            var roller = new TemplatedPathRoller("log-{Date}.txt");
            const string newer = "log-20150101.txt";
            const string older = "log-20141231.txt";
            var matched = roller.SelectMatches(new[] { older, newer }).OrderByDescending(m => m.Date).Select(m => m.Filename).ToArray();
            Assert.Equal(new[] { newer, older }, matched);
        }
    }
}
