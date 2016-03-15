using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Sinks.RollingFile;
using Serilog.Tests.Support;

namespace Serilog.Tests.Sinks.RollingFile
{
    [TestFixture]
    public class RollingFileSinkTests
    {
        [Test]
        public void LogEventsAreEmittedToTheFileNamedAccordingToTheEventTimestamp()
        {
            TestRollingEventSequence(Some.InformationEvent());
        }

        [Test]
        public void WhenTheDateChangesTheCorrectFileIsWritten()
        {
            var e1 = Some.InformationEvent();
            var e2 = Some.InformationEvent(e1.Timestamp.AddDays(1));
            TestRollingEventSequence(e1, e2);
        }

        [Test]
        public void WhenRetentionCountIsSetOldFilesAreDeleted()
        {
            LogEvent e1 = Some.InformationEvent(),
                     e2 = Some.InformationEvent(e1.Timestamp.AddDays(1)),
                     e3 = Some.InformationEvent(e2.Timestamp.AddDays(5));

            TestRollingEventSequence(new[] { e1, e2, e3 }, 2,
                files =>
                {
                    Assert.AreEqual(3, files.Count);
                    Assert.That(!File.Exists(files[0]));
                    Assert.That(File.Exists(files[1]));
                    Assert.That(File.Exists(files[2]));
                });
        }

        [Test]
        public void IfTheLogFolderDoesNotExistItWillBeCreated()
        {
            var fileName = Some.String() + "-{Date}.txt";
            var temp = Some.TempFolderPath();
            var folder = Path.Combine(temp, Guid.NewGuid().ToString());
            var pathFormat = Path.Combine(folder, fileName);

            ILogger log = null;

            try
            {
                log = new LoggerConfiguration()
                    .WriteTo.RollingFile(pathFormat, retainedFileCountLimit: 3)
                    .CreateLogger();

                log.Write(Some.InformationEvent());

                Assert.That(Directory.Exists(folder));
            }
            finally
            {
                var disposable = (IDisposable)log;
                if (disposable != null) disposable.Dispose();
                Directory.Delete(temp, true);
            }
        }
        
        [Test]
        public void WhenFileSizeLimitIsReachedNextLogFileSequenceIsCreated()
        {
            SetupAndCleanup((logger, logFilePath, nextLogFilePath, maxBytes) => {
                // fill the current log file
                logger.Information(new string('a', maxBytes));

                // now write a few more bytes that would push it past the file size limit
                logger.Information(new string('b', 10));

                // make sure that the first log file is at least as long as the file size limit
                // then, check that the next log file exists
                var size = new FileInfo(logFilePath).Length;
                Assert.That(size >= maxBytes);
                Assert.That(File.Exists(nextLogFilePath));
            });
        }

        [Test]
        public void WhenNextLogEventCannotFitWithinFileSizeLimitNextLogFileSequenceIsCreated()
        {
            SetupAndCleanup((logger, logFilePath, nextLogFilePath, maxBytes) => {
                // write a few bytes less than the file size limit
                logger.Information(new string('a', maxBytes - 10));

                // now write a few more bytes that would push us past the file size limit
                logger.Information(new string('b', 20));

                // make sure that the first log file has not reached file size limit
                // then, check that the next log file exists
                var size = new FileInfo(logFilePath).Length;
                Assert.That(size < maxBytes);
                Assert.That(File.Exists(nextLogFilePath));
            });
        }
        
        void SetupAndCleanup(Action<ILogger,string,string,int> actAndAssert)
        {
            Clock.SetTestDateTimeNow(Some.Instant().Date);
	        const int maxBytes = 100;
            const string template = "{Message}";
            var fileName = Guid.NewGuid().ToString() + "-{Date}.txt";
            var pathFormat = Path.Combine(Path.GetTempPath(), fileName);
            var logFilePath = pathFormat.Replace("{Date}", Some.Instant().ToString("yyyyMMdd"));
            var nextLogFilePath = pathFormat.Replace("{Date}.txt", string.Format("{0}_001.txt", Some.Instant().ToString("yyyyMMdd")));
            ILogger log = null;

            try
            {
                log = new LoggerConfiguration()
                    .WriteTo.RollingFile(pathFormat, fileSizeLimitBytes: maxBytes, outputTemplate: template)
                    .CreateLogger();

                actAndAssert(log, logFilePath, nextLogFilePath, maxBytes);
            }
            finally
            {
                var disposable = log as IDisposable;
                if (disposable != null) disposable.Dispose();
                GC.Collect();
                if (File.Exists(logFilePath)) File.Delete(logFilePath);
                if (File.Exists(nextLogFilePath)) File.Delete(nextLogFilePath);
            }
        }

        static void TestRollingEventSequence(params LogEvent[] events)
        {
            TestRollingEventSequence(events, null, f => {});
        }

        static void TestRollingEventSequence(
            IEnumerable<LogEvent> events,
            int? retainedFiles,
            Action<IList<string>> verifyWritten)
        {
            var fileName = Some.String() + "-{Date}.txt";
            var folder = Some.TempFolderPath();
            var pathFormat = Path.Combine(folder, fileName);

            var log = new LoggerConfiguration()
                .WriteTo.RollingFile(pathFormat, retainedFileCountLimit: retainedFiles)
                .CreateLogger();

            var verified = new List<string>();

            try
            {
                foreach (var @event in events)
                {
                    Clock.SetTestDateTimeNow(@event.Timestamp.DateTime);
                    log.Write(@event);

                    var expected = pathFormat.Replace("{Date}", @event.Timestamp.ToString("yyyyMMdd"));
                    Assert.That(File.Exists(expected));

                    verified.Add(expected);
                }
            }
            finally
            {
                ((IDisposable)log).Dispose();
                verifyWritten(verified);
                Directory.Delete(folder, true);
            }
        }
    }
}
