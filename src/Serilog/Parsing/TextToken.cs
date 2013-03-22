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