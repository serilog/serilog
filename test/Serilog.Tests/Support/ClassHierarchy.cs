namespace Serilog.Tests.Support
{

    public abstract class DummyAbstractClass
    {
    }

    public class DummyConcreteClassWithDefaultConstructor
    {
        // ReSharper disable once UnusedParameter.Local
        public DummyConcreteClassWithDefaultConstructor(string param = "")
        {
        }
    }

    public class DummyConcreteClassWithoutDefaultConstructor
    {
        // ReSharper disable once UnusedParameter.Local
        public DummyConcreteClassWithoutDefaultConstructor(string param)
        {
        }
    }
}
