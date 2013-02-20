using System.IO;

namespace Serilog.Sinks
{
    class LogEventPropertyTokenValue : LogEventPropertyValue
    {
        private readonly string _token;

        public LogEventPropertyTokenValue(string token)
        {
            _token = token ?? "";
        }

        internal override void Render(TextWriter output, string format = null)
        {
            output.Write(_token);
        }
    }
}
