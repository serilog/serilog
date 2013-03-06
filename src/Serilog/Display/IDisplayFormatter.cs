using System.IO;
using Serilog.Events;

namespace Serilog.Display
{
    public interface IDisplayFormatter
    {
        void Format(LogEvent logEvent, TextWriter output);
    }
}
