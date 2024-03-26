namespace Serilog.Tests.Support;

public interface IAmAnInterface
{
}

public abstract class AnAbstractClass
{
}

class ConcreteImpl : AnAbstractClass, IAmAnInterface
{
    ConcreteImpl()
    {
    }

    public static ConcreteImpl Instance { get; } = new();
}

public class ClassWithStaticAccessors
{
    public static IAmAnInterface InterfaceProperty => ConcreteImpl.Instance;
    public static AnAbstractClass AbstractProperty => ConcreteImpl.Instance;

    public static IAmAnInterface InterfaceField = ConcreteImpl.Instance;
    public static AnAbstractClass AbstractField = ConcreteImpl.Instance;

    // ReSharper disable once UnusedMember.Local
    static IAmAnInterface PrivateInterfaceProperty => ConcreteImpl.Instance;
#pragma warning disable 169
    static IAmAnInterface PrivateInterfaceField = ConcreteImpl.Instance;
#pragma warning restore 169
    public IAmAnInterface InstanceInterfaceProperty => ConcreteImpl.Instance;
    public IAmAnInterface InstanceInterfaceField = ConcreteImpl.Instance;
}
