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
    /// Very simple date-only based roller. Does not delete old
    /// files.
    /// </summary>
    sealed class RollingFileSink : ILogEventSink, IDisposable
    {
        readonly string _pathFormat;
        readonly ITextFormatter _textFormatter;
        readonly object _syncRoot = new object();

        bool _isDisposed;
        DateTime? _limitOfCurrentFile;
        FileSink _currentFile;

        public RollingFileSink(string pathFormat, ITextFormatter textFormatter)
        {
            if (pathFormat == null) throw new ArgumentNullException("pathFormat");
            _pathFormat = pathFormat;
            _textFormatter = textFormatter;
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

                AlignCurrentFileTo(logEvent.TimeStamp);
                _currentFile.Emit(logEvent);
            }
        }

        void AlignCurrentFileTo(DateTimeOffset timeStamp)
        {
            if (!_limitOfCurrentFile.HasValue)
            {
                OpenFile(timeStamp);
            }
            else if (timeStamp >= _limitOfCurrentFile.Value)
            {
                CloseFile();
                OpenFile(timeStamp);
            }
        }

        void OpenFile(DateTimeOffset timeStamp)
        {
            var limit = timeStamp.Date.AddDays(1);
            var path = string.Format(_pathFormat, timeStamp.Date.ToString("yyyy-MM-dd"));
            _currentFile = new FileSink(path, _textFormatter);
            _limitOfCurrentFile = limit;
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
            _limitOfCurrentFile = null;
        }
    }
}
