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
using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.File
{
    sealed class FileSink : ILogEventSink, IDisposable
    {
        readonly TextWriter _output;
        readonly ITextFormatter _textFormatter;

        public FileSink(string path, ITextFormatter textFormatter)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (textFormatter == null) throw new ArgumentNullException("textFormatter");
            _textFormatter = textFormatter;
            _output = new StreamWriter(System.IO.File.Open(path, FileMode.Append, FileAccess.Write, FileShare.Read));
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            _textFormatter.Format(logEvent, _output);
            _output.Flush();
        }

        public void Dispose()
        {
            _output.Dispose();
        }
    }
}
