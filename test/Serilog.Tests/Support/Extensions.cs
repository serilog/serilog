using Serilog.Events;

namespace Serilog.Tests.Support
{
    static class Extensions
    {
        public static object LiteralValue(this LogEventProperty @this)
        {
            return ((LogEventPropertyLiteralValue)@this.Value).Value;
        }
    }
}
