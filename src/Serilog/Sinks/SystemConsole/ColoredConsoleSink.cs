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
using System.Collections.Generic;
using Serilog.Formatting.Display;
using Serilog.Parsing;

#if NET40
using IPropertyDictionary = System.Collections.Generic.IDictionary<string, Serilog.Events.LogEventPropertyValue>;
#else
using IPropertyDictionary = System.Collections.Generic.IReadOnlyDictionary<string, Serilog.Events.LogEventPropertyValue>;
#endif

#if !PROFILE259
namespace Serilog.Sinks.SystemConsole
{
    class ColoredConsoleSink : ILogEventSink
    {
        readonly IFormatProvider _formatProvider;

        class Palette
        {
            public ConsoleColor Base { get; set; }
            public ConsoleColor BaseText { get; set; }
            public ConsoleColor Highlight { get; set; }
            public ConsoleColor HighlightText { get; set; }
        }

        static readonly Palette DefaultPalette = new Palette { Base = ConsoleColor.Black, BaseText = ConsoleColor.Gray,
                                                               Highlight = ConsoleColor.DarkGray, HighlightText = ConsoleColor.Gray };

        static readonly IDictionary<LogEventLevel, Palette> LevelPalettes = new Dictionary<LogEventLevel, Palette>
        {
            { LogEventLevel.Verbose,     new Palette { Base = ConsoleColor.Black, BaseText = ConsoleColor.DarkGray,
                                                       Highlight = ConsoleColor.Black, HighlightText = ConsoleColor.Gray } },
            { LogEventLevel.Debug,       new Palette { Base = ConsoleColor.Black, BaseText = ConsoleColor.Gray,
                                                       Highlight = ConsoleColor.Black, HighlightText = ConsoleColor.White } },
            { LogEventLevel.Information, new Palette { Base = ConsoleColor.Black, BaseText = ConsoleColor.White,
                                                       Highlight = ConsoleColor.DarkBlue, HighlightText = ConsoleColor.White } },
            { LogEventLevel.Warning,     new Palette { Base = ConsoleColor.Black, BaseText = ConsoleColor.Yellow,
                                                       Highlight = ConsoleColor.DarkYellow, HighlightText = ConsoleColor.White } },
            { LogEventLevel.Error,       new Palette { Base = ConsoleColor.Black, BaseText = ConsoleColor.Red,
                                                       Highlight = ConsoleColor.Red, HighlightText = ConsoleColor.White } },
            { LogEventLevel.Fatal,       new Palette { Base = ConsoleColor.DarkRed, BaseText = ConsoleColor.White,
                                                       Highlight = ConsoleColor.Red, HighlightText = ConsoleColor.White } }
        };

        readonly object _syncRoot = new object();
        readonly MessageTemplate _outputTemplate;

        public ColoredConsoleSink(string outputTemplate, IFormatProvider formatProvider)
        {
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            _outputTemplate = new MessageTemplateParser().Parse(outputTemplate);
            _formatProvider = formatProvider;
        }

        const string StackFrameLinePrefix = "   ";

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            var outputProperties = OutputProperties.GetOutputProperties(logEvent);
            var palette = GetPalette(logEvent.Level);
            var output = Console.Out;
            
            lock (_syncRoot)
            {
                try
                {
                    foreach (var outputToken in _outputTemplate.Tokens)
                    {
                        var propertyToken = outputToken as PropertyToken;
                        if (propertyToken == null)
                        {
                            RenderOutputToken(palette, outputToken, outputProperties, output);
                        }
                        else switch (propertyToken.PropertyName)
                        {
                            case OutputProperties.MessagePropertyName:
                                RenderMessageToken(logEvent, palette, output);
                                break;
                            case OutputProperties.ExceptionPropertyName:
                                RenderExceptionToken(palette, propertyToken, outputProperties, output);
                                break;
                            default:
                                RenderOutputToken(palette, outputToken, outputProperties, output);
                                break;
                        }
                    }
                }
                finally { Console.ResetColor(); }
            }
        }

        void RenderExceptionToken(Palette palette, MessageTemplateToken outputToken, IPropertyDictionary outputProperties, TextWriter output)
        {
            var sw = new StringWriter();
            outputToken.Render(outputProperties, sw, _formatProvider);
            var lines = new StringReader(sw.ToString());
            string nextLine;
            while ((nextLine = lines.ReadLine()) != null)
            {
                if (nextLine.StartsWith(StackFrameLinePrefix))
                    SetBaseColors(palette);
                else
                    SetHighlightColors(palette);
                output.WriteLine(nextLine);
            }
        }

        void RenderMessageToken(LogEvent logEvent, Palette palette, TextWriter output)
        {
            foreach (var messageToken in logEvent.MessageTemplate.Tokens)
            {
                var messagePropertyToken = messageToken as PropertyToken;
                if (messagePropertyToken != null)
                {
                    SetHighlightColors(palette);
                    messageToken.Render(logEvent.Properties, output, _formatProvider);
                }
                else
                {
                    SetBaseColors(palette);
                    messageToken.Render(logEvent.Properties, output, _formatProvider);
                }
            }
        }

        void RenderOutputToken(Palette palette, MessageTemplateToken outputToken, IPropertyDictionary outputProperties, TextWriter output)
        {
            SetBaseColors(palette);
            outputToken.Render(outputProperties, output, _formatProvider);
        }

        static Palette GetPalette(LogEventLevel level)
        {
            Palette palette;
            if (!LevelPalettes.TryGetValue(level, out palette))
                palette = DefaultPalette;

            return palette;
        }

        static void SetBaseColors(Palette palette)
        {
            SetColors(palette.Base, palette.BaseText);
        }

        static void SetHighlightColors(Palette palette)
        {
            SetColors(palette.Highlight, palette.HighlightText);
        }

        static void SetColors(ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }
    }
}
#endif
