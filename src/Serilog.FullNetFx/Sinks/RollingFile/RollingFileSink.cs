// Copyright 2013 Nicholas Blumhardt
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
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.IOFile;

namespace Serilog.Sinks.RollingFile
{
    /// <summary>
    /// Date-based rolling only is supported.
    /// </summary>
    sealed class RollingFileSink : ILogEventSink, IDisposable
    {
        readonly TemplatedPathRoller _roller;
        readonly ITextFormatter _textFormatter;
        readonly long? _fileSizeLimitBytes;
        readonly object _syncRoot = new object();

        bool _isDisposed;
        DateTime? _nextCheckpoint;
        FileSink _currentFile;

        public RollingFileSink(string pathTemplate, ITextFormatter textFormatter, long? fileSizeLimitBytes)
        {
            if (pathTemplate == null) throw new ArgumentNullException("pathTemplate");
            _roller = new TemplatedPathRoller(pathTemplate);
            _textFormatter = textFormatter;
            _fileSizeLimitBytes = fileSizeLimitBytes;
        }

        // Simplifications:
        // Events that come in out-of-order (e.g. around the rollovers)
        // may end up written to a later file than their timestamp
        // would indicate. 
        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            lock (_syncRoot)
            {
                if (_isDisposed) throw new ObjectDisposedException("The rolling file has been disposed.");

                AlignCurrentFileTo(Clock.DateTimeNow);
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
            string path;
            DateTime nextCheckpoint;
            _roller.GetLogFilePath(now, out path, out nextCheckpoint);
            _nextCheckpoint = nextCheckpoint;
            _currentFile = new FileSink(path, _textFormatter, _fileSizeLimitBytes);
        }

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
