// Copyright 2013-2015 Serilog Contributors
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

namespace Serilog.Sinks.TextWriter
{
    class TextWriterSink : ILogEventSink
    {
        readonly System.IO.TextWriter _textWriter;
        readonly ITextFormatter _textFormatter;
        readonly object _syncRoot = new object();

        public TextWriterSink(System.IO.TextWriter textWriter, ITextFormatter textFormatter)
        {
            if (textFormatter == null) throw new ArgumentNullException(nameof(textFormatter));
            _textWriter = textWriter;
            _textFormatter = textFormatter;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            lock (_syncRoot)
            {
                _textFormatter.Format(logEvent, _textWriter);
                _textWriter.Flush();
            }
        }
    }
}
