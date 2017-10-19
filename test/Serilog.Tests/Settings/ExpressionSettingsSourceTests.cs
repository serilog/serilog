using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serilog.Events;
using Serilog.Settings.KeyValuePairs;
using Serilog.Tests.Support;
using TestDummies;
using Xunit;

namespace Serilog.Tests.Settings
{
    public class ExpressionSettingsSourceTests
    {
        [Fact]
        public void AddExpressionIsAvailableForSettingsSourceBuilder()
        {
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .ReadFrom.KeyValuePairs(b=> b.AddExpression(lc =>
                    lc.MinimumLevel.Error()))
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger()
                ;

            log.Information("a message that should not be seen");
            Assert.Null(evt);
            log.Error("a message that should be seen seen");
            Assert.NotNull(evt);
        }

        [Fact]
        public void SupportMinimumLevel()
        {
            var actual = new ExpressionSettingsSource(lc =>
                lc
                    .MinimumLevel.Verbose()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Information()
                    .MinimumLevel.Warning()
                    .MinimumLevel.Error()
                    .MinimumLevel.Fatal()
            ).GetKeyValuePairs().ToList();

            var expected = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("minimum-level", "Verbose"),
                new KeyValuePair<string, string>("minimum-level", "Debug"),
                new KeyValuePair<string, string>("minimum-level", "Information"),
                new KeyValuePair<string, string>("minimum-level", "Warning"),
                new KeyValuePair<string, string>("minimum-level", "Error"),
                new KeyValuePair<string, string>("minimum-level", "Fatal")
            };

            Assert.Equal(expected.ToList(), actual, new KeyValuePairComparer<string, string>());
        }

        [Fact]
        public void SupportMinimumLevelOverrides()
        {
            var actual = new ExpressionSettingsSource(lc =>
                lc
                    .MinimumLevel.Override("Foo", LogEventLevel.Error)
                    .MinimumLevel.Override("Bar.Qux", LogEventLevel.Warning)
            ).GetKeyValuePairs().ToList();

            var expected = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("minimum-level:override:Foo", "Error"),
                new KeyValuePair<string, string>("minimum-level:override:Bar.Qux", "Warning")
            };

            Assert.Equal(expected.ToList(), actual, new KeyValuePairComparer<string, string>());
        }

        [Fact]
        public void SupportEnrichWithProperty()
        {
            var actual = new ExpressionSettingsSource(lc =>
                lc
                    .Enrich.WithProperty("Prop1", "Prop1Value", false)
                    .Enrich.WithProperty("Prop2", 42, false)
                    .Enrich.WithProperty("Prop3", new Uri("https://www.perdu.com/bar"), false)
                    .Enrich.WithProperty("Prop4", true, false)
            ).GetKeyValuePairs().ToList();

            var expected = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("enrich:with-property:Prop1", "Prop1Value"),
                new KeyValuePair<string, string>("enrich:with-property:Prop2", "42"),
                new KeyValuePair<string, string>("enrich:with-property:Prop3", "https://www.perdu.com/bar"),
                new KeyValuePair<string, string>("enrich:with-property:Prop4", "True"),
            };

            Assert.Equal(expected.ToList(), actual, new KeyValuePairComparer<string, string>());
        }

        [Fact]
        public void SupportEnrichWithExtensionMethod()
        {
            var actual = new ExpressionSettingsSource(lc =>
                lc
                    .Enrich.WithDummyThreadId()
            ).GetKeyValuePairs().ToList();

            var expected = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("using:TestDummies", typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName),
                new KeyValuePair<string, string>("enrich:WithDummyThreadId", "")
            };

            Assert.Equal(expected.ToList(), actual, new KeyValuePairComparer<string, string>());
        }


        [Fact]
        public void SupportEnrichFromLogContext()
        {
            var actual = new ExpressionSettingsSource(lc =>
                lc
                    .Enrich.FromLogContext()
            ).GetKeyValuePairs().ToList();

            var expected = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("enrich:FromLogContext", "")
            };

            Assert.Equal(expected.ToList(), actual, new KeyValuePairComparer<string, string>());
        }

        [Fact]
        public void SupportWriteTo()
        {
            var actual = new ExpressionSettingsSource(lc =>
                    lc
                    .WriteTo.DummyRollingFile(
                                @"C:\toto.log",
                                LogEventLevel.Warning,
                                null,
                                null)
            ).GetKeyValuePairs().ToList();

            var expected = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("using:TestDummies", typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName),
                new KeyValuePair<string, string>("write-to:DummyRollingFile.pathFormat", @"C:\toto.log"),
                new KeyValuePair<string, string>("write-to:DummyRollingFile.restrictedToMinimumLevel", "Warning")
            };

            Assert.Equal(expected.ToList(), actual, new KeyValuePairComparer<string, string>());
        }


        [Fact]
        public void SupportAuditTo()
        {
            var actual = new ExpressionSettingsSource(lc =>
                lc
                    .AuditTo.DummyRollingFile(
                        @"C:\toto.log",
                        LogEventLevel.Warning,
                        null,
                        null)
            ).GetKeyValuePairs().ToList();

            var expected = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("using:TestDummies", typeof(DummyLoggerConfigurationExtensions).GetTypeInfo().Assembly.FullName),
                new KeyValuePair<string, string>("audit-to:DummyRollingFile.pathFormat", @"C:\toto.log"),
                new KeyValuePair<string, string>("audit-to:DummyRollingFile.restrictedToMinimumLevel", "Warning")
            };

            Assert.Equal(expected.ToList(), actual, new KeyValuePairComparer<string, string>());
        }
    }
}
