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
using System.Collections.Generic;
using System.IO;
using Serilog.Events;

namespace Serilog.Parsing
{
    public class TextToken : MessageTemplateToken
    {
        private readonly string _text;

        public TextToken(string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            _text = text;
        }

        public override void Render(IReadOnlyDictionary<string, LogEventProperty> properties, TextWriter output)
        {
            if (output == null) throw new ArgumentNullException("output");
            output.Write(_text);
        }

        public override bool Equals(object obj)
        {
            var tt = obj as TextToken;
            return tt != null && tt._text == _text;
        }

        public override int GetHashCode()
        {
            return _text.GetHashCode();
        }

        public override string ToString()
        {
            return _text;
        }
    }
}