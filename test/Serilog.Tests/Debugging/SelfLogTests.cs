using System;
using System.Collections.Generic;
using Serilog.Debugging;
using Xunit;

namespace Serilog.Tests.Debugging
{
    public class SelfLogTests
    {
        [ThreadStatic]
        static List<string> Messages;

        [Fact]
        public void MessagesAreWrittenWhenOutputIsSet()
        {
            Messages = new List<string>();
            SelfLog.Enable(m =>
            {
                Messages = Messages ?? new List<string>();
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
    }
}
