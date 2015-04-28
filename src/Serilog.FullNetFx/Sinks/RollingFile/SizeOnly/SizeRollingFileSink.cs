using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.RollingFile.SizeOnly
{
    /// <summary>
    /// Write log events to a series of files. Each file will be suffixed with a
    /// 5 digit sequence number. No special templating in the path specification is
    /// considered.
    /// </summary>
    public sealed class SizeRollingFileSink : ILogEventSink, IDisposable
    {
        private static readonly string ThisObjectName = (typeof (SizeLimitedFileSink).Name);
        private readonly string _filePathTemplate;
        private readonly ITextFormatter _formatter;
        private readonly long _fileSizeLimitBytes;
        private readonly Encoding _encoding;
        private SizeLimitedFileSink _currentSink;
        private readonly object _syncRoot = new object();
        private bool _disposed = false;
        private readonly string _folderPath;

        /// <summary>
        /// Construct a <see cref="SizeRollingFileSink"/>
        /// </summary>
        /// <param name="filePathTemplate"></param>
        /// <param name="formatter"></param>
        /// <param name="fileSizeLimitBytes">
        /// The size in bytes at which a new file should be created</param>
        /// <param name="encoding"></param>
        public SizeRollingFileSink(
            string filePathTemplate,
            ITextFormatter formatter,
            long fileSizeLimitBytes,
            Encoding encoding = null)
        {
            _filePathTemplate = filePathTemplate;
            _formatter = formatter;
            _fileSizeLimitBytes = fileSizeLimitBytes;
            _encoding = encoding;
            _currentSink = GetLatestSink();
            _folderPath = Path.GetDirectoryName(filePathTemplate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logEvent"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public void Emit(LogEvent logEvent)
        {

            if (logEvent == null) throw new ArgumentNullException("logEvent");

            lock (_syncRoot)
            {
                if (_disposed)
                    throw new ObjectDisposedException(ThisObjectName, "The rolling file sink has been disposed");
                _currentSink = NewFileWhenLimitReached();

                if(_currentSink != null)
                    _currentSink.Emit(logEvent);
            }
        }

        private SizeLimitedFileSink GetLatestSink()
        {
            IList<SizeLimitedLogFile> existingFiles = GetExistingFiles(_filePathTemplate, _fileSizeLimitBytes).ToList();
            SizeLimitedLogFile nextFile;
            if (existingFiles.Any())
            {
                var latest = existingFiles.OrderByDescending(x => x.FileNameComponents.Sequence).First();
                nextFile = latest.Next();
            }
            else
            {
                nextFile = ParseRollingLogfile(_filePathTemplate, _fileSizeLimitBytes);
            }

            return new SizeLimitedFileSink(_formatter, _folderPath, nextFile, _encoding);
        }

        private SizeLimitedFileSink NewFileWhenLimitReached()
        {
            if(_currentSink.SizeLimitReached)
            {
                var next = _currentSink.LogFile.Next();
                _currentSink.Dispose();
                return new SizeLimitedFileSink(_formatter, _folderPath, next,_encoding);
            }

            return _currentSink;
        }

        private static IEnumerable<SizeLimitedLogFile> GetExistingFiles(string filePathTemplate, long fileSizeLimitBytes)
        {
            var directoryName = Path.GetDirectoryName(filePathTemplate);
            if (string.IsNullOrEmpty(directoryName))
            {
#if ASPNETCORE50
                directory = Directory.GetCurrentDirectory();
#else
                directoryName = Environment.CurrentDirectory;
#endif
            }

            directoryName = Path.GetFullPath(directoryName);
            return
                Directory.GetFiles(directoryName)
                .Select(x => ParseRollingLogfile(x, fileSizeLimitBytes))
                .Where(rollingFile => rollingFile != null);
        }

        private static SizeLimitedLogFile ParseRollingLogfile(string path, long fileSizeLimitBytes)
        {
            var fileNameComponents = FileNameParser.ParseLogFileName(path);

            return new SizeLimitedLogFile(fileNameComponents, fileSizeLimitBytes);
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or 
        /// resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            lock (_syncRoot)
            {
                if (!_disposed && _currentSink != null)
                {
                    _currentSink.Dispose();
                    _currentSink = null;
                    _disposed = true;
                }
            }
        }
    }
}