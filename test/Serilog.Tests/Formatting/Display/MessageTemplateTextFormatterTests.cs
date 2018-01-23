using System;
using System.Globalization;
using System.IO;

using Serilog.Events;

using Xunit;
using Serilog.Tests.Support;
using Serilog.Formatting.Display;

namespace Serilog.Tests.Formatting.Display
{
    public class MessageTemplateTextFormatterTests
    {
        [Fact]
        public void UsesFormatProvider()
        {
            var french = new CultureInfo("fr-FR");
            var formatter = new MessageTemplateTextFormatter("{Message}", french);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{0}", 12.345));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal("12,345", sw.ToString());
        }

        [Fact]
        public void MessageTemplatesContainingFormatStringPropertiesRenderCorrectly()
        {
            var formatter = new MessageTemplateTextFormatter("{Message}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{Message}", "Hello, world!"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal("\"Hello, world!\"", sw.ToString());
        }

        [Fact]
        public void UppercaseFormatSpecifierIsSupportedForStrings()
        {
            var formatter = new MessageTemplateTextFormatter("{Name:u}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{Name}", "Nick"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal("NICK", sw.ToString());
        }

        [Fact]
        public void LowercaseFormatSpecifierIsSupportedForStrings()
        {
            var formatter = new MessageTemplateTextFormatter("{Name:w}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{Name}", "Nick"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal("nick", sw.ToString());
        }

        [Theory]
        [InlineData(LogEventLevel.Verbose, 1, "V")]
        [InlineData(LogEventLevel.Verbose, 2, "Vb")]
        [InlineData(LogEventLevel.Verbose, 3, "Vrb")]
        [InlineData(LogEventLevel.Verbose, 4, "Verb")]
        [InlineData(LogEventLevel.Verbose, 5, "Verbo")]
        [InlineData(LogEventLevel.Verbose, 6, "Verbos")]
        [InlineData(LogEventLevel.Verbose, 7, "Verbose")]
        [InlineData(LogEventLevel.Verbose, 8, "Verbose")]
        [InlineData(LogEventLevel.Debug, 1, "D")]
        [InlineData(LogEventLevel.Debug, 2, "De")]
        [InlineData(LogEventLevel.Debug, 3, "Dbg")]
        [InlineData(LogEventLevel.Debug, 4, "Dbug")]
        [InlineData(LogEventLevel.Debug, 5, "Debug")]
        [InlineData(LogEventLevel.Debug, 6, "Debug")]
        [InlineData(LogEventLevel.Information, 1, "I")]
        [InlineData(LogEventLevel.Information, 2, "In")]
        [InlineData(LogEventLevel.Information, 3, "Inf")]
        [InlineData(LogEventLevel.Information, 4, "Info")]
        [InlineData(LogEventLevel.Information, 5, "Infor")]
        [InlineData(LogEventLevel.Information, 6, "Inform")]
        [InlineData(LogEventLevel.Information, 7, "Informa")]
        [InlineData(LogEventLevel.Information, 8, "Informat")]
        [InlineData(LogEventLevel.Information, 9, "Informati")]
        [InlineData(LogEventLevel.Information, 10, "Informatio")]
        [InlineData(LogEventLevel.Information, 11, "Information")]
        [InlineData(LogEventLevel.Information, 12, "Information")]
        [InlineData(LogEventLevel.Error, 1, "E")]
        [InlineData(LogEventLevel.Error, 2, "Er")]
        [InlineData(LogEventLevel.Error, 3, "Err")]
        [InlineData(LogEventLevel.Error, 4, "Eror")]
        [InlineData(LogEventLevel.Error, 5, "Error")]
        [InlineData(LogEventLevel.Error, 6, "Error")]
        [InlineData(LogEventLevel.Fatal, 1, "F")]
        [InlineData(LogEventLevel.Fatal, 2, "Fa")]
        [InlineData(LogEventLevel.Fatal, 3, "Ftl")]
        [InlineData(LogEventLevel.Fatal, 4, "Fatl")]
        [InlineData(LogEventLevel.Fatal, 5, "Fatal")]
        [InlineData(LogEventLevel.Fatal, 6, "Fatal")]
        [InlineData(LogEventLevel.Warning, 1, "W")]
        [InlineData(LogEventLevel.Warning, 2, "Wn")]
        [InlineData(LogEventLevel.Warning, 3, "Wrn")]
        [InlineData(LogEventLevel.Warning, 4, "Warn")]
        [InlineData(LogEventLevel.Warning, 5, "Warni")]
        [InlineData(LogEventLevel.Warning, 6, "Warnin")]
        [InlineData(LogEventLevel.Warning, 7, "Warning")]
        [InlineData(LogEventLevel.Warning, 8, "Warning")]
        public void FixedLengthLevelIsSupported(
            LogEventLevel level,
            int width, 
            string expected)
        {
            var formatter = new MessageTemplateTextFormatter($"{{Level:t{width}}}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Write(level, "Hello"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal(expected, sw.ToString());
        }

        [Fact]
        public void FixedLengthLevelSupportsUpperCasing()
        {
            var formatter = new MessageTemplateTextFormatter("{Level:u3}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("Hello"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal("INF", sw.ToString());
        }

        [Fact]
        public void FixedLengthLevelSupportsLowerCasing()
        {
            var formatter = new MessageTemplateTextFormatter("{Level:w3}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("Hello"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal("inf", sw.ToString());
        }

        [Fact]
        public void DefaultLevelLengthIsFullText()
        {
            var formatter = new MessageTemplateTextFormatter("{Level}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("Hello"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal("Information", sw.ToString());
        }

        [Fact]
        public void AligmentAndWidthCanBeCombined()
        {
            var formatter = new MessageTemplateTextFormatter("{Level,5:w3}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("Hello"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal("  inf", sw.ToString());
        }

        enum Size
        {
            Large
        }

        class SizeFormatter : IFormatProvider, ICustomFormatter
        {
            private readonly IFormatProvider _innerFormatProvider;

            public SizeFormatter(IFormatProvider innerFormatProvider)
            {
                _innerFormatProvider = innerFormatProvider;
            }

            public object GetFormat(Type formatType)
            {
                return formatType == typeof(ICustomFormatter) ? this : _innerFormatProvider.GetFormat(formatType);
            }

            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                if (arg is Size)
                {
                    var size = (Size)arg;
                    return size == Size.Large ? "Huge" : size.ToString();
                }

                var formattable = arg as IFormattable;
                if (formattable != null)
                {
                    return formattable.ToString(format, _innerFormatProvider);
                }

                return arg.ToString();
            }
        }

        [Fact]
        public void AppliesCustomFormatterToEnums()
        {
            var formatter = new MessageTemplateTextFormatter("{Message}", new SizeFormatter(CultureInfo.InvariantCulture));
            var evt = DelegatingSink.GetLogEvent(l => l.Information("Size {Size}", Size.Large));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal("Size Huge", sw.ToString());
        }

        [Fact]
        public void NonMessagePropertiesAreRendered()
        {
            var formatter = new MessageTemplateTextFormatter("{Properties}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.ForContext("Foo", 42).Information("Hello from {Bar}!", "bar"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal("{Foo: 42}", sw.ToString());
        }

        [Fact]
        public void DoNotDuplicatePropertiesAlreadyRenderedInOutputTemplate()
        {
            var formatter = new MessageTemplateTextFormatter("{Foo} {Properties}", CultureInfo.InvariantCulture);
            var evt = DelegatingSink.GetLogEvent(l => l.ForContext("Foo", 42).ForContext("Bar", 42).Information("Hello from bar!"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal("42 {Bar: 42}", sw.ToString());
        }

        [Theory]
        [InlineData("", "Hello, \"World\"!")]
        [InlineData(":j", "Hello, \"World\"!")]
        [InlineData(":l", "Hello, World!")]
        [InlineData(":lj", "Hello, World!")]
        [InlineData(":jl", "Hello, World!")]
        public void AppliesLiteralFormattingToMessageStringsWhenSpecified(string format, string expected)
        {
            var formatter = new MessageTemplateTextFormatter("{Message" + format + "}", null);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("Hello, {Name}!", "World"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal(expected, sw.ToString());
        }

        [Theory]
        [InlineData("", "{ Name: \"World\" }")]
        [InlineData(":j", "{\"Name\":\"World\"}")]
        [InlineData(":lj", "{\"Name\":\"World\"}")]
        [InlineData(":jl", "{\"Name\":\"World\"}")]
        public void AppliesJsonFormattingToMessageStructuresWhenSpecified(string format, string expected)
        {
            var formatter = new MessageTemplateTextFormatter("{Message" + format + "}", null);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{@Obj}", new {Name = "World"}));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal(expected, sw.ToString());
        }

        [Theory]
        [InlineData("", "{ Name: \"World\" }")]
        [InlineData(":j", "{\"Name\":\"World\"}")]
        [InlineData(":lj", "{\"Name\":\"World\"}")]
        [InlineData(":jl", "{\"Name\":\"World\"}")]
        public void AppliesJsonFormattingToTopLevelStructuresWhenSpecified(string format, string expected)
        {
            var formatter = new MessageTemplateTextFormatter("{Properties" + format + "}", null);
            var evt = DelegatingSink.GetLogEvent(l => l.Information("{Name}", "World"));
            var sw = new StringWriter();
            formatter.Format(evt, sw);
            Assert.Equal(expected, sw.ToString());
        }
    }
}
