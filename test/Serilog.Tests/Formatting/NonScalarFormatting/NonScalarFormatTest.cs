using Serilog.Tests.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Serilog.Tests.Formatting.NonScalars
{
    class CustomFormatter : IFormatProvider, ICustomFormatter
    {
        public Dictionary<string, Func<Object, string>> Formatters { get; }

        public CustomFormatter()
        {
            this.Formatters = new Dictionary<string, Func<object, string>>();
        }

        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (this.Formatters.TryGetValue(format, out var f))
                return f(arg);
            if (arg is IFormattable formattable)
                return formattable.ToString(format, null);
            return arg.ToString();
        }
    }

    public class NonScalarFormattingTests
    {
        [Fact]
        public void FormatOfScalarsScenario()
        {
            // example database values
            var db = new Dictionary<int, string>
            {
                { 3, "kylie" },
                { 4, "stereo mcs" },
                { 5, "bilderbuch" }
            };

            // logger
            var cf = new CustomFormatter();
            cf.Formatters["id"] = id => db[(int)id];
            var ss = new StringSink(outputTemplate: "{Message:lj}", formatProvider: cf);

            ILogger l =
                new LoggerConfiguration()
                    .WriteTo.Sink(ss)
                    .CreateLogger();

            l.Information("artist: {value:id}", 4); // this works with Serilog 2.9

            Assert.Equal("artist: stereo mcs", ss.ToString());
        }

        [Fact]
        public void FormatOfNonScalarsScenario()
        {
            // example database values
            var db = new Dictionary<int, string>
            {
                { 3, "kylie" },
                { 4, "stereo mcs" },
                { 5, "bilderbuch" }
            };

            // logger
            var cf = new CustomFormatter();
            cf.Formatters["id"] = id => db[(int)id];
            cf.Formatters["ids"] = ids => string.Join(", ", ((int[])ids).Select(id => db[id]));
            var ss = new StringSink(outputTemplate: "{Message:lj}", formatProvider: cf);

            ILogger l =
                new LoggerConfiguration()
                    .WriteTo.Sink(ss)
                    .CreateLogger();

            l.Information("artists: {values:ids}", new[] { 3, 4, 5 }); // this requires that the format is passed for nonscalars

            // logger writes resolved strings
            Assert.Equal("artists: kylie, stereo mcs, bilderbuch", ss.ToString());
        }

        class FormatCollector : IFormatProvider, ICustomFormatter
        {
            public List<string> Formats { get; }

            public FormatCollector()
            {
                this.Formats = new List<string>();
            }

            public object GetFormat(Type formatType)
            {
                return formatType == typeof(ICustomFormatter) ? this : null;
            }

            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                this.Formats.Add(format); // collect formats
                return arg.ToString(); // value not evaluated here
            }
        }

        [Fact]
        public void FormatOfNonScalarsArePassed()
        {
            var fc = new FormatCollector();
            var ss = new StringSink(outputTemplate: "{Message:lj}", formatProvider: fc);

            ILogger l =
                new LoggerConfiguration()
                    .WriteTo.Sink(ss)
                    .CreateLogger();

            l.Information("artist: {value:id}", 4); // this works with Serilog 2.9
            l.Information("artists: {values:ids}", new[] { 3, 4, 5 }); // this requires passing format for nonscalars

            // logger writes resolved strings
            Assert.Equal(fc.Formats.ToArray(), new[] { "id", "ids" });
        }
    }
}
