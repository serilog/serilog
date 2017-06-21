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
            var result = (System.TimeSpan?)SettingValueConversions.ConvertToType("00:01:00", typeof(System.TimeSpan?));
            Assert.Equal(System.TimeSpan.FromMinutes(1), result);
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
    }
}
