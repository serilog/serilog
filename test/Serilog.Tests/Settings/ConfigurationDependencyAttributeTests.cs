using Xunit;
using Serilog.Settings;

namespace Serilog.Tests.Settings
{
    public class ConfigurationDependencyAttributeTests
    {
        [Fact]
        public void DependencyTypePropertyContainsTypePassedInConstructor()
        {
            var attribute = new ConfigurationDependencyAttribute(typeof(ConfigurationDependencyAttributeTests));
            Assert.True(attribute.DependencyType == typeof(ConfigurationDependencyAttributeTests));
        }
    }
}