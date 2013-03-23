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
using System.Globalization;
using System.IO;

namespace Serilog.Events
{
    public class LogEventPropertyLiteralValue : LogEventPropertyValue
    {
        readonly object _value;

        public LogEventPropertyLiteralValue(object value)
        {
            _value = value;
        }

        public object Value { get { return _value; } }

        internal override void Render(TextWriter output, string format = null)
        {
            if (_value == null)
            {
                output.Write("null");
            }
            else
            {
                var s = _value as string;
                if (s != null)
                {
                    if (format != "l")
                    {
                        output.Write("\"");
                        output.Write(s.Replace("\"", "\\\""));
                        output.Write("\"");
                    }
                    else
                    {
                        output.Write(s);
                    }
                }
                else
                {
                    var f = _value as IFormattable;
                    if (f != null)
                    {
                        output.Write(f.ToString(format, CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        output.Write(_value.ToString());
                    }
                }
            }
        }
    }
}