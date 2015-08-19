using System;
using System.IO;
using System.Linq;
using Xunit;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Tests.Sinks.IOFile
{
    public class FileSinkTests
    {
        [Fact]
        public void FileIsWrittenIfNonexistent()
        {
            var path = Some.NonexistentTempFilePath();
            TestLoggingAndDelete(path);
        }

        [Fact]
        public void FileIsAppendedToWhenAlreadyCreated()
        {
            var path = Some.TempFilePath();
            TestLoggingAndDelete(path);
        }

        [Fact]
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
                    Assert.True(size > 0);
                    Assert.True(size < maxBytes);
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

                    var refile = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    var content = new StreamReader(refile).ReadToEnd();
                    refile.Dispose();

                    Assert.True(content.Contains(message.Text));
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
                File.Delete(path);
            }
        }
    }
}
