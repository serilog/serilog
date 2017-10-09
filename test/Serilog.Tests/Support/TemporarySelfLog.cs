using System;
using System.Collections.Generic;
using Serilog.Debugging;

namespace Serilog.Tests.Support
{
    public class TemporarySelfLog : IDisposable
    {
        TemporarySelfLog(Action<string> output)
        {
            SelfLog.Enable(output);
        }

        public void Dispose()
        {
            SelfLog.Disable();
        }

        public static IDisposable SaveTo(List<string> target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            return new TemporarySelfLog(target.Add);
        }
    }
}
