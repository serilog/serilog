using System;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using Serilog.Settings.KeyValuePairs;
using Xunit;

namespace Serilog.Tests.Settings
{
    public class SettingValueConversionsTests
    {
        [Fact]
        public void ConvertibleValuesConvertToTIfTargetIsNullable()
        {
            var result = (int?)SettingValueConversions.ConvertToType("3", typeof(int?));
            Assert.True(result == 3);
        }

        [Fact]
        public void NullValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)SettingValueConversions.ConvertToType(null, typeof(int?));
            Assert.True(result == null);
        }

        [Fact]
        public void EmptyStringValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)SettingValueConversions.ConvertToType("", typeof(int?));
            Assert.True(result == null);
        }

        [Fact]
        public void ValuesConvertToNullableTimeSpan()
        {
            var result = (TimeSpan?)SettingValueConversions.ConvertToType("00:01:00", typeof(TimeSpan?));
            Assert.Equal(TimeSpan.FromMinutes(1), result);
        }

        [Fact]
        public void ValuesConvertToEnumMembers()
        {
            var result = (LogEventLevel)SettingValueConversions.ConvertToType("Information", typeof(LogEventLevel));
            Assert.Equal(LogEventLevel.Information, result);
        }

        [Fact]
        public void StringValuesConvertToDefaultInstancesIfTargetIsInterface()
        {
            var result = SettingValueConversions.ConvertToType("Serilog.Formatting.Json.JsonFormatter", typeof(ITextFormatter));
            Assert.IsType<JsonFormatter>(result);
        }

        [Theory]
        [InlineData("3.14:21:18.986", 3 /*days*/, 14 /*hours*/, 21 /*min*/, 18 /*sec*/, 986 /*ms*/)]
        [InlineData("4", 4, 0, 0, 0, 0)] // minimal : days
        [InlineData("2:0", 0, 2, 0, 0, 0)] // minimal hours
        [InlineData("0:5", 0, 0, 5, 0, 0)] // minimal minutes
        [InlineData("0:0:7", 0, 0, 0, 7, 0)] // minimal seconds
        [InlineData("0:0:0.2", 0, 0, 0, 0, 200)] // minimal milliseconds
        public void TimeSpanValuesCanBeParsed(string input, int expDays, int expHours, int expMin, int expSec, int expMs)
        {
            var expectedTimeSpan = new TimeSpan(expDays, expHours, expMin, expSec, expMs);
            var actual = SettingValueConversions.ConvertToType(input, typeof(TimeSpan));

            Assert.IsType<TimeSpan>(actual);
            Assert.Equal(expectedTimeSpan, actual);
        }
    }
}
