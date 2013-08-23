// Copyright 2013 Serilog Contributors
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
using System.Text;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.IOFile
{
    sealed class FileSink : ILogEventSink, IDisposable
    {
        const int BytesPerCharacterApproximate = 1;
        readonly TextWriter _output;
        readonly ITextFormatter _textFormatter;
        readonly object _syncRoot = new object();

        public FileSink(string path, ITextFormatter textFormatter, long? fileSizeLimitBytes)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (textFormatter == null) throw new ArgumentNullException("textFormatter");
            if (fileSizeLimitBytes.HasValue && fileSizeLimitBytes < 0) throw new ArgumentException("Negative value provided; file size limit must be non-negative");

            _textFormatter = textFormatter;

            TryCreateDirectory(path);

            var file = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.Read);
            var outputWriter = new StreamWriter(file, Encoding.UTF8);
            if (fileSizeLimitBytes != null)
            {
                var initialBytes = file.Length;
                var remainingCharacters = Math.Max(fileSizeLimitBytes.Value - initialBytes, 0L) / BytesPerCharacterApproximate;
                _output = new CharacterCountLimitedTextWriter(outputWriter, remainingCharacters);
            }
            else
            {
                _output = outputWriter;
            }
        }

        static void TryCreateDirectory(string path)
        {
            try
            {
                var directory = Path.GetDirectoryName(path);
                if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Failed to create directory {0}: {1}", path, ex);
            }
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            lock (_syncRoot)
            {
                _textFormatter.Format(logEvent, _output);
                _output.Flush();
            }
        }

        public void Dispose()
        {
            _output.Dispose();
        }
    }
}
