namespace Serilog.Tests.Support
{
    public abstract class DummyAbstractClass
    {
    }

    public class DummyConcreteClassWithDefaultConstructor : DummyAbstractClass
    {
        // ReSharper disable once UnusedParameter.Local
        public DummyConcreteClassWithDefaultConstructor(string param = "")
        {
        }
    }

    public class DummyConcreteClassWithoutDefaultConstructor : DummyAbstractClass
    {
        // ReSharper disable once UnusedParameter.Local
        public DummyConcreteClassWithoutDefaultConstructor(string param)
        {
        }
    }
}
