using System.IO;

namespace Opi.Values
{
    class LogEventPropertyNullValue : LogEventPropertyValue
    {
        internal override void Render(TextWriter output, string format = null)
        {
            output.Write("null");
        }
    }
}