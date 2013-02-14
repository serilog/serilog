using System;
using System.Collections.Generic;
using System.IO;

namespace Opi.Parsing
{
    class TextToken : MessageTemplateToken
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
    }
}