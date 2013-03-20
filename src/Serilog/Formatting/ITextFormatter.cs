using System.IO;
using Serilog.Events;

namespace Serilog.Formatting
{
    public interface ITextFormatter
    {
        void Format(LogEvent logEvent, TextWriter output);
    }
}
