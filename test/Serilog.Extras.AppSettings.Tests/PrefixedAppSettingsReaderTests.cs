using NUnit.Framework;

namespace Serilog.Extras.AppSettings.Tests
{
    [TestFixture]
    public class PrefixedAppSettingsReaderTests
    {
        [Test]
        public void ConvertibleValuesConvertToTIfTargetIsNullable()
        {
            var result = (int?)PrefixedAppSettingsReader.ConvertToType("3", typeof(int?));
            Assert.That(result == 3);
        }

        [Test]
        public void NullValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)PrefixedAppSettingsReader.ConvertToType(null, typeof(int?));
            Assert.That(result == null);
        }

        [Test]
        public void EmptyStringValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)PrefixedAppSettingsReader.ConvertToType("", typeof(int?));
            Assert.That(result == null);
        }
    }
}
