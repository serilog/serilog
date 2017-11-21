
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

        public static string StringField = nameof(StringField);
        public static int IntField = 666;
        public static IAmAnInterface InterfaceField = ConcreteImpl.Instance;
        public static AnAbstractClass AbstractField = ConcreteImpl.Instance;

        // ReSharper disable once UnusedMember.Local
        static string PrivateStringProperty => nameof(PrivateStringProperty);
#pragma warning disable 169
        static string PrivateStringField = nameof(PrivateStringField);
#pragma warning restore 169
        public string InstanceStringProperty => nameof(InstanceStringProperty);
        public string InstanceStringField = nameof(InstanceStringField);

    }
}
