using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Tests.Sinks.IOFile
{
    [TestFixture]
    public class FileSinkTests
    {
        [Test]
        public void FileIsWrittenIfNonexistent()
        {
            var path = Some.NonexistentTempFilePath();
            TestLoggingAndDelete(path);
        }

        [Test]
        public void FileIsAppendedToWhenAlreadyCreated()
        {
            var path = Some.TempFilePath();
            TestLoggingAndDelete(path);
        }

        [Test]
        public void WhenLimitIsSpecifiedFileSizeIsRestricted()
        {
            const int maxBytes = 100;
            var path = Some.NonexistentTempFilePath();
            ExecuteAndCleanUpFile(
                path,
                p => new LoggerConfiguration()
                    .WriteTo.File(p, fileSizeLimitBytes: maxBytes)
                    .CreateLogger(),
                log =>
                {
                    log.Information(new string('n', maxBytes + 1));
                    var size = new FileInfo(path).Length;
                    Assert.That(size > 0);
                    Assert.That(size < maxBytes);
                });
        }

        static void TestLoggingAndDelete(string path)
        {
            ExecuteAndCleanUpFile(
                path,
                p => new LoggerConfiguration()
                    .WriteTo.File(p)
                    .CreateLogger(),
                log => 
                {
                    var message = Some.MessageTemplate();

                    log.Write(new LogEvent(
                        DateTimeOffset.Now,
                        LogEventLevel.Information,
                        null,
                        message,
                        Enumerable.Empty<LogEventProperty>()));

                    var refile = System.IO.File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    var content = new StreamReader(refile).ReadToEnd();
                    refile.Dispose();

                    Assert.That(content.Contains(message.Text));
                });
        }

        static void ExecuteAndCleanUpFile(string path, Func<string, ILogger> configure, Action<ILogger> test)
        {
            ILogger log = null;

            try
            {
                log = configure(path);
                test(log);
            }
            finally
            {
                var disposable = (IDisposable) log;
                    if (disposable != null) disposable.Dispose();
                System.IO.File.Delete(path);
            }
        }
    }
}
