using System.Collections.Generic;
using Serilog.Capturing;
using Xunit;
using System.Linq;
using System.Reflection;
using System.ServiceModel;

namespace Serilog.Tests.Capturing
{
    public class GetablePropertyFinderTests
    {
        [Fact]
        public void GetPropertiesRecursiveIntegerTypeYieldNoResult()
        {
            var result = default(int).GetType().GetPropertiesRecursive();
            Assert.Empty(result);
        }

        [Fact]
        public void GetPropertiesRecursiveBooleanTypeYieldNoResult()
        {
            var result = default(bool).GetType().GetPropertiesRecursive();
            Assert.Empty(result);
        }

        [Fact]
        public void GetPropertiesRecursiveCharTypeYieldNoResult()
        {
            var result = default(char).GetType().GetPropertiesRecursive();
            Assert.Empty(result);
        }

        [Fact]
        public void GetPropertiesRecursiveObjectTypeYieldNoResult()
        {
            var result = new object().GetType().GetPropertiesRecursive();
            Assert.Empty(result);
        }

        [Fact]
        public void GetPropertiesRecursiveStringTypeYieldResult()
        {
            var result = string.Empty.GetType().GetPropertiesRecursive();
            Assert.NotEmpty(result);
        }

        [Fact]
        // https://github.com/serilog/serilog/issues/1235
        public void GetPropertiesRecursiveBaseTypeSucceedsOnWcfProxyType()
        {
            EndpointAddress remoteAddress = new("http://localhost");
            BasicHttpBinding binding = new();

            ChannelFactory<IMyChannel> myFactory = new(binding, remoteAddress);
            var channel = myFactory.CreateChannel();

            var _ = channel.GetType().GetPropertiesRecursive().ToList();
        }

        [System.ServiceModel.ServiceContract]
        interface IMyChannel
        {
            [System.ServiceModel.OperationContract]
            string Get();
        }
    }
}
