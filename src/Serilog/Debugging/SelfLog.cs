using System.IO;

namespace Serilog.Debugging
{
    public static class SelfLog
    {
        public static TextWriter Out { get; set; }

        internal static void WriteLine(string format, object arg0 = null, object arg1 = null, object arg2 = null)
        {
            var o = Out;
            if (o != null)
                o.WriteLine(format, arg0, arg1, arg2);
        }
    }
}
