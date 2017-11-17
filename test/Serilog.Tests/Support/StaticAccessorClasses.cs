
namespace Serilog.Tests.Support
{
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

        public static ConcreteImpl Instance { get; } = new ConcreteImpl();
    }

    public class ClassWithStaticAccessors
    {
        public static string StringProperty => nameof(StringProperty);
        public static int IntProperty => 42;
        public static IAmAnInterface InterfaceProperty => ConcreteImpl.Instance;
        public static AnAbstractClass AbstractProperty => ConcreteImpl.Instance;
        // ReSharper disable once UnusedMember.Local
        static string PrivateStringProperty => nameof(PrivateStringProperty);
        public string InstanceStringProperty => nameof(InstanceStringProperty);
        public static string StringField = nameof(StringField);
    }
}
