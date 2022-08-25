using Serilog.Debugging;
using Serilog.Tests.Support;
using System;
using System.Collections.Generic;
using TestDummies;
using Xunit;

namespace Serilog.Tests.Debugging
{
    public class SelfLogTests
    {
        [ThreadStatic]
        static List<string>? Messages;

        [Fact]
        public void MessagesAreWrittenWhenOutputIsSet()
        {
            Messages = new();
            SelfLog.Enable(m =>
            {
                Messages ??= new();
                Messages.Add(m);
            });

            SelfLog.WriteLine("Hello {0} {1} {2}", 0, 1, 2);
            Assert.Contains(Messages, m => m.EndsWith("Hello 0 1 2"));

            // Better to do this here than in another test, since at this point
            // we've confirmed there's actually something to disable.
            var count = Messages.Count;
            SelfLog.Disable();
            SelfLog.WriteLine("Unwritten");
            Assert.Equal(Messages.Count, count);
        }

        [Fact]
        public void WritingToUndeclaredSinkWritesToSelfLog()
        {
            Messages = new();
            SelfLog.Enable(m =>
            {
                Messages ??= new();
                Messages.Add(m);
            });

            var settings = new Dictionary<string, string>
            {
                ["write-to:DummyRollingFile.pathFormat"] = "C:\\"
            };

            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(settings)
                .CreateLogger();

            DummyRollingFileSink.Reset();
            DummyRollingFileAuditSink.Reset();

            log.Write(Some.InformationEvent());

            Assert.Single(Messages);
            Assert.Contains(Messages, m => m.EndsWith("Setting \"DummyRollingFile\" could not be matched to an implementation in any of the loaded assemblies. " +
                "To use settings from additional assemblies, specify them with the \"serilog:using\" key."));
        }
    }
}
