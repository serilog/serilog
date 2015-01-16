// Copyright 2014 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.IOFile;

namespace Serilog.Sinks.RollingFile
{
    /// <summary>
    /// Write log events to a series of files. Each file will be named according to
    /// the date of the first log entry written to it. Only simple date-based rolling is
    /// currently supported.
    /// </summary>
    public sealed class RollingFileSink : ILogEventSink, IDisposable
    {
        readonly TemplatedPathRoller _roller;
        readonly ITextFormatter _textFormatter;
        readonly long? _fileSizeLimitBytes;
        readonly int? _retainedFileCountLimit;
        readonly Encoding _encoding;
        readonly object _syncRoot = new object();

        bool _isDisposed;
        DateTime? _nextCheckpoint;
        FileSink _currentFile;

        /// <summary>Construct a <see cref="RollingFileSink"/>.</summary>
        /// <param name="pathFormat">String describing the location of the log files,
        /// with {Date} in the place of the file date. E.g. "Logs\myapp-{Date}.log" will result in log
        /// files such as "Logs\myapp-2013-10-20.log", "Logs\myapp-2013-10-21.log" and so on.</param>
        /// <param name="textFormatter">Formatter used to convert log events to text.</param>
        /// <param name="fileSizeLimitBytes">The maximum size, in bytes, to which a log file will be allowed to grow.
        /// For unrestricted growth, pass null. The default is 1 GB.</param>
        /// <param name="retainedFileCountLimit">The maximum number of log files that will be retained,
        /// including the current log file. For unlimited retention, pass null. The default is 31.</param>
        /// <param name="encoding">Character encoding used to write the text file. The default is UTF-8.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <remarks>The file will be written using the UTF-8 character set.</remarks>
        public RollingFileSink(string pathFormat, 
                              ITextFormatter textFormatter,
                              long? fileSizeLimitBytes,
                              int? retainedFileCountLimit, 
                              Encoding encoding = null)
        {
            if (pathFormat == null) throw new ArgumentNullException("pathFormat");
            if (fileSizeLimitBytes.HasValue && fileSizeLimitBytes < 0) throw new ArgumentException("Negative value provided; file size limit must be non-negative");
            if (retainedFileCountLimit.HasValue && retainedFileCountLimit < 1) throw new ArgumentException("Zero or negative value provided; retained file count limit must be at least 1");

            _roller = new TemplatedPathRoller(pathFormat);
            _textFormatter = textFormatter;
            _fileSizeLimitBytes = fileSizeLimitBytes;
            _retainedFileCountLimit = retainedFileCountLimit;
            _encoding = encoding ?? Encoding.UTF8;
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        /// <remarks>Events that come in out-of-order (e.g. around the rollovers)
        /// may end up written to a later file than their timestamp
        /// would indicate.</remarks> 
        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            lock (_syncRoot)
            {
                if (_isDisposed) throw new ObjectDisposedException("The rolling file has been disposed.");

                AlignCurrentFileTo(Clock.DateTimeNow);

                // If the file was unable to be opened on the last attempt, it will remain
                // null until the next checkpoint passes, at which time another attempt will be made to
                // open it.
                if (_currentFile != null)
                    _currentFile.Emit(logEvent);
            }
        }

        void AlignCurrentFileTo(DateTime now)
        {
            if (!_nextCheckpoint.HasValue)
            {
                OpenFile(now);
            }
            else if (now >= _nextCheckpoint.Value)
            {
                CloseFile();
                OpenFile(now);
            }
        }

        void OpenFile(DateTime now)
        {
            var date = now.Date;

            // We only take one attempt at it because repeated failures
            // to open log files REALLY slow an app down.
            _nextCheckpoint = date.AddDays(1);            

            var existingFiles = Enumerable.Empty<string>();
            try
            {
                existingFiles = Directory.GetFiles(_roller.LogFileDirectory, _roller.DirectorySearchPattern)
                                         .Select(Path.GetFileName);
            }
            catch (DirectoryNotFoundException) { }

            var latestForThisDate = _roller
                .SelectMatches(existingFiles)
                .Where(m => m.Date == date)
                .OrderByDescending(m => m.SequenceNumber)
                .FirstOrDefault();

            var sequence = latestForThisDate != null ? latestForThisDate.SequenceNumber : 0;

            const int maxAttempts = 3;
            for (var attempt = 0; attempt < maxAttempts; attempt++)
            {
                string path;
                _roller.GetLogFilePath(now, sequence, out path);

                try
                {
                    _currentFile = new FileSink(path, _textFormatter, _fileSizeLimitBytes, _encoding);
                }
                catch (IOException ex)
                {
                    var errorCode = Marshal.GetHRForException(ex) & ((1 << 16) - 1);
                    if (errorCode == 32 || errorCode == 33)
                    {
                        SelfLog.WriteLine("Rolling file target {0} was locked, attempting to open next in sequence (attempt {1})", path, attempt + 1);
                        sequence++;
                        continue;
                    }

                    throw;
                }

                ApplyRetentionPolicy(path);
                return;
            }
        }

        void ApplyRetentionPolicy(string currentFilePath)
        {
            if (_retainedFileCountLimit == null) return;
            
            var currentFileName = Path.GetFileName(currentFilePath);

            // We consider the current file to exist, even if nothing's been written yet,
            // because files are only opened on response to an event being processed.
            var potentialMatches = Directory.GetFiles(_roller.LogFileDirectory, _roller.DirectorySearchPattern)
                .Select(Path.GetFileName)
                .Union(new [] { currentFileName });

            var newestFirst = _roller
                .SelectMatches(potentialMatches)
                .OrderByDescending(m => m.Date)
                .ThenByDescending(m => m.SequenceNumber)
                .Select(m => m.Filename);

            var toRemove = newestFirst
                .Where(n => StringComparer.OrdinalIgnoreCase.Compare(currentFileName, n) != 0)
                .Skip(_retainedFileCountLimit.Value - 1)
                .ToList();

            foreach (var obsolete in toRemove)
            {
                var fullPath = Path.Combine(_roller.LogFileDirectory, obsolete);
                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception ex)
                {
                    SelfLog.WriteLine("Error {0} while removing obsolete file {1}", ex, fullPath);
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or 
        /// resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            lock (_syncRoot)
            {
                if (_currentFile == null) return;
                CloseFile();
                _isDisposed = true;
            }
        }

        void CloseFile()
        {
            _currentFile.Dispose();
            _currentFile = null;
            _nextCheckpoint = null;
        }
    }
}
