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
using System.Text;
using System.Threading;

namespace Serilog.Sinks.IOFile
{
    sealed class CharacterCountLimitedTextWriter : TextWriter
    {
        readonly TextWriter _outputWriter;
        long _remainingCharacters;

        public CharacterCountLimitedTextWriter(TextWriter outputWriter, long remainingCharacters)
        {
            if (outputWriter == null) throw new ArgumentNullException("outputWriter");
            _outputWriter = outputWriter;
            _remainingCharacters = remainingCharacters;
        }

        public override Encoding Encoding
        {
            get
            {
                return _outputWriter.Encoding;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _outputWriter.Dispose();

            base.Dispose(disposing);
        }

        public override void Write(char value)
        {
            var remaining = Interlocked.Decrement(ref _remainingCharacters);
            if (remaining >= 0)
            {
                _outputWriter.Write(value);
            }
            else
            {
                // Prevent underflow (interlocking prevents torn reads)
                Interlocked.Exchange(ref _remainingCharacters, 0L);
            }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            var remaining = Interlocked.Add(ref _remainingCharacters, -count);
            if (remaining >= 0)
            {
                _outputWriter.Write(buffer, index, count);
            }
            else
            {
                // Prevent underflow (interlocking prevents torn reads)
                Interlocked.Exchange(ref _remainingCharacters, 0L);
            }
        }

        public override void Flush()
        {
            _outputWriter.Flush();
        }
    }
}