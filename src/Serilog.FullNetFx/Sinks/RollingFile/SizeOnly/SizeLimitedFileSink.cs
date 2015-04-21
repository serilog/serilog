using System;
using System.IO;
using System.Text;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.RollingFile.SizeOnly
{
    internal class SizeLimitedFileSink : ILogEventSink, IDisposable
    {
        private static readonly string ThisObjectName =
            typeof (SizeLimitedFileSink).Name;

        private readonly ITextFormatter _formatter;
        private readonly SizeLimitedLogFile _sizeLimitedLogFile;
        private readonly StreamWriter _output;
        private readonly object _syncRoot = new object();
        private bool _disposed = false;
        private bool _sizeLimitReached = false;

        public SizeLimitedFileSink(ITextFormatter formatter, string folderPath, SizeLimitedLogFile sizeLimitedLogFile, Encoding encoding = null)
        {
            _formatter = formatter;
            _sizeLimitedLogFile = sizeLimitedLogFile;
            _output = OpenFileForWriting(folderPath, sizeLimitedLogFile, encoding ?? Encoding.UTF8);
        }

        internal SizeLimitedFileSink(ITextFormatter formatter, SizeLimitedLogFile sizeLimitedLogFile, StreamWriter writer)
        {
            _formatter = formatter;
            _sizeLimitedLogFile = sizeLimitedLogFile;
            _output = writer;
        }

        private StreamWriter OpenFileForWriting(string folderPath, SizeLimitedLogFile sizeLimitedLogFile, Encoding encoding)
        {
            EnsureDirectoryCreated(folderPath);
            var fullPath = Path.Combine(folderPath, sizeLimitedLogFile.FullName);
            var stream = File.Open(fullPath, FileMode.Append, FileAccess.ReadWrite, FileShare.Read);
            return new StreamWriter(stream, encoding ?? Encoding.UTF8);
        }

        private static void EnsureDirectoryCreated(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            lock (_syncRoot)
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(ThisObjectName, "Cannot write to disposed file");
                }

                if (_output == null) return;

                _formatter.Format(logEvent, _output);
                _output.Flush();

                if (_output.BaseStream.Length > _sizeLimitedLogFile.SizeLimitBytes)
                    _sizeLimitReached = true;
            }
        }

        internal bool SizeLimitReached { get { return _sizeLimitReached; } }

        internal SizeLimitedLogFile LogFile { get { return _sizeLimitedLogFile; } }

        public void Dispose()
        {
            if (!_disposed)
            {
                _output.Dispose();
                _disposed = true;
            }
        }
    }
}