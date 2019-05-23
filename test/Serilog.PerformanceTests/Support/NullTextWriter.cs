using System.IO;
using System.Text;

namespace Serilog.PerformanceTests.Support
{
    class NullTextWriter : TextWriter
    {
        public override void Write(char value)
        {
        }

        public override Encoding Encoding { get; } = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

        public override void Write(bool value)
        {
        }

        public override void Write(char[] buffer)
        {
        }

        public override void Write(char[] buffer, int index, int count)
        {
        }

        public override void Write(decimal value)
        {
        }

        public override void Write(double value)
        {
        }

        public override void Write(int value)
        {
        }

        public override void Write(long value)
        {
        }

        public override void Write(object value)
        {
        }

        public override void Write(float value)
        {
        }

        public override void Write(string value)
        {
        }

        public override void Write(string format, object arg0)
        {
        }

        public override void Write(string format, object arg0, object arg1)
        {
        }

        public override void Write(string format, object arg0, object arg1, object arg2)
        {
        }

        public override void Write(string format, params object[] arg)
        {
        }

        public override void Write(uint value)
        {
        }

        public override void Write(ulong value)
        {
        }

        public override string ToString() => string.Empty;

        public override void Flush()
        {
        }

        public override void WriteLine()
        {
        }

        public override void WriteLine(bool value)
        {
        }

        public override void WriteLine(char value)
        {
        }

        public override void WriteLine(char[] buffer)
        {
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
        }

        public override void WriteLine(decimal value)
        {
        }

        public override void WriteLine(double value)
        {
        }

        public override void WriteLine(int value)
        {
        }

        public override void WriteLine(long value)
        {
        }

        public override void WriteLine(object value)
        {
        }

        public override void WriteLine(float value)
        {
        }

        public override void WriteLine(string value)
        {
        }

        public override void WriteLine(string format, object arg0)
        {
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
        }

        public override void WriteLine(string format, params object[] arg)
        {
        }

        public override void WriteLine(uint value)
        {
        }

        public override void WriteLine(ulong value)
        {
        }
    }
}
