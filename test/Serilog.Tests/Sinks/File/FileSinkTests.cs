using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Tests.Sinks.File
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

        static void TestLoggingAndDelete(string path)
        {
            ILogger log = null;

            try
            {
                log = new LoggerConfiguration()
                    .WriteTo.File(path)
                    .CreateLogger();

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
