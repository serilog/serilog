namespace Serilog.Tests.Capturing;

public class GettablePropertyFinderTests
{
    [Fact]
    public void GetPropertiesRecursiveIntegerTypeYieldNoResult()
    {
        var result = default(int).GetType().GetPropertiesInHierarchy();
        Assert.Empty(result);
    }

    [Fact]
    public void GetPropertiesRecursiveBooleanTypeYieldNoResult()
    {
        var result = default(bool).GetType().GetPropertiesInHierarchy();
        Assert.Empty(result);
    }

    [Fact]
    public void GetPropertiesRecursiveCharTypeYieldNoResult()
    {
        var result = default(char).GetType().GetPropertiesInHierarchy();
        Assert.Empty(result);
    }

    [Fact]
    public void GetPropertiesRecursiveObjectTypeYieldNoResult()
    {
        var result = new object().GetType().GetPropertiesInHierarchy();
        Assert.Empty(result);
    }

    [Fact]
    public void GetPropertiesRecursiveStringTypeYieldResult()
    {
        var result = string.Empty.GetType().GetPropertiesInHierarchy();
        Assert.NotEmpty(result);
    }

    [Fact]
    // https://github.com/serilog/serilog/issues/1235
    public void GetPropertiesRecursiveBaseTypeSucceedsOnWcfProxyType()
    {
        var remoteAddress = new System.ServiceModel.EndpointAddress("http://localhost");
        var binding = new System.ServiceModel.BasicHttpBinding();

        var myFactory = new System.ServiceModel.ChannelFactory<IMyChannel>(binding, remoteAddress);
        var channel = myFactory.CreateChannel();

        var _ = channel.GetType().GetPropertiesInHierarchy().ToList();
    }

    [System.ServiceModel.ServiceContract]
    interface IMyChannel
    {
        [System.ServiceModel.OperationContract]
        string Get();
    }

    [Fact]
    public void ShouldOnlyGetInheritedNewProperty()
    {
        var property = typeof(InheritedNewClass).GetPropertiesInHierarchy().Single();

        Assert.Equal(typeof(InheritedNewClass).GetProperty("Property"), property);
    }

    public class InheritedNewClass :
        BaseClass
    {
        public new string Property { get; set; } = null!;
    }

    [Fact]
    public void ShouldOnlyGetInheritedProperty()
    {
        var property = typeof(InheritedClass).GetPropertiesInHierarchy().Single();

        Assert.Equal(typeof(InheritedClass).GetProperty("Property"), property);
    }

    public class InheritedClass :
        BaseClass
    {
        public override string Property { get; set; } = null!;
    }

    public class BaseClass
    {
        public virtual string Property { get; set; } = null!;
    }
}
